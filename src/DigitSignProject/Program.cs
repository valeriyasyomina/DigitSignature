using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSignProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Input souce file name: ");
                string souseFileName = Console.ReadLine();   

                DigitSignature digitSignature = new DigitSignature();
                digitSignature.GenerateSignature(souseFileName);

                Console.WriteLine("Digit signature generated in file " + digitSignature.ORIGIN_DIGIT_SIGNATURE_FILE_NAME);

                Console.WriteLine("Input check file name: ");
                string checkFileName = Console.ReadLine();  

                Console.WriteLine("Input signature file name: ");
                string signFileName = Console.ReadLine();

                bool isSignatureCorrect = digitSignature.Check(checkFileName, signFileName);
                if (isSignatureCorrect)
                    Console.WriteLine("OK: Digit signature correct!");
                else
                    Console.WriteLine("ERROR: Digit signature incorrect!");
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}
