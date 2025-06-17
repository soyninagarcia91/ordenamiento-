using System;

namespace JuegoAdivinanza
{
    class Program
    {
        static void cargarValoresAleatorios(int[] aNumeros)
        {
            Random rnd = new Random();
            for (int i = 0; i < aNumeros.Length; i++)
            {
                aNumeros[i] = rnd.Next(1, 1001); // 1..1000 inclusive
            }
        }

        static void ordenarArregloAleatorios(int[] aNumeros)
        {
            Array.Sort(aNumeros); // Orden creciente
        }

        static int buscarNumero(int[] aNumeros, int valorObjetivo)
        {
            int inicio = 0;
            int fin = aNumeros.Length - 1;
            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                if (aNumeros[medio] == valorObjetivo)
                {
                    return medio;
                }
                else if (aNumeros[medio] < valorObjetivo)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }
            return -1;
        }

        static void mostrarMenu()
        {
            Console.WriteLine("Seleccione una opcion:");
            Console.WriteLine(" a - Comenzar nueva partida");
            Console.WriteLine(" b - Ingresar numero y buscar");
            Console.WriteLine(" c - Salir");
        }

        static void Main(string[] args)
        {
            int[] aNumeros = new int[20];
            cargarValoresAleatorios(aNumeros);
            ordenarArregloAleatorios(aNumeros);

            string opcion;
            do
            {
                mostrarMenu();
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "a":
                        cargarValoresAleatorios(aNumeros);
                        ordenarArregloAleatorios(aNumeros);
                        Console.WriteLine("Nueva partida iniciada!");
                        break;
                    case "b":
                        Console.Write("Ingrese un numero para buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int valor))
                        {
                            int indice = buscarNumero(aNumeros, valor);
                            if (indice >= 0)
                                Console.WriteLine($"El numero {valor} fue encontrado en la posicion {indice}.");
                            else
                                Console.WriteLine($"El numero {valor} no se encuentra en el arreglo.");
                        }
                        else
                        {
                            Console.WriteLine("Numero invalido.");
                        }
                        break;
                    case "c":
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            } while (opcion != "c");
        }
    }
}
