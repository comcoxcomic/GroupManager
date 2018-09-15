using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupManager.Views
{
    public partial class PermissionView : Form
    {
        Permission.Permission Permission;
        GroupView View;
        public PermissionView(Permission.Permission permission, GroupView view)
        {
            this.Permission = permission;
            this.View = view;
            InitializeComponent();
        }

        private void PermissionView_Load(object sender, EventArgs e)
        {
            this.UUID.DataBindings.Add("Text", Permission, "UUID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.Content.DataBindings.Add("Text", Permission, "Content", true, DataSourceUpdateMode.OnPropertyChanged);
            this.comboBox1.DataSource = Enum.GetNames(typeof(Permission.PermissionType));
            this.comboBox1.SelectedIndex = this.comboBox1.FindString(Permission.PermissionType.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Permission.PermissionType = (Permission.PermissionType)Enum.Parse(typeof(Permission.PermissionType), this.comboBox1.SelectedItem.ToString(), true);
            var p_idx = View.Config.Permission.FindIndex(x => x.UUID == Permission.UUID);
            View.Config.Permission[p_idx] = Permission;
            View.LoadPermission();
            this.Close();
        }
    }
}
