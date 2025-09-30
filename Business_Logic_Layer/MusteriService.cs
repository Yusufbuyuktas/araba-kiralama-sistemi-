using Data_Access_Layer;
using System.Collections.Generic;
//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class MusteriService
    {
        public MusteriService musteriService;

        private static MusteriService _instance;
        private readonly MusteriRepository musteriRepository;
        public MusteriService()
        {
            musteriRepository = new MusteriRepository();
        }
        public static MusteriService Instance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new MusteriService();
                }
                return _instance;
            }
        }

        public List<Musteri> MusteriGetir()
        {
            return musteriRepository.MusteriListele();
        }
        public bool MusteriEkle(Musteri musteri)
        {
            if (string.IsNullOrEmpty(musteri.AdSoyad))
            {
                return false;
            }
            if (string.IsNullOrEmpty(musteri.Tel) || musteri.Tel.Length !=10)
            {
                return false;
            }
            if (string.IsNullOrEmpty(musteri.Email))
            {
                return false;
            }
            musteriRepository.MusteriEkle(musteri);
                return true;
        }
        public bool MusteriSil(Musteri musteri)
        {
            if (string.IsNullOrEmpty(musteri.TakipCihazi) || musteri.TakipCihazi.Length != 8)
            {
                return false;
            }
            musteriRepository.MusteriSil(musteri);
            return true;
        }
    }
}
