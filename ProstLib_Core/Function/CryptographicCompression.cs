using System;
using System.Collections.Generic;
using System.Text;

namespace ProstLib
{
    public static class CryptographicCompression
    {
        public static bool Encrypt_Compress_AES128(string key, string iv, string input_filePath, string output_filePath)
        {
            try
            {
                // 1. 파일 -> Bytes 변환
                byte[] bytes = System.IO.File.ReadAllBytes(input_filePath);

                // 2. 압축
                bytes = Compress.CompressByteToByte(bytes);

                // 3. 암호화
                bytes = AES.Encrypt(bytes, Converter.HexStringToByteHex(Function.FillPadding(key)), Converter.HexStringToByteHex(iv));

                // 4. 파일 저장
                System.IO.File.WriteAllBytes(output_filePath, bytes);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool Decrypt_Decompress_AES128(string key, string iv, string input_filePath, string output_filePath)
        {
            try
            {
                // 1. 파일 -> Bytes 변환
                byte[] bytes = System.IO.File.ReadAllBytes(input_filePath);

                // 2. 복호화
                bytes = AES.Decrypt(bytes, Converter.HexStringToByteHex(Function.FillPadding(key)), Converter.HexStringToByteHex(iv));

                // 3. 압축해제 
                bytes = Compress.DecompressByteToByte(bytes);

                // 4. 파일 저장
                System.IO.File.WriteAllBytes(output_filePath, bytes);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
