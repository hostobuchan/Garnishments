using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class SelectListForm<T> : Form
    {
        public T SelectedItem { get; private set; }

        public SelectListForm(string Title, List<T> Items, Func<T, IComparable> Sort = null)
        {
            InitializeComponent();
            this.Text = Title;
            this.lstList.DataSource = Sort == null ? Items.OrderBy(el => el).ToList() : Items.OrderBy(Sort).ToList();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstList.SelectedItem is T)
            {
                this.SelectedItem = (T)lstList.SelectedItem;
                this.DialogResult = Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
