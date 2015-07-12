<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="IsTakvimi.aspx.cs" Inherits="Akios.Web.Takvim.IsTakvimi" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnClientAppointmentInserting(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
        //function OnClientAppointmentResizing(sender, eventArgs) {
        //    //eventArgs.set_cancel(true);
        //}
        function OnClientAppointmentDeleting(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
        function OnClientAppointmenMoving(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
    </script>
    <style type="text/css">
        .rsWeekView .rsWrap {
            height: 0px !important;
        }

        .rsWeekView .rsDateWrap {
            height: 12px !important;
        }

        .rsWeekView .rsLastWrap {
            height: 12px !important;
        }

        .rsWeekView .rsApt {
            height: 60px !important;
        }
    </style>
    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 10%; vertical-align: top">
                <telerik:RadCalendar ID="RadCalendarIsTakvimi"
                    runat="server"
                    CalendarTableStyle-HorizontalAlign="Right"
                    EnableMonthYearFastNavigation="true"
                    EnableMultiSelect="false"
                    AutoPostBack="true"
                    OnSelectionChanged="RadCalendarIsTakvimi_SelectionChanged">
                </telerik:RadCalendar>
            </td>
            <td>
                <telerik:RadScheduler ID="RadSchedulerIsTakvimi"
                    runat="server"
                    Height="720px"
                    DayStartTime="00:00:00"
                    DayEndTime="24:00:00"
                    DataKeyField="ID"
                    DataSubjectField="Subject"
                    DataStartField="Start"
                    DataEndField="End"
                    Culture="tr-TR"
                    FirstDayOfWeek="Monday"
                    LastDayOfWeek="Sunday"
                    EnableRecurrenceSupport="false"
                    AllowDelete="false"
                    ShowFullTime="true"
                    HoursPanelTimeFormat="HH:mm"
                    DayView-ShowHoursColumn="false"
                    WeekView-ShowHoursColumn="false"
                    MonthView-AdaptiveRowHeight="true"
                    AllowInsert="False"
                    SelectedView="WeekView"
                    MinutesPerRow="10"
                    OnAppointmentCommand="RadSchedulerIsTakvimi_AppointmentCommand"
                    OnAppointmentCreated="RadSchedulerIsTakvimi_AppointmentCreated"
                    OnClientAppointmentInserting="OnClientAppointmentInserting"
                    OnClientAppointmentDeleting="OnClientAppointmentDeleting"
                    OnClientAppointmentMoving="OnClientAppointmenMoving"
                    OnFormCreated="RadSchedulerIsTakvimi_FormCreated"
                    OnNavigationCommand="RadSchedulerIsTakvimi_NavigationCommand">
                    <Localization ContextMenuGoToToday="Bugün" HeaderDay="Günlük Görünüm" HeaderMonth="Aylık Görünüm" HeaderNextDay="Sonraki Gün" HeaderPrevDay="Önceki Gün" HeaderToday="Bugün"
                        HeaderWeek="Haftalık Görünüm" AdvancedAllDayEvent="Tüm gün" AdvancedCalendarCancel="İptal" AdvancedCalendarOK="Tamam" AdvancedCalendarToday="Bugün" AdvancedClose="Kapat"
                        AdvancedDaily="Günlük" AdvancedDay="Gün" AdvancedDays="Gün(ler)" AdvancedDescription="Tanım" AdvancedDone="Bitti" AdvancedEvery="Her" AdvancedEveryWeekday="Her haftagünü"
                        AdvancedFirst="birinci" AdvancedFourth="dördüncü" AdvancedFrom="Başlangıç zamanı" AdvancedHourly="Saatlik" AdvancedHours="saat(ler)" AdvancedInvalidNumber="Geçersiz sayı"
                        AdvancedLast="son" AdvancedMaskDay="gün" AllDay="tüm gün işleri" ReminderWeek="hafta" Save="Kaydet" Show24Hours="24 saati göster" ShowAdvancedForm="Seçenekler"
                        ShowBusinessHours="İş saatlerini göster" HeaderTimeline="Ajanda Görünümü" ShowMore="Tümü" />
                    <DayView UserSelectable="true" />
                    <WeekView UserSelectable="true" />
                    <MonthView VisibleAppointmentsPerDay="3" />
                    <TimelineView UserSelectable="true" ShowDateHeaders="true" GroupingDirection="Vertical" NumberOfSlots="7" />
                    <MultiDayView UserSelectable="false" />
                    <AdvancedEditTemplate>
                        <table class="AnaTablo" style="width: 100%">
                            <tr>
                                <th style="width: 10%">Sipariş No:</th>
                                <td class="TdRenkSolaYasla">
                                    <asp:Label ID="LabelEditSiparisNo" runat="server"> <%# Eval("Subject") %></asp:Label>
                                    <asp:Label ID="LabelEditTeslimatID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Müşteri Adı Soyadı:</th>
                                <td>
                                    <asp:Label ID="LabelEditMusteriAdSoyad" runat="server">LabelEditTempMusteriAdSoyad Müşteri Adı Soyadı</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Adres:</th>
                                <td>
                                    <asp:Label ID="LabelEditAdres" runat="server">Adres</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Telefon:</th>
                                <td>
                                    <asp:Label ID="LabelEditTelefon" runat="server">Telefon</asp:Label></td>
                            </tr>
                            <tr>
                                <th>Teslimat Tarihi:</th>
                                <td>
                                    <telerik:RadDateTimePicker ID="DateTimePickerTeslimatTarihSaat" runat="server" Width="200px"></telerik:RadDateTimePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Teslimat Ekibi</th>
                                <td>
                                    <telerik:RadListBox ID="ListBoxTeslimatEkibi" runat="server" Height="300" Width="350" SelectionMode="Multiple" CheckBoxes="true" DataValueField="ID" DataTextField="AD">
                                    </telerik:RadListBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Teslimat Durumu</th>
                                <td>
                                    <asp:CheckBox ID="chcBoxTeslimatDurumu" runat="server" Text="Teslimat Tamamlandı" Checked="false" ToolTip="Teslimat tamamlandı ise seçiniz" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <telerik:RadButton ID="RadButtonIsKaydet" runat="server" Text="Kaydet" CommandName="IsKaydet">
                                        <Icon PrimaryIconCssClass="rbSave" SecondaryIconRight="4" SecondaryIconTop="3"></Icon>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RadButtonIsIptal" runat="server" Text="İptal" CommandName="IsIptal">
                                        <Icon PrimaryIconCssClass="rbCancel" PrimaryIconLeft="4" PrimaryIconTop="3"></Icon>
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </AdvancedEditTemplate>
                    <AppointmentTemplate>
                        <div>
                            <asp:Label ID="LableTeslimatDurum" runat="server">&nbsp;&nbsp;</asp:Label>
                            <asp:LinkButton ID="LabelAppointmentSiparisNo" runat="server" Font-Bold="true"><%# Eval("Subject") %></asp:LinkButton>
                            <br />
                            <asp:Label ID="LabelAppointmentMusteriAdSoyad" runat="server">Müşteri Adı Soyadı</asp:Label>
                            <asp:Label ID="LabelAppointmentAdresIlIlce" runat="server">Adres İl İlçe</asp:Label>
                            <asp:Label ID="LabelAppointmentAdres" runat="server">Adres</asp:Label>
                            <asp:Label ID="LabelAppointmentTelefon" runat="server">Telefon</asp:Label>
                            <asp:Label ID="LabelAppointmentTeslimatEkibi" runat="server">Teslimat Ekibi</asp:Label>
                        </div>
                    </AppointmentTemplate>
                </telerik:RadScheduler>
            </td>
        </tr>
    </table>

    <telerik:RadAjaxManager ID="RadAjaxManagerIsTakvimi" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanelIsTakvimi">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadSchedulerIsTakvimi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadSchedulerIsTakvimi" LoadingPanelID="RadAjaxLoadingPanelIsTakvimi"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadCalendarIsTakvimi" LoadingPanelID="RadAjaxLoadingPanelIsTakvimi"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadCalendarIsTakvimi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadSchedulerIsTakvimi" LoadingPanelID="RadAjaxLoadingPanelIsTakvimi"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelIsTakvimi" runat="server">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
