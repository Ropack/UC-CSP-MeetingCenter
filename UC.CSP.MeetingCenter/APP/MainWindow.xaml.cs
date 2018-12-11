using Microsoft.Win32;
using System;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Facades;
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

        #region Centers tab 
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

        private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            string fileName = "";
            if (dialog.ShowDialog() ?? false)
            {
                fileName = dialog.FileName;
            }

            CenterFacade.ImportFromCsv(fileName);
            CentersListBox.ItemsSource = CenterFacade.GetAllCenters();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationFacade.LoadData();
            CentersListBox.ItemsSource = CenterFacade.GetAllCenters();

            CentersComboBox.ItemsSource = CenterFacade.GetAllCenters();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationFacade.Save();
        }

        #region Meetings planning tab
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsComboBox.SelectedItem is Room selectedRoom &&
                ReservationDatePicker.SelectedDate is DateTime selectedDate)
            {
                var form = new ReservationForm(FormMode.New, selectedRoom, selectedDate);
                if (form.ShowDialog() ?? false)
                {
                    var reservation = form.RetrieveFormData();
                    ReservationFacade.Create(reservation);

                    ReservationListBox.ItemsSource = (RoomsComboBox.SelectedItem as Room).Reservations;
                    ReservationListBox.Items.Refresh();
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
            //if (CentersComboBox.SelectedItem is Center selectedCenter)
            //    if (RoomsComboBox.SelectedItem is Room selectedRoom)
            if (ReservationListBox.SelectedItem is Reservation selectedReservation)
            {
                var form = new ReservationForm(FormMode.Edit, selectedReservation);
                if (form.ShowDialog() ?? false)
                {
                    var reservation = form.RetrieveFormData();
                    ReservationFacade.Update(reservation);
                    ReservationListBox.Items.Refresh();
                }
            }
        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {

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

        #endregion
    }
}
