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
    /// Interaction logic for AccountAssetRequestHistoryControl.xaml
    /// </summary>
    public partial class AccountAssetRequestHistoryControl : UserControl
    {
        Data.Requests.AssetRequest[] RequestHistory { get; set; }

        public AccountAssetRequestHistoryControl(Data.Requests.AssetRequest[] requestHistory)
        {
            this.RequestHistory = requestHistory;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var prevRequests = this.RequestHistory.Where(req => req[Data.Enums.Status.GarnisheeResponse] != null).OrderByDescending(req => req.CurrentStatus.Date);
            var prevRequest = prevRequests.FirstOrDefault();

            if (prevRequest != null)
            {
                this.txtPrevious_Date.Text = $"{prevRequest[Data.Enums.Status.Requested].Date:d MMM, yyyy}";
                this.txtPrevious_Result.Text = $"Result: {prevRequest.CurrentStatus.Result?.Description}";
                this.txtPrevious_Agent.Text = $"Agent Used: {prevRequest.RegisteredAgent}";
                this.txtPrevious_Note.Text = $"{prevRequest.CurrentStatus.Note}";

                var prevSuccessRequest = prevRequests.FirstOrDefault(req => req[Data.Enums.Status.GarnisheeResponse]?.Result?.Good ?? false);
                if (prevSuccessRequest != null)
                {
                    this.txtPreviousSuccess_Date.Text = $"{prevSuccessRequest[Data.Enums.Status.Requested].Date:d MMM, yyyy}";
                    this.txtPreviousSuccess_Result.Text = $"Result: {prevSuccessRequest.CurrentStatus.Result?.Description}";
                    this.txtPreviousSuccess_Agent.Text = $"Agent Used: {prevSuccessRequest.RegisteredAgent}";
                    this.txtPreviousSuccess_Note.Text = $"{prevSuccessRequest.CurrentStatus.Note}";
                }
            }
            else
            {
                prevRequest = this.RequestHistory.Where(req => req.CurrentStatus.Type != Data.Enums.Status.Requested).OrderByDescending(req => req.CurrentStatus.Date).FirstOrDefault();
                if (prevRequest != null)
                {
                    this.txtPrevious_Date.Text = $"{prevRequest[Data.Enums.Status.Requested].Date:d MMM, yyyy}";
                    this.txtPrevious_Result.Text = $"Final Status: {Data.Dictionaries.StatusDescription[prevRequest.CurrentStatus.Type]}";
                    this.txtPrevious_Agent.Text = $"Agent Used: {(prevRequest.RegisteredAgent ?? (object)"N/A")}";
                    this.txtPrevious_Note.Text = $"{prevRequest.CurrentStatus.Result?.Description}\r\n{prevRequest.CurrentStatus.Note}";
                }
            }
        }
    }
}
