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
    /// Interaction logic for AssetRequestHistoryControl.xaml
    /// </summary>
    public partial class AssetRequestHistoryControl : UserControl
    {
        Data.Requests.InfoRequest[] RequestHistory { get; set; }

        public AssetRequestHistoryControl(Data.Requests.InfoRequest[] requestHistory)
        {
            this.RequestHistory = requestHistory;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtTotal.Text = RequestHistory.Length.ToString();
            var completed = RequestHistory.Where(request => request[Data.Enums.Status.GarnisheeResponse] != null);
            var success = completed.Count(request => request[Data.Enums.Status.GarnisheeResponse].Result?.Good ?? false);
            this.txtSuccess.Text = $"{((float)success / (float)(completed.Count() > 0 ? completed.Count() : 1)):P2}";
            this.txtRegisteredAgent.Text = $"{(this.RequestHistory.GroupBy(req => req.RegisteredAgent).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key ?? (object)"None")}";
            this.lstHistory.ItemsSource = this.RequestHistory;
        }

        private void LstHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
