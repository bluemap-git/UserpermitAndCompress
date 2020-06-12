using System;
using System.Collections.Generic;
using System.Text;

namespace ProstLib_Core
{
    class Converter
    {
    }
}
using System;

namespace ProstLib
{
    public class Converter
    {
        /// <summary>
        /// inputString을 CRC32로 변환
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static uint StringToCRC32(string inputString)
        {
            return (new CRC32()).GetCrc32(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(inputString)));
        }

        /// <summary>
        /// 입력된 값으로 userpermit을 생성하여 반환
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="mkey"></param>
        /// <param name="hwid"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string GetUserpermit(string mid, string mkey, string hwid, string iv = "00000000000000000000000000000000")
        {
            //String To HEX
            byte[] IV = Converter.HexStringToByteHex(iv);
            byte[] M_ID = Converter.HexStringToByteHex(mid);
            byte[] M_KEY = Converter.HexStringToByteHex(mkey);
            byte[] HW_ID = Converter.HexStringToByteHex(hwid);

            //암호화
            //Encrypred HW ID = Aes(HW_ID, M_KEY, IV)
            byte[] Encrypred_HW_ID = AES.Encrypt(HW_ID, M_KEY, IV);
            //String To HEX
            string strEncrypred_HW_ID = Converter.ByteHexToHexString(Encrypred_HW_ID);
            //Checksum = CRC32(EncrypredHWID)
            string checksum = StringToCRC32(strEncrypred_HW_ID).ToString("X2");

            return strEncrypred_HW_ID + checksum + mid;
        }

        /// <summary>
        /// Stream을 byte[]로 변환
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(System.IO.Stream stream)
        {
            System.IO.MemoryStream TempMemoryStream;
            Int32 reads = 0; //임시 메모리스트림에 작성 
            using (System.IO.Stream st = stream)
            {
                using (System.IO.MemoryStream output = new System.IO.MemoryStream())
                {
                    st.Position = 0; Byte[] buffer = new Byte[256];
                    while (0 < (reads = st.Read(buffer, 0, buffer.Length)))
                    {
                        output.Write(buffer, 0, reads);
                    }
                    TempMemoryStream = output;
                    output.Flush();
                } // in using 
            } // out using
            byte[] bytes = TempMemoryStream.ToArray();

            return bytes;
        }

        /// <summary>
        /// 16진수 문자를 16진수 Byte[]로 변환
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        /// 
        public static byte[] HexStringToByteHex(string strHex)
        {
            if (strHex == null)
                return null;
            else if (strHex.Length == 0)
                return null;
            else if (strHex.Length % 2 != 0)
            {
                //MessageBox.Show("HexString는 홀수일 수 없습니다. - " + strHex);
                return null;
            }

            byte[] bytes = new byte[strHex.Length / 2];

            for (int count = 0; count < strHex.Length; count += 2)
            {
                bytes[count / 2] = System.Convert.ToByte(strHex.Substring(count, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 16진수 Byte[]를 16진수 문자로 변환
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        /// 
        public static string ByteHexToHexString(byte[] strHex)
        {
            string result = string.Empty;
            foreach (byte c in strHex)
                result += c.ToString("x2").ToUpper();
            return result;
        }

        public static string ByteToString(byte[] strHex)
        {
            string result = string.Empty;
            foreach (byte c in strHex)
                result += c.ToString("x2");
            return result;
        }
    }
}
