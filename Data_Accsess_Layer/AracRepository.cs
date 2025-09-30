using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AracRepository
    {
        private readonly string connectionString;

        public AracRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["arac_kiralama"].ConnectionString;
        }
        public void AracEkle(Arac arac)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Araclar WHERE TakipCihazi = @TakipCihazi";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                    checkCommand.Parameters.AddWithValue("@TakipCihazi", arac.TakipCihazi);

                    conn.Open();
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Araç ZATEN VAR YA DA GEÇERSİZ CİHAZ İSMİ");
                        return ;
                    }

                    string query = "INSERT INTO Araclar (Marka, Plaka, TakipCihazi, Durum, FiyatKatSayi) " +
                                   "VALUES (@Marka, @Plaka, @TakipCihazi, @Durum, @FiyatKatSayi)";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@Marka", arac.Marka);
                    command.Parameters.AddWithValue("@Plaka", arac.Plaka);
                    command.Parameters.AddWithValue("@TakipCihazi", arac.TakipCihazi);
                    command.Parameters.AddWithValue("@Durum", arac.Durum);
                    command.Parameters.AddWithValue("@FiyatKatSayi", arac.FiyatKatSayi);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        public void AracSil(Arac arac)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Araclar WHERE TakipCihazi = @TakipCihazi";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TakipCihazi", arac.TakipCihazi);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Arac> AracListele()
        {
            try
            {
                List<Arac> araclar = new List<Arac>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT a.Marka, a.Plaka, a.TakipCihazi, a.FiyatKatSayi, CASE WHEN m.TakipCihazi IS NOT NULL THEN 'Kiralandı' ELSE 'Müsait' END AS Durum FROM Araclar a LEFT JOIN Musteriler m   ON a.TakipCihazi = m.TakipCihazi; ";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        Arac arac = new Arac
                        {
                            Marka = reader["Marka"].ToString(),
                            Plaka = reader["Plaka"].ToString(),
                            TakipCihazi = reader["TakipCihazi"].ToString(),
                            Durum = reader["Durum"].ToString(),
                            FiyatKatSayi = Convert.ToDecimal(reader["FiyatKatSayi"])  //HATALI
                        };
                        araclar.Add(arac);
                    }
                    reader.Close();
                }
                return araclar;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error  " + ex.Message);
                return new List<Arac>();
            }

        }
        public List<Arac> MusaitAracListele()
        {
            try
            {
                List<Arac> araclar = new List<Arac>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT  Marka, Plaka, TakipCihazi, Durum, FiyatKatSayi  FROM Araclar WHERE TakipCihazi NOT IN (SELECT TakipCihazi FROM Musteriler)";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        Arac arac = new Arac
                        {
                            Marka = reader["Marka"].ToString(),
                            Plaka = reader["Plaka"].ToString(),
                            TakipCihazi = reader["TakipCihazi"].ToString(),
                            Durum = reader["Durum"].ToString(),
                            FiyatKatSayi = Convert.ToDecimal(reader["FiyatKatSayi"])  //HATALI
                        };
                        araclar.Add(arac);
                    }
                    reader.Close();
                }
                return araclar;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error  " + ex.Message);
                return new List<Arac>();
            }

        }
        public bool MusaitMi(string TakipCihazi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Durum FROM Araclar WHERE TakipCihazi = @TakipCihazi";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TakipCihazi", TakipCihazi);

                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"       Takip cihazı '{TakipCihazi}' ile eşleşen araç bulunamadı!");
                    return false; 
                }

                string durum = result.ToString();

                if (durum == "KİRALANDI")
                {
                    Console.WriteLine($"Araç zaten kirada! (Takip cihazı: {TakipCihazi})");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
