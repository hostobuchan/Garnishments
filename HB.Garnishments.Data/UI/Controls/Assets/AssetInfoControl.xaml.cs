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

namespace HB.Garnishments.UI.Controls.Assets
{
    /// <summary>
    /// Interaction logic for AssetInfoControl.xaml
    /// </summary>
    public partial class AssetInfoControl : UserControl
    {
        Data.Interfaces.IAssetInfo Info { get; set; }

        public AssetInfoControl(Data.Interfaces.IAssetInfo info)
        {
            this.Info = info;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Type.Content = $"{this.Info.Type}";
            this.Name.Text = this.Info.Name;
            this.Contact.Text = this.Info.Contact;
            this.Address1.Text = this.Info.Address1;
            this.Address2.Text = this.Info.Address2;
            this.CSZ.Text = $"{this.Info.City}, {this.Info.State} {this.Info.Zip}";
            StringBuilder sb = new StringBuilder();
            foreach (var phone in this.Info.Phones)
            {
                sb.AppendLine($"{phone.Type} {phone.PhoneNumber.AsPhone()}");
            }
            this.Phones.Text = sb.ToString();
            this.Good.Content = !this.Info.Good.HasValue ? "" : this.Info.Good.Value ? "Known Good Info" : "Known Bad Info";
        }
    }
}
