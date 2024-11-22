
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfAppKfzKennzeichenApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetKennzeichen_Click(object sender, RoutedEventArgs e)
        {
            await getData();
        }

        private async Task getData()
        {
            string strResult = await client.GetStringAsync(client.BaseAddress);
            lbKennzeichen.Items.Add(strResult);
            Kennzeichen[]? infos = await client.GetFromJsonAsync<Kennzeichen[]>(client.BaseAddress);
            if (infos == null)
                return;
            List<Kennzeichen> listInfos = new List<Kennzeichen>();
            if (listInfos == null)
                return;
            listInfos.AddRange(infos);
            gridKennzeichen.ItemsSource = listInfos;
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnPost_Click(object sender, RoutedEventArgs e)
        {

            Kennzeichen? k = new Kennzeichen(txtKennzeichen.Text,txtOrt.Text, txtKreis.Text,txtBundesland.Text,txtWappen.Text);
            if(k == null) return;
            await client.PostAsJsonAsync<Kennzeichen>(client.BaseAddress,k);
            await getData();
        }

        private void gridKennzeichen_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(gridKennzeichen.SelectedItem == null)
                return;
            Kennzeichen? k = gridKennzeichen.SelectedItem as Kennzeichen;
            txtId.Text = k.id.ToString();
            txtKennzeichen.Text = k.kennzeichen;
            txtOrt.Text = k.ort;
            txtKreis.Text =k.kreis;
            txtBundesland.Text =k.bundesland;
            txtWappen.Text =k.wappen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client.BaseAddress = new Uri("https://localhost:7296/api/KennzeichenInfos/");
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Kennzeichen? k = new Kennzeichen(Convert.ToInt32(txtId.Text), txtKennzeichen.Text, txtOrt.Text, txtKreis.Text, txtBundesland.Text, txtWappen.Text);
            if(k==null) return;
            try
            {
                //string s = $"https://localhost:7296/api/KennzeichenInfos/{k.id}";
                //client.BaseAddress= new Uri(s) ;
                 Task<HttpResponseMessage> t =  client.PutAsJsonAsync<Kennzeichen>(client.BaseAddress, k);
                await getData();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Kennzeichen? k = new Kennzeichen(Convert.ToInt32(txtId.Text), txtKennzeichen.Text, txtOrt.Text, txtKreis.Text, txtBundesland.Text, txtWappen.Text);
            if (k == null) return;
            await client.DeleteFromJsonAsync<Kennzeichen>(client.BaseAddress);
            await getData();
        }
    }

}