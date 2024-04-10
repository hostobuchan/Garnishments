namespace System.Windows.Forms
{
    public partial class SelectDateForm : Form
    {
        public DateTime SelectedDate { get; private set; }

        public SelectDateForm(string Title)
        {
            InitializeComponent();
            this.Text = Title;
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SelectedDate = dtpDateTime.Value;
            this.DialogResult = Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
