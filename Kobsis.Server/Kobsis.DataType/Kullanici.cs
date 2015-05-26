namespace Kobsis.DataType
{
    public class Kullanici
    {
        public int? KullaniciID { get; set; }
        public string Sifre { get; set; }
        public int? MusteriID { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciRol Rol { get; set; }
        public int RolID { get; set; }
    }
}
