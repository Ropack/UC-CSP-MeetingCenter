using System;
using System.Windows;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for CenterForm.xaml
    /// </summary>
    public partial class CenterForm : Window
    {
        private Center Center { get; set; }
        public CenterForm(FormMode mode)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(new Center());
        }
        public CenterForm(FormMode mode, Center center)
        {
            InitializeComponent();
            SetTitle(mode);
            InitForm(center);
        }

        public Center RetrieveFormData()
        {
            Center.Code = CodeTextBox.Text;
            Center.Description = DescriptionTextBox.Text;
            Center.Name = NameTextBox.Text;
            return Center;
        }

        private void InitForm(Center center)
        {
            Center = center;
            CodeTextBox.Text = Center.Code;
            DescriptionTextBox.Text = Center.Description;
            NameTextBox.Text = Center.Name;
        }

        private void SetTitle(FormMode mode)
        {
            if (mode == FormMode.New)
            {
                TitleTextBlock.Text = "Create new Center";
            }
            else if (mode == FormMode.Edit)
            {
                TitleTextBlock.Text = "Edit Center";
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
