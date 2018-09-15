using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.API.Tencent
{
    public class TencentYoutu
    {
        // 30 days
        const double EXPIRED_SECONDS = 2592000;
        const int HTTP_BAD_REQUEST = 400;
        const int HTTP_SERVER_ERROR = 500;

        /// <summary>
        /// 识别一个图像是否为色情图像
        /// </summary>
        /// <param name="image_path">图片路径</param>
        /// <returns>返回的结果，JSON字符串，字段参见API文档</returns>
        public static string imageporn(string image_path)
        {
            string expired = Utility.UnixTime(EXPIRED_SECONDS);
            string methodName = "youtu/imageapi/imageporn";
            StringBuilder postData = new StringBuilder();

            string pars = "\"app_id\":\"{0}\",\"image\":\"{1}\"";
            pars = string.Format(pars, Conf.Instance().APPID, Utility.ImgBase64(image_path));
            postData.Append("{");
            postData.Append(pars);
            postData.Append("}");
            string result = Http.HttpPost(methodName, postData.ToString(), Auth.appSign(expired, Conf.Instance().USER_ID));
            return result;
        }

        /// <summary>
        /// 识别一个图像是否为色情图像
        /// </summary>
        /// <param name="url">图片的url</param>
        /// <returns>返回的结果，JSON字符串，字段参见API文档</returns>
        public static string imagepornurl(string url)
        {
            string expired = Utility.UnixTime(EXPIRED_SECONDS);
            string methodName = "youtu/imageapi/imageporn";
            StringBuilder postData = new StringBuilder();

            string pars = "\"app_id\":\"{0}\",\"url\":\"{1}\"";
            pars = string.Format(pars, Conf.Instance().APPID, url);
            postData.Append("{");
            postData.Append(pars);
            postData.Append("}");
            string result = Http.HttpPost(methodName, postData.ToString(), Auth.appSign(expired, Conf.Instance().USER_ID));
            return result;
        }

        /// <summary>
        /// 根据用户上传的照片，识别并且获取图片中的文字信息
        /// </summary>
        /// <param name="image_path">图片路径</param>
        /// <returns>返回的结果，JSON字符串，字段参见API文档</returns>
        public static string generalocr(string image_path)
        {
            string expired = Utility.UnixTime(EXPIRED_SECONDS);
            string methodName = "youtu/ocrapi/generalocr";
            StringBuilder postData = new StringBuilder();

            string pars = "\"app_id\":\"{0}\",\"image\":\"{1}\"";
            pars = string.Format(pars, Conf.Instance().APPID, Utility.ImgBase64(image_path));
            postData.Append("{");
            postData.Append(pars);
            postData.Append("}");
            string result = Http.HttpPost(methodName, postData.ToString(), Auth.appSign(expired, Conf.Instance().USER_ID));
            return result;
        }

        /// <summary>
        /// 根据用户上传的照片，识别并且获取图片中的文字信息
        /// </summary>
        /// <param name="url">图片的url</param>
        /// <returns>返回的结果，JSON字符串，字段参见API文档</returns>
        public static string generalocrurl(string url)
        {
            string expired = Utility.UnixTime(EXPIRED_SECONDS);
            string methodName = "youtu/ocrapi/generalocr";
            StringBuilder postData = new StringBuilder();

            string pars = "\"app_id\":\"{0}\",\"url\":\"{1}\"";
            pars = string.Format(pars, Conf.Instance().APPID, url);
            postData.Append("{");
            postData.Append(pars);
            postData.Append("}");
            string result = Http.HttpPost(methodName, postData.ToString(), Auth.appSign(expired, Conf.Instance().USER_ID));
            return result;
        }

        public class Auth
        {
            const string AUTH_URL_FORMAT_ERROR = "-1";
            const string AUTH_SECRET_ID_KEY_ERROR = "-2";

            /// <summary>
            /// HMAC-SHA1 算法签名
            /// </summary>
            /// <param name="str"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            private static byte[] HmacSha1Sign(string str, string key)
            {
                byte[] keyBytes = Utility.StrToByteArr(key);
                HMACSHA1 hmac = new HMACSHA1(keyBytes);
                byte[] inputBytes = Utility.StrToByteArr(str);
                return hmac.ComputeHash(inputBytes);
            }
            /// <summary>
            /// 签名串拼接
            /// </summary>
            /// <param name="uid"></param>
            /// <param name="appid"></param>
            /// <param name="secretid"></param>
            /// <param name="stime"></param>
            /// <param name="etime"></param>
            /// <returns></returns>
            private static string SetOrignal(string uid, string appid, string secretid, string stime, string etime = "0")
            {
                return string.Format("u={0}&a={1}&k={2}&e={3}&t={4}&r={5}&f={6}", uid, appid, secretid, etime, stime, new Random()
                   .Next(0, 1000000000), "");
            }
            /// <summary>
            /// 签名
            /// </summary>
            /// <param name="expired">过期时间</param>
            /// <param name="userid">暂时不用</param>
            /// <returns>签名</returns>
            public static string appSign(string expired, string userid)
            {
                if (string.IsNullOrEmpty(Conf.Instance().SECRET_ID) || string.IsNullOrEmpty(Conf.Instance().SECRET_KEY))
                {
                    return AUTH_SECRET_ID_KEY_ERROR;
                }

                string time = Utility.UnixTime();

                string plainText = SetOrignal(Conf.Instance().USER_ID, Conf.Instance().APPID, Conf.Instance().SECRET_ID, time, expired);

                byte[] signByteArrary = Utility.JoinByteArr(HmacSha1Sign(plainText, Conf.Instance().SECRET_KEY), Utility.StrToByteArr(plainText));

                return Convert.ToBase64String(signByteArrary);

            }
        }

        public class Http
        {
            /// <summary>
            /// send http request with POST method
            /// </summary>
            /// <param name="methodName">请求的接口名称</param>
            /// <param name="postData">请求数据</param>
            /// <param name="authorization">签名</param>
            /// <returns></returns>
            public static string HttpPost(string methodName, string postData, string authorization)
            {
                string ret = string.Empty;
                try
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData); //转化为UTF8
                    HttpWebRequest webReq = null;

                    if (Conf.Instance().END_POINT.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                        webReq = WebRequest.Create((Conf.Instance().END_POINT + methodName)) as HttpWebRequest;
                        webReq.ProtocolVersion = HttpVersion.Version10;
                    }
                    else
                    {
                        webReq = (HttpWebRequest)WebRequest.Create(new Uri(Conf.Instance().END_POINT + methodName));
                    }


                    webReq.Method = "POST";
                    webReq.ContentType = "text/json";
                    webReq.Headers.Add(HttpRequestHeader.Authorization, authorization);
                    webReq.ContentLength = byteArray.Length;
                    Stream newStream = webReq.GetRequestStream();
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                    HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    ret = sr.ReadToEnd();
                    sr.Close();
                    response.Close();
                    newStream.Close();
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            int errorcode = (int)response.StatusCode;
                            ret = statusText(errorcode);
                        }
                        else
                        {
                            // no http status code available
                        }
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return ret;
            }

            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true; //总是接受   
            }

        }

        /// <summary>
        /// return the status message 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string statusText(int status)
        {
            string statusText = "UNKOWN;status=" + status;

            switch (status)
            {
                case 0:
                    statusText = "CONNECT_FAIL";
                    break;
                case 200:
                    statusText = "HTTP OK";
                    break;
                case 400:
                    statusText = "BAD_REQUEST";
                    break;
                case 401:
                    statusText = "UNAUTHORIZED";
                    break;
                case 403:
                    statusText = "FORBIDDEN";
                    break;
                case 404:
                    statusText = "NOTFOUND";
                    break;
                case 411:
                    statusText = "REQ_NOLENGTH";
                    break;
                case 423:
                    statusText = "SERVER_NOTFOUND";
                    break;
                case 424:
                    statusText = "METHOD_NOTFOUND";
                    break;
                case 425:
                    statusText = "REQUEST_OVERFLOW";
                    break;
                case 500:
                    statusText = "INTERNAL_SERVER_ERROR";
                    break;
                case 503:
                    statusText = "SERVICE_UNAVAILABLE";
                    break;
                case 504:
                    statusText = "GATEWAY_TIME_OUT";
                    break;
            }
            return statusText;
        }

        public class Conf
        {
            const string PKG_VERSION = "1.0.*";
            public string YOUTU_END_POINT { get { return "http://api.youtu.qq.com/"; } }
            public string TENCENTYUN_END_POINT { get { return "https://youtu.api.qcloud.com/"; } }

            // 请到 open.youtu.qq.com查看您对应的appid相关信息并填充
            // 请统一 通过 setAppInfo 设置 

            public string APPID { get; set; }
            public string SECRET_ID { get; set; }
            public string SECRET_KEY { get; set; }

            public string END_POINT { get; set; }

            /// <summary>
            /// 开发者 QQ
            /// </summary>
            public string USER_ID { get; set; }

            private static Conf instance = null;

            private Conf() { }

            public static Conf Instance()
            {
                if (instance == null)
                {
                    instance = new Conf();
                }

                return instance;
            }

            /// <summary>
            /// 初始化 应用信息
            /// </summary>
            /// <param name="appid"></param>
            /// <param name="secretId"></param>
            /// <param name="secretKey"></param>
            /// <param name="userid"></param>
            public void setAppInfo(string appid, string secretId, string secretKey, string userid, string end_point)
            {
                this.APPID = appid;
                this.SECRET_ID = secretId;
                this.SECRET_KEY = secretKey;
                this.USER_ID = userid;
                this.END_POINT = end_point;
            }

        }

        /// <summary>
        /// 通用方法
        /// </summary>
        public class Utility
        {
            /// <summary>
            /// 字符串转字节数组
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static byte[] StrToByteArr(string str)
            {
                return System.Text.Encoding.UTF8.GetBytes(str);
            }
            /// <summary>
            /// 字节数组转字符串
            /// </summary>
            /// <param name="byteArray"></param>
            /// <returns></returns>
            public static string ByteArrToStr(byte[] byteArray)
            {
                return System.Text.Encoding.UTF8.GetString(byteArray);
            }
            /// <summary>
            /// UnixTime时间戳
            /// </summary>
            /// <param name="expired">有效期（单位：秒）</param>
            /// <returns></returns>
            public static string UnixTime(double expired = 0)
            {
                var time = (DateTime.Now.AddSeconds(expired).ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                return time.ToString();
            }
            /// <summary>
            /// 字节数组合并
            /// </summary>
            /// <param name="byte1"></param>
            /// <param name="byte2"></param>
            /// <returns></returns>
            public static byte[] JoinByteArr(byte[] byte1, byte[] byte2)
            {
                byte[] full = new byte[byte1.Length + byte2.Length];
                Stream s = new MemoryStream();
                s.Write(byte1, 0, byte1.Length);
                s.Write(byte2, 0, byte2.Length);
                s.Position = 0;
                int r = s.Read(full, 0, full.Length);
                if (r > 0)
                {
                    return full;
                }
                throw new Exception("读取错误!");
            }
            /// <summary>
            /// 获取网络图片
            /// </summary>
            /// <param name="url">URL地址</param>
            /// <returns>Bitmap图片</returns>
            public static Bitmap GetWebImage(string url)
            {
                Bitmap img = null;
                HttpWebRequest req;
                HttpWebResponse res = null;
                try
                {
                    System.Uri httpUrl = new System.Uri(url);
                    req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                    req.Timeout = 180000; //设置超时值10秒
                    req.Method = "GET";
                    res = (HttpWebResponse)(req.GetResponse());
                    img = new Bitmap(res.GetResponseStream());//获取图片流                 

                }
                catch (Exception ex)
                {
                    throw new Exception("图片读取出错!");
                }
                finally
                {
                    res.Close();
                }
                return img;
            }
            /// <summary>
            /// 图片转Base64
            /// </summary>
            /// <param name="path">图片路径</param>
            /// <param name="isWebImg">是否网络图片 默认 false </param>
            /// <returns>Base64</returns>
            public static string ImgBase64(string path, bool isWebImg = false)
            {
                Image img;
                if (isWebImg)
                {
                    img = GetWebImage(path);
                }
                else
                {
                    if (!File.Exists(path))
                    {
                        throw new Exception("文件不存在!");
                    }
                    img = Image.FromFile(path);
                }
                MemoryStream ms = new MemoryStream();
                string file_etx = Path.GetExtension(path).ToLower();
                switch (file_etx)
                {
                    case ".jpg":
                        img.Save(ms, ImageFormat.Jpeg);
                        break;
                    case ".png":
                        img.Save(ms, ImageFormat.Png);
                        break;
                    case ".gif":
                        img.Save(ms, ImageFormat.Gif);
                        break;
                    case ".bmp":
                        img.Save(ms, ImageFormat.Bmp);
                        break;
                    default:
                        img.Save(ms, ImageFormat.Jpeg);
                        break;

                }
                return Convert.ToBase64String(ms.ToArray());

            }
            /// <summary>
            /// Base64转Image
            /// </summary>
            /// <param name="Base64String">Base64String</param>
            /// <returns>Image图片</returns>
            public static Image Base64Img(string Base64String)
            {
                MemoryStream mm = new MemoryStream(Convert.FromBase64String(Base64String));
                return Image.FromStream(mm);
            }
        }
    }
}
