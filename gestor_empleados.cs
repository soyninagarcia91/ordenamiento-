using System;

struct Empleado
{
    public int Legajo;
    public string Nombre;
    public string Sector;
    public double Salario;
}

class GestorEmpleados
{
    static void MostrarMenu()
    {
        Console.WriteLine("Menú de opciones:");
        Console.WriteLine("1 - Cargar empleados");
        Console.WriteLine("2 - Mostrar lista de empleados");
        Console.WriteLine("3 - Empleado con mayor salario");
        Console.WriteLine("4 - Buscar empleados por sector");
        Console.WriteLine("0 - Salir");
        Console.Write("Seleccione una opción: ");
    }

    static Empleado[] CargarEmpleados()
    {
        Console.Write("¿Cuántos empleados cargará? ");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
        {
            Console.Write("Ingrese un número válido: ");
        }

        Empleado[] empleados = new Empleado[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"\nEmpleado {i + 1}");
            Empleado e = new Empleado();

            Console.Write("Legajo: ");
            while (!int.TryParse(Console.ReadLine(), out e.Legajo))
            {
                Console.Write("Legajo inválido. Intente nuevamente: ");
            }

            Console.Write("Nombre: ");
            e.Nombre = Console.ReadLine();

            Console.Write("Sector: ");
            e.Sector = Console.ReadLine();

            Console.Write("Salario: ");
            while (!double.TryParse(Console.ReadLine(), out e.Salario) || e.Salario < 0)
            {
                Console.Write("Salario inválido. Intente nuevamente: ");
            }

            empleados[i] = e;
        }
        return empleados;
    }

    static void MostrarEmpleados(Empleado[] empleados)
    {
        if (empleados == null || empleados.Length == 0)
        {
            Console.WriteLine("No hay empleados cargados.");
            return;
        }
        Console.WriteLine("Legajo\tNombre\tSector\tSalario");
        foreach (var e in empleados)
        {
            Console.WriteLine($"{e.Legajo}\t{e.Nombre}\t{e.Sector}\t{e.Salario}");
        }
    }

    static void MostrarMayorSalario(Empleado[] empleados)
    {
        if (empleados == null || empleados.Length == 0)
        {
            Console.WriteLine("No hay empleados cargados.");
            return;
        }
        Empleado mayor = empleados[0];
        foreach (var e in empleados)
        {
            if (e.Salario > mayor.Salario)
            {
                mayor = e;
            }
        }
        Console.WriteLine($"Empleado con mayor salario: {mayor.Nombre} (Legajo {mayor.Legajo}) - {mayor.Salario}");
    }

    static void BuscarPorSector(Empleado[] empleados)
    {
        if (empleados == null || empleados.Length == 0)
        {
            Console.WriteLine("No hay empleados cargados.");
            return;
        }
        Console.Write("Ingrese el sector a buscar: ");
        string sector = Console.ReadLine().Trim();
        var encontrados = Array.FindAll(empleados, e => e.Sector.Equals(sector, StringComparison.OrdinalIgnoreCase));
        if (encontrados.Length == 0)
        {
            Console.WriteLine("No se encontraron empleados de ese sector.");
        }
        else
        {
            Console.WriteLine("Empleados encontrados:");
            foreach (var e in encontrados)
            {
                Console.WriteLine($"{e.Legajo}\t{e.Nombre}\t{e.Sector}\t{e.Salario}");
            }
        }
    }

    static void Main()
    {
        Empleado[] empleados = null;
        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    empleados = CargarEmpleados();
                    break;
                case "2":
                    MostrarEmpleados(empleados);
                    break;
                case "3":
                    MostrarMayorSalario(empleados);
                    break;
                case "4":
                    BuscarPorSector(empleados);
                    break;
                case "0":
                    Console.WriteLine("Fin del programa.");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
            Console.WriteLine();
        } while (opcion != "0");
    }
}

