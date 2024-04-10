namespace System.Windows.Forms.Delegates.EventArgs
{
    public class ProgressEventArgs : System.EventArgs
    {
        public int Progress { get; private set; }

        public ProgressEventArgs(int Progress)
        {
            this.Progress = Progress;
        }
    }

    public class MultiProgressEventArgs : ProgressEventArgs
    {
        public int Section { get; private set; }
        public int TotalSections { get; private set; }

        public MultiProgressEventArgs(int Progress, int Section, int TotalSections) : base(Progress)
        {
            this.Section = Section;
            this.TotalSections = TotalSections;
        }
    }
}
