using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public partial class MultiProgressForm : Form
    {
        public MultiProgressForm()
        {
            InitializeComponent();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }
        public MultiProgressForm(Action function) : this()
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
        public MultiProgressForm(Task task) : this()
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

        public void UpdateProgress(int Progress, int Section, int TotalSections)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new Delegates.EventArgs.MultiProgressUpdatedEventHandler(UpdateProgress), new object[] { Progress, Section, TotalSections });
                }
                return;
            }

            int ttlProgress = (Progress / TotalSections) + ((Section - 1) * 100 / TotalSections); ;

            this.pbSectionProgress.Value = Progress > 100 ? 100 : Progress < 0 ? 0 : Progress;
            this.pbTotalProgress.Value = ttlProgress > 100 ? 100 : ttlProgress < 0 ? 0 : ttlProgress;
            this.pbSectionProgress.Style = this.pbSectionProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            this.pbTotalProgress.Style = this.pbTotalProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
        }

        public void SafeClose(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new EventHandler(SafeClose), new object[] { sender, e });
                }
                return;
            }
            this.Close();
        }
    }
}
