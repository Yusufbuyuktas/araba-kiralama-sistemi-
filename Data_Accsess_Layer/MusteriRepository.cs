using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System;

namespace Data_Access_Layer
{
    public class MusteriRepository
    {
        private readonly string connectionString;

        public MusteriRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["arac_kiralama"].ConnectionString;
        }
        public List<Musteri> MusteriListele()
        {
            List<Musteri> musteriler = new List<Musteri>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Musteriler";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Musteri musteri = new Musteri
                    {
                        AdSoyad = reader["AdSoyad"].ToString(),
                        Tel = reader["Tel"].ToString(),
                        Email = reader["Email"].ToString(),
                        TakipCihazi = reader["TakipCihazi"].ToString()
                    };
                    musteriler.Add(musteri);
                }
                reader.Close();
            }
            return musteriler;
        }
        public void MusteriEkle(Musteri musteri)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Musteriler (AdSoyad, Tel, Email, TakipCihazi)" +
                    "VALUES (@AdSoyad, @Tel, @Email, @TakipCihazi)";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@AdSoyad", musteri.AdSoyad);
                command.Parameters.AddWithValue("@Tel", musteri.Tel);
                command.Parameters.AddWithValue("@Email", musteri.Email);
                command.Parameters.AddWithValue("@TakipCihazi", musteri.TakipCihazi);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }
        public void MusteriSil(Musteri musteri)
        {
            using (SqlConnection connectin = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Musteriler WHERE TakipCihazi = @TakipCihazi";

                SqlCommand command = new SqlCommand(query, connectin);

                command.Parameters.AddWithValue("@TakipCihazi", musteri.TakipCihazi);

                connectin.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}