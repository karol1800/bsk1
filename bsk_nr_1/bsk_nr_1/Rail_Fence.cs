using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bsk_nr_1
{
    class Rail_Fence
    {
        public void RailFence_start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Rail Fence:");
                Console.WriteLine("1.Implement variables");
                Console.WriteLine("2.Encrypt");
                Console.WriteLine("3.Decrypt");
                Console.WriteLine("4.Get Back to Main menu");
                ConsoleKeyInfo button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        RailFence_variables();
                        break;
                    case ConsoleKey.D2:
                        Rail_Fence_encrypt();
                        break;
                    case ConsoleKey.D3:
                        Rail_Fence_decrypt();
                        break;
                    case ConsoleKey.D4:
                        Menu.Main_menu();
                        break;
                }
            }
        }
        public void Rail_Fence_decrypt()
        {

            string[] variables = new string[2];
            int i = 0,key1,key2;
            Console.Clear();
            using (StreamReader sr = new StreamReader("RailFence_Enc_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            key1 = int.Parse(variables[1]);
            
            Console.WriteLine("New or Old key");
            Console.WriteLine("1.Stantard");
            Console.WriteLine("2.New");
            ConsoleKeyInfo button = Console.ReadKey();
            switch (button.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + railFenceDecrypter(variables[0], key1));
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Wprowadz nowy klucz");
                    Console.WriteLine("Implement Key");
                    key2= int.Parse(Console.ReadLine());
                    Console.WriteLine("Encrypted: " + variables[0]);
                    Console.WriteLine("Decrypted: " + railFenceDecrypter(variables[0], key2));
                    break;

            }
            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            RailFence_start();
        }
        public void Rail_Fence_encrypt()
        {
            string[] variables = new string[2];
            int i = 0,key;
            Console.Clear();
            using (StreamReader sr = new StreamReader("RailFence_Dec_Variables.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    variables[i] = line;
                    i++;
                }
            }
            key = int.Parse(variables[1]);
            Console.WriteLine("Decrypted: " + variables[0]);
            string encryptedtext = railFenceCryper(variables[0], key);
            Console.WriteLine("Encrypted: " + encryptedtext);
            using (StreamWriter writer = new StreamWriter("RailFence_Enc_Variables.txt"))
            {
                writer.WriteLine(encryptedtext);
                writer.WriteLine(variables[1]);
            }

            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            RailFence_start();
        }
        public void RailFence_variables()
        {
            Console.Clear();
            Console.WriteLine("Implement Password");
            string word = Console.ReadLine();
            Console.WriteLine("Implement Key");
            string key = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("RailFence_Dec_Variables.txt"))
            {
                writer.WriteLine(word);
                writer.WriteLine(key);
            }

            Console.WriteLine("Press Any Button to Back");
            Console.ReadKey();
            RailFence_start();
        }
        public string railFenceCryper(String wejscie, int n)
        {
            String wyjscie = "";
            //pobranie wierzcholkow
            for (int i = 0; i < wejscie.Length; i = i + 2 * (n - 1))
            {
                wyjscie += wejscie[i];
            }
            //pobranie srodkowych czesci wejscia
            for (int i = 1; i <= (n - 2); i++)
            {
                for (int k = 0; k < wejscie.Length; k = k + 2 * (n - 1))
                {
                    if ((k + i) < wejscie.Length)
                    {
                        wyjscie += wejscie[k + i];
                    }
                    if ((k + 2 * (n - 1) - i) < wejscie.Length)
                    {
                        wyjscie += wejscie[k + 2 * (n - 1) - i];
                    }
                }
            }
            //pobranie ostatnich elementow
            for (int i = 0; i < wejscie.Length; i = i + 2 * (n - 1))
            {
                if ((i + n - 1) < wejscie.Length)
                {
                    wyjscie += wejscie[i + n - 1];
                }
            }
            return wyjscie;
        }
        public String railFenceDecrypter(String wejscie, int n) {
        String wyjscie = "";
        int calkowitaDlugosc = wejscie.Length;
        
        int[] dlugoscWiersza = new int[n];//dlugosc wiersza
        int[] licznikWiersza = new int[n]; //ilsoc elementow w wierszu
        dlugoscWiersza[0] = 0;
        dlugoscWiersza[n - 1] = 0;
 
        //dlugosc wiersza 0
        for (int i = 0; i < wejscie.Length; i = i + 2 * (n - 1)) {
            dlugoscWiersza[0]++;
        } 
        //dlugosc ostatniego wiersza
        
            for (int i = 0; i < wejscie.Length; i = i + 2 * (n - 1)) {
                if ((i + n - 1) < wejscie.Length) {
                    dlugoscWiersza[n - 1]++;
                }
            }
        //dlugosc srodkowych wierszy
        if(n>2)
        {
        for (int i = 1; i <= (n - 2); i++) {
            dlugoscWiersza[i] = 0;
            for (int k = 0; k < wejscie.Length; k = k + 2 * (n - 1)) {
                if ((k + i) < wejscie.Length) {
                    dlugoscWiersza[i]++;
                }
                if ((k + 2 * (n - 1) - i) < calkowitaDlugosc) {
 
                    dlugoscWiersza[i]++;
                }
 
            }
        }
        }
        int licznik = 0;
        Boolean operacja = true;
        //obliczanie offset dla wierszy 
        for (int i = 0; i < n; i++) {
            if (i == 0) {
                licznikWiersza[0] = 0;
            } else {
                licznikWiersza[i] = licznikWiersza[i - 1] + dlugoscWiersza[i - 1];
            }
        }
        //odczytywanie liter schodkowo 
        for (int i = 0; i < calkowitaDlugosc; i++) {
 
            wyjscie += wejscie[licznikWiersza[licznik]];
            
            licznikWiersza[licznik]++;
            if (operacja == true) {
 
                if (licznik >= (n - 1)) {
                    licznik--;
                    operacja = false;
                } else {
                    licznik++;
                }
            } else {
                licznik--;
                if (licznik == 0) {
                    operacja = true;
                  
                }
                if(licznik < 0)
                {
                    operacja = true;
                    licznik = 1;
                }
                   
            }
        }
 
        return wyjscie;
    }
    }


}

