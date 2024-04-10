using System;
using System.Windows.Forms;

namespace EvaluationCriteria.UIControls.ContextMenus
{
    public partial class SimpleSetContextMenu<T, K> : ContextMenu where T : Accounts.EvaluateeAccount<K> where K : Accounts.EvaluateeDebtor
    {
        public event Func<string, string> NameRequested;
        public event EventHandler DataUpdated;
        protected string OnNameRequested(string name) { return this.NameRequested?.Invoke(name) ?? string.Empty; }

        CriteriaSets.SimpleSets.SimpleSetDataHandler<T, K> Manager { get; set; }

        public SimpleSetContextMenu(CriteriaSets.SimpleSets.SimpleSetDataHandler<T, K> manager)
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
                if (lb.SelectedItem is CriteriaSets.SimpleSets.SimpleSet<T, K>)
                {
                    var set = lb.SelectedItem as CriteriaSets.SimpleSets.SimpleSet<T, K>;
                    this.MenuItems.Add(new MenuItem($"Edit - \"{set.Name}\"", new EventHandler((o, ev) => EditClicked(set))));
                }
            }
        }

        private void EditClicked(CriteriaSets.SimpleSets.SimpleSet<T, K> set)
        {
            try
            {
                var newName = string.Empty;
                if (!string.IsNullOrEmpty(newName = OnNameRequested(set.Name)))
                {
                    this.Manager.UpdateSet(set, newName);
                    try
                    {
                        this.DataUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Update Set Description\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
