using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YelpProject.GUISetup
{
    class DialogSetup
    {
        public DialogSetup(int status, String text) {
            OpenDialog(status,text);
        }

        private void OpenDialog(int status, String text)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            var caption = "Status Update";
            if (status != 0)
            {
                var messageBoxText = text + " Success!";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                var messageBoxText = text + " Fail!";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
