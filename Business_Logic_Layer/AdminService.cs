using Data_Access_Layer;
//using System.Collections.Generic;
//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class AdminService
    {
        private AdminRepository adminRepository;
    
        public AdminService()
        {
            adminRepository = new AdminRepository();
        }

        public bool AdminGiris(string kullaniciAdi, string sifreHash)
        {
            var admin = adminRepository.AdminGetir(kullaniciAdi);

            if (admin == null)
            {
                return false;
            }
            if (admin.SifreHash != sifreHash)
            {
                return false;
            }
            return true;
        }



    }
}
