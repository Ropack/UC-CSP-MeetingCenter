using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.BL.Facades;
using UC.CSP.MeetingCenter.BL.Validation;
using ValidationError = UC.CSP.MeetingCenter.BL.Validation.ValidationError;

namespace UC.CSP.MeetingCenter.APP
{
    /// <summary>
    /// Interaction logic for StockAccessoryForm.xaml
    /// </summary>
    public partial class StockAccessoryForm : Window
    {
        private AccessoryFacade AccessoryFacade { get; }
        private StockFormMode Mode { get; }
        public StockAccessoryForm(StockFormMode mode)
        {
            InitializeComponent();
            AccessoryFacade = new AccessoryFacade();
            SetTitle(mode);
            InitForm();
            Mode = mode;
        }

        private void InitForm()
        {
            var categories = AccessoryFacade.GetCategories();
            CategoryComboBox.ItemsSource = categories;
        }

        private void SetTitle(StockFormMode mode)
        {
            if (mode == StockFormMode.In)
            {
                TitleTextBlock.Text = "Accept delivery of Accessory";
            }
            else if (mode == StockFormMode.Out)
            {
                TitleTextBlock.Text = "Hand over Accessory";
            }
            else
            {
                throw new ArgumentException("Unsupported form mode");
            }
        }
        public AccessoryStockDTO RetrieveFormData()
        {
            var accessoryStock = new AccessoryStockDTO();
            var validationErrors = new List<ValidationError>();
            accessoryStock.Mode = Mode;

            if (int.TryParse(CountTextBox.Text, out var count))
            {
                accessoryStock.Count = count;
            }
            else
            {
                validationErrors.Add(new ValidationError("Count must be a number."));
            }

            if (AccessoryComboBox.SelectedItem is AccessoryDTO accessory)
            {
                accessoryStock.Accessory = accessory;
            }
            else
            {
                validationErrors.Add(new ValidationError("Please select accessory."));
                throw new ValidationException(validationErrors);
            }
            accessoryStock.Validate(validationErrors);

            return accessoryStock;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExecuteSafe(() =>
            {
                var accessory = RetrieveFormData();
                if (Mode == StockFormMode.In)
                {
                    AccessoryFacade.Receipt(accessory);
                }
                else // out
                {
                    AccessoryFacade.Issue(accessory);
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

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccessoryComboBox.ItemsSource = AccessoryFacade.GetByCategory((int) CategoryComboBox.SelectedValue);
        }
    }
}
