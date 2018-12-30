using Microsoft.Win32;
using System;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Facades;
using UC.CSP.MeetingCenter.BL.Services;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CenterFacade CenterFacade { get; }
        private RoomFacade RoomFacade { get; }
        private ReservationFacade ReservationFacade { get; }
        private ApplicationFacade ApplicationFacade { get; }
        public MainWindow()
        {
            InitializeComponent();
            CenterFacade = new CenterFacade();
            RoomFacade = new RoomFacade();
            ReservationFacade = new ReservationFacade();
            ApplicationFacade = new ApplicationFacade();
        }


        private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Do you really want to import data from file? All current data will be lost.", "CSV Import",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.ExecuteSafe(() => {
                    var dialog = new OpenFileDialog();
                    string fileName = "";
                    if (dialog.ShowDialog() ?? false)
                    {
                        fileName = dialog.FileName;
                    }

                    CenterFacade.ImportFromCsv(fileName);

                    RefreshMeetingCenterTab();
                    RefreshReservationsTab();
                }, errorMessageText: "Import failed.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CenterFacade.GetById(1);
            CenterFacade.Update(new Center());
            CenterFacade.GetById(1);
            ApplicationFacade.LoadData();
            ReservationDatePicker.SelectedDate = DateTime.Today;
            RefreshMeetingCenterTab();
            RefreshReservationsTab();
            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ApplicationFacade.HasDataChanged())
            {
                var messageBoxResult = MessageBox.Show("Do you want to save the changes?", "Meeting Center", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.None:
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        this.ExecuteSafe(() => { ApplicationFacade.Save(); }, errorAction: () => e.Cancel = true);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationFacade.Save();
        }

        #region Centers tab 

        private void RefreshMeetingCenterTab()
        {
            CentersListBox.ItemsSource = CenterFacade.GetAllCenters();
            CentersListBox.Items.Refresh();
            RoomsListBox.Items.Refresh();
        }

        private void NewCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                var form = new CenterForm(FormMode.New);
                if (form.ShowDialog() ?? false)
                {
                    var center = form.RetrieveFormData();
                    CenterFacade.Create(center);
                    CentersListBox.Items.Refresh();
                }
            }
        }

        private void EditCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                var form = new CenterForm(FormMode.Edit, selectedCenter);
                if (form.ShowDialog() ?? false)
                {
                    var center = form.RetrieveFormData();
                    CenterFacade.Update(center);
                    CentersListBox.Items.Refresh();
                }
            }
        }

        private void DeleteCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                if (MessageBox.Show($"Do you really want to delete room {selectedCenter.Name}?", "Delete",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CenterFacade.Delete(selectedCenter);
                    CentersListBox.Items.Refresh();
                }
            }
        }

        private void NewRoomButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new RoomForm(FormMode.New);
            if (form.ShowDialog() ?? false)
            {
                var room = form.RetrieveFormData();
                RoomFacade.Create(room);
                RoomsListBox.Items.Refresh();
            }
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsListBox.SelectedItem is Room selectedRoom)
            {
                var form = new RoomForm(FormMode.Edit, selectedRoom);
                if (form.ShowDialog() ?? false)
                {
                    var room = form.RetrieveFormData();
                    RoomFacade.Update(room);
                    RoomsListBox.Items.Refresh();
                }
            }
        }

        private void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsListBox.SelectedItem is Room selectedRoom)
            {
                if (MessageBox.Show($"Do you really want to delete room {selectedRoom.Name}?", "Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    RoomFacade.Delete(selectedRoom);
                    RoomsListBox.Items.Refresh();
                }
            }
        }

        private void CentersListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                RoomsListBox.ItemsSource = selectedCenter.Rooms;
                CenterNameTextBlock.Text = selectedCenter.Name;
                CenterDescriptionTextBlock.Text = selectedCenter.Description;
                CenterCodeTextBlock.Text = selectedCenter.Code;
            }
        }

        private void RoomsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RoomsListBox.SelectedItem is Room selectedRoom)
            {
                RoomNameTextBlock.Text = selectedRoom.Name;
                RoomDescriptionTextBlock.Text = selectedRoom.Description;
                RoomCodeTextBlock.Text = selectedRoom.Code;
                RoomCapacityTextBlock.Text = selectedRoom.Capacity.ToString();
                RoomVideoCheckBox.IsChecked = selectedRoom.HasVideo;
            }
        }

        #endregion
        

        #region Meetings planning tab

        private void RefreshReservationsTab()
        {
            CentersComboBox.ItemsSource = CenterFacade.GetAllCenters();
            CentersComboBox.Items.Refresh();
            if (RoomsComboBox.SelectedItem is Room selectedRoom &&
                ReservationDatePicker.SelectedDate is DateTime selectedDate)
            {
                ReservationListBox.ItemsSource =
                    ReservationFacade.GetReservationsByRoomAndDate(selectedRoom.Id, selectedDate);
            }
            else
            {
                ReservationListBox.ItemsSource = null;
            }
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExecuteSafe(() => {
                //var dialog = new OpenFileDialog();
                //string fileName = "";
                //if (dialog.ShowDialog() ?? false)
                //{
                //    fileName = dialog.FileName;
                //}

                var service = new JsonExportService();
                service.Export("export.json");
            }, errorMessageText: "Export failed.");
        }

        private void NewReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsComboBox.SelectedItem is Room selectedRoom &&
                ReservationDatePicker.SelectedDate is DateTime selectedDate)
            {
                var form = new ReservationForm(FormMode.New, selectedRoom, selectedDate);
                if (form.ShowDialog() ?? false)
                {
                    RefreshReservationsTab();
                }
            }
            else
            {
                MessageBox.Show("Please select Center, Room and Date of meeting first.", "",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem is Reservation selectedReservation)
            {
                var form = new ReservationForm(FormMode.Edit, selectedReservation);
                if (form.ShowDialog() ?? false)
                {
                    RefreshReservationsTab();
                }
            }
        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem is Reservation selectedReservation)
            {
                if (MessageBox.Show($"Do you really want to reservation {selectedReservation}?", "Delete",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ReservationFacade.Delete(selectedReservation);
                    ReservationListBox.Items.Refresh();
                }
            }
        }

        private void CentersComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CentersComboBox.SelectedItem is Center selectedCenter)
            {
                RoomsComboBox.ItemsSource = selectedCenter.Rooms;
            }
            else
            {
                RoomsComboBox.ItemsSource = null;
            }
        }

        private void RoomsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RefreshReservationsTab();
        }

        private void ReservationDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RefreshReservationsTab();
        }

        private void ReservationListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ReservationListBox.SelectedItem is Reservation selectedReservation)
            {
                FromToTextBlock.Text = selectedReservation.ToString();
                NoteTextBlock.Text = selectedReservation.Note;
                CustomerTextBlock.Text = selectedReservation.Customer;
                PersonsCountTextBlock.Text = selectedReservation.ExpectedPersonsCount.ToString();
                ReservationVideoCheckBox.IsChecked = selectedReservation.VideoConference;
            }
        }

        #endregion
    }
}

