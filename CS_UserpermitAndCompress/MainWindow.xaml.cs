using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace CS_UserpermitAndCompress
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string iv = "00000000000000000000000000000000";
        string input_filePath = @"..\File\101KR003F4N00.000";
        string input_encKey = "7A933AEB4B";
        string output_EncryptPath = @"..\Encrypt\101KR003F4N00.000";
        string output_DecryptPath = @"..\Decrypt\101KR003F4N00.000";

        private void Btn_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            ProstLib.CryptographicCompression.Encrypt_Compress_AES128(input_encKey, iv, input_filePath, output_EncryptPath);

            System.Diagnostics.Process.Start(@"..\Encrypt");
        }


        private void Btn_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            ProstLib.CryptographicCompression.Decrypt_Decompress_AES128(input_encKey, iv, output_EncryptPath, output_DecryptPath);

            System.Diagnostics.Process.Start(@"..\Decrypt");
        }

        private void Btn_Check_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs1 = new FileStream(input_filePath, FileMode.Open);
            FileStream fs2 = new FileStream(output_DecryptPath, FileMode.Open);

            if (fs1.Length == fs2.Length)
            {
                MessageBox.Show("성공");
            }
            else
            {
                MessageBox.Show("실패");
            }
            fs1.Close();
            fs2.Close();
        }
    }
}
