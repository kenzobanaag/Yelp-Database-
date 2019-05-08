using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace YelpProject.GUISetup
{
    class PhotoSetup
    {
        private List<string> filePaths;
        private OpenFileDialog newDialog;
        private Window newTest;
        private int count = 0;

        public PhotoSetup() { }

        public PhotoSetup(List<string> paths) {
            filePaths = paths;
        }

        public void LoadImages()
        {
            if(filePaths.Count > 0)
            {
                LoadCurrentImage(SetCurrentImage());
            }
        }

        public Image SetCurrentImage()
        {
            BitmapImage myBitmapImage = new BitmapImage();
            try
            {
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(filePaths[count]);
                myBitmapImage.DecodePixelWidth = 500;
                myBitmapImage.EndInit();
            }
            catch(System.IO.DirectoryNotFoundException err)
            {
                return null;
            }
            catch(UriFormatException err)
            {
                filePaths.RemoveAt(count);
                return null;
            }
            catch(System.IO.FileNotFoundException err)
            {
                filePaths.RemoveAt(count);
                return null;
            }
            

            Image myNewImage = new Image();
            myNewImage.Width = 500;
            
            myNewImage.Source = myBitmapImage;
            return myNewImage;
        }

        public void LoadCurrentImage(Image myNewImage)
        {
            if(myNewImage != null)
            {
                if (newTest != null)
                {
                    newTest.Close();
                }

                System.Windows.Controls.Button next = new System.Windows.Controls.Button()
                {
                    Height = 25,
                    Content = "Next Photo"
                };

                next.Click += Next_Click;

                System.Windows.Controls.Button prev = new System.Windows.Controls.Button()
                {
                    Height = 25,
                    Content = "Previous Photo",
                };
                prev.Click += Prev_Click;


                StackPanel newPanel = new StackPanel();
                newPanel.Children.Add(next);
                newPanel.Children.Add(myNewImage);
                newPanel.Children.Add(prev);

                newTest = new Window()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                newTest.Content = newPanel;
                newTest.Show();
            }
            else
            {
                DialogSetup newDialog = new DialogSetup(0, "File Not Found, Open Photo");
            }
            
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0)
            {
                count--;
                LoadImages();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (count < filePaths.Count -1)
            {
                count++;
                LoadImages();
            }
        }
        

        public String AddImage()
        {
            String path = "";
            newDialog = new OpenFileDialog()
            {
                FileName = "Select an image",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*",
                Title = "Open image file",
                
            };
            if (newDialog.ShowDialog() == DialogResult.OK)
            {
                path = newDialog.FileName;
            }
            return path;
        }

        public Window GetWindow()
        {
            return newTest;
        }
    }
}
