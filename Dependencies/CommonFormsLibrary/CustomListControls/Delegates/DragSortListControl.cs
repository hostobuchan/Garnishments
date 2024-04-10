namespace System.Windows.Forms.Delegates
{
    public delegate void DragSortListControlItemEventHandler(DragSortListControlItem Item);
    public delegate bool DragSortListControlItemAllowEventHandler(DragSortListControlItem Item);
    public delegate int DragSortListControlItemNeedIntEventHandler(DragSortListControlItem Item);
    public delegate void IntegerEventHandler(int Value);
}
