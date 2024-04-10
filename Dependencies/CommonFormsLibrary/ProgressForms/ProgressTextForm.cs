using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public partial class ProgressTextForm : Form
    {
        private Task Task { get; set; }

        public string DescriptionText { get { return this.txtDescription.Text; } set { this.txtDescription.Text = value; } }

        public ProgressTextForm()
        {
            InitializeComponent();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }
        public ProgressTextForm(Action function, string Text) : this()
        {
            this.txtDescription.Text = Text;
            this.txtDescription.ForeColor = Color.Black;
            this.Shown += new EventHandler((o, e) =>
            {
                function.BeginInvoke(new AsyncCallback((ar) =>
                {
                    try
                    {
                        ((Action)ar.AsyncState).EndInvoke(ar);
                    }
                    catch (AggregateException ax)
                    {
                        this.Invoke(new Action<AggregateException>((a) =>
                        {
                            MessageBox.Show(ax.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DialogResult result = Forms.DialogResult.Yes;
                            ax.Flatten();
                            for (int i = 0; i < ax.InnerExceptions.Count && result == Forms.DialogResult.Yes; i++)
                            {
                                if (i < ax.InnerExceptions.Count - 1)
                                    result = MessageBox.Show(ax.InnerExceptions[i].Message + "\n\nSee More Errors?", "Internal Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                else
                                    result = MessageBox.Show(ax.InnerExceptions[i].Message, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }), ax);
                    }
                    catch (Exception ex) { this.Invoke(new EventHandler((o2, e2) => MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)), o, e); }
                    finally
                    {
                        SafeClose(this, EventArgs.Empty);
                    }
                }), function);
            });
        }
        public ProgressTextForm(Task task, string text) : this()
        {
            this.txtDescription.Text = text;
            this.txtDescription.ForeColor = Color.Black;
            this.Shown += new EventHandler((o, e) =>
            {
                this.Task = task;
                task.ContinueWith((result) =>
                {
                    try
                    {
                        if (result.IsFaulted)
                        {
                            if (result.Exception.Flatten().InnerExceptions.Count > 1)
                            {
                                bool cont = true;
                                foreach (var ex in result.Exception.Flatten().InnerExceptions)
                                {
                                    this.Invoke(new Action(() => cont = MessageBox.Show(ex.Message + "\n\nShow More?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes), o, e);
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
                    }
                    catch { }
                    finally
                    {
                        SafeClose(this, EventArgs.Empty);
                    }
                });
            });
        }

        public void UpdateProgress(int Progress, string Description)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new Delegates.EventArgs.TextProgressUpdatedEventHandler(UpdateProgress), new object[] { Progress, Description });
                }
                return;
            }

            if (!string.IsNullOrEmpty(Description))
                this.txtDescription.Text = Description;
            this.pbProgress.Value = Progress > 100 ? 100 : Progress < 0 ? 0 : Progress;
            this.pbProgress.Style = this.pbProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
        }

        public void UpdateProgress(int Progress)
        {
            UpdateProgress(Progress, "");
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

        private void testTimer_Tick(object sender, EventArgs e)
        {
            if ((this.Task?.IsCompleted ?? false) || (this.Task?.IsCanceled ?? false))
            {
                this.SafeClose(sender, e);
            }
        }
    }
}
