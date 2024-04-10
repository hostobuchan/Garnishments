namespace System.Windows.Forms.Delegates.EventArgs
{
    public class SaveFileDialogEventArgs : System.EventArgs
    {
        public string Title { get; private set; }
        public string Filter { get; private set; }
        public int FilterIndex { get; private set; }

        public SaveFileDialogEventArgs(string Title, string Filter, int FilterIndex)
        {
            this.Title = Title;
            this.Filter = Filter;
            this.FilterIndex = FilterIndex;
        }
    }
}
