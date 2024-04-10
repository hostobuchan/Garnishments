using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.ContextMenus
{
    public partial class EvaluationSetContextMenu : ContextMenu
    {
        public event EventHandler DataChanged;
        Data.CriteriaSets.CriteriaDataHandler Manager { get; set; }

        public EvaluationSetContextMenu(Data.CriteriaSets.CriteriaDataHandler manager)
        {
            this.Manager = manager;
            InitializeComponent();
        }

        protected override void OnPopup(EventArgs e)
        {
            this.MenuItems.Clear();
            if (this.SourceControl is ListBox)
            {
                var lb = this.SourceControl as ListBox;
                if (lb.SelectedItem is Data.CriteriaSets.EvaluationSet)
                {
                    var eval = lb.SelectedItem as Data.CriteriaSets.EvaluationSet;
                    this.MenuItems.Add(new MenuItem($"Edit - \"{eval}\"", new EventHandler((o, ev) => EditClicked(eval))));
                }
            }
        }

        private void EditClicked(Data.CriteriaSets.EvaluationSet evalSet)
        {
            EnterNameForm enf = new EnterNameForm("Rename Set", Text: evalSet.Description);
            if (enf.ShowDialog(this.SourceControl.Parent) == DialogResult.OK)
            {
                try
                {
                    this.Manager.UpdateRoutineEvaluation(evalSet, enf.EnteredText);
                    try
                    {
                        this.DataChanged?.Invoke(this, EventArgs.Empty);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Update Set Description\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
