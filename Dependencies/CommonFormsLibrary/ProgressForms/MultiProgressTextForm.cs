using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
    public partial class MultiProgressTextForm : Form
    {
        public MultiProgressTextForm()
        {
            InitializeComponent();
            if (!(this.Parent is Form)) this.StartPosition = FormStartPosition.CenterScreen;
        }
        public MultiProgressTextForm(Action function, string Text) : this()
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
                    catch (Exception ex)
                    {
                        var exx = ex;
                        while (exx.InnerException != null)
                        {
                            exx = exx.InnerException;
                        }
                        this.Invoke(new EventHandler((o2, e2) => MessageBox.Show(exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)), o, e);
                    }
                    finally
                    {
                        SafeClose(this, EventArgs.Empty);
                    }
                }), function);
            });
        }

        public void UpdateProgress(int Progress, string Description, string SubDescription, int Section, int TotalSections)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new Delegates.EventArgs.TextMultiProgressUpdatedEventHandler(UpdateProgress), new object[] { Progress, Description, SubDescription, Section, TotalSections });
                }
                return;
            }

            int tProgress = (Progress / TotalSections) + ((Section - 1) * 100 / TotalSections);
            if (!string.IsNullOrEmpty(Description))
                this.txtDescription.Text = Description;
            if (!string.IsNullOrEmpty(SubDescription))
                this.txtSubDescription.Text = SubDescription;

            this.pbSectionProgress.Value = Progress > 100 ? 100 : Progress < 0 ? 0 : Progress;
            this.pbTotalProgress.Value = tProgress > 100 ? 100 : tProgress < 0 ? 0 : tProgress;
            this.pbSectionProgress.Style = this.pbSectionProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            this.pbTotalProgress.Style = this.pbTotalProgress.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
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
