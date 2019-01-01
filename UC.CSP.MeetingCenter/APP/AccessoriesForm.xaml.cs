using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.BL.Facades;
using UC.CSP.MeetingCenter.BL.Validation;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for AccessoriesForm.xaml
    /// </summary>
    public partial class AccessoriesForm : Window
    {
        private AccessoryFacade AccessoryFacade { get; }
        private FormMode Mode { get; }
        public AccessoriesForm(FormMode mode)
        {
            InitializeComponent();
            AccessoryFacade = new AccessoryFacade();
            SetTitle(mode);
            InitForm(new AccessoryDTO());

            Mode = mode;
        }
        private AccessoryDTO Accessory { get; set; }
        public AccessoriesForm(FormMode mode, AccessoryDTO accessory)
        {
            InitializeComponent();
            AccessoryFacade = new AccessoryFacade();
            SetTitle(mode);
            InitForm(accessory);

            Mode = mode;
        }
        public AccessoryDTO RetrieveFormData()
        {
            var validationErrors = new List<ValidationError>();
            Accessory.Name = NameTextBox.Text;
            Accessory.CategoryId = (CategoryComboBox.SelectedItem as CategoryDTO)?.Id ?? 0;

            if (int.TryParse(RecommendedMinCountTextBox.Text, out var recommendedMinCount))
            {
                Accessory.RecommendedMinCount = recommendedMinCount;
            }
            else
            {
                validationErrors.Add(new ValidationError("Recommended minimum stock must be a number."));
            }
            Accessory.Validate(validationErrors);

            return Accessory;
        }

        private void InitForm(AccessoryDTO accessory)
        {
            var categories = AccessoryFacade.GetCategories();
            CategoryComboBox.ItemsSource = categories;
            Accessory = accessory;
            NameTextBox.Text = accessory.Name;
            RecommendedMinCountTextBox.Text = accessory.RecommendedMinCount.ToString();
            CategoryComboBox.SelectedIndex =
                categories.IndexOf(categories.FirstOrDefault(c => c.Id == accessory.CategoryId));
        }

        private void SetTitle(FormMode mode)
        {
            if (mode == FormMode.New)
            {
                TitleTextBlock.Text = "Create new Accessory";
            }
            else if (mode == FormMode.Edit)
            {
                TitleTextBlock.Text = "Edit Accessory";
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
                var accessory = RetrieveFormData();

                AccessoryFacade.Save(accessory);
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
