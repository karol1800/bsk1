using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace bsk_nr_1
{
    public class Caesar
    {
        public void Caesar_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Caesar:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        Caesar_variables();
                        break;
                    case ConsoleKey.D2:
                        Caesar_encrypt();
                        break;
                    case ConsoleKey.D3:
                        Caesar_decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void Caesar_variables()
        {
     
            Console.Clear();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key#1");
            string key1 = Console.ReadLine();
            Console.WriteLine("Implement Key#2");
            string key2 = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("Caesar_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key1);
                writer.WriteLine(key2);
            }
                
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();   
            Caesar_start();
        }
        public void Caesar_decrypt()
        {

            string[] variables = new string[3];
            int i = 0, key1a,key2a, key1b, key2b;
            Console.Clear();
            using (StreamReader sr = new StreamReader("Caesar_Enc_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            
           
            Console.WriteLine("New or Old key");
            Console.WriteLine("1.Stantard");
            Console.WriteLine("2.New");
            ConsoleKeyInfo button = Console.ReadKey();
            switch (button.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    key1a = int.Parse(variables[1]);
                    key2a = int.Parse(variables[2]);
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + CaesarDecrypt(variables[0], key1a, key2a));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key#1");
                    key1b = int.Parse(Console.ReadLine());
                    Console.WriteLine("Implement Key#2");
                    key2b = int.Parse(Console.ReadLine());
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + CaesarDecrypt(variables[0], key1b, key2b));
                    break;

            }
            
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Caesar_start();
        }
        public void Caesar_encrypt()
        {
            string[] variables = new string[3];
            int i = 0, key1,key2;
            Console.Clear();
            using (StreamReader sr = new StreamReader("Caesar_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            key1 = int.Parse(variables[1]);
            key2 = int.Parse(variables[2]);
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = CaesarEncrypt(variables[0], key1,key2);
            Console.WriteLine("Encrypted: " + encryptedtext);
            using (StreamWriter writer = new StreamWriter("Caesar_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
                writer.WriteLine(variables[2]);
            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Caesar_start();
        }
        public static string CaesarEncrypt(string message, int k0, int k1)
        {
            string al = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string exit = "";
            for (int i = 0; i < message.Length; i++)
            {
                int ind = mod(al.IndexOf(char.ToUpper(message.ElementAt(i))) * k1 + k0, al.Length);
                exit += al.ElementAt(ind);
            }
            return exit;

        }
        public static string CaesarDecrypt(string message, int k0, int k1)
        {
            string al = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int fi = 12;
            string exit= "";
            for (int i = 0; i < message.Length; i++)
            {
                BigInteger ind = mod((al.IndexOf(char.ToUpper(message.ElementAt(i))) + (al.Length - k0)) * BigInteger.Pow(k1, fi - 1), al.Length);
                exit += al.ElementAt((int)ind);
            }
            return exit;
        }

        static int mod(int a, int n)
        {
            return (a % n + n) % n;
        }
        static BigInteger mod(BigInteger a, int n)
        {
            return (a % n + n) % n;
        }


    }
    


}

