using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bsk_nr_1
{
    class Vigener
    {
        public void Vigener_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Vigener:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                Caesar caesar = new Caesar();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        Vigener_variables();
                        break;
                    case ConsoleKey.D2:
                        Vigener_encrypt();
                        break;
                    case ConsoleKey.D3:
                        Vigener_Decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void Vigener_variables()
        {
            Console.Clear();
            VigenereCipher Vcipher = new VigenereCipher();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key");
            string key = Console.ReadLine();
            Console.WriteLine("Press Any Button to Back");
            using (StreamWriter writer = new StreamWriter("Vigener_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key);
            }
                
            
            Console.ReadKey();
            Vigener_start();
        }
        public void Vigener_encrypt()
        {   
            string[] variables = new string[2];
            int i = 0;
            Console.Clear();
            using (StreamReader sr = new StreamReader("Vigener_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            
            VigenereCipher vcipher = new VigenereCipher();
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = vcipher.Encrypt(variables[1], variables[0]);
            Console.WriteLine("Encrypted: " + encryptedtext);

            using (StreamWriter writer = new StreamWriter("Vigener_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Vigener_start();
        }
        public void Vigener_Decrypt()
        {

            VigenereCipher vcipher = new VigenereCipher();
            string[] variables = new string[2];
            int i = 0;
            string key;
            Console.Clear();
            using (StreamReader sr = new StreamReader("Vigener_Enc_Variables.txt"))
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
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + vcipher.Decrypt(variables[1], variables[0]));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key");
                    key = Console.ReadLine();
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + vcipher.Decrypt(variables[0], key));
                    break;

            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Vigener_start();
        }
        class VigenereCipher
    {
        Dictionary<sbyte, char> AlphabetOrder = new Dictionary<sbyte, char>();

        public VigenereCipher()
        {
            //Tworzymy wlasny Alfabet [A-Z]
            #region alphabet
            AlphabetOrder.Add(0, 'A');
            AlphabetOrder.Add(1, 'B');
            AlphabetOrder.Add(2, 'C');
            AlphabetOrder.Add(3, 'D');
            AlphabetOrder.Add(4, 'E');
            AlphabetOrder.Add(5, 'F');
            AlphabetOrder.Add(6, 'G');
            AlphabetOrder.Add(7, 'H');
            AlphabetOrder.Add(8, 'I');
            AlphabetOrder.Add(9, 'J');
            AlphabetOrder.Add(10, 'K');
            AlphabetOrder.Add(11, 'L');
            AlphabetOrder.Add(12, 'M');
            AlphabetOrder.Add(13, 'N');
            AlphabetOrder.Add(14, 'O');
            AlphabetOrder.Add(15, 'P');
            AlphabetOrder.Add(16, 'Q');
            AlphabetOrder.Add(17, 'R');
            AlphabetOrder.Add(18, 'S');
            AlphabetOrder.Add(19, 'T');
            AlphabetOrder.Add(20, 'U');
            AlphabetOrder.Add(21, 'V');
            AlphabetOrder.Add(22, 'W');
            AlphabetOrder.Add(23, 'X');
            AlphabetOrder.Add(24, 'Y');
            AlphabetOrder.Add(25, 'Z');
            #endregion
        }

        private bool CheckIfEmptyString(string Key, string Text) // Sprawdzenie czy napis lub klucz sa puste
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrWhiteSpace(Key))
            {
                return true; // klucz jest pusty
            }
            if (string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Text))
            {
                return true; // napis jest pusty
            }
            return false; // napis i klucz nie sa puste
        }

        public string Encrypt(string Key, string Text)
        {
            try
            {
                Key = Key.ToUpper();
                Text = Text.ToUpper();

                if (CheckIfEmptyString(Key, Text)) { return "Please input a valid string value!"; }

                string ciphertext = "";

                int i = 0;

                foreach (char element in Text)
                {
                    if (!Char.IsLetter(element)) { ciphertext += element; }  //jezeli znak nie jest znakiem z alfabetu znak pozostaje taki sam
                    else
                    {
                        //pobieramy jaka wartosc jest przypisana do danego znaku w alfabecie 
                        sbyte TOrder = AlphabetOrder.FirstOrDefault(x => x.Value == element).Key; // dla znaku z tekstu
                        sbyte KOrder = AlphabetOrder.FirstOrDefault(x => x.Value == Key[i]).Key;  // dla znaku z klucza
                        
                        sbyte Final = (sbyte)(TOrder + KOrder); // dodajemy te dwie wartosci o otrzymujemy zafrzyfrowany znak z przeciecia z tablicy Vinegere
                        if (Final > 25) { Final -= 26; } // jezeli wartosc >25 odejmujemy -26, aby miescilo sie w alfabecie
                        ciphertext += AlphabetOrder[Final]; // dodajemy zaszyfrowany znak do slowa
                        i++;
                    }

                    if (i == Key.Length) { i = 0; } // powielanie klucza aby dopasowac go do dl. slowa
                }

                return ciphertext;

            }
            catch (Exception E)
            {
                return "Error: " + E.Message;
            }
        }

        public string Decrypt(string Key, string Text)
        {
            try
            {
                Key = Key.ToUpper();
                Text = Text.ToUpper();

                if (CheckIfEmptyString(Key, Text)) { return "Please input a valid string value!"; }

                string plaintext = "";

                int i = 0;

                foreach (char element in Text)
                {
                    if (!Char.IsLetter(element)) { plaintext += element; }
                    else
                    {
                        sbyte TOrder = AlphabetOrder.FirstOrDefault(x => x.Value == element).Key; 
                        sbyte KOrder = AlphabetOrder.FirstOrDefault(x => x.Value == Key[i]).Key;
                        sbyte Final = (sbyte)(TOrder - KOrder); // odejmujemy wartosc zaszyfrowanego znaku od wartosci klucza, aby otrzymac pierwotny znak
                        if (Final < 0) { Final += 26; } // jezeli wyszlismy poza alfabet dodajemy +26
                        plaintext += AlphabetOrder[Final];
                        i++;
                    }
                    if (i == Key.Length) { i = 0; }
                }

                return plaintext;

            }
            catch (Exception E)
            {
                return "Error: " + E.Message;
            }
        }

    }

    }
}
