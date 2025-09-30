using System;
using System.Net.Http.Headers;
using System.Threading;
using Business_Logic_Layer;
using System.Security.Cryptography;
using System.Text;
using Data_Access_Layer;
//using System.Data.SqlClient;
//using Data_Access_Layer;
//using araba_kiralama_projesi_UI;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections;

namespace araba_kiralama_projesi_UI
{
    class Program
    {
        static int Main()
        {
            Console.Title = "Araba Kiralama Uygulaması";

            int AdminGirisSonuc = AdminGirisEkrani();
            if (AdminGirisSonuc == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        static int AdminGirisEkrani()
        {
                AdminService adminService = new AdminService();
                bool girisBasarili = false;

                int girisHakki = 3;

                while (!girisBasarili && girisHakki > 0)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("      =============================================");
                    Console.WriteLine("      ||                                         ||");
                    Console.WriteLine("      ||    MÜŞTERİ TAKİP SİSTEMİ'NE HOŞGELDİNİZ ||");
                    Console.WriteLine("      ||                                         ||");
                    Console.WriteLine("      =============================================");
                    Console.WriteLine();
                    Console.Write("      =>    KULLANICI ADI:");
                    string KullaniciAdi = Console.ReadLine();
                    Console.Write("      =>    ŞİFRE:");
                    string Sifre = SifreGir();
                    string sifreHash = Hashleme(ref Sifre);

                    // 26b627a35586fe50bafc0ead5821de43f2d5445cbb4e0c831119e292fe122993
                    // Console.Write(sifreHash);
                    // Console.ReadKey();

                    girisBasarili = adminService.AdminGiris(KullaniciAdi, sifreHash);

                    if (girisBasarili)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("      =>>>>  GİRİŞ BAŞARILI  <<<<=");
                        Console.WriteLine();
                        Console.WriteLine("        Sisteme giriş yapılıyor...");
                        Thread.Sleep(2000);
                        AnaMenu();
                        return 1;
                    }
                    else
                    {
                        girisHakki--;
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("      !!!  GİRİŞ BAŞARISIZ  !!!");
                        Console.WriteLine();
                        Console.WriteLine("      => Kullanıcı adı veya şifre hatalı!..");

                        if (girisHakki > 0)
                        {
                            Console.WriteLine("      => Lütfen tekrar deneyin. Kalan giriş hakkınız: " + girisHakki);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine("      => Deneme hakkınız bitti. Program kapatılıyor...");
                            return 0;
                        }
                    }
                }

            return 0;

        }
        static string SifreGir()
        {
            string sifre = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Spacebar)
                {
                    sifre += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
                {
                    sifre = sifre.Substring(0, (sifre.Length - 1));
                    int cursorPos = Console.CursorLeft;
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return sifre;
        }
        static string Hashleme(ref string sifre)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(sifre);
                byte[] hash = sha256Hash.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
        static void AnaMenu()
        {
            bool devam = true;

            while (devam)
            {
                int decision;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("    =====================================================");
                    Console.WriteLine("    =              MÜŞTERİ TAKİP SİSTEMİ                =");
                    Console.WriteLine("    =====================================================");
                    Console.WriteLine();
                    Console.WriteLine("    1 => Müşteri işlemleri");
                    Console.WriteLine("    2 => Araç işlemleri ");
                    Console.WriteLine("    3 => Çıkış yap");
                    string input = Console.ReadLine();
                    Console.Clear();


                    if (int.TryParse(input, out decision) & decision > 0 & decision < 4)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("  !! Geçersiz seçim, tekrar dene ");
                    }
                }

                if (decision == 1)
                {
                    MusteriService musteriService = new MusteriService();
                    bool musteriDevam = true;
                    while (musteriDevam)
                    {
                        while (true)
                        {

                            Console.WriteLine();
                            Console.WriteLine("    =====================================================");
                            Console.WriteLine("    =                 MÜŞTERİ İŞLEMLERİ                 =");
                            Console.WriteLine("    =====================================================");
                            Console.WriteLine();
                            Console.WriteLine("    1 => Müşteri ekle");
                            Console.WriteLine("    2 => Müşteri sil");
                            Console.WriteLine("    3 => Müşterileri listele");
                            Console.WriteLine("    4 => Ana menü");
                            string input = Console.ReadLine();
                            Console.Clear();

                            if (int.TryParse(input, out decision) && decision > 0 && decision < 5)
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine();
                                Console.WriteLine("  !! Geçersiz seçim, tekrar dene ");
                            }

                        }

                        switch (decision)
                        {
                            case 1:
                                MusteriEkle(musteriService);
                                break;
                            case 2:
                                MusteriSil(musteriService);
                                break;
                            case 3:
                                MusterileriListele(musteriService);
                                break;
                            case 4:
                                musteriDevam = false;
                                break;
                            default:
                                Console.WriteLine("  Bir hata oluştu!!");
                                break;
                        }
                    }
                }
                else if (decision == 2)
                {
                    AracService aracService = new AracService();
                    bool aracDevam = true;
                    while (aracDevam)
                    {
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("    =====================================================");
                            Console.WriteLine("    =                   ARAÇ İŞLEMLERİ                  =");
                            Console.WriteLine("    =====================================================");
                            Console.WriteLine();
                            Console.WriteLine("    1 => Araç ekle");
                            Console.WriteLine("    2 => Araç sil");
                            Console.WriteLine("    3 => Araçları listele");
                            Console.WriteLine("    4 => Ana menü");
                            string input = Console.ReadLine();
                            Console.Clear();

                            if (int.TryParse(input, out decision) & decision > 0 & decision < 5)
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine();
                                Console.WriteLine("  !! Geçersiz seçim, tekrar dene ");
                            }
                        }
                        switch (decision)
                        {
                            case 1:
                                AracEkle(aracService);
                                break;
                            case 2:
                                AracSil(aracService);
                                break;
                            case 3:
                                AraclariListele(aracService);
                                break;
                            case 4:
                                aracDevam = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (decision == 3)
                {
                    devam = false;
                }
            }
        }

        static void MusteriEkle(MusteriService musteriService)
        {
            try
            {
                AracService AracService = new AracService();
                MusaitAraclariListele(AracService);
                Console.WriteLine();

                Console.Write("  => İsim: ");
                string isim = Console.ReadLine();

                Console.Write("  => Telefon: ");
                string Telefon = Console.ReadLine();

                Console.Write("  => Email: ");
                string Email = Console.ReadLine();

                Console.Write("  => Takip Cihazı: ");
                String TakipCihazi = Console.ReadLine();

                if (MusaitMİ(AracService,TakipCihazi))
                {
                    var musteri = new Data_Access_Layer.Musteri()
                    {
                        AdSoyad = isim,
                        Tel = Telefon,
                        Email = Email,
                        TakipCihazi = TakipCihazi
                    };

                    bool sonuc = musteriService.MusteriEkle(musteri);

                    Console.WriteLine();

                    if (sonuc)
                    {
                        Console.WriteLine("  Müşteri başarıya eklendi");
                    }
                    else
                    {
                        Console.WriteLine("  Müşteri eklenemedi");
                    }
                }      
            }
            catch (Exception)
            {
                Console.WriteLine("    !! BEKLENMEDİK BİR HATA OLUŞTU !!");
                Console.WriteLine("    Cihaz bulunamadı veya müsait değil");
                Console.WriteLine("    Girilen cihazın müsait olup olmadığını kontrol edin");
            }
            Thread.Sleep(2000);
        }
        static void MusteriSil(MusteriService musteriService)
        {
            Console.WriteLine();
            Console.WriteLine("          MÜŞTERİ SİL");
            Console.WriteLine("------------------------------------------------");
            Console.Write("      => Silinecek Müşteri Takip Cihazı: ");
            string TakipCihazi = Console.ReadLine();

            var musteri = new Data_Access_Layer.Musteri()
            {
                TakipCihazi = TakipCihazi
            };

            bool sonuc = musteriService.MusteriSil(musteri);
            if (sonuc)
            {
                Console.WriteLine("    Müşteri başarıyla silindi");
            }
            else
            {
                Console.WriteLine("    Müşteri silinemedi");
                Console.WriteLine("    Müşteriler arasında '"+TakipCihazi+"' bulunamadı ");
            }
            Thread.Sleep(1200);
        }
        static void MusterileriListele(MusteriService musteriService)
        {
            Console.Clear();
            var musteriler = musteriService.MusteriGetir();
            Console.WriteLine();
            Console.WriteLine("       MÜŞTERİLER ");

            foreach (var musteri in musteriler)
            {
                Console.WriteLine("    ---------------------------------------------");
                Console.WriteLine($"    => Ad Soyad   : {musteri.AdSoyad}");
                Console.WriteLine($"    => Telefon    : {musteri.Tel}");
                Console.WriteLine($"    => Email      : {musteri.Email}");
                Console.WriteLine($"    => Araç ID    : {musteri.TakipCihazi}");
                Console.WriteLine("    ---------------------------------------------");
            }
        }
        static void AracEkle(AracService aracService)
        {
           
            Console.Write("  => Marka: ");
            string Marka = Console.ReadLine();

            Console.Write("  => Plaka: ");
            string Plaka = Console.ReadLine();

            Console.Write("  => Takip cihazı: ");
            string TakipCihazi = Console.ReadLine();

            Console.Write("  => Fiyat (x1000): ");
            decimal FiyatKatSayisi = Convert.ToDecimal(Console.ReadLine());

            var arac = new Data_Access_Layer.Arac()
            {
                Marka = Marka,
                Plaka = Plaka,
                TakipCihazi = TakipCihazi,
                Durum = "MÜSAİT",
                FiyatKatSayi = (int)FiyatKatSayisi
            };
            bool sonuc = aracService.AracEkle(arac);
            if (sonuc)
            {
                Console.WriteLine();
                Console.WriteLine("   => Araç başarıyla eklendi");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("   => Araç eklenemedi");
            }
            Thread.Sleep(1200);
        }
        static void AracSil(AracService aracService)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("     => Silinecek araç Takip Cihazı: ");
            string TakipCihazi = Console.ReadLine();
            var musteri = new Data_Access_Layer.Arac()
            {
                TakipCihazi = TakipCihazi
            };
            bool sonuc = aracService.AracSil(musteri);
            if (sonuc)
            {
                Console.WriteLine("  Araç başarıyla silindi");
            }
            else
            {
                Console.WriteLine("  Araç silinemedi");
            }
            Thread.Sleep(1200);
        }
        static void AraclariListele(AracService aracService)
        {
            Console.Clear();

            var araclar = aracService.TumAraclariGetir();

            foreach (var arac in araclar)
            {
                Console.WriteLine("    ---------------------------------------------");
                Console.WriteLine($"  * {arac.TakipCihazi}");
                Console.WriteLine($"    => Marka         : {arac.Marka}");
                Console.WriteLine($"    => Plaka         : {arac.Plaka}");
                Console.WriteLine($"    => Durum         : {arac.Durum}");
                Console.WriteLine($"    => Fiyat         : {arac.FiyatKatSayi * 1000}");
                Console.WriteLine("    ---------------------------------------------");
            }

        }
        static void MusaitAraclariListele(AracService aracService)
        {
            Console.Clear();

            var araclar = aracService.MusaitAraclariGetir();
                Console.WriteLine();
                Console.WriteLine("    MÜSAİT ARAÇLAR");
            foreach (var arac in araclar)
            {
                Console.WriteLine("    ---------------------------------------------");
                Console.WriteLine($"  * {arac.TakipCihazi}");
                Console.WriteLine($"    => Marka         : {arac.Marka}");
                Console.WriteLine($"    => Plaka         : {arac.Plaka}");
                Console.WriteLine($"    => Durum         : {arac.Durum}");
                Console.WriteLine($"    => Fiyat         : {arac.FiyatKatSayi * 1000}");
                Console.WriteLine("    ---------------------------------------------");
            }
        }
        static bool MusaitMİ(AracService aracService, string Takipcihazi)
        {
            bool musaitMi = aracService.MusaitMi(Takipcihazi);

            if (musaitMi)
            {
                return true;
            }
            else
            {
                Console.WriteLine("       Araç şu anda kirada veya bulunamadı.");
                return false;
            }

        }
    }
            
}