using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobsisSiparisTakip.Business.DataTypes
{
    public class Firma_Musteri
    {
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriAdres { get; set; }
        public string MusteriIl { get; set; }
        public string MusteriIlce { get; set; }
        public string MusteriEvTel { get; set; }
        public string MusteriIsTel { get; set; }
        public string MusteriCepTel { get; set; }
        public string MusteriSemt { get; set; }

        public Firma_Musteri()
        {
            this.MusteriAd = null;
            this.MusteriSoyad = null;
            this.MusteriAdres = null;
            this.MusteriIl = null;
            this.MusteriIlce = null;
            this.MusteriEvTel = null;
            this.MusteriIsTel = null;
            this.MusteriCepTel = null;
            this.MusteriSemt = null;
        }
    }

    public class Siparis
    {
        public string SiparisID { get; set; }
        public string SiparisNo { get; set; }
        public DateTime? SiparisTarih { get; set; }
        public string BayiAd { get; set; }
        public string IcKapiModel { get; set; }
        public string DisKapiModel { get; set; }
        public string DisKapiRenk { get; set; }
        public string IcKapiRenk { get; set; }
        public string KilitSistem { get; set; }
        public string Cita { get; set; }
        public string Esik { get; set; }
        public string AluminyumRenk { get; set; }
        public string AksesuarRenk { get; set; }
        public string IcPervazRenk { get; set; }
        public string DisPervazRenk { get; set; }
        public string AplikeRenk { get; set; }
        public string Kanat { get; set; }
        public string KasaCitaRenk { get; set; }
        public string ContaRenk { get; set; }
        public string ZirhTip { get; set; }
        public string ZirhRenk { get; set; }
        public string TacTip { get; set; }
        public string PervazTip { get; set; }
        public string CekmeKolu { get; set; }
        public string CekmeKolTakilmaSekli { get; set; }
        public string CekmeKolRenk { get; set; }
        public string KapiNo { get; set; }
        public string BarelTip { get; set; }
        public string Baba { get; set; }
        public string Durbun { get; set; }
        public string Taktak { get; set; }
        public string KapiTipi { get; set; }
        public string YanginKapiCins { get; set; }
        public string Durum { get; set; }
        public string KayitYapanKamera { get; set; }
        public string KayitYapmayanKamera { get; set; }
        public string Alarm { get; set; }
        public string OtomatikKilit { get; set; }
        public string BolmeKayitSayi { get; set; }
        public string CamTip { get; set; }
        public string Ferforje { get; set; }
        public string FerforjeRenk { get; set; }
        public string MudahaleKol { get; set; }
        public string PanikBar { get; set; }
        public string Mentese { get; set; }
        public string KasaTip { get; set; }
        public string HidrolikKapatici { get; set; }
        public string MetalRenk { get; set; }
        public string KasaKaplama { get; set; }
        public string FirmaAdi { get; set; }
        public string SiparisAdedi { get; set; }
        public string NakitPesin { get; set; }
        public string NakitKalan { get; set; }
        public string NakitOdemeNot { get; set; }
        public string KKartiPesin { get; set; }
        public string KKartiKalan { get; set; }
        public string KKartiOdemeNot { get; set; }
        public string CekPesin { get; set; }
        public string CekKalan { get; set; }
        public string CekOdemeNot { get; set; }
        public string Not { get; set; }
        public string Cumba { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }


        public Siparis()
        {
            this.SiparisID = null;
            this.SiparisNo = null;
            this.SiparisTarih = null;
            this.BayiAd = null;
            this.IcKapiModel = null;
            this.DisKapiModel = null;
            this.DisKapiRenk = null;
            this.IcKapiRenk = null;
            this.KilitSistem = null; this.Cita = null;
            this.Esik = null;
            this.AluminyumRenk = null;
            this.AksesuarRenk = null;
            this.ContaRenk = null;
            this.TacTip = null;
            this.PervazTip = null;
            this.CekmeKolu = null;
            this.KapiNo = null;
            this.BarelTip = null;
            this.Baba = null;
            this.Durbun = null;
            this.Taktak = null;
            this.KapiTipi = null;
            this.Durum = null;
            this.KayitYapanKamera = null;
            this.KayitYapmayanKamera = null;
            this.Alarm = null;
            this.OtomatikKilit = null;
            this.FirmaAdi = null;
            this.SiparisAdedi = null;
            this.NakitPesin = null;
            this.NakitKalan = null;
            this.NakitOdemeNot = null;
            this.KKartiPesin = null;
            this.KKartiKalan = null;
            this.KKartiOdemeNot = null;
            this.CekPesin = null;
            this.CekKalan = null;
            this.CekOdemeNot = null;
            this.Not = null;
            this.IcPervazRenk = null;
            this.DisPervazRenk = null;
            this.AplikeRenk = null;
            this.Kanat = null;
            this.KasaCitaRenk = null;
            this.ZirhTip = null;
            this.ZirhRenk = null;
            this.CekmeKolTakilmaSekli = null;
            this.CekmeKolRenk = null;
            this.BolmeKayitSayi = null;
            this.CamTip = null;
            this.Ferforje = null;
            this.FerforjeRenk = null;
            this.YanginKapiCins = null;
            this.MudahaleKol = null;
            this.PanikBar = null;
            this.Mentese = null;
            this.KasaTip = null;
            this.HidrolikKapatici = null;
            this.MetalRenk = null;
            this.KasaKaplama = null;
            this.Cumba = null;
        }
    }

    public class Olcum
    {
        public string MontajdaTakilacak { get; set; }
        public string OlcumBilgi { get; set; }
        public DateTime? OlcumTarih { get; set; }
        public string OlcumAlanKisi { get; set; }
        public string MontajSekli { get; set; }
        public string TeslimSekli { get; set; }
        public string IcKasaGenislik { get; set; }
        public string IcKasaYukseklik { get; set; }
        public string DisKasaIcPervazFark { get; set; }
        public string DuvarKalinlik { get; set; }
        public string DisSolPervaz { get; set; }
        public string DisUstPervaz { get; set; }
        public string DisSagPervaz { get; set; }
        public string IcSolPervaz { get; set; }
        public string IcUstPervaz { get; set; }
        public string IcSagPervaz { get; set; }
        public string Acilim { get; set; }

        public Olcum()
        {
            this.MontajdaTakilacak = null;
            this.OlcumBilgi = null;
            this.OlcumAlanKisi = null;
            this.MontajSekli = null;
            this.TeslimSekli = null;
            this.IcKasaGenislik = null;
            this.IcKasaYukseklik = null;
            this.DisKasaIcPervazFark = null;
            this.DuvarKalinlik = null;
            this.DisSolPervaz = null;
            this.DisUstPervaz = null;
            this.DisSagPervaz = null;
            this.IcSolPervaz = null;
            this.IcUstPervaz = null;
            this.IcSagPervaz = null;
            this.Acilim = null;
        }
    }

    public class Sozlesme
    {
        public string Pesinat { get; set; }
        public string KalanOdeme { get; set; }
        public string Fiyat { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNumarası { get; set; }
        public DateTime? MontajTeslimTarih { get; set; }
        public string MontajDurum { get; set; }

        public Sozlesme()
        {
            this.Pesinat = null;
            this.KalanOdeme = null;
            this.Fiyat = null;
            this.VergiDairesi = null;
            this.VergiNumarası = null;
            this.MontajTeslimTarih = null;
            this.MontajDurum = null;
        }
    }
}
