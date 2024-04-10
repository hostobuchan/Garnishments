namespace System.Windows.Forms
{
    public partial class DragSortListControlItem : UserControl
    {
        #region Events
        protected internal event Delegates.DragSortListControlItemEventHandler DraggingItem;
        protected internal event Delegates.DragSortListControlItemNeedIntEventHandler GetIndex;
        protected internal event Delegates.DragSortListControlItemAllowEventHandler ItemSelected;
        public event EventHandler IndexChanged;
        protected void OnDraggingItem() { if (this.DraggingItem != null) this.DraggingItem(this); }
        protected int OnGetIndex() { if (this.GetIndex != null) return this.GetIndex(this); else return -1; }
        internal void OnIndexChanged(object sender) { if (this.IndexChanged != null) this.IndexChanged(sender, EventArgs.Empty); }
        private bool OnSelected() { if (this.ItemSelected != null) return this.ItemSelected(this); else return false; }
        #endregion

        #region Private Properties
        private DragSortListControl ParentContainer { get; set; }
        #endregion

        #region Protected Properties
        protected Enums.ButtonState bState { get; set; }
        protected Enums.MouseCapture bMouse { get; set; }
        #endregion

        #region Properties
        public int Index { get { return this.OnGetIndex(); } }
        public bool Selected { get; protected internal set; }
        #endregion

        public DragSortListControlItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DragSortListControlItem
            // 
            this.DoubleBuffered = true;
            this.Name = "DragSortListControlItem";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DragSortListControlItem_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragSortListControlItem_MouseDown);
            this.MouseEnter += new System.EventHandler(this.DragSortListControlItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DragSortListControlItem_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragSortListControlItem_MouseUp);
            this.MouseMove += new MouseEventHandler(DragSortListControlItem_MouseMove);
            this.AllowDrop = true;
            this.ResumeLayout(false);

        }

        internal void SetParentContainer(DragSortListControl ListControl)
        {
            if (this.ParentContainer != null && this.ParentContainer != ListControl)
                this.ParentContainer.RemoveItem(this);
            this.ParentContainer = ListControl;
        }

        #region Mouse Handling

        private void DragSortListControlItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Left && AllowDrop)
            {
                OnDraggingItem();
            }
        }

        private void DragSortListControlItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Selected == false)
            {
                if (OnSelected())
                {
                    this.Selected = true;
                }
            }
        }

        private void DragSortListControlItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.bState = Enums.ButtonState.ButtonDown;
            Refresh();
        }

        private void DragSortListControlItem_MouseEnter(object sender, EventArgs e)
        {

            this.bMouse = Enums.MouseCapture.Inside;
            Refresh();
        }

        private void DragSortListControlItem_MouseLeave(object sender, EventArgs e)
        {
            this.bMouse = Enums.MouseCapture.Outside;
            this.bState = Enums.ButtonState.ButtonUp;
            Refresh();
        }

        private void DragSortListControlItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.bState = Enums.ButtonState.ButtonUp;
            Refresh();
        }

        #endregion

        #region Drawing

        // Suggest Custom Drawing of this Object
        //
        // item can be Selected, bMouse is inside or outside item, bState suggests mouse down and up for butten emulation
        //
        //

        #endregion
    }
}
