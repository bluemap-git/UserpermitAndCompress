using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;

namespace PerformanceTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerateKeyPair();   
        }

        static void GenerateKeyPair()
        {
            var stopwatch = new Stopwatch();
            
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    System.IO.File.Delete("dsaparam.txt");
                    System.IO.File.Delete("iho.crt");
                    System.IO.File.Delete("iho.key");
                }
                catch (System.Exception e)
                {

                }

                RunOpenSSL("dsaparam -out dsaparam.txt 2048", stopwatch);
                RunOpenSSL("req -x509 -sha256 -nodes -days 365 -newkey dsa:dsaparam.txt -keyout iho.key -out iho.crt -subj \"/C=US/ST=YourState/L=YourCity/O=YourOrganization/OU=YourOrganizationalUnit/CN=yourdomain.com\"", stopwatch);
            }

            Console.WriteLine("키생성 평균 성능 : {0} μs", (stopwatch.Elapsed.TotalMilliseconds / 10.0) * 1000);
        }
        
        static void RunOpenSSL(string arguments, Stopwatch stopwatch)
        {
            // 새 프로세스 인스턴스를 생성합니다.
            ProcessStartInfo startInfo = new ProcessStartInfo();

            // 실행하고자 하는 외부 프로그램의 경로를 지정합니다.
            // 예를 들어, "notepad.exe"와 같이 실행 가능한 파일명을 사용할 수 있습니다.
            startInfo.FileName = "C:\\Program Files\\OpenSSL-Win64\\bin\\openssl.exe";

            // 프로그램 실행에 필요한 인자를 전달할 수 있습니다.
            startInfo.Arguments = arguments;

            // 창을 숨기거나 보이게 설정할 수 있습니다.
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            // 프로그램의 출력을 현재의 콘솔 창으로 리디렉션하고 싶다면 아래 옵션을 사용합니다.
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            // 프로세스를 시작합니다.
            Process process = new Process();
            process.StartInfo = startInfo;

            try
            {
                stopwatch.Start();
                process.Start();

                // 리디렉션된 출력을 읽습니다.
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // 외부 프로그램이 종료될 때까지 대기합니다.
                process.WaitForExit();
                stopwatch.Stop();

                // 출력과 에러를 콘솔에 표시합니다.
                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.Error.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void EncryptDecrypt()
        {
            string iv = "00000000000000000000000000000000";
            string key = "DB0FFDC650";

            string inputFilePath = "..\\File\\125SG0020230420_110804453.gml";

            byte[] bytes = System.IO.File.ReadAllBytes(inputFilePath);

            byte[] cypherText = null;
            byte[] plainText = null;

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                cypherText = ProstLib.AES.Encrypt(bytes, ProstLib.Converter.HexStringToByteHex(ProstLib.Function.FillPadding(key)), ProstLib.Converter.HexStringToByteHex(iv));
            }
            stopwatch.Stop();
            Console.WriteLine("암호화 평균 성능: {0} μs", (stopwatch.Elapsed.TotalMilliseconds / 10.0) * 1000);

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                plainText = ProstLib.AES.Decrypt(cypherText, ProstLib.Converter.HexStringToByteHex(ProstLib.Function.FillPadding(key)), ProstLib.Converter.HexStringToByteHex(iv));
            }
            stopwatch.Stop();
            Console.WriteLine("복호화 평균 성능: {0} μs", (stopwatch.Elapsed.TotalMilliseconds / 10.0) * 1000);
        }
    }
}
