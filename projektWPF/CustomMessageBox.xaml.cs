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

namespace projektWPF
{
    public partial class CustomMessageBox : Window
    {
        public enum CustomMessageBoxResult
        {
            Restart,
            Quit
        }

        public CustomMessageBoxResult Result { get; private set; }

        public CustomMessageBox(string message, string title)
        {
            InitializeComponent();
            this.Title = title;
            MessageTextBlock.Text = message;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CustomMessageBoxResult.Restart;
            this.DialogResult = true;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CustomMessageBoxResult.Quit;
            this.DialogResult = true;
        }

        public static CustomMessageBoxResult Show(string message, string title)
        {
            CustomMessageBox dialog = new CustomMessageBox(message, title);
            dialog.ShowDialog();
            return dialog.Result;
        }
    }
}