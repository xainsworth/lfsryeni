using System;
using System.Collections;
using System.Collections.Generic;


namespace ConsoleApp2
{
    internal class Program
    {
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static void PrintR(string n, params char[] columns)
        {
            int width = 7;
            string row = "|";
            row += AlignCentre(n, width) + "|";
            foreach (char column in columns)
            {
                row += AlignCenter(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static void PrintRenk(string n, params char[] columns)
        {
            int width = 7;
            string row = "|";
            row += AlignCentre(n, width) + "|";
            foreach (char column in columns)
            {
                row += AlignCenter(column, width) + "|";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(row);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static string AlignCenter(char tex, int width)
        {
            string v = "   ";
            return v + tex + v;
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', 6);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(7);
            }
        }

        public const int tableWidth = 49;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            

            Console.Write("Bir n değeri girin: ");
            int n = int.Parse(Console.ReadLine() ?? "3");

            int[,] lfsr = new int[n, n];
            string output = "", b_output = "";
            const string f = "0101111011011011";
            // const string f = "0000000000000000111111111111111101010101101010100101010110101010000000001111111100000000111111110000111100001111000011110000111100110011001100110011001100110011010101010101010101010101010101010101101010100101010110101010010101100110100110010110011010011001000000001111111111111111000000000000011000000000100000000000000000110011001100111100110011001100000011111111000000001111111100000101010110101010101010100101010101100110111001101111100110011001010101010101010110101010101010100110011010011001100110010110011000111100110000110011110011000011001011111111010011111001100111110110100101101001011010010110100100111100110000111100001100111100001111000011110000111100001111000111100110010111011010111101011001011010010110101010010110100101001100111100110011001100001100110110100101101001100101101001011000001111000011111111000011110000011010011001011010010110011010010011001111001100001100111100110001011010010110100101101001011010001111000011110011000011110000110110011001100110011001100110011001011010101001011010010101011010";
            int first_bit;
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                Console.Write("LFSR " + i + ": ");
                for (int j = 0; j < n; j++)
                {
                    lfsr[i, j] = r.Next(2);
                    Console.Write(lfsr[i, j]);
                }
                Console.WriteLine();

            }

            Console.Write("Kaydırma işlemine geçmek için bir tuşa basın.");
            Console.ReadKey();
            Console.Clear();

            for (int i = 0; i < Math.Pow(2, n); i++)
            {
                b_output = "";
                for (int j = 0; j < n; j++)
                {
                    Console.Write("LFSR " + (j + 1) + ": ");
                    for (int k = 0; k < n; k++)
                    {
                        Console.Write(lfsr[j, k]);
                    }
                    Console.WriteLine();

                    if ((n >= 2 && n <= 4) || (n >= 6 && n <= 7) || (n == 9)) // if ((n >= 2 && n <= 4) || (n >= 6 && n <= 7) || (n == 9))
                    {
                        first_bit = lfsr[j, n - 1] ^ lfsr[j, n - 2];
                    }
                    else if (n == 5)
                    {
                        first_bit = lfsr[j, n - 4] ^ lfsr[j, n - 1];
                    }
                    else if (n == 8)
                    {
                        first_bit = (lfsr[j, n - 1] ^ lfsr[j, n - 3]) ^ (lfsr[j, n - 4] ^ lfsr[j, n - 5]);
                    }
                    else
                    {
                        first_bit = lfsr[j, n - 4] ^ lfsr[j, n - 1];
                    }

                    b_output += lfsr[j, n - 1];

                    for (int k = n - 1; k > 0; k--)
                    {
                        lfsr[j, k] = lfsr[j, k - 1];
                    }
                    lfsr[j, 0] = first_bit;
                }

                int y = Convert.ToInt32(b_output, 2);
                output += f[y];
                if (true)
                {
                    Console.WriteLine("\n\n");
                    PrintLine();
                    PrintRow("N", "2^3", "2^2", "2^1", "2^0", "F(N)");
                    PrintLine();
                    string b;
                    for (int num = 0; num <= 15; num++)
                    {
                        b = Convert.ToString(num, 2).PadLeft(4, '0');
                        if (num == y) PrintRenk(num.ToString(), b[0], b[1], b[2], b[3], f[num]);
                        else PrintR(num.ToString(), b[0], b[1], b[2], b[3], f[num]);
                    }

                    /*
                    if (y >= 2)
                    {
                        b = Convert.ToString(y - 2, 2).PadLeft(10, '0');
                        PrintR((y - 2).ToString(), b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], f[y - 2]);
                        b = Convert.ToString(y - 1, 2).PadLeft(10, '0');
                        PrintR((y - 1).ToString(), b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], f[y - 1]);
                    }
                    b = Convert.ToString(y, 2).PadLeft(10, '0');
                    PrintRenk(y.ToString(), b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], f[y]);
                    if (y <= 1021)
                    {
                        b = Convert.ToString(y + 1, 2).PadLeft(10, '0');
                        PrintR((y + 1).ToString(), b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], f[y + 1]);
                        b = Convert.ToString(y + 2, 2).PadLeft(10, '0');
                        PrintR((y + 2).ToString(), b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], f[y + 2]);
                    }

                    */
                    PrintLine();
                    Console.WriteLine("\n\nOutput: " + b_output);
                    Console.Write("\nFonksiyondan çıkan: " + f[y] + "\nBitstream:" + output + "\nDevam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    Console.Clear();
                }


            }



            Console.WriteLine(output);
            string asc = "", sonuc = "", b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            for (int i = 0; i <= output.Length / 6; i++)
            {
                asc = "";
                for (int j = 0; j < 6; j++)
                {
                    if ((i * 6) + j < output.Length) asc = asc + output[(i * 6) + j];
                    else asc += "0";
                }
                sonuc += b64[Convert.ToInt32(asc, 2)];
            }

            while (sonuc.Length % 4 != 0) sonuc += "=";

            Console.WriteLine("İşlem bitti.\n\nBitstream: " + output + "\n\nElde edilen anahtar: " + sonuc + "\n");
            System.Threading.Thread.Sleep(-1);
        }
    }
}
