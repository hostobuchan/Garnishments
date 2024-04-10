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
    /// Interaction logic for RegisteredAgentControl.xaml
    /// </summary>
    public partial class RegisteredAgentControl : UserControl
    {
        bool AllowEdit { get; set; }
        Data.Assets.Base.RegisteredAgent Agent { get; set; }

        public event EventHandler EditClicked;

        public RegisteredAgentControl(Data.Assets.Base.RegisteredAgent agent, bool editable = false)
        {
            this.AllowEdit = editable;
            this.Agent = agent;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ID.Text = $"{this.Agent.ID}";
            this.Name.Text = this.Agent.Name;
            this.Attn.Text = $"ATTN: {this.Agent.Attention}";
            this.Address.Text = this.Agent.Address1;
            this.CSZ.Text = this.Agent.CityStateZip;
            this.Phone.Text = $"Phone: {this.Agent.Phone.AsPhone()}";
            this.Fax.Text = $"Fax: {this.Agent.Fax.AsPhone()}";
            this.Email.Text = this.Agent.Email;
            this.HomePage.Text = this.Agent.HomePage;
            this.btnEdit.Visibility = this.AllowEdit ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.EditClicked?.Invoke(sender, e);
        }
    }
}
