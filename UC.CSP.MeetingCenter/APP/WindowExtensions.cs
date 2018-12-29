using System;
using System.Linq;
using System.Windows;
using UC.CSP.MeetingCenter.BL.Validation;

namespace UC.CSP.MeetingCenter.APP
{
    public static class WindowExtensions
    {
        public static bool ExecuteSafe(this Window window, Action action,
            string successMessageText = null, string errorMessageText = null,
            Action errorAction = null)
        {
            if (errorAction == null)
            {
                errorAction = () => { };
            }
            try
            {
                action();

                if (!string.IsNullOrEmpty(successMessageText))
                {
                    MessageBox.Show(successMessageText, "Success",
                        MessageBoxButton.OK, MessageBoxImage.None);
                }

                return true;
            }
            catch (ValidationException e)
            {
                errorAction();
                MessageBox.Show(string.Join(Environment.NewLine, e.ValidationErrors.Select(v => v.Message)), "Validation failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                errorAction();
                if (string.IsNullOrEmpty(errorMessageText))
                {
                    errorMessageText = "Unexpected error occured.";
                }
                MessageBox.Show(errorMessageText, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

    }
}