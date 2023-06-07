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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;

namespace lab8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void resPSPButton_Click(object sender, RoutedEventArgs e)
        {
            resultPSPText.Text = "Последовательность: \n";
            resultPSPText.Text += RSAgenerator.getRSAres() + "\n";
        }

        byte[] result;

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            int[] keyArr = new int[] { 61, 60, 23, 22, 21, 20 };
            
            string s = "";
            for (int i = 0; i < keyArr.Length; i++)
            {
                s += Encoding.ASCII.GetString(new byte[] { Convert.ToByte(keyArr[i]) });
            }

            byte[] key = ASCIIEncoding.ASCII.GetBytes(s);
            RC4 encoder = new RC4(key);
            string testString = encryptTextBox.Text;
            byte[] testBytes = ASCIIEncoding.ASCII.GetBytes(testString);
            result = encoder.Encode(testBytes, testBytes.Length);

            decryptTextBox.Text = ASCIIEncoding.ASCII.GetString(result);

        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            int[] keyArr = new int[] { 61, 60, 23, 22, 21, 20 };
            string s = "";
            for (int i = 0; i < keyArr.Length; i++)
            {
                s += Encoding.ASCII.GetString(new byte[] { Convert.ToByte(keyArr[i]) });
            }

            byte[] key = ASCIIEncoding.ASCII.GetBytes(s);

            RC4 decoder = new RC4(key);
            byte[] decryptedBytes = decoder.Decode(result, result.Length);
            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);

            encryptTextBox.Text = decryptedString;
        }
    }
}
