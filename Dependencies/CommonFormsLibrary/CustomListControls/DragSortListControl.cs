using System.Data;
using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    public partial class DragSortListControl : UserControl
    {
        private DragSortListControlItem selectedItem = null;
        private System.Threading.Timer scrollTimer = null;
        private int scrollSpeed = 0;

        private Cursor DragCursorMove;
        private Cursor DragCursorNo = Cursors.No;

        public event Delegates.DragSortListControlItemEventHandler ItemAdded;
        public event Delegates.DragSortListControlItemEventHandler ItemRemoved;
        protected void OnItemAdded(DragSortListControlItem Item) { if (this.ItemAdded != null) this.ItemAdded(Item); }
        protected void OnItemRemoved(DragSortListControlItem Item) { if (this.ItemRemoved != null) this.ItemRemoved(Item); }

        public DragSortListControlItem SelectedItem { get { return this.selectedItem; } }
        public bool ShowDraggedItem { get; set; }
        public bool ShowSelectedItem { get; set; }
        public object[] Items { get { return this.flpListBox.Controls.OfType<DragSortListControlItem>().OrderBy(el => el.Index).ToArray(); } }

        public DragSortListControl()
        {
            InitializeComponent();
        }

        protected virtual bool AllowedDropItem(object Item)
        {
            if (Item is DragSortListControlItem)
                return true;
            else
                return false;
        }

        public virtual void AddItem(DragSortListControlItem Item, int? Index = null)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Delegates.DragDropListAddItemEventHandler(AddItem), Item, Index);
                return;
            }

            Item.Margin = new Padding(0);
            Item.ItemSelected += new Delegates.DragSortListControlItemAllowEventHandler(Item_ItemSelected);
            Item.GetIndex += new Delegates.DragSortListControlItemNeedIntEventHandler(Item_GetIndex);
            Item.DraggingItem += new Delegates.DragSortListControlItemEventHandler(Item_DraggingItem);
            Item.DragDrop += new DragEventHandler(SortableFlowLayoutPanel_DragDrop);
            Item.DragEnter += new DragEventHandler(SortableFlowLayoutPanel_DragEnter);
            Item.DragLeave += new EventHandler(SortableFlowLayoutPanel_DragLeave);
            Item.DragOver += new DragEventHandler(SortableFlowLayoutPanel_DragOver);
            Item.SetParentContainer(this);
            this.flpListBox.Controls.Add(Item);
            if (Index.HasValue)
                this.flpListBox.Controls.SetChildIndex(Item, Index.Value);
            SetChildAnchors();
            OnItemAdded(Item);
        }

        public void RemoveItem(DragSortListControlItem Item)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Delegates.DragSortListControlItemEventHandler(RemoveItem), Item);
                return;
            }

            this.flpListBox.Controls.Remove(Item);
            Item.ItemSelected -= new Delegates.DragSortListControlItemAllowEventHandler(Item_ItemSelected);
            Item.GetIndex -= new Delegates.DragSortListControlItemNeedIntEventHandler(Item_GetIndex);
            Item.DraggingItem -= new Delegates.DragSortListControlItemEventHandler(Item_DraggingItem);
            Item.DragDrop -= new DragEventHandler(SortableFlowLayoutPanel_DragDrop);
            Item.DragEnter -= new DragEventHandler(SortableFlowLayoutPanel_DragEnter);
            Item.DragLeave -= new EventHandler(SortableFlowLayoutPanel_DragLeave);
            Item.DragOver -= new DragEventHandler(SortableFlowLayoutPanel_DragOver);
            SetChildAnchors();
            OnItemRemoved(Item);
        }

        public void Clear()
        {
            foreach (DragSortListControlItem item in this.Items)
            {
                this.RemoveItem(item);
            }
        }

        internal void SetChildAnchors()
        {
            if (this.flpListBox.Controls.Count > 0)
            {
                for (int i = 0; i < this.flpListBox.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        int Adjust = 0;
                        if (this.flpListBox.VerticalScroll.Visible) Adjust += SystemInformation.VerticalScrollBarWidth;
                        if (this.BorderStyle == Forms.BorderStyle.Fixed3D) Adjust += SystemInformation.Border3DSize.Width * 2;

                        this.flpListBox.Controls[i].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                        this.flpListBox.Controls[i].Width = this.Width - Adjust;
                    }
                    else
                    {
                        this.flpListBox.Controls[i].Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    }
                }
            }
            UpdateIndexes();
        }

        internal void UpdateIndexes()
        {
            foreach (DragSortListControlItem SFLPI in this.flpListBox.Controls.OfType<DragSortListControlItem>())
            {
                SFLPI.OnIndexChanged(this);
            }
        }

        private bool Item_ItemSelected(DragSortListControlItem Item)
        {
            if (this.ShowSelectedItem)
            {
                if (this.selectedItem != null)
                {
                    this.selectedItem.Selected = false;
                    this.selectedItem.Refresh();
                }
                this.selectedItem = Item;
                return true;
            }
            else
            {
                return false;
            }
        }

        private int Item_GetIndex(DragSortListControlItem Item)
        {
            return this.flpListBox.Controls.IndexOf(Item);
        }

        protected virtual void Item_SetIndex(DragSortListControlItem Item, int newIndex)
        {
            if (this.flpListBox.Controls.Contains(Item))
            {
                this.flpListBox.Controls.SetChildIndex(Item, newIndex);
                UpdateIndexes();
                SetChildAnchors();
            }
            else
            {
                // Throw Error?
            }
        }

        protected void Item_DraggingItem(DragSortListControlItem Item)
        {
            Bitmap bmp = new Bitmap(Item.Width, Item.Height);
            Item.DrawToBitmap(bmp, new Rectangle(Drawing.Point.Empty, bmp.Size));
            bmp.MakeTransparent(Item.BackColor);
            Cursor cur = new Forms.Cursor(new Bitmap(bmp, new Drawing.Size((int)(bmp.Size.Width * 0.75), (int)(bmp.Size.Height * 0.75))).GetHicon());

            this.DragCursorMove = cur;
            DataObject obj = new DataObject(DataFormats.Serializable, Item);
            DoDragDrop(obj, DragDropEffects.All);
        }

        private void SortableFlowLayoutPanel_Resize(object sender, EventArgs e)
        {
            if (this.flpListBox.Controls.Count > 0)
            {
                int Adjust = 0;
                if (this.flpListBox.VerticalScroll.Visible) Adjust += SystemInformation.VerticalScrollBarWidth;
                if (this.BorderStyle == Forms.BorderStyle.Fixed3D) Adjust += SystemInformation.Border3DSize.Width * 2;

                this.flpListBox.Controls[0].Width = this.Width - Adjust;
            }
        }

        #region Drag Drop Functions

        private void SortableFlowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new DragEventHandler(SortableFlowLayoutPanel_DragDrop), sender, e);
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                bool PanelAdded = false;
                DragSortListControlItem item = e.Data.GetData(DataFormats.Serializable) as DragSortListControlItem;
                int newIndex;
                if (sender is DragSortListControlItem) // A Listed Item Initiated Drop Command
                {
                    DragSortListControlItem newItemLocation = sender as DragSortListControlItem;
                    newIndex = this.flpListBox.Controls.IndexOf(newItemLocation);
                }
                else // Should Be Panel Initiating Drop Command
                {
                    newIndex = this.flpListBox.Controls.Count - 1;
                    PanelAdded = true;
                }

                if (this.flpListBox.Controls.Contains(item))
                {
                    this.flpListBox.Controls.SetChildIndex(item, newIndex);
                }
                else
                {
                    try
                    {
                        this.AddItem(item, PanelAdded ? newIndex + 1 : newIndex);
                    }
                    catch { }
                }
                UpdateIndexes();
                SetChildAnchors();
            }

            cancelScrollTimer();
        }

        private void SortableFlowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new DragEventHandler(SortableFlowLayoutPanel_DragDrop), sender, e);
                return;
            }
            if (e.Data.GetDataPresent(DataFormats.Serializable) && AllowedDropItem(e.Data.GetData(DataFormats.Serializable)) && this.AllowDrop)
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SortableFlowLayoutPanel_DragLeave(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new DragEventHandler(SortableFlowLayoutPanel_DragDrop), sender, e);
                return;
            }
            // Cancel Scrolling

            cancelScrollTimer();
        }

        private void SortableFlowLayoutPanel_DragOver(object sender, DragEventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new DragEventHandler(SortableFlowLayoutPanel_DragDrop), sender, e);
                return;
            }
            // Check Mouse Position and Check if Scrolling is Required

            Drawing.Point mouseLoc = this.PointToClient(MousePosition);
            if (mouseLoc.X > 0 && mouseLoc.X < this.flpListBox.Width)
            {
                if (mouseLoc.Y > 0 && mouseLoc.Y < this.flpListBox.Height / 8) // Scroll Up Fast
                {
                    setScrollTimer(-1, 200);
                }
                else if (mouseLoc.Y > 0 && mouseLoc.Y < this.flpListBox.Height / 4) // Scroll Up Slow
                {
                    setScrollTimer(-1, 500);
                }
                else if (mouseLoc.Y < this.flpListBox.Height && mouseLoc.Y > this.flpListBox.Height * 7 / 8) // Scroll Down Fast
                {
                    setScrollTimer(1, 200);
                }
                else if (mouseLoc.Y < this.flpListBox.Height && mouseLoc.Y > this.flpListBox.Height * 3 / 4) // Scroll Down Slow
                {
                    setScrollTimer(1, 500);
                }
                else // Don't Scroll
                {
                    cancelScrollTimer();
                }
            }
            else
            {
                cancelScrollTimer();
            }
        }

        #endregion

        #region Scroll Functions

        private void setScrollTimer(int direction, int speed)
        {
            this.scrollSpeed = speed;
            if (this.scrollTimer == null)
            {
                this.scrollTimer = new System.Threading.Timer(new System.Threading.TimerCallback(ScrollTick), direction, 0, speed);
            }
        }

        private void cancelScrollTimer()
        {
            if (this.scrollTimer != null)
            {
                this.scrollTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                this.scrollTimer = null;
            }
        }

        private void ScrollTick(object direction)
        {
            if (this.scrollTimer == null) return;
            this.scrollTimer.Change(this.scrollSpeed, this.scrollSpeed);
            if (InvokeRequired)
            {
                this.Invoke(new EventHandler((o, e) => ScrollTick(o)), direction, EventArgs.Empty);
                return;
            }

            if (direction is int)
            {
                int oldValue = this.flpListBox.VerticalScroll.Value;
                int newValue = oldValue + ((int)direction * getScrollModifier());

                if (newValue > flpListBox.VerticalScroll.Maximum)
                    this.flpListBox.VerticalScroll.Value = this.flpListBox.VerticalScroll.Maximum;
                else if (newValue < flpListBox.VerticalScroll.Minimum)
                    this.flpListBox.VerticalScroll.Value = this.flpListBox.VerticalScroll.Minimum;
                else
                {
                    this.flpListBox.VerticalScroll.Value = newValue;
                    this.flpListBox.Invalidate();
                }
            }
        }

        private int getScrollModifier()
        {
            return this.flpListBox.Controls.Count > 0 ? this.flpListBox.VerticalScroll.Maximum / this.flpListBox.Controls.Count : 0;
        }

        #endregion

        private void DragSortListControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (this.ShowDraggedItem)
            {
                e.UseDefaultCursors = false;
                if (e.Effect == DragDropEffects.None)
                {
                    Cursor.Current = this.DragCursorNo;
                }
                else
                {
                    Cursor.Current = this.DragCursorMove;
                }
            }
        }
    }
}
