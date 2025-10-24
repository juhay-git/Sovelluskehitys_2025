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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tekstikentta_1.Text = "klikattiin";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\k5000833\\Documents\\sovelluskehitys.mdf;Integrated Security=True;Connect Timeout=30";
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
    }
}