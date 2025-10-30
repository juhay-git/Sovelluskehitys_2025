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
using System.Data;
using Microsoft.Data.SqlClient;


namespace Sovelluskehitys_2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\k5000833\\Documents\\sovelluskehitys.mdf;Integrated Security=True;Connect Timeout=30";
        public MainWindow()
        {
            InitializeComponent();
            Paivita_DataGrid(this, null);
        }

        private void Paivita_DataGrid(object sender, RoutedEventArgs e)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            string kysely = "SELECT * FROM tuotteet";
            SqlCommand komento = yhteys.CreateCommand();
            komento.CommandText = kysely;

            SqlDataAdapter adapteri = new SqlDataAdapter(komento);
            DataTable taulu = new DataTable("tuotteet");
            adapteri.Fill(taulu);

            tuotelista.ItemsSource = taulu.DefaultView;

            yhteys.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            string kysely = "INSERT INTO tuotteet (nimi, hinta, varastosaldo) VALUES ('"+tekstikentta_1.Text+"', "+tekstikentta_2.Text+", "+tekstikentta_3.Text+");";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();

            yhteys.Close();

            Paivita_DataGrid(sender, e);

            tekstikentta_1.Clear();
            tekstikentta_2.Clear();
            tekstikentta_3.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Paivita_DataGrid(sender, e);
        }
    }
}