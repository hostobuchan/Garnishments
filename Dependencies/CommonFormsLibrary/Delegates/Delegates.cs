namespace System.Windows.Forms.Delegates.EventArgs
{
    public delegate void ProgressUpdatedEventHandler(int Progress);
    public delegate void MultiProgressUpdatedEventHandler(int Progress, int Section, int TotalSections);
    public delegate void TextProgressUpdatedEventHandler(int Progress, string Description);
    public delegate void TextMultiProgressUpdatedEventHandler(int Progress, string Description, string SubDescription, int Section, int TotalSections);
    public delegate void ProcessCompletedEventHandler(object sender, ProcessCompletedEventArgs e);
}
