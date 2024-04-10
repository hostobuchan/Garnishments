namespace System.Windows.Forms
{
    public class ProcessCompletedEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public Exception Exception { get; private set; }

        public ProcessCompletedEventArgs(bool Success, Exception Exception) : base()
        {
            this.Success = Success;
            this.Exception = Exception;
        }
    }
}
