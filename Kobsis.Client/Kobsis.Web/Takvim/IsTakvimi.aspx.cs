using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.Util;
using Kobsis.Web.Helper;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace Kobsis.Web.Takvim
{
    public partial class IsTakvimi : KobsisBasePage
    {
        private List<Appointment> Appointments
        {
            get
            {
                return Session["Takvim_Appointments"] as List<Appointment>;
            }
            set
            {
                Session["Takvim_Appointments"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TakvimVarsayilanAyarlari();
                HaftaTeslimatlariniYukle();
                IsleriTakvimeYukle();
                PersonelListesiYukle();
            }
        }

        private void TakvimVarsayilanAyarlari()
        {
            RadCalendarIsTakvimi.SelectedDate = DateTime.Now.Date;
            RadSchedulerIsTakvimi.SelectedDate = DateTime.Now.Date;
            RadSchedulerIsTakvimi.ShowFooter = true;
        }

        protected void RadCalendarIsTakvimi_SelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            IsleriTakvimeYukle(e.SelectedDates[0].Date);
        }

        private void IsleriTakvimeYukle(DateTime dtSelectedDate)
        {
            DateTime dtBaslangic = new DateTime();
            DateTime dtBitis = new DateTime();

            if (RadSchedulerIsTakvimi.SelectedView == SchedulerViewType.DayView)
            {
                dtBaslangic = dtSelectedDate;
                dtBitis = dtBaslangic;
            }
            else if (RadSchedulerIsTakvimi.SelectedView == SchedulerViewType.WeekView)
            {
                dtBaslangic = HaftaBaslangicGunu(dtSelectedDate);
                dtBitis = HaftaBitisGunu(dtBaslangic);
            }
            else if (RadSchedulerIsTakvimi.SelectedView == SchedulerViewType.MonthView || RadSchedulerIsTakvimi.SelectedView == SchedulerViewType.TimelineView)
            {
                DateTime dtTemp = dtSelectedDate;
                dtBaslangic = new DateTime(dtTemp.Year, dtTemp.Month, 1);
                dtBitis = dtBaslangic.AddMonths(1).AddDays(-1);
            }

            TeslimatlariListele(dtBaslangic, dtBitis);
            RadSchedulerIsTakvimi.SelectedDate = dtSelectedDate;
            RadSchedulerIsTakvimi.Rebind();
            IsleriTakvimeYukle();
        }

        private void IsleriTakvimeYukle()
        {
            TeslimatlariAppointmenteCevir();
            RadSchedulerIsTakvimi.DataSource = this.Appointments;
        }

        private void HaftaTeslimatlariniYukle()
        {
            DateTime dtBaslangic = HaftaBaslangicGunu(RadSchedulerIsTakvimi.SelectedDate);
            DateTime dtBitis = HaftaBitisGunu(dtBaslangic);
            TeslimatlariListele(dtBaslangic, dtBitis);
        }

        private void TeslimatlariListele(DateTime dtBaslangic, DateTime dtBitis)
        {
            SessionManager.TeslimatListesi = new TeslimatBS().TeslimatlariListele(dtBaslangic, dtBitis);
        }

        public DateTime HaftaBaslangicGunu(DateTime dtStart)
        {
            int diff = dtStart.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return dtStart.AddDays(-1 * diff).Date;
        }

        public DateTime HaftaBitisGunu(DateTime dtStart)
        {
            return HaftaBaslangicGunu(dtStart).AddDays(6);
        }

        private void TeslimatlariAppointmenteCevir()
        {
            DataTable dtTeslimatlar = SessionManager.TeslimatListesi;
            if (dtTeslimatlar == null)
                return;

            List<Appointment> appointmentList = new List<Appointment>();
            DateTime tempDate = new DateTime();

            for (int i = 0; i < dtTeslimatlar.Rows.Count; i++)
            {
                DataRow row = dtTeslimatlar.Rows[i];
                if (row == null)
                    continue;

                int teslimatId = Convert.ToInt32(row[0]);
                string seriKodu = SessionManager.SiparisSeri.FirstOrDefault(q => q.SiparisSeriID == Convert.ToInt32(row["SeriID"].ToString())).SeriKodu;
                string siparisNo = seriKodu + "-" + row["SiparisNo"].ToString();
                int siparisAdedi;
                if (Int32.TryParse(row["Adet"].ToString(), out siparisAdedi))
                {
                    if (siparisAdedi > 1)
                    {
                        siparisNo += " ADT:" + siparisAdedi.ToString();
                    }
                }
                DateTime teslimatTarihi = Convert.ToDateTime(row["TeslimTarih"]);
                if (tempDate.Date == teslimatTarihi.Date)
                {
                    tempDate = tempDate.AddHours(0.5);
                    if (tempDate > teslimatTarihi)
                        teslimatTarihi = tempDate;
                }
                tempDate = teslimatTarihi;

                Appointment app = new Appointment(teslimatId, teslimatTarihi, teslimatTarihi.AddHours(0.5), siparisNo);
                appointmentList.Add(app);
            }

            this.Appointments = appointmentList;
        }

        private void PersonelListesiYukle()
        {
            if (SessionManager.MusteriBilgi.MusteriID != null && (SessionManager.PersonelListesi == null || SessionManager.PersonelListesi.Rows.Count == 0))
                SessionManager.PersonelListesi = new PersonelBS().PersonelListesiGetir(SessionManager.MusteriBilgi.MusteriID.Value);
        }

        protected void RadSchedulerIsTakvimi_AppointmentCommand(object sender, AppointmentCommandEventArgs e)
        {
            if (e.CommandName == "IsKaydet")
            {
                RadListBox lstBoxPersonelListesi = (RadListBox)e.Container.FindControl("ListBoxTeslimatEkibi");
                List<string> personelListesi = new List<string>();
                foreach (RadListBoxItem item in lstBoxPersonelListesi.Items)
                {
                    if (item.Checked)
                        personelListesi.Add(item.Value);
                }
                Label lblTeslimatId = (Label)e.Container.FindControl("LabelEditTeslimatID");
                RadDateTimePicker dtTeslimTarihi = (RadDateTimePicker)e.Container.FindControl("DateTimePickerTeslimatTarihSaat");
                CheckBox chcTeslimatDurumu = (CheckBox)e.Container.FindControl("chcBoxTeslimatDurumu");

                if (dtTeslimTarihi.SelectedDate == null)
                {
                    MessageBox.Uyari(this.Page, "Lütfen teslim tarihi seçiniz!");
                    return;
                }

                var teslimatDurumu = chcTeslimatDurumu.Checked == true ? "K" : "A";
                var status = new TeslimatBS().TeslimatGuncelle(lblTeslimatId.Text, dtTeslimTarihi.SelectedDate.Value, personelListesi, teslimatDurumu, SessionManager.KullaniciBilgi.KullaniciAdi, DateTime.Now, SessionManager.MusteriBilgi.Kod);
                if (status)
                {
                    MessageBox.Basari(this, "Teslimat bilgisi güncellendi.");
                    RadSchedulerIsTakvimi.Rebind();
                    IsleriTakvimeYukle(RadCalendarIsTakvimi.SelectedDate);
                }
                else
                {
                    MessageBox.Hata(this, "Teslimat güncelleme işleminde hata oluştu!");
                }
            }
            else if (e.CommandName == "IsIptal")
            {
                RadSchedulerIsTakvimi.Rebind();
            }
        }

        protected void RadSchedulerIsTakvimi_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            LinkButton linkSiparisNo = (LinkButton)e.Container.FindControl("LabelAppointmentSiparisNo");
            Label lblAdres = (Label)e.Container.FindControl("LabelAppointmentAdres");
            Label lblTeslimatEkibi = (Label)e.Container.FindControl("LabelAppointmentTeslimatEkibi");
            Label lblMusteriAdSoyad = (Label)e.Container.FindControl("LabelAppointmentMusteriAdSoyad");
            Label lblAdresIlIlce = (Label)e.Container.FindControl("LabelAppointmentAdresIlIlce");
            Label lblTelefon = (Label)e.Container.FindControl("LabelAppointmentTelefon");
            Label lblTeslimatDurum = (Label)e.Container.FindControl("LableTeslimatDurum");


            RadScheduler scheduler = sender as RadScheduler;

            switch (scheduler.SelectedView)
            {
                case SchedulerViewType.DayView:
                    lblTeslimatEkibi.Visible = true;
                    lblAdres.Visible = true;
                    lblMusteriAdSoyad.Visible = true;
                    lblTelefon.Visible = true;
                    lblAdresIlIlce.Visible = true;
                    break;
                case SchedulerViewType.WeekView:
                    lblTeslimatEkibi.Visible = true;
                    lblAdres.Visible = true;
                    lblMusteriAdSoyad.Visible = true;
                    lblTelefon.Visible = true;
                    lblAdresIlIlce.Visible = true;
                    break;
                case SchedulerViewType.MonthView:
                    lblTeslimatEkibi.Visible = false;
                    lblAdres.Visible = false;
                    lblMusteriAdSoyad.Visible = false;
                    lblTelefon.Visible = false;
                    lblAdresIlIlce.Visible = false;
                    break;
                case SchedulerViewType.TimelineView:
                    lblTeslimatEkibi.Visible = false;
                    lblAdres.Visible = false;
                    lblMusteriAdSoyad.Visible = false;
                    lblTelefon.Visible = false;
                    lblAdresIlIlce.Visible = false;
                    break;
            }

            DataTable dtTeslimatlar = SessionManager.TeslimatListesi;
            if (dtTeslimatlar == null)
                return;

            DataRow[] rows = dtTeslimatlar.Select("ID=" + e.Appointment.ID);
            if (rows.Length == 0)
                return;
            DataRow row = rows[0];
            lblAdres.Text = (row["Adres"] != DBNull.Value) ? row["Adres"].ToString() : String.Empty;
            lblTeslimatEkibi.Text = (row["Personel"] != DBNull.Value) ? row["Personel"].ToString() : String.Empty;
            lblMusteriAdSoyad.Text = (row["Musteri"] != DBNull.Value) ? row["Musteri"].ToString() : String.Empty;
            lblAdresIlIlce.Text = (row["IlIlce"] != DBNull.Value) ? row["IlIlce"].ToString() : String.Empty;
            lblTelefon.Text = (row["Tel"] != DBNull.Value) ? row["Tel"].ToString() : String.Empty;
            lblTeslimatDurum.BackColor = row["DURUM"].ToString() == "A" ? Color.Red : Color.Blue;
            var siparisId = (row["SiparisID"] != DBNull.Value) ? row["SiparisID"].ToString() : String.Empty;
            var siparisSeri = (row["SeriID"] != DBNull.Value) ? row["SeriID"].ToString() : String.Empty;

            linkSiparisNo.PostBackUrl = "~/Siparis/SiparisGoruntule.aspx?SiparisID=" + siparisId + "&SiparisSeri=" + siparisSeri;


        }

        protected void RadSchedulerIsTakvimi_FormCreated(object sender, SchedulerFormCreatedEventArgs e)
        {
            DataTable dtTeslimatlar = SessionManager.TeslimatListesi;
            if (dtTeslimatlar == null)
                return;

            RadScheduler scheduler = (RadScheduler)sender;

            if (e.Container.Mode == SchedulerFormMode.AdvancedEdit)
            {
                Label lblSiparisNo = (Label)e.Container.FindControl("LabelEditSiparisNo");
                Label lblTeslimatID = (Label)e.Container.FindControl("LabelEditTeslimatID");
                Label lblAdres = (Label)e.Container.FindControl("LabelEditAdres");
                Label lblMusteriAdSoyad = (Label)e.Container.FindControl("LabelEditMusteriAdSoyad");
                Label lblTelefon = (Label)e.Container.FindControl("LabelEditTelefon");
                RadDateTimePicker dtTeslimTarihi = (RadDateTimePicker)e.Container.FindControl("DateTimePickerTeslimatTarihSaat");
                RadListBox lstBoxPersonelListesi = (RadListBox)e.Container.FindControl("ListBoxTeslimatEkibi");
                CheckBox chcTeslimatDurumu = (CheckBox)e.Container.FindControl("chcBoxTeslimatDurumu");

                DataRow[] rows = dtTeslimatlar.Select("ID=" + e.Appointment.ID);
                if (rows.Length == 0)
                    return;
                DataRow row = rows[0];
                string ilIlce = (row["IlIlce"] != DBNull.Value) ? row["IlIlce"].ToString() : String.Empty;
                string adres = (row["Adres"] != DBNull.Value) ? row["Adres"].ToString() : String.Empty;

                lblAdres.Text = adres + " " + ilIlce;
                lblTeslimatID.Text = (row["ID"] != DBNull.Value) ? row["ID"].ToString() : String.Empty;
                lblSiparisNo.Text = (row["SiparisNo"] != DBNull.Value) ? row["SiparisNo"].ToString() : String.Empty;
                lblMusteriAdSoyad.Text = (row["Musteri"] != DBNull.Value) ? row["Musteri"].ToString() : String.Empty;
                lblTelefon.Text = (row["Tel"] != DBNull.Value) ? row["Tel"].ToString() : String.Empty;
                chcTeslimatDurumu.Checked = (row["Durum"].ToString() == "K") ? true : false;
                if (row["TeslimTarih"] != DBNull.Value)
                    dtTeslimTarihi.SelectedDate = Convert.ToDateTime(row["TeslimTarih"]);

                lstBoxPersonelListesi.DataSource = SessionManager.PersonelListesi;
                lstBoxPersonelListesi.DataBind();

                if (row["PersonelID"] != DBNull.Value)
                {
                    string[] personeller = row["PersonelID"].ToString().Split(new char[] { ',' });
                    foreach (var item in personeller)
                    {
                        RadListBoxItem listItem = lstBoxPersonelListesi.FindItemByValue(item);
                        if (listItem != null)
                            listItem.Checked = true;
                    }
                }

            }
        }

        protected void RadSchedulerIsTakvimi_NavigationCommand(object sender, SchedulerNavigationCommandEventArgs e)
        {
            if (e.Command == SchedulerNavigationCommand.SwitchToMonthView)
            {
                int ayGunSayisi = DateTime.DaysInMonth(RadCalendarIsTakvimi.SelectedDate.Year, RadCalendarIsTakvimi.SelectedDate.Month);
                DateTime dtBaslangic = new DateTime(RadCalendarIsTakvimi.SelectedDate.Year, RadCalendarIsTakvimi.SelectedDate.Month, 1);
                DateTime dtBitis = dtBaslangic.AddDays(ayGunSayisi - 1);

                TeslimatlariListele(dtBaslangic, dtBitis);
                IsleriTakvimeYukle();
            }
        }
    }
}