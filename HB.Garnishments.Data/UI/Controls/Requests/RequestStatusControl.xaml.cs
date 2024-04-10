using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HB.Garnishments.UI.Controls.Requests
{
    /// <summary>
    /// Interaction logic for RequestStatusControl.xaml
    /// </summary>
    public partial class RequestStatusControl : UserControl
    {
        public event EventHandler AddNewStatus;
        public event EventHandler<Data.Requests.Status> ChangeResult;
        public event EventHandler<Data.Requests.Status> UpdateNote;

        Data.Requests.Status Status { get; set; }

        public RequestStatusControl(Data.Requests.Status status)
        {
            this.Status = status;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtNotes.Text = this.Status.Note;
            this.txtID.Content = this.Status.ID.ToString();
            this.txtStatus.Content = Data.Dictionaries.StatusDescription[this.Status.Type];
            this.txtUser.Content = this.Status.User.DisplayName;
            this.txtResult.Content = this.Status.Result?.Description;
        }

        private void BtnAddNote_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateNote?.Invoke(sender, this.Status);
        }

        private void BtnChangeResult_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeResult?.Invoke(sender, this.Status);
        }

        private void BtnNewStatus_Click(object sender, RoutedEventArgs e)
        {
            this.AddNewStatus?.Invoke(sender, e);
        }
    }
}
