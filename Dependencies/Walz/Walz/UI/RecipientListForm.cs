using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Walz.Data.UI
{
    public partial class RecipientListForm : Form
    {
        public event Action RecipientModified;
        protected void OnRecipientModified() { if (this.RecipientModified != null) RecipientModified(); }

        public RecipientListForm(List<Recipient> recipients)
        {
            InitializeComponent();
            this.dgvRecipients.DataSource = new BindingSource() { DataSource = recipients };
        }

        private void dgvRecipients_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            OnRecipientModified();
        }
    }
}
