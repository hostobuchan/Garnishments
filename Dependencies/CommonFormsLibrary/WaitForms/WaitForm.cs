using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }
        public WaitForm(Action function) : this()
        {
            this.Shown += new EventHandler((o, e) =>
            {
                function.BeginInvoke(new AsyncCallback((ar) =>
                {
                    try
                    {
                        ((Action)ar.AsyncState).EndInvoke(ar);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (AggregateException ax)
                    {
                        this.Invoke(new EventHandler((o2, e2) => MessageBox.Show(ax.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)), o, e);
                        this.DialogResult = DialogResult.Abort;
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new EventHandler((o2, e2) => MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)), o, e);
                        this.DialogResult = DialogResult.Abort;
                    }
                    finally
                    {
                        SafeClose(this, EventArgs.Empty);
                    }
                }), function);
            });
        }
        public WaitForm(Task task) : this()
        {
            this.Shown += new EventHandler((o, e) =>
            {
                task.ContinueWith((result) =>
                {
                    if (result.IsFaulted)
                    {
                        if (result.Exception.Flatten().InnerExceptions.Count > 1)
                        {
                            foreach (var ex in result.Exception.Flatten().InnerExceptions)
                            {
                                bool cont = true;
                                this.Invoke(new EventHandler((o2, e2) => cont = MessageBox.Show(ex.Message + "\n\nShow More?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes), o, e);
                                if (!cont) break;
                            }
                            this.DialogResult = DialogResult.Abort;
                        }
                        else
                        {
                            this.Invoke(new EventHandler((o2, e2) => MessageBox.Show(result.Exception.Flatten().InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)), o, e);
                            this.DialogResult = DialogResult.Abort;
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    SafeClose(this, EventArgs.Empty);
                });
            });
        }

        public void SafeClose(object sender, EventArgs e)
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)
            {
                i.BeginInvoke(new EventHandler(SafeClose), new object[] { sender, e });
                return;
            }
            this.Close();
        }
    }
}
