using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptologyMatrixCirculation
{
    public class Decrypt
    {
        public string DecryptedText { get; set; }
        private List<List<List<byte>>> DecryptedBlocks { get; set; }
        private List<List<string>> UsedCirculations { get; set; }
        private string DecryptedBits { get; set; }

        public Decrypt(List<List<string>> usedCirculations)
        {
            this.DecryptedBlocks = new List<List<List<byte>>>();
            this.UsedCirculations = usedCirculations;
        }

        public void Run(string encryptedText)
        {
            string bits = DecryptText(encryptedText);
            List<List<List<byte>>> encryptedBlocks = MakeBlocks(bits);

            DecryptBits(encryptedBlocks, UsedCirculations[0]);

            DecryptedText = Encoding.UTF8.GetString(
                Regex.Split(DecryptedBits.Replace(" ", string.Empty), "(.{8})")
                .Where(binary => !String.IsNullOrEmpty(binary))
                .Select(binary => Convert.ToByte(binary, 2))
                .ToArray()
            );

            Console.WriteLine($"\n DecryptedText = {DecryptedText}");
        }

        private List<List<List<byte>>> MakeBlocks(string encryptedBits)
        {
            List<string> blocks = new List<string>();

            int index;

            for (int i = 0; i < encryptedBits.Length/64 ; i++)
            {
                index = i * 64;
                blocks.Add(encryptedBits.Substring(index, 64));
            }

            List<List<string>> blocksStr = new List<List<string>>();

            foreach (string line in blocks)
            {
                List<string> block = new List<string>();

                for (int j = 0; j < 8; j++)
                {
                    index = j * 8;
                    block.Add(line.Substring(index, 8));
                }
                blocksStr.Add(block);
            }

            List<List<List<byte>>> blockList = new List<List<List<byte>>>();

            foreach (List<string> block in blocksStr)
            {
                List<List<byte>> byteList = new List<List<byte>>();
                foreach (string line in block)
                {
                    List<byte> bitList = new List<byte>();
                    foreach (char element in line)
                    {
                        bitList.Add(Convert.ToByte(element.ToString()));
                    }
                    byteList.Add(bitList);
                }
                blockList.Add(byteList);
            }
            return blockList;
        }

        private void DecryptBits(List<List<List<byte>>> encryptedBlocks, List<string> circulations)
        {
            byte[,] bitsMatrix = new byte[8, 8];
            int index = 0;

            foreach (List<List<byte>> block in encryptedBlocks)
            {
                foreach (List<byte> line in block)
                {
                    foreach (byte bit in line)
                    {
                        int posX = Convert.ToInt32(circulations[index].Substring(0, 1));
                        int posY = Convert.ToInt32(circulations[index].Substring(1, 1));
                        bitsMatrix[posY, posX] = bit;
                        index++;
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        DecryptedBits += bitsMatrix[j, i];
                    }
                    DecryptedBits += " ";
                }
                index = 0;
            }
        }

        private string DecryptText(string encryptedText)
        {
            string bits = null;
            string decimalValue = "";

            foreach (char value in encryptedText)
            {
                if (Char.IsDigit(value))
                {
                    if (decimalValue.Length == 3)
                    {
                        int digit = Convert.ToInt32(decimalValue.ToString());
                        bits += ConvertToBinary(digit);

                        decimalValue = value.ToString();
                    }
                    else
                    {
                        decimalValue += value;
                    }
                }
                else
                {
                    if (decimalValue.Length == 3)
                    {
                        int digit = Convert.ToInt32(decimalValue.ToString());
                        bits += ConvertToBinary(digit);
                        decimalValue = "";
                    }

                    byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
                    
                    foreach (byte byt in bytes)
                    {
                        bits += ConvertToBinary(byt);
                    }
                }
            }
            if (decimalValue.Length > 0)
            {
                int digit = Convert.ToInt32(decimalValue.ToString());
                bits += ConvertToBinary(digit);
                decimalValue = "";
            }
            return bits;
        }

        private string ConvertToBinary(int digit)
        {
            string bits = null;

            for (int i = 0; i < 8; i++)
            {
                bits += (digit % 2);
                digit = digit / 2;
            }

            char[] array = bits.ToCharArray();
            Array.Reverse(array);

            return new string(array);
        }
    }
}
