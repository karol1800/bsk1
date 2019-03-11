using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsk_nr_1
{
    public class Menu
    {
        public static void Main_menu()
        {
            

            while (true)
            {
                Caesar caesar = new Caesar();
                Matrix_A matrix_a = new Matrix_A();
                Matrix_B matrix_b = new Matrix_B();
                Matrix_C matrix_c = new Matrix_C();
                Rail_Fence rail_fence = new Rail_Fence();
                Vigener vigener = new Vigener();
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1.Rail Fence");
                Console.WriteLine("2.Matrix A");
                Console.WriteLine("3.Matrix B");
                Console.WriteLine("4.Matrix C");
                Console.WriteLine("5.Caesar");
                Console.WriteLine("6.Vigener");
                Console.WriteLine("7.Exit");
                ConsoleKeyInfo button=Console.ReadKey();
                
                switch (button.Key)
                {
                    case ConsoleKey.D1:
                        rail_fence.RailFence_start();
                        break;
                    case ConsoleKey.D2:
                        matrix_a.Matrix_A_start();
                        break;
                    case ConsoleKey.D3:
                        matrix_b.Matrix_B_start();
                        break;
                    case ConsoleKey.D4:
                        matrix_c.Matrix_C_start();
                        break;
                    case ConsoleKey.D5:
                        caesar.Caesar_start();
                        break;
                    case ConsoleKey.D6:
                        vigener.Vigener_start();                        
                        break;
                    case ConsoleKey.D7:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
