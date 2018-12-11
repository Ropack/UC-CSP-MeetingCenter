using System;
using Microsoft.Win32;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Facades;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CenterFacade CenterFacade { get; }
        private RoomFacade RoomFacade { get; }
        private ApplicationFacade ApplicationFacade { get; }
        public MainWindow()
        {
            InitializeComponent();
            CenterFacade = new CenterFacade();
            RoomFacade = new RoomFacade();
            ApplicationFacade = new ApplicationFacade();
        }

        #region Centers tab 
        private void NewCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                var form = new CenterForm();
                if (form.ShowDialog().Value)
                {
                    CenterFacade.Create(new Center()
                    {
                        Name = form.NameTextBox.Text,
                        Code = form.CodeTextBox.Text,
                        Description = form.DescriptionTextBox.Text
                    });
                    CentersListBox.Items.Refresh();
                }
            }
        }

        private void EditCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentersListBox.SelectedItem is Center selectedCenter)
            {
                var form = new CenterForm();
                form.NameTextBox.Text = selectedCenter.Name;
                form.CodeTextBox.Text = selectedCenter.Code;
                form.DescriptionTextBox.Text = selectedCenter.Description;
                if (form.ShowDialog().Value)
                {
                    var center = selectedCenter;
                    center.Name = form.NameTextBox.Text;
                    center.Code = form.CodeTextBox.Text;
                    center.Description = form.DescriptionTextBox.Text;
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
            var form = new RoomForm();
            if (form.ShowDialog().Value)
            {
                RoomFacade.Create(new Room()
                {
                    Name = form.NameTextBox.Text,
                    Code = form.CodeTextBox.Text,
                    Description = form.DescriptionTextBox.Text,
                    Capacity = Convert.ToInt32(form.CapacityTextBox.Text)
                    // TODO: validate user input
                });
                RoomsListBox.Items.Refresh();
            }
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsListBox.SelectedItem is Room selectedRoom)
            {
                var form = new RoomForm();
                form.NameTextBox.Text = selectedRoom.Name;
                form.CodeTextBox.Text = selectedRoom.Code;
                form.DescriptionTextBox.Text = selectedRoom.Description;
                form.CapacityTextBox.Text = selectedRoom.Capacity.ToString();
                if (form.ShowDialog().Value)
                {
                    CenterFacade.Update(new Center()
                    {
                        Name = form.NameTextBox.Text,
                        Code = form.CodeTextBox.Text,
                        Description = form.DescriptionTextBox.Text
                    });
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
            if (dialog.ShowDialog().Value)
            {
                fileName = dialog.FileName;
            }

            CenterFacade.ImportFromCsv(fileName);
            CentersListBox.ItemsSource = CenterFacade.GetAllCenters();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationFacade.LoadData();
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

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationFacade.Save();
        }
        #endregion

        #region Meetings planning tab
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewReservationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditReservationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
