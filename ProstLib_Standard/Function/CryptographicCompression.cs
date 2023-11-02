using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProstLib
{
    public static class CryptographicCompression
    {
        public static bool Encrypt_Compress_AES128(string key, string iv, string input_filePath, string output_filePath)
        {
            try
            {
                // 1. 압축
                string FileName = System.AppDomain.CurrentDomain.BaseDirectory + (new System.IO.FileInfo(input_filePath)).Name;
                Compress.CompressFilesToFile(new string[] { input_filePath }, FileName);

                // 2. 파일->Bytes 변환
                byte[] bytes = System.IO.File.ReadAllBytes(FileName);

                // 3. 암호화
                bytes = AES.Encrypt(bytes, Converter.HexStringToByteHex(Function.FillPadding(key)), Converter.HexStringToByteHex(iv));

                // 4. 파일 저장
                System.IO.File.WriteAllBytes(output_filePath, bytes);

                // 5. 임시 파일 제거
                System.IO.File.Delete(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static bool Decrypt_Decompress_AES128(string key, string iv, string input_filePath, string output_FolderPath)
        {
            try
            {
                // 1. 파일 -> Bytes 변환
                byte[] bytes = System.IO.File.ReadAllBytes(input_filePath);

                // 2. 복호화
                var paddedKey = Function.FillPadding(key);
                bytes = AES.Decrypt(bytes, Converter.HexStringToByteHex(paddedKey), Converter.HexStringToByteHex(iv));

                // 3. Bytes -> 파일 변환
                string FileName = System.AppDomain.CurrentDomain.BaseDirectory + (new System.IO.FileInfo(input_filePath)).Name;
                System.IO.File.WriteAllBytes(FileName, bytes);

                // 4. 압축해제 / 파일 저장
                Compress.DecompressFileToFile(FileName, output_FolderPath);
                
                // 5. 임시 파일 제거
                System.IO.File.Delete(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
