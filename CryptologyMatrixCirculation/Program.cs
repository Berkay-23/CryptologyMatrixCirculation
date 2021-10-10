using System;
using System.Collections.Generic;

namespace CryptologyMatrixCirculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Circulations circulations = new Circulations();
            Encrypt encrypt = new Encrypt();
            Decrypt decrypt = new Decrypt();

        AskForCryption:
            Console.Write(" {0}\n {1}\n {2}\n\n Select function: ", "Encryption (e)", "Decryption (d)", "Quit (q)");

            switch (Console.ReadLine().ToLower().Trim())
            {
                case "e":
                AskForMetodsE:
                    Console.WriteLine("\n ------------------------------------ \n");
                    List<string> circulationE = AskForMetods(circulations);

                    if (circulationE == null)
                    {
                        Console.Write(" Wrong choice!");
                        Console.WriteLine("\n ------------------------------------ \n");
                        goto AskForMetodsE;
                    }
                    else
                    {
                        Console.Write($"\n Write to Text: ");
                        string defaultText = Convert.ToString(Console.ReadLine());

                        encrypt.Run(defaultText, circulationE);

                        Console.WriteLine("\n ------------------------------------ ");
                        Console.WriteLine($"\n Encrypted Text: {encrypt.EncryptedText}");
                        Console.WriteLine("\n ------------------------------------ \n");

                        AskForMenuE:
                        Console.Write(" {0}\n {1}\n {2}\n\n Select: ", "Decrypt (d)", "Main Menu (m)", "Quit (q)");

                        switch (Console.ReadLine().ToLower())
                        {
                            case "d":
                                encrypt.EncryptedBits = null;
                                decrypt.DecryptedBits = null;
                                goto AskForMetodsD;

                            case "m":
                                Console.WriteLine("\n ------------------------------------ \n");
                                encrypt.EncryptedBits = null;
                                decrypt.DecryptedBits = null;
                                goto AskForCryption;

                            case "q":
                                Environment.Exit(0);
                                break;

                            default:
                                Console.Write("Wrong choice!");
                                Console.WriteLine("\n ------------------------------------ \n");
                                goto AskForMenuE;
                        }
                    }

                    break;

                case "d":
                AskForMetodsD:
                    Console.WriteLine("\n ------------------------------------ \n");
                    
                    List<string> circulationD = AskForMetods(circulations);

                    if (circulationD == null)
                    {
                        Console.Write(" Wrong choice!");
                        Console.WriteLine("\n ------------------------------------ \n");
                        goto AskForMetodsD;
                    }
                    else
                    {
                        Console.Write($"\n Write to Encrypted Text: ");
                        string defaultText = Convert.ToString(Console.ReadLine());

                        decrypt.Run(defaultText, circulationD);

                        Console.WriteLine("\n ------------------------------------ ");
                        Console.Write($" Decrypted Text: {decrypt.DecryptedText}");
                        Console.WriteLine("\n ------------------------------------ \n");

                    AskForMenuD:
                        Console.Write(" {0}\n {1}\n {2}\n\n Select: ", "Encrypt (e)", "Main Menu (m)", "Quit (q)");

                        switch (Console.ReadLine().ToLower())
                        {
                            case "e":
                                encrypt.EncryptedBits = null;
                                decrypt.DecryptedBits = null;
                                goto AskForMetodsE;

                            case "m":
                                Console.WriteLine("\n ------------------------------------ \n");
                                encrypt.EncryptedBits = null;
                                decrypt.DecryptedBits = null;
                                goto AskForCryption;

                            case "q":
                                Environment.Exit(0);
                                break;

                            default:
                                Console.Write("Wrong choice!");
                                Console.WriteLine("\n ------------------------------------ \n");
                                goto AskForMenuD;
                        }
                    }
                    break;

                case "q":
                    Environment.Exit(0);
                    break;

                default:
                    Console.Write(" Wrong choice!");
                    Console.WriteLine("\n ------------------------------------ \n");
                    goto AskForCryption;
            }
            
            Console.Read();
        }

        private static List<string> AskForMetods(Circulations circulations)
        {
            AskForCirculation:
            Console.Write(" {0} \n {1} \n {2} \n {3}\n\n Select the cryption method: ", "Spiral (1)", "Symmetrical (2)", "Diyagonal (3)", "Quit (q)");
            string response = Console.ReadLine().ToLower();

            List<string> circulation = new List<string>();
            //List<List<string>> circulationList = new List<List<string>>();

            switch (response)
            {
                case "1":
                    circulation = circulations.circulation_1;
                    //circulationList.Add(circulation);
                    break;

                case "2":
                    circulation = circulations.circulation_2;
                    //circulationList.Add(circulation);
                    break;

                case "3":
                    circulation = circulations.circulation_3;
                    //circulationList.Add(circulation);
                    break;

                case "q":
                    Environment.Exit(0);
                    break;

                default:

                    //if (response.Contains("1"))
                    //    circulationList.Add(circulations.circulation_1);

                    //if (response.Contains("2"))
                    //    circulationList.Add(circulations.circulation_2);

                    //if (response.Contains("3"))
                    //    circulationList.Add(circulations.circulation_3);

                    goto AskForCirculation;
            }
            return circulation;
        }
    }
}
