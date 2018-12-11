using System;
using System.Windows;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for RoomForm.xaml
    /// </summary>
    public partial class RoomForm : Window
    {
        private Room Room { get; set; }
        public RoomForm(FormMode mode)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(new Room());
        }
        public RoomForm(FormMode mode, Room room)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(room);
        }
        public Room RetrieveFormData()
        {
            Room.Code = CodeTextBox.Text;
            Room.Description = DescriptionTextBox.Text;
            Room.Name = NameTextBox.Text;
            Room.Capacity = Convert.ToInt32(CapacityTextBox.Text);
            return Room;
        }

        private void InitForm(Room room)
        {
            Room = room;
            CodeTextBox.Text = Room.Code;
            DescriptionTextBox.Text = Room.Description;
            NameTextBox.Text = Room.Name; 
            CapacityTextBox.Text = Room.Capacity.ToString();
        }

        private void SetTitle(FormMode mode)
        {
            if (mode == FormMode.New)
            {
                TitleTextBlock.Text = "Create new Room";
            }
            else if (mode == FormMode.Edit)
            {
                TitleTextBlock.Text = "Edit Room";
            }
            else
            {
                throw new ArgumentException("Unsupported form mode");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
