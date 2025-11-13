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
        SqlConnection yhteys;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Paivita_DataGrid(string kysely, string taulun_nimi, DataGrid grid)
        {
            SqlCommand komento = yhteys.CreateCommand();
            komento.CommandText = kysely;

            SqlDataAdapter adapteri = new SqlDataAdapter(komento);
            DataTable taulu = new DataTable(taulun_nimi);
            adapteri.Fill(taulu);

            grid.ItemsSource = taulu.DefaultView;
        }

        private void Paivita_ComboBox(object sender, RoutedEventArgs e)
        {
            SqlCommand komento = new SqlCommand("SELECT * FROM tuotteet", yhteys);
            SqlDataReader lukija = komento.ExecuteReader();

            /* tehdään datataulu comboboxin sisältöa varten */
            DataTable taulu = new DataTable();
            taulu.Columns.Add("id", typeof(string));
            taulu.Columns.Add("nimi", typeof(string));

            /* tehdään sidos että comboboxissa näytetään datataulu */
            cb_tuotelista.ItemsSource = taulu.DefaultView;
            cb_tuotelista.DisplayMemberPath = "nimi";
            cb_tuotelista.SelectedValuePath = "id";

            while (lukija.Read())
            {
                int id = lukija.GetInt32(0);
                string nimi = lukija.GetString(1);
                taulu.Rows.Add(id, nimi);
                //cb_tuotelista.Items.Add(lukija["nimi"].ToString());
                //cb_tuotelista.Items.Add(lukija.GetString(1));
            }
            lukija.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tekstikentta_1.Text == "" || tekstikentta_2.Text == "" || tekstikentta_3.Text == "")
            {
                MessageBox.Show("Täytä kaikki kentät ennen tallennusta.");
                return;
            }
            string kysely = "INSERT INTO tuotteet (nimi, hinta, varastosaldo) VALUES ('"+tekstikentta_1.Text+"', "+tekstikentta_2.Text+", "+tekstikentta_3.Text+");";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();


            Paivita_DataGrid("SELECT * FROM tuotteet", "tuotteet", tuotelista);
            Paivita_ComboBox(sender, e);

            tekstikentta_1.Clear();
            tekstikentta_2.Clear();
            tekstikentta_3.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Paivita_DataGrid("SELECT * FROM tuotteet", "tuotteet", tuotelista);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cb_tuotelista.SelectedValue == null)
            {
                MessageBox.Show("Valitse poistettava tuote ensin.");
                return;
            }
            string id = cb_tuotelista.SelectedValue.ToString();
            MessageBox.Show("Poistetaan tuote, id: " + id);

            string kysely = "DELETE FROM tuotteet WHERE id = " + id + ";";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();


            Paivita_DataGrid("SELECT * FROM tuotteet", "tuotteet", tuotelista);
            Paivita_ComboBox(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Check päällä");
            asiakkaat_tab.IsEnabled = true;
        }

        private void valinta_boksi_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Check pois");
            asiakkaat_tab.IsEnabled = false;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (asiakas_nimi.Text == "" || asiakas_osoite.Text == "" || asiakas_puhelin.Text == "")
            {
                MessageBox.Show("Täytä kaikki kentät ennen tallennusta.");
                return;
            }
            string kysely = "INSERT INTO asiakkaat (nimi, osoite, puhelin) VALUES ('" + asiakas_nimi.Text + "', '" + asiakas_osoite.Text + "','" + asiakas_puhelin.Text + "');";
            // INSERT INTO asiakkaat (nimi, osoite, puhelin) VALUES ('Masa','Masaosoite 1', '0401234567');
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();


            Paivita_DataGrid("SELECT * FROM asiakkaat", "asiakkaat", asiakaslista);


            asiakas_nimi.Clear();
            asiakas_osoite.Clear();
            asiakas_puhelin.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                yhteys = new SqlConnection(polku);
                yhteys.Open();

                Paivita_DataGrid("SELECT * FROM tuotteet", "tuotteet", tuotelista);
                Paivita_DataGrid("SELECT * FROM asiakkaat", "asiakkaat", asiakaslista);
                Paivita_ComboBox(this, null);
                //asiakkaat_tab.IsEnabled = false;
            }
            catch (Exception ex)
            {
                tilaviesti.Text = "Tietokantayhteyden avaus epäonnistui. " + ex.Message;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            yhteys.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}