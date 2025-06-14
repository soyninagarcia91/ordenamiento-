using System;

struct ConfiguracionCasa
{
    public double Temperatura;
    public int IluminacionInterior;
    public bool LucesExterioresEncendidas;
}

class ProgramaCasaInteligente
{
    static void MostrarMenu()
    {
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1 - Ajustar temperatura");
        Console.WriteLine("2 - Regular iluminación interior");
        Console.WriteLine("3 - Encender/Apagar iluminación exterior");
        Console.WriteLine("4 - Ver configuración actual");
        Console.WriteLine("0 - Salir");
    }

    static double PedirTemperatura()
    {
        double temperatura;
        Console.Write("Ingrese la temperatura deseada (20.0 - 25.5): ");
        while (!double.TryParse(Console.ReadLine(), out temperatura) || temperatura < 20.0 || temperatura > 25.5)
        {
            Console.Write("Valor inválido. Ingrese una temperatura entre 20.0 y 25.5: ");
        }
        return temperatura;
    }

    static int PedirIluminacionInterior()
    {
        int valor;
        Console.Write("Ingrese nivel de iluminación interior (0 - 100): ");
        while (!int.TryParse(Console.ReadLine(), out valor) || valor < 0 || valor > 100)
        {
            Console.Write("Valor inválido. Ingrese un número entero entre 0 y 100: ");
        }
        return valor;
    }

    static void MostrarConfiguracion(ConfiguracionCasa config)
    {
        Console.WriteLine("\nConfiguración actual:");
        Console.WriteLine($"Temperatura: {config.Temperatura}°C");
        Console.WriteLine($"Iluminación interior: {config.IluminacionInterior}");
        Console.WriteLine($"Luces exteriores encendidas: {(config.LucesExterioresEncendidas ? "Sí" : "No")}");
    }

    static void Main(string[] args)
    {
        ConfiguracionCasa casa = new ConfiguracionCasa
        {
            Temperatura = 22.0,
            IluminacionInterior = 50,
            LucesExterioresEncendidas = false
        };

        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    casa.Temperatura = PedirTemperatura();
                    break;
                case "2":
                    casa.IluminacionInterior = PedirIluminacionInterior();
                    break;
                case "3":
                    casa.LucesExterioresEncendidas = !casa.LucesExterioresEncendidas;
                    Console.WriteLine($"Luces exteriores {(casa.LucesExterioresEncendidas ? "encendidas" : "apagadas")}");
                    break;
                case "4":
                    MostrarConfiguracion(casa);
                    break;
                case "0":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
            Console.WriteLine();
        } while (opcion != "0");
    }
}
