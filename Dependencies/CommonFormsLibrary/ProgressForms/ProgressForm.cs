using System.ComponentModel;

namespace System.Windows.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }
        public ProgressForm(Action function) : this()
        {
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
                            DialogResult result = Forms.DialogResult.No;
                            ax.Flatten();
                            for (int i = 0; i < ax.InnerExceptions.Count && result == Forms.DialogResult.No; i++)
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

        public void UpdateProgress(int Progress)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new Delegates.EventArgs.ProgressUpdatedEventHandler(UpdateProgress), new object[] { Progress });
                }
                return;
            }

            this.pbProgress.Value = Progress > 100 ? 100 : Progress < 0 ? 0 : Progress;
            this.pbProgress.Style = this.pbProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
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
