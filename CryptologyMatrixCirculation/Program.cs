using System;
using System.Collections.Generic;

namespace CryptologyMatrixCirculation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Circulations circulations = new Circulations();

            //Console.WriteLine("Select the encryption method or methods : \n {0} \n {1} \n {2} ", "1 (spiral)", "2 (symmetrical)", "3 (diyagonal)");
            //string response = Console.ReadLine().ToLower();

            //List<string> circulation = null;

            //while (true)
            //{

            //    switch (response)
            //    {
            //        case "1":
            //            circulation = circulations.circulation_1;
            //            break;

            //        case "2":
            //            circulation = circulations.circulation_2;
            //            break;

            //        case "3":
            //            circulation = circulations.circulation_3;
            //            break;

            //        default:
            //            break;
            //    }



            //}
            Console.Write(" Write the TEXT: ");
            string defaultText = Convert.ToString(Console.ReadLine());

            Encrypt encrypt = new Encrypt();
            encrypt.Run(defaultText);

            Decrypt decrypt = new Decrypt(encrypt.UsedCirculations);
            decrypt.Run(encrypt.EncryptedText);

            Console.Read();
        }
    }
}
