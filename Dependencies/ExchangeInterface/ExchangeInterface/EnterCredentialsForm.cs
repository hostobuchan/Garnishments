using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace ExchangeInterface
{
    public partial class EnterCredentialsForm : Form
    {
        public string EnteredUser { get; private set; }
        public string EnteredPassword { get; private set; }

        public EnterCredentialsForm()
        {
            InitializeComponent();
        }

        private void EnterCredentialsForm_Load(object sender, EventArgs e)
        {
            var windowsId = System.Security.Principal.WindowsIdentity.GetCurrent();
            UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), IdentityType.Sid, windowsId.User.Value.ToString());
            this.txtEmail.Text = user.EmailAddress;
        }

        private void EnterCredentialsForm_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtEmail.Text))
            {
                this.txtPassword.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtEmail.Text) && !string.IsNullOrEmpty(this.txtPassword.Text))
            {
                this.EnteredUser = this.txtEmail.Text;
                this.EnteredPassword = this.txtPassword.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
               
               // this.Close();
            }
        }
    }
}
