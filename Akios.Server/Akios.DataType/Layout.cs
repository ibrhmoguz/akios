namespace Akios.DataType
{
    public class Layout
    {
        public int YerlesimTabloID { get; set; }
        public int YerlesimID { get; set; }
        public int SeriID { get; set; }
        public int? YerlesimParentID { get; set; }
        public int KontrolTipID { get; set; }
        public string KontrolAdi { get; set; }
        public string Mask { get; set; }
        public string TextMode { get; set; }
        public int? Yukseklik { get; set; }
        public int? Genislik { get; set; }
        public bool Enabled { get; set; }
        public bool PostBack { get; set; }
        public string CssClass { get; set; }
        public string PrintCssClass { get; set; }
        public string Style { get; set; }
        public int? RowSpan { get; set; }
        public int? ColSpan { get; set; }
        public string Text { get; set; }
        public byte[] ImajData { get; set; }
        public int? RefID { get; set; }
        public int? ImajID { get; set; }
        public string KolonAdi { get; set; }
        public string VeriTipi { get; set; }
        public bool SorgulanacakMi { get; set; }
    }
}
