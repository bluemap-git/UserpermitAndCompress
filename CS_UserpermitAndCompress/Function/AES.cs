namespace CS_UserpermitAndCompress
{
    public class AES
    {
        //암호화
        public static byte[] Encrypt(byte[] plainText, byte[] Key, byte[] IV)
        {
            byte[] cipherText;
            //byte[] plain = { 0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08 };


            // Create an Aes object
            // with the specified key and IV.
            using (System.Security.Cryptography.Aes aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
                aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                System.Security.Cryptography.ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                cipherText = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
            }

            return cipherText;

            //byte[] result = new byte[newPlain.Length];
            //System.Buffer.BlockCopy(cipherText, 0, result, 0, newPlain.Length);

            //// Return the encrypted bytes from the memory stream.
            //return result;
        }

        //복호화
        public static byte[] Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            byte[] plainText;
            // Create an Aes object
            // with the specified key and IV.
            using (System.Security.Cryptography.Aes aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
                aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                plainText = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
            }

            return plainText;

        }
    }
}
