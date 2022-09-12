using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieCollectionWPF.Messages
{
    /// <summary>
    /// Interaction logic for CompleteMessage.xaml
    /// </summary>
    public partial class CompleteMessage : Window
    {
        public CompleteMessage()
        {
            InitializeComponent();
        }
        public void Show(string completeMessage)
        {
            txtBlockMessageShow.Text = completeMessage;
            ShowDialog();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
