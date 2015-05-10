using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobsisSiparisTakip.Business.DataTypes
{
    public class Musteri
    {
        public int? MusteriID { get; set; }
        public string YetkiliKisi { get; set; }
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string Tel { get; set; }
        public string Mobil { get; set; }
        public string Faks { get; set; }
        public string Web { get; set; }
        public string Mail { get; set; }
        public int? LogoID { get; set; }
        public string Kod { get; set; }
    }
}
