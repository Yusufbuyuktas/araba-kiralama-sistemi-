using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Data_Access_Layer
{
    public class AdminRepository
    {
        private readonly string connectionString;

        public Admin AdminGetir(string kullaniciAdi)
        {
            Admin admin = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT*FROM Admins WHERE KullaniciAdi COLLATE Latin1_General_CS_AS = @kullaniciAdi";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    admin = new Admin()
                    {
                        KullaniciAdi = reader["KullaniciAdi"].ToString(),
                        SifreHash = reader["SifreHash"].ToString()
                    };
                }
                reader.Close();
            }
            return admin;
        }
        public AdminRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["arac_kiralama"].ConnectionString;
        }
        public void AdminEkle(Admin admin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Admins (KullaniciAdi, SifreHash) VALUES (@KullaniciAdi, @SifreHash)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciAdi", admin.KullaniciAdi);
                    cmd.Parameters.AddWithValue("@SifreHash", admin.SifreHash);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


            }
        }
        public bool AdminGiris(string kullaniciAdi, string sifreHash)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE KullaniciAdi = @KullaniciAdi AND SifreHash = @SifreHash";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                    cmd.Parameters.AddWithValue("@SifreHash", sifreHash);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
        }
    }   
}