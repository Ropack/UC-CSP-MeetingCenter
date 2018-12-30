using System;
using System.Collections.Generic;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Facades;
using UC.CSP.MeetingCenter.BL.Validation;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        private Reservation Reservation { get; set; }
        private ReservationFacade ReservationFacade { get; }
        private FormMode Mode { get; }
        public ReservationForm(FormMode mode, Room selectedRoom, DateTime selectedDate)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(new Reservation()
            {
                Room = selectedRoom,
                RoomId = selectedRoom.Id,
                Date = selectedDate.Date
            });

            Mode = mode;
            ReservationFacade = new ReservationFacade();
        }
        public ReservationForm(FormMode mode, Reservation reservation)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(reservation);

            Mode = mode;
            ReservationFacade = new ReservationFacade();
        }
        public Reservation RetrieveFormData()
        {
            var validationErrors = new List<ValidationError>();

            if (int.TryParse(PersonsCountTextBox.Text, out var expectedPersonsCount))
            {
                Reservation.ExpectedPersonsCount = expectedPersonsCount;
            }
            else
            {
                validationErrors.Add(new ValidationError("Person count is in a wrong format."));
            }
            Reservation.Customer = CustomerTextBox.Text;
            if (TimeSpan.TryParse($"{TimeFromHoursTextBox.Text}:{TimeFromMinutesTextBox.Text}", out var timeFrom) &&
                TimeSpan.TryParse($"{TimeToHoursTextBox.Text}:{TimeToMinutesTextBox.Text}", out var timeTo))
            {
                Reservation.TimeFrom = Convert.ToDateTime(timeFrom.ToString());
                Reservation.TimeTo = Convert.ToDateTime(timeTo.ToString());
            }
            else
            {
                validationErrors.Add(new ValidationError("Time is in a wrong format."));
            }
            Reservation.VideoConference = VideoConferenceCheckBox.IsChecked ?? false;
            Reservation.Note = NoteTextBox.Text;

            Reservation.Validate(validationErrors);
            return Reservation;
        }

        private void InitForm(Reservation reservation)
        {
            Reservation = reservation;
            PersonsCountTextBox.Text = Reservation.ExpectedPersonsCount.ToString();
            CustomerTextBox.Text = Reservation.Customer;
            TimeFromHoursTextBox.Text = Reservation.TimeFrom.TimeOfDay.Hours.ToString();
            TimeFromMinutesTextBox.Text = Reservation.TimeFrom.TimeOfDay.Minutes.ToString();
            TimeToHoursTextBox.Text = Reservation.TimeTo.TimeOfDay.Hours.ToString();
            TimeToMinutesTextBox.Text = Reservation.TimeTo.TimeOfDay.Minutes.ToString();
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
                var reservation = RetrieveFormData();
                if (Mode == FormMode.New)
                {
                    ReservationFacade.Create(reservation);
                }
                else if (Mode == FormMode.Edit)
                {
                    ReservationFacade.Update(reservation);
                }
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
