Araç Kiralama Sistemi (C# & MySQL)
Proje Hakkında:

Bu proje, C# ve MySQL kullanılarak geliştirilmiş basit bir Araç Kiralama Otomasyonudur.
Projeyi olabildiğince profesyonel ve okunabilir yapmak için katmanlı mimamri ullanılmıştır. Proje 3 katmandan oluşur:
Veri Katmanı (Database Layer)
İş Mantığı Katmanı (Logic Layer) 
Arayüz Katmanı (UI Layer).

Katmanlı Mimari:

*Database Layer (Veri Katmanı)

MySQL veritabanı ile iletişimi sağlar.
Ekleme, silme, güncelleme ve listeleme işlemleri burada yapılır.
SQL sorgularında parametreli komutlar kullanılarak güvenlik sağlanmıştır.

* Logic Layer (İş Mantığı Katmanı)

Uygulamanın kuralları ve kontrolleri burada bulunur.
UI katmanından gelen verileri kontrol eder ve Database Layer’a gönderir.
Verilerin doğruluğunu ve tutarlılığını sağlar.

*UI Layer (Arayüz Katmanı)

Windows Forms (WinForms) ile tasarlanmıştır.(henüz bitmedi ama şu an terminal üzerinden kullanılabiliyor)

Admin girişi, araç ekleme, müşteri işlemleri ve kiralama ekranlarını içerir.
Basit ve kullanışlı bir arayüz sunar.

**Güvenlik

Admin girişlerinde şifreleme (hashing) yöntemi kullanılmıştır.
Şifreler veritabanında düz metin olarak tutulmaz, SHA veya benzeri hash algoritmasıyla saklanır.
Bu sayede veritabanı ele geçirilse bile şifrelerin okunması engellenir.

ayrıca üç kere yanlış şifre girilirse sistem kendini kapatıyor

*Veritabanı Yapısı

Veritabanı MySQL üzerinde oluşturulmuştur.
Tablolar:

Arabalar (Cars) → CarID, Marka, Model, Yıl, GünlükÜcret, Durum
Müşteriler (Customers) → CustomerID, Ad, Soyad, Telefon, Email
Kiralama (Rentals) → RentalID, CarID, CustomerID, ToplamÜcret
Adminler (Admins) → AdminID, KullanıcıAdı, Şifre (Hashlenmiş)

=> Özellikler

Admin giriş sistemi

Araç ekleme, silme, listeleme

Müşteri ekleme, silme, listeleme

Araç kiralama ve teslim etme işlemleri

MySQL veritabanı bağlantısı

Katmanlı mimari

Şifreleme (Hash) sistemi

Hata yönetimi ve basit doğrulamalar

*** Kullanılan Teknolojiler ***
Bileşen	Teknoloji
Programlama Dili	C# (.NET Framework / WinForms)
Veritabanı	MySQL
Mimari	3 Katmanlı Mimari
IDE	Visual Studio
Veritabanı Aracı	MySQL Workbench

 Projeyi Çalıştırmak İçin

Bu projeyi klonla:

git clone https://github.com/<kullanıcıadın>/araba-kiralama-sistemi.git


Visual Studio’da .sln dosyasını aç.

Database bağlantı cümlesini (connection string) kendi MySQL ayarlarına göre düzenle.

Veritabanı tablolarını oluşturmak için SQL script’leri çalıştır.

Projeyi başlat ve Admin Girişi ekranından sisteme giriş yap.


=> Gelecekte Eklenebilecek Özellikler

Müşteri girişi ekleme

gerçek zamanlı ücret hesaplama

Arama ve filtreleme özellikleri


--Geliştirici:

Yusuf Büyüktaş
Sakarya Üniversitesi Yazılım Mühendisliği öğrencisi
📧 e-posta: buyuktasyusuf688@gmail.com
