using System;

namespace CS_UserpermitAndCompress
{
    /// <summary>
    /// 순환 중복 검사
    /// </summary>
    public class CRC32
    {
        private UInt32[] crc32Table;
        private const int BUFFER_SIZE = 8192;

        private Int32 _TotalBytesRead = 0;
        public Int32 TotalBytesRead
        {
            get
            {
                return _TotalBytesRead;
            }
        }

        /// <summary>
        /// 지정된 스트림에 대한 CRC32를 반환
        /// </summary>
        /// <param name="input">CRC32를 계산할 스트림</param>
        /// <returns>CRC32</returns>
        public UInt32 GetCrc32(System.IO.Stream input)
        {
            return GetCrc32AndCopy(input, null);
        }

        /// <summary>
        /// 지정된 스트림에 대한 CRC32를 반환하고 입력 내용을 출력 스트림에 기록하십시오.
        /// </summary>
        /// <param name="input">The stream over which to calculate the CRC32</param>
        /// <param name="output">The stream into which to deflate the input</param>
        /// <returns>the CRC32 calculation</returns>
        public UInt32 GetCrc32AndCopy(System.IO.Stream input, System.IO.Stream output)
        {
            unchecked
            {
                UInt32 crc32Result;
                crc32Result = 0xFFFFFFFF;
                byte[] buffer = new byte[BUFFER_SIZE];
                int readSize = BUFFER_SIZE;

                _TotalBytesRead = 0;
                int count = input.Read(buffer, 0, readSize);
                if (output != null) output.Write(buffer, 0, count);
                _TotalBytesRead += count;
                while (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        crc32Result = ((crc32Result) >> 8) ^ crc32Table[(buffer[i]) ^ ((crc32Result) & 0x000000FF)];
                    }
                    count = input.Read(buffer, 0, readSize);
                    if (output != null) output.Write(buffer, 0, count);
                    _TotalBytesRead += count;

                }

                return ~crc32Result;
            }
        }


        /// <summary>
        /// Construct an instance of the CRC32 class, pre-initialising the table
        /// for speed of lookup.
        /// </summary>
        public CRC32()
        {
            unchecked
            {
                // This is the official polynomial used by CRC32 in PKZip.
                // Often the polynomial is shown reversed as 0x04C11DB7.
                UInt32 dwPolynomial = 0xEDB88320;
                UInt32 i, j;

                crc32Table = new UInt32[256];

                UInt32 dwCrc;
                for (i = 0; i < 256; i++)
                {
                    dwCrc = i;
                    for (j = 8; j > 0; j--)
                    {
                        if ((dwCrc & 1) == 1)
                        {
                            dwCrc = (dwCrc >> 1) ^ dwPolynomial;
                        }
                        else
                        {
                            dwCrc >>= 1;
                        }
                    }
                    crc32Table[i] = dwCrc;
                }
            }
        }
    }
}
