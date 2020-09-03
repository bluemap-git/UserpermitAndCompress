using System;
using System.Diagnostics;
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

        #region Zip
        private void selectSavePath_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Zip Files (*.zip)|*.zip| All Files(*.*)| *.*", //필터
                DefaultExt = "zip" // 기본 확장자
            };

            if (sfd.ShowDialog() == true)
            {
                tbSavePath.ToolTip = tbSavePath.Text = sfd.FileName;
            }
        }

        private void selectFolderPath_Click(object sender, RoutedEventArgs e)
        {
            var cofd = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };

            if (cofd.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                tbFolderPath.ToolTip = tbFolderPath.Text = cofd.FileName;
            }
        }

        private void btnZip_Click(object sender, RoutedEventArgs e)
        {
            if (tbFolderPath.Text.Length == 0 || tbSavePath.Text.Length == 0)
            {
                MessageBox.Show("경로 선택 바람.");
                return;
            }
            else
            {
                ProstLib.Compress.CompressFileToFile(tbFolderPath.Text, tbSavePath.Text);
                MessageBox.Show("압축 완료.");

            }
        }
        #endregion

        #region Unzip

        private void selectZipFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Zip Files (*.zip)|*.zip| All Files(*.*)| *.*", //필터
                DefaultExt = "zip", // 기본 확장자
            };

            if (ofd.ShowDialog() == true)
            {
                tbZipFilePath.ToolTip = tbZipFilePath.Text = ofd.FileName;
            }
        }
        private void selectUnzipFolderPath_Click(object sender, RoutedEventArgs e)
        {
            var cofd = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };

            if (cofd.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                tbUnzipFolderPath.ToolTip = tbUnzipFolderPath.Text = cofd.FileName;
            }
        }

        private void btnUnzip_Click(object sender, RoutedEventArgs e)
        {
            if (tbZipFilePath.Text.Length == 0 || tbUnzipFolderPath.Text.Length == 0)
            {
                MessageBox.Show("경로 선택 바람.");
                return;
            }
            else
            {
                ProstLib.Compress.DecompressFileToFile(tbZipFilePath.Text, tbUnzipFolderPath.Text);
                MessageBox.Show("압축 해제 완료.");
            }
        }
        #endregion

        #region Userpermit
        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            //InitializationVector
            //byte[] IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] IV = ProstLib.Converter.HexStringToByteHex(tbIV.Text);



            //M_ID
            string strM_ID = tbMID.Text;


            //M_KEY
            //byte[] M_KEY = new byte[] { 0x4D, 0x5A, 0x79, 0x67, 0x70, 0x65, 0x77, 0x4A, 0x73, 0x43, 0x70, 0x52, 0x72, 0x66, 0x4F, 0x72 };
            byte[] M_KEY = ProstLib.Converter.HexStringToByteHex(tbMKEY.Text);
            string strM_KEY = ProstLib.Converter.ByteHexToHexString(M_KEY);



            //HW_ID
            //byte[] HW_ID = new byte[] { 0x40, 0x38, 0x4B, 0x45, 0xB5, 0x45, 0x96, 0x20, 0x11, 0x14, 0xFE, 0x99, 0x04, 0x22, 0x01 };
            byte[] HW_ID = ProstLib.Converter.HexStringToByteHex(tbHWID.Text);
            string strHW_ID = ProstLib.Converter.ByteHexToHexString(HW_ID);



            //Encrypred HW ID = Aes(HW_ID, M_KEY, IV)
            byte[] Encrypred_HW_ID = ProstLib.AES.Encrypt(HW_ID, M_KEY, IV);
            string encrypredHWID = ProstLib.Converter.ByteHexToHexString(Encrypred_HW_ID);
            tbEncrypredHWID.Text = encrypredHWID;



            //Checksum = CRC32(EncrypredHWID)
            string checksum = ProstLib.Converter.StringToCRC32(tbEncrypredHWID.Text).ToString("X2");
            while (checksum.Length < 8)
                checksum = "0" + checksum;
            tbChecksum.Text = checksum;



            //User Permit = EncrypredHWID + Checksum + M_ID
            tbUserPermit.Text = ProstLib.Converter.GetUserpermit(tbMID.Text, tbMKEY.Text, tbHWID.Text, tbIV.Text); ;
        }

        private void btnResultDecryption_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserPermit.Text.Length == 46)
            {
                //The user permit is 28 characters long and must be written as ASCII text with the following mandatory
                //사용자 허가는 28자로 다음과 같은 필수 항목이 포함된 ASCII 텍스트로 작성되어야 함
                //UserPermit(24) = EncrypredHWID(10) + Checksum(8) + M_ID(6)

                tbDecryptionEncryptedHWID.Text = tbUserPermit.Text.Substring(0, 32);

                tbDecryptionChecksum.Text = tbUserPermit.Text.Substring(32, 8);

                tbDecryptionM_ID.Text = tbUserPermit.Text.Substring(40, 6);

                tbDecryptionHW_ID.Text = string.Empty;
                foreach (byte c in ProstLib.AES.Decrypt(ProstLib.Converter.HexStringToByteHex(tbDecryptionEncryptedHWID.Text), ProstLib.Converter.HexStringToByteHex(tbMKEY.Text), ProstLib.Converter.HexStringToByteHex(tbIV.Text)))
                    tbDecryptionHW_ID.Text += c.ToString("x2").ToUpper();


                //EncrypredHWID + Checksum + M_ID
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbEncrypredHWID.Text = string.Empty;
            tbChecksum.Text = string.Empty;
            tbUserPermit.Text = string.Empty;
            tbDecryptionEncryptedHWID.Text = string.Empty;
            tbDecryptionChecksum.Text = string.Empty;
            tbDecryptionM_ID.Text = string.Empty;
            tbDecryptionHW_ID.Text = string.Empty;
        }

        #endregion

        string iv = "00000000000000000000000000000000";
        string input_filePath = @"..\File\104KR00KR01_20200103F24.h5";
        string input_encKey = "DB0FFDC650";
        string output_EncryptPath = @"..\File\104KR00_20200102F24.h5";
        string output_DecryptPath = @"..\File\104KR00_20200102F24_dec.h5";

        private void Btn_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            ProstLib.CryptographicCompression.Encrypt_Compress_AES128(input_encKey, iv, input_filePath, output_EncryptPath);
            //System.Diagnostics.Process.Start(@"..\Encrypt");
        }

        private void Btn_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            ProstLib.CryptographicCompression.Decrypt_Decompress_AES128(input_encKey, iv, output_EncryptPath, output_DecryptPath);
            //System.Diagnostics.Process.Start(@"..\Decrypt");
        }

        private void Btn_Check_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs1 = new FileStream(input_filePath, FileMode.Open);
            FileStream fs2 = new FileStream(output_DecryptPath, FileMode.Open);

            if (fs1.Length == fs2.Length)
            {
                MessageBox.Show("성공");
                System.Diagnostics.Process.Start(@"..\File");
            }
            else
            {
                MessageBox.Show("실패");
            }
            fs1.Close();
            fs2.Close();
        }

        private void Button_Click_TabEncrypt_Encrypt(object sender, RoutedEventArgs e)
        {
            var strPlainText = TabEncrypt_PlainText.Text;
            var strKey = TabEncrypt_Key.Text;

            var bytePlainText = ProstLib.Converter.HexStringToByteHex(strPlainText);
            var byteKey = ProstLib.Converter.HexStringToByteHex(strKey);

            var paddedKey = ProstLib.Function.FillPadding(byteKey);

            var iv = new byte[16];

            var byteEncryptedText = ProstLib.AES.Encrypt(bytePlainText, paddedKey, iv);
            var strEncryptedText = ProstLib.Converter.ByteHexToHexString(byteEncryptedText);
            TabEncrypt_EncryptedText.Text = strEncryptedText;
        }

        private void Button_Click_TabDecrypt_Decrypt(object sender, RoutedEventArgs e)
        {
            var strEncryptedText = TabDecrypt_EncryptedText.Text;
            var strKey = TabDecrypt_Key.Text;

            var byteEncryptedText = ProstLib.Converter.HexStringToByteHex(strEncryptedText);
            var byteKey = ProstLib.Converter.HexStringToByteHex(strKey);

            var paddedKey = ProstLib.Function.FillPadding(byteKey);

            var iv = new byte[16];

            var bytePlainText = ProstLib.AES.Decrypt(byteEncryptedText, paddedKey, iv);
            var strPlainText = ProstLib.Converter.ByteHexToHexString(bytePlainText);
            TabDecrypt_PlainText.Text = strPlainText;
        }

        private void Button_Click_TabEncrypt_SelectInputFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();

            if (dialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                TabEncrypt_InputFilePath.Text = dialog.FileName;
                var fileName = 
                    Path.GetFileNameWithoutExtension(dialog.FileName) + 
                    "_encrypted" + 
                    Path.GetExtension(dialog.FileName);
                TabEncrypt_OutputFilePath.Text = Path.Combine(
                    Path.GetDirectoryName(dialog.FileName),
                    fileName);
            }
        }

        private void Button_Click_TabEncrypt_FileEncrypt(object sender, RoutedEventArgs e)
        {
            var inputFilePath = TabEncrypt_InputFilePath.Text;
            var outputFilePath = TabEncrypt_OutputFilePath.Text;
            var key = TabEncrypt_FileKey.Text;

            try
            {
                if (ProstLib.CryptographicCompression.Encrypt_Compress_AES128(key, iv, inputFilePath, outputFilePath))
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_TabEncrypt_ShowOutputFile(object sender, RoutedEventArgs e)
        {
            var outputFilePath = TabEncrypt_OutputFilePath.Text;

            if (File.Exists(outputFilePath))
            {
                var arg = "/select, \"" + outputFilePath + "\"";
                Process.Start("explorer.exe", arg);
            }
            else
            {
                MessageBox.Show("No output file");
            }
        }

        private void Button_Click_TabDecrypt_SelectInputFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();

            if (dialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                TabDecrypt_InputFilePath.Text = dialog.FileName;
                var fileName =
                    Path.GetFileNameWithoutExtension(dialog.FileName) +
                    "_decrypted" +
                    Path.GetExtension(dialog.FileName);
                TabDecrypt_OutputFilePath.Text = Path.Combine(
                    Path.GetDirectoryName(dialog.FileName),
                    fileName);
            }
        }

        private void Button_Click_TabDecrypt_ShowOutputFile(object sender, RoutedEventArgs e)
        {
            var outputFilePath = TabDecrypt_OutputFilePath.Text;

            if (File.Exists(outputFilePath))
            {
                var arg = "/select, \"" + outputFilePath + "\"";
                Process.Start("explorer.exe", arg);
            }
            else
            {
                MessageBox.Show("No output file");
            }
        }

        private void Button_Click_TabDecrypt_FileEncrypt(object sender, RoutedEventArgs e)
        {
            var inputFilePath = TabDecrypt_InputFilePath.Text;
            var outputFilePath = TabDecrypt_OutputFilePath.Text;
            var key = TabDecrypt_FileKey.Text;

            try
            {
                if (ProstLib.CryptographicCompression.Decrypt_Decompress_AES128(key, iv, inputFilePath, outputFilePath))
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
