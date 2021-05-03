using System;
using ICSharpCode.SharpZipLib.Zip;

namespace ProstLib
{
    public class Compress
    {
        #region Compress
        /// <summary>
        /// 폴더 압축하기
        /// </summary>
        /// <param name="sourceDirectoryPath">소스 디렉토리 경로</param>
        /// <param name="targetFilePath">타겟 ZIP 파일 경로</param>
        public static void CompressDirectoryToFile(string sourceDirectoryPath, string targetFilePath)
        {
            System.IO.DirectoryInfo sourceDirectoryInfo = new System.IO.DirectoryInfo(sourceDirectoryPath);
            System.IO.FileStream targetFileStream = new System.IO.FileStream(targetFilePath, System.IO.FileMode.Create);
            ZipOutputStream zipOutputStream = new ZipOutputStream(targetFileStream);
            zipOutputStream.SetComment(sourceDirectoryPath);
            zipOutputStream.SetLevel(9);
            byte[] bufferByteArray = new byte[2048];
            foreach (System.IO.FileInfo sourceFileInfo in sourceDirectoryInfo.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                string sourceFileName = sourceFileInfo.FullName.Substring(sourceDirectoryInfo.FullName.Length + 1);
                zipOutputStream.PutNextEntry(new ZipEntry(sourceFileName));
                using (System.IO.FileStream sourceFileStream = sourceFileInfo.OpenRead())
                {
                    while (true)
                    {
                        int readCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                        if (readCount == 0)
                        {
                            break;
                        }
                        zipOutputStream.Write(bufferByteArray, 0, readCount);
                    }
                }
                zipOutputStream.CloseEntry();
            }
            zipOutputStream.Finish();
            zipOutputStream.Close();
        }


        /// <summary>
        /// 파일들 압축하기
        /// </summary>
        /// <param name="sourceDirectoryPath">소스 디렉토리 경로</param>
        /// <param name="targetFilePath">타겟 ZIP 파일 경로</param>
        public static void CompressFilesToFile(string[] sourceFilePath, string targetFilePath)
        {
            System.IO.FileStream targetFileStream = new System.IO.FileStream(targetFilePath, System.IO.FileMode.Create);
            ZipOutputStream zipOutputStream = new ZipOutputStream(targetFileStream);
            zipOutputStream.SetLevel(9);
            byte[] bufferByteArray = new byte[2048];
            foreach (string filePath in sourceFilePath)
            {
                System.IO.FileInfo sourceFileInfo = new System.IO.FileInfo(filePath);
                string sourceFileName = sourceFileInfo.FullName.Substring(filePath.Length + 1);
                zipOutputStream.PutNextEntry(new ZipEntry(sourceFileName));
                using (System.IO.FileStream sourceFileStream = sourceFileInfo.OpenRead())
                {
                    while (true)
                    {
                        int readCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                        if (readCount == 0)
                        {
                            break;
                        }
                        zipOutputStream.Write(bufferByteArray, 0, readCount);
                    }
                }
                zipOutputStream.CloseEntry();
            }
            zipOutputStream.Finish();
            zipOutputStream.Close();
        }

        /// <summary>
        /// 파일 압축 해제하기
        /// </summary>
        /// <param name="sourceFilePath">소스 파일 경로</param>
        /// <param name="targetDirectoryPath">타겟 디렉토리 경로</param>
        public static void DecompressFileToFile(string sourceFilePath, string targetDirectoryPath)
        {
            System.IO.DirectoryInfo targetDirectoryInfo = new System.IO.DirectoryInfo(targetDirectoryPath);

            if (!targetDirectoryInfo.Exists)
            {
                targetDirectoryInfo.Create();
            }

            System.IO.FileStream sourceFileStream = new System.IO.FileStream(sourceFilePath, System.IO.FileMode.Open);
            ZipInputStream zipInputStream = new ZipInputStream(sourceFileStream);

            byte[] bufferByteArray = new byte[2048];

            while (true)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(sourceFilePath);

                ZipEntry zipEntry = zipInputStream.GetNextEntry();

                if (zipEntry == null)
                {
                    break;
                }

                if (zipEntry.Name.LastIndexOf('\\') > 0)
                {
                    string subdirectory = fi.Name;

                    if (!System.IO.Directory.Exists(System.IO.Path.Combine(targetDirectoryInfo.FullName, subdirectory)))
                    {
                        targetDirectoryInfo.CreateSubdirectory(subdirectory);
                    }
                }

                System.IO.FileInfo targetFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(targetDirectoryInfo.FullName, fi.Name));

                using (System.IO.FileStream targetFileStream = targetFileInfo.Create())
                {
                    while (true)
                    {
                        int readCount = zipInputStream.Read(bufferByteArray, 0, bufferByteArray.Length);

                        if (readCount == 0)
                        {
                            break;
                        }

                        targetFileStream.Write(bufferByteArray, 0, readCount);
                    }
                }
            }
            zipInputStream.Close();
        }
        #endregion

















    }
}
