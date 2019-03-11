using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bsk_nr_1
{
    class Matrix_C
    {
        public void Matrix_C_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Matrix C:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        MatrixC_variables();
                        break;
                    case ConsoleKey.D2:
                        MatrixC_encrypt();
                        break;
                    case ConsoleKey.D3:
                        MatrixC_decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void MatrixC_variables()
        {
            Console.Clear();
            //VigenereCipher Vcipher = new VigenereCipher();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key");
            string key = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("MatrixC_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key);
            }

            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_C_start();
        }
        public void MatrixC_decrypt()
        {

            string[] variables = new string[2];
            int i = 0;
            string key;
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixC_Enc_Variables.txt"))
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
                    Console.WriteLine("Decrypted: " + MatrixCDeCrypt(variables[0], key_from_word(variables[1])));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key");
                    key = Console.ReadLine();
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + MatrixCDeCrypt(variables[0], key_from_word(key)));
                    break;

            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_C_start();
        }
        public void MatrixC_encrypt()
        {
            string[] variables = new string[2];
            int i = 0;
            string key_word;
            Console.Clear();
            using (StreamReader sr = new StreamReader("MatrixC_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = MatrixCCrypt(variables[0], key_from_word(variables[1]));
            Console.WriteLine("Encrypted: " + encryptedtext);
            using (StreamWriter writer = new StreamWriter("MatrixC_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            Matrix_C_start();
        }
        static string MatrixCCrypt(string wejscie2, int[] klucz)
        {
            string wejscie = wejscie2;
            string wyjscie = "";
            int wejscie_lenth = wejscie.Length;
            char[,] tablica = new char[klucz.Length, wejscie.Length];
            int suma = 0;
            int licznikWejscia = 0;
            wejscie = wejscie.Replace(" ", string.Empty);
            //stworzenie szyfru
            for (int i = 0; i < wejscie.Length; i++)
            {
                int licznik = 0;
                //obliczamy liczbe slow w wierszu
                for (int j = 0; j < klucz.Length; j++)
                {

                    if ((wejscie.Length) <= (suma + licznik))
                    {
                        break;
                    }
                    if (i % klucz.Length == klucz[j] - 1)
                    {
                        licznik++;
                        break;
                    }
                    else
                    {
                        licznik++;
                    }

                }
                //wypelniamy pomocnicza tabele wartosciami z wejscia na podstawie klucza
                for (int k = 0; k < licznik; k++)
                {
                    int m = 0;
                    //szukamy wolnego miejsca w tablicy
                    ///  int abc = tablica.GetLength(klucz[k] - 1);
                    //    Console.WriteLine("wartosc klucz[k] " + (klucz[k] - 1));
                    //    Console.WriteLine("tablica " + tablica[0].GetLength(3));
                    for (m = 0; m < tablica.GetLength(1); m++)
                    {
                        if (tablica[klucz[k] - 1, m] == 0)
                        {
                            break;
                        }
                    }
                    tablica[klucz[k] - 1, m] = wejscie[licznikWejscia];
                    licznikWejscia++;
                }

                suma += licznik;
                if (suma >= wejscie.Length)
                {
                    break;
                }

            }
            //po stworzeniu tabeli odczytujemy wartosci
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                for (int j = 0; j < tablica.GetLength(1); j++)
                {
                    if (tablica[i, j] != 0)
                    {
                        wyjscie += tablica[i, j];
                    }
                }
            }
            return wyjscie;
        }
        static string  MatrixCDeCrypt(string wejscie, int[] klucz)
        {

            string wyjscie = "";
            int najwiekszyElement = 0;
            int iloscWierszy = 0;
            int maksymalnaIloscBlokow = wejscie.Length;
            int[] tablicaDlugosciBlokow = new int[maksymalnaIloscBlokow * klucz.Length];
            int[] iloscWierszyElementuKlucza = new int[maksymalnaIloscBlokow * klucz.Length];
            int[] odleglosciElementowKlucza = new int[klucz.Length];
            int pomCalkowitaDlugosc = wejscie.Length;
            int maksymalnaDlugosc = 0;

            int zlicz = 0;

            ///obliczamy dlugosci blokow i ilosc wierszy
            while (pomCalkowitaDlugosc > 0)
            {
                int ostatniaIloscWierszy = iloscWierszy;
                for (int j = 0; j < pomCalkowitaDlugosc; j++)
                {
                    if (j >= klucz.Length)
                    {
                        break;
                    }
                    if (klucz[j] - 1 == zlicz % klucz.Length)
                    {
                        iloscWierszy++;
                        pomCalkowitaDlugosc = pomCalkowitaDlugosc - (j + 1);
                        tablicaDlugosciBlokow[zlicz] = (j + 1);
                        break;
                    }
                }
                if (ostatniaIloscWierszy == iloscWierszy)
                {
                    tablicaDlugosciBlokow[zlicz] = pomCalkowitaDlugosc;
                    iloscWierszy++;
                    break;
                }
                zlicz++;
            }


            ////obliczamy teoretyczne dlugsci wieszy na podstawie klucza
            for (int i = 0; i < klucz.Length; i++)
            {
                int licznikOdleglosci = 0;
                for (int j = 0; j < klucz.Length; j++)
                {
                    if ((klucz[i] - 1) == klucz[j] - 1)
                    {
                        licznikOdleglosci++;
                        break;
                    }
                    else
                        licznikOdleglosci++;
                }
                odleglosciElementowKlucza[klucz[i] - 1] = licznikOdleglosci;
            }
            ///obliczamy prawdziwe dlugosci wierszy kluczy
            for (int i = 0; i < klucz.Length; i++)
            {
                int licznik = 0;
                for (int j = 0; j < iloscWierszy; j++)
                {
                    if (odleglosciElementowKlucza[i] <= tablicaDlugosciBlokow[j])
                    {
                        licznik++;
                    }
                }
                if (i >= iloscWierszyElementuKlucza.Length)
                {
                    break;
                }
                iloscWierszyElementuKlucza[i] = licznik;
            }
            //tworzymy tabele
            char[,] odwzoronaTablica = new char[klucz.Length, wejscie.Length];
            int suma = 0;
            //gdy mamy ilosc wierszy, odwzorowana tabele oraz ilosc wierszy dla poszczegolnych kluczy po prostu je zczytujemy
            //by stworzyc tabele 
            for (int i = 0; i < iloscWierszyElementuKlucza.Length; i++)
            {
                for (int j = 0; j < iloscWierszyElementuKlucza[i]; j++)
                {
                    odwzoronaTablica[i, j] = wejscie[suma];
                    suma++;
                }
            }
            int[] liczenieTabelek = new int[klucz.Length];
            //odczytujemy tabele za pomoca klucza
            for (int i = 0; i < iloscWierszy; i++)
            {
                for (int j = 0; j < tablicaDlugosciBlokow[i]; j++)
                {
                    wyjscie += odwzoronaTablica[klucz[j] - 1, liczenieTabelek[klucz[j] - 1]];
                    liczenieTabelek[klucz[j] - 1]++;
                }
            }

            return wyjscie;
        }
        static int[] key_from_word(string slowo)
        {
            int[] klucz = new int[slowo.Length];
            char obecneSlowo;
            int counter = 1;
            char[] tabelaZLieterami = new char[slowo.Length];
            for (int i = 0; i < slowo.Length; i++)
            {
                obecneSlowo = slowo[i];
                counter = 1;
                for (int j = 0; j < slowo.Length; j++)
                {
                    if (obecneSlowo > slowo[j])
                    {
                        counter++;
                    }
                }
                tabelaZLieterami[i] = obecneSlowo;
                for (int j = 0; j < tabelaZLieterami.Length; j++)
                {
                    if (obecneSlowo == tabelaZLieterami[j] && i != j)
                    {
                        counter++;
                    }
                }
                klucz[i] = counter;
            }
            return klucz;
        }
    }


}

