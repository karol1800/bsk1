using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bsk_nr_1
{
    class Matrix_B
    {
        public void Matrix_B_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Matrix B:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        MatrixB_variables();
                        break;
                    case ConsoleKey.D2:
                        MatrixB_encrypt();
                        break;
                    case ConsoleKey.D3:
                        MatrixB_decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void MatrixB_decrypt()
        {

            string[] variables = new string[2];
            int i = 0;
            string key;
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixB_Enc_Variables.txt"))
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
                    Console.WriteLine("Decrypted: " + MatrixBDecrypt(variables[0], (variables[1])));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key");
                    key = Console.ReadLine();
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + MatrixBDecrypt(variables[0], key));
                    break;

            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_B_start();
        }
        public void MatrixB_encrypt()
        {
            string[] variables = new string[2];
            int i = 0;
            
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixB_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            
           
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = MatrixBCrypt(variables[0], variables[1]);
            Console.WriteLine("Encrypted: " + encryptedtext);
            using (StreamWriter writer = new StreamWriter("MatrixB_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
            }


            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_B_start();
        }
        public void MatrixB_variables()
        {
            Console.Clear();
            //VigenereCipher Vcipher = new VigenereCipher();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key");
            string key = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("MatrixB_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key);
            }

            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_B_start();
        }
        
        static string MatrixBCrypt(string message, string key)
        {
            string exit = "";
            int ord_count = 0;
            int cur_col = 1;
            int x = 0;
            int min = 65;
            int count = 1;
            int[] word_into_key = new int[key.Length];
            int rows = (message.Length / key.Length) + 1;
            int columns = key.Length;
            int msgcount = 0;
            char[,] encrypted = new char[rows, columns];
            message = message.ToUpper();
            message = message.Replace(" ", "");
            key = key.Replace(" ", "");
            key = key.ToUpper();
            //zamiana slowa w klucz
            while (min < 91)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] == min)
                    {
                        word_into_key[i] = count;
                        count++;
                    }
                }
                min++;
            }
            //wypelnienie macierzy
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (msgcount < message.Length)
                        encrypted[i, j] = message[msgcount];
                    msgcount++;
                }
            }
            //kodowanie
            while (x < key.Length)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (cur_col == word_into_key[j])
                    {
                        ord_count = j;//wybieranie kolumn wedlug klucza
                        continue;
                    }
                }
                //zwrocenie zakodowanej wartosci
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j == ord_count && (int)encrypted[i, j] >= 65)
                        {
                            exit = exit + encrypted[i, j];
                        }
                    }
                }
                cur_col++;
                x++;
            }


            return exit;
        }

        static string MatrixBDecrypt(string message, string key)
        {
            string exit = string.Empty;
            int[] word_into_key = new int[key.Length];
            int rows = (message.Length / key.Length) + 1;
            int columns = key.Length;
            char[,] decrypted = new char[rows, columns];
            int kiw_count = 0;
            int current_col = 1;
            int x = 0;
            int temp_col = columns;
            int row_count = 0;
            int lettercount = 0;
            int min = 65;
            int counter = 1;
            message = message.ToUpper();
            message = message.Replace(" ", "");
            key = key.Replace(" ", "");
            key = key.ToUpper();
            //zamiana slowa w klucz
            while (min < 91)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] == min)
                    {
                        word_into_key[i] = counter;
                        counter++;
                    }
                }
                min++;
            }
            while (x < key.Length)
            {
                row_count = 0;
                temp_col = columns;
                for (int j = 0; j < key.Length; j++)
                {
                    if (current_col == word_into_key[j])
                    {
                        kiw_count = j;//znalezienie kolumny rozpoczynajacej 
                        temp_col = j;
                        continue;
                    }
                }
                
                while (temp_col < message.Length)
                {
                    if (lettercount < message.Length)
                    decrypted[row_count, kiw_count] = message[lettercount];
                    lettercount++;
                    row_count++;
                    temp_col = temp_col+columns;
                }
                current_col++;
                x++;
            }
            //odkodowanie
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((int)decrypted[i, j] >= 65)
                    {
                        exit = exit + decrypted[i, j];
                    }
                }
            
            }

            return exit;
        }



    }
}


