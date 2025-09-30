using Data_Access_Layer;
using System.Collections.Generic;
//using System;
//using System.Linq;
//using System.Runtime.Remoting.Messaging;
//using System.Text;
//using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class AracService
    {

        private AracRepository aracRepository;

        public AracService()
        {
            aracRepository = new AracRepository();
        }
        public List<Arac> TumAraclariGetir()
        {
            return aracRepository.AracListele();
        }
        public List<Arac> MusaitAraclariGetir()
        {
            return aracRepository.MusaitAracListele();
        }
        public bool AracEkle(Arac arac)
        {
            if (string.IsNullOrEmpty(arac.Plaka))
            {
                return false;   
            }
            if (string.IsNullOrEmpty(arac.TakipCihazi))
            {
                return false;
            }
            if (arac.FiyatKatSayi <= 0)
            {
                return false;
            }
            aracRepository.AracEkle(arac);
                return true;

        }
        public bool AracSil(Arac arac)
        {
            if (arac.TakipCihazi.Length <= 0)
            {
                return false;
            }
            aracRepository.AracSil(arac);
            return true;
        }
        public bool MusaitMi(string TakipCihazi) 
        {
            return aracRepository.MusaitMi(TakipCihazi);
        }
    }
}
