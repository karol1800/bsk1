using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bsk_nr_1
{
    class Matrix_A
    {
        public void Matrix_A_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Matrix A:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        MatrixA_variables();
                        break;
                    case ConsoleKey.D2:
                        MatrixA_encrypt();
                        break;
                    case ConsoleKey.D3:
                        MatrixA_decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void MatrixA_variables()
        {
            Console.Clear();
            //VigenereCipher Vcipher = new VigenereCipher();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key");
            string key = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("MatrixA_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key);
            }

            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_A_start();
        }
        public void MatrixA_decrypt()
        {

            string[] variables = new string[2];
            int i = 0;
            string key_word1,key_word2;
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixA_Enc_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            key_word1 = variables[1];
            int[] keytab1 = new int[key_word1.Length];
            for (int j = 0; j < key_word1.Length; j++)
            {
                keytab1[j] = int.Parse(key_word1[j].ToString());
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
                    Console.WriteLine("Decrypted: " + MatrixADeCrypt(variables[0], keytab1));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key");
                    key_word2 = Console.ReadLine();
                    int[] keytab2 = new int[key_word2.Length];
                    for (int j = 0; j < key_word2.Length; j++)
                    {
                        keytab2[j] = int.Parse(key_word2[j].ToString());
                    }
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + MatrixADeCrypt(variables[0], keytab2));
                    break;

            }
            
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_A_start();
        }
        public void MatrixA_encrypt()
        {
            string[] variables = new string[2];
            int i = 0;
            string key_word;
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixA_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            key_word = variables[1];
            int[] keytab = new int[key_word.Length];
            for (int j = 0; j < key_word.Length; j++)
            {
                keytab[j] = int.Parse(key_word[j].ToString());
            }
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = MatrixACrypt(variables[0], keytab);
            Console.WriteLine("Encrypted: " + encryptedtext);
            using (StreamWriter writer = new StreamWriter("MatrixA_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_A_start();
        }
        static string MatrixACrypt(string wejscie, int[] klucz)
        {
            string wyjscie = "";
            float dlugoscKlucza = klucz.Length;
            float dlugoscWejscia = wejscie.Length;
            float liczbaWierszyPoPrzecinku = dlugoscWejscia / dlugoscKlucza; //obliczenie liczby wierszy
            int liczbaWierszyWInt;
            //dodanie ilosci wierszy w przypadku niepenlego wiersza
            if (liczbaWierszyPoPrzecinku > (int)liczbaWierszyPoPrzecinku)
            {
                liczbaWierszyWInt = (int)liczbaWierszyPoPrzecinku + 1;
            }
            else
            {
                liczbaWierszyWInt = (int)liczbaWierszyPoPrzecinku;
            }
            int[] tablicaPomocnicza = new int[(int)dlugoscKlucza];
            //zczytywanie z wejscia
            for (int i = 0; i < liczbaWierszyWInt; i++)
            {

                for (int j = 0; j < klucz.Length; j++)
                {
                    if (((klucz[j] - 1) + (i * klucz.Length)) < wejscie.Length)
                    {
                        wyjscie += wejscie[(klucz[j] - 1) + (i * klucz.Length)];
                    }
                }
            }
            return wyjscie;
        }
        public string MatrixADeCrypt(string wejscie, int[] klucz)
        {
            string wyjscie = "";
            float dlugoscKlucza = klucz.Length;
            float dlugoscWejscia = wejscie.Length;
            float liczbaWierszyPoPrzecinku = dlugoscWejscia / dlugoscKlucza; //liczba wierszy
            int liczbaWierszyWInt;
            //sprawdzenie czy nie ma niepelnyc wierszy, w przypadku gdy jest, zwiekszenie ilosci wierszy o 1
            if (liczbaWierszyPoPrzecinku > (int)liczbaWierszyPoPrzecinku)
            {
                liczbaWierszyWInt = (int)liczbaWierszyPoPrzecinku + 1;
            }
            else
            {
                liczbaWierszyWInt = (int)liczbaWierszyPoPrzecinku;
            }


            //tworzenie tabeli przed kodowaniem
            for (int i = 0; i < liczbaWierszyWInt; i++)
            {
                char[] tab = new char[klucz.Length];
                for (int j = 0; j < klucz.Length; j++)
                {
                    if ((j + (i * klucz.Length)) < wejscie.Length)
                    {
                        int roznia = (wejscie.Length - (i * klucz.Length));
                        int counter = 0;
                        if (roznia < klucz.Length)
                        {
                            for (int k = 0; k < klucz.Length; k++)
                            {
                                if (roznia >= klucz[k])
                                {
                                    tab[klucz[k] - 1] = wejscie[counter + (i * klucz.Length)];
                                    counter++;
                                }
                            }
                            break;
                        }
                        else
                        {
                            tab[klucz[j] - 1] = wejscie[j + (i * klucz.Length)];
                        }
                    }
                }
                //odczytywanie odwzorowanej tabeli
                for (int k = 0; k < tab.Length; k++)
                {
                    if (tab[k] != 0)
                    {
                        wyjscie += tab[k];
                    }
                }
            }
            return wyjscie;
        }
    }


}


