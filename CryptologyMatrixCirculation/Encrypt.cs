using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptologyMatrixCirculation
{
    public class Encrypt
    {
        public List<List<List<byte>>> EncryptedBlocks { get; set; }
        public string EncryptedBits { get; set; }
        public string EncryptedText { get; set; }

        public Encrypt()
        {
            this.EncryptedBlocks = new List<List<List<byte>>>();
        }

        public void Run(string text, List<string> circilation)
        {
            byte[] utf8_Bytes = Encoding.UTF8.GetBytes(text);

            List<List<byte>> bitsList = ToBitList(utf8_Bytes);
            List<List<List<byte>>> blocks = MakeBlock(bitsList);

            List<List<List<byte>>> encryptedBlocks = MixTheBits(circilation, blocks);

            EncryptedBlocks = encryptedBlocks;

            EncryptedText = EncryptText(GetByteList());
        }

        private List<List<byte>> ToBitList(byte[] bytes)
        {
            List<List<byte>> bitsList = new List<List<byte>>();

            byte number;

            foreach (byte byt in bytes)
            {
                List<byte> charBits = new List<byte>();
                number = byt;

                for (int i = 0; i < 8; i++)
                {
                    charBits.Add((byte)(number % 2));
                    number = (byte)(number / 2);
                }
                charBits.Reverse();
                bitsList.Add(charBits);
            }

            int remainder = 8 - bitsList.Count % 8;

            for (int i = 0; i < remainder; i++)
                bitsList.Add(new List<byte>() { 0, 0, 0, 0, 0, 0, 0, 0 });
                

            return bitsList;
        }

        private List<List<List<byte>>> MakeBlock(List<List<byte>> bitsList)
        {
            List<List<List<byte>>> blocks = new List<List<List<byte>>>();

            int counter = 0;

            List<List<byte>> block = new List<List<byte>>();

            foreach (List<byte> bits in bitsList)
            {
                if (counter % 8 == 0)
                {
                    block = new List<List<byte>>();
                    block.Add(bits);
                }
                else
                {
                    block.Add(bits);
                }
                if (block.Count == 8)
                {
                    blocks.Add(block);
                }
                counter++;
            }
            return blocks;
        }

        private List<List<List<byte>>> MixTheBits(List<string> circulation, List<List<List<byte>>> blocks)  
        {
            List<List<List<byte>>> newBlocks = new List<List<List<byte>>>();
            byte[,] bitsMatrix = new byte[8, 8];
            int x = 0, y = 0;

            foreach (List<List<byte>> block in blocks)
            {
                List<List<byte>> newBlock = new List<List<byte>>();

                foreach (List<byte> line in block)
                {
                    foreach (byte bit in line)
                    {
                        bitsMatrix[x, y] = bit;
                        y++;
                    }
                    x++;
                    y = 0;
                }

                List<byte> newBitList = new List<byte>();
                int counter = 0;
                x = 0;
                
                foreach (string positions in circulation)
                {
                    int posX = Convert.ToInt32(positions.Substring(1, 1));
                    int posY = Convert.ToInt32(positions.Substring(0, 1));

                    byte bit = bitsMatrix[posY, posX];

                    if (counter % 8 == 0)
                    {
                        newBitList = new List<byte>();
                        newBitList.Add(bit);
                    }
                    else
                    {
                        newBitList.Add(bit);
                    }

                    if(newBitList.Count == 8)
                    {
                        newBlock.Add(newBitList);
                    }
                    EncryptedBits += bit;
                    counter++;
                }
                newBlocks.Add(newBlock);
            }
            return newBlocks;
        }

        private List<string> GetByteList()
        {
            List<string> byteList = new List<string>();

            int index;

            for (int i = 0; i < EncryptedBits.Length/8; i++)
            {
                index = i * 8;
                byteList.Add(EncryptedBits.Substring(index, 8));
            }
            return byteList;
        }

        private string EncryptText(List<string> byteList)
        {
            string encryptText = null;

            foreach (string byt in byteList)
            {
                string text = Encoding.UTF8.GetString(Regex.Split(byt, "(.{8})").Where(binary => !String.IsNullOrEmpty(binary)).Select(binary => Convert.ToByte(binary, 2)).ToArray());

                char character = Convert.ToChar(text);

                bool valid = true;

                try
                {
                    Convert.ToByte(character);
                }
                catch (OverflowException)
                {
                    valid = false;
                }
                
                if ((Char.IsSymbol(character) || Char.IsLower(character) || Char.IsUpper(character) || Char.IsLetter(character) || Char.IsPunctuation(character)) && valid == true)
                {
                    encryptText += character;
                }
                else
                {
                    int decimalResult = 0;

                    for (int i = 0; i < byt.Length; i++)
                    {
                        int bit = Convert.ToInt32((byt[byt.Length - (i + 1)]).ToString());

                        if (bit == 1)
                            decimalResult = decimalResult + (int)Math.Pow(2,i);
                    }

                    string decimalValue = String.Format("{0:000}",decimalResult);

                    encryptText += decimalValue;
                }
            }
            return encryptText;
        }
    }
}
