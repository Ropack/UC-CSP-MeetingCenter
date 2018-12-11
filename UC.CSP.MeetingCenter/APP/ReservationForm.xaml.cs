using System;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Queries;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        public ReservationForm()
        {
            InitializeComponent();
        }
        private Reservation Reservation { get; set; }
        public ReservationForm(FormMode mode, Room selectedRoom, DateTime selectedDate)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(new Reservation()
            {
                MeetingRoom = selectedRoom,
                Date = selectedDate.Date
            });
        }
        public ReservationForm(FormMode mode, Reservation reservation)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(reservation);
        }
        public Reservation RetrieveFormData()
        {
            Reservation.ExpectedPersonsCount = Convert.ToInt32(PersonsCountTextBox.Text);
            Reservation.Customer = CustomerTextBox.Text;
            Reservation.TimeFrom = TimeSpan.Parse($"{TimeFromHoursTextBox.Text}:{TimeFromMinutesTextBox.Text}");
            Reservation.TimeTo = TimeSpan.Parse($"{TimeToHoursTextBox.Text}:{TimeToMinutesTextBox.Text}");
            Reservation.VideoConference = VideoConferenceCheckBox.IsChecked ?? false;
            Reservation.Note = NoteTextBox.Text;
            return Reservation;
        }

        private void InitForm(Reservation reservation)
        {
            Reservation = reservation;
            PersonsCountTextBox.Text = Reservation.ExpectedPersonsCount.ToString();
            CustomerTextBox.Text = Reservation.Customer;
            TimeFromHoursTextBox.Text = Reservation.TimeFrom.Hours.ToString();
            TimeFromMinutesTextBox.Text = Reservation.TimeFrom.Minutes.ToString();
            TimeToHoursTextBox.Text = Reservation.TimeTo.Hours.ToString();
            TimeToMinutesTextBox.Text = Reservation.TimeTo.Minutes.ToString();
            VideoConferenceCheckBox.IsChecked = Reservation.VideoConference;
            NoteTextBox.Text = Reservation.Note;
        }

        private void SetTitle(FormMode mode)
        {
            if (mode == FormMode.New)
            {
                TitleTextBlock.Text = "Create new Reservation";
            }
            else if (mode == FormMode.Edit)
            {
                TitleTextBlock.Text = "Edit Reservation";
            }
            else
            {
                throw new ArgumentException("Unsupported form mode");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExecuteSafe(() =>
            {
                RetrieveFormData().Validate();
                DialogResult = true;
                Close();
            });
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
