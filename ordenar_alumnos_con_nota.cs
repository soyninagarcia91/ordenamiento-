using System;

struct Alumno
{
    public int Legajo;
    public string Apellido;
    public string Nombre;
    public double Nota;
}

class ProgramaOrdenarAlumnos
{
    static void OrdenarPorApellido(Alumno[] alumnos)
    {
        for (int i = 0; i < alumnos.Length - 1; i++)
        {
            for (int j = 0; j < alumnos.Length - i - 1; j++)
            {
                if (string.Compare(alumnos[j].Apellido, alumnos[j + 1].Apellido, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    Alumno aux = alumnos[j];
                    alumnos[j] = alumnos[j + 1];
                    alumnos[j + 1] = aux;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        Console.Write("¿Cuántos alumnos va a cargar? ");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
        {
            Console.Write("Ingrese un número válido: ");
        }

        Alumno[] alumnos = new Alumno[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"\nAlumno {i + 1}");

            Console.Write("Legajo: ");
            while (!int.TryParse(Console.ReadLine(), out alumnos[i].Legajo))
            {
                Console.Write("Legajo inválido. Intente nuevamente: ");
            }

            Console.Write("Apellido: ");
            alumnos[i].Apellido = Console.ReadLine();

            Console.Write("Nombre: ");
            alumnos[i].Nombre = Console.ReadLine();

            Console.Write("Nota: ");
            while (!double.TryParse(Console.ReadLine(), out alumnos[i].Nota) || alumnos[i].Nota < 0 || alumnos[i].Nota > 10)
            {
                Console.Write("Nota inválida. Intente nuevamente: ");
            }
        }

        OrdenarPorApellido(alumnos);

        Console.WriteLine("\nListado ordenado por apellido:");
        Console.WriteLine("Legajo\tApellido\tNombre\tNota");
        foreach (var a in alumnos)
        {
            Console.WriteLine($"{a.Legajo}\t{a.Apellido}\t{a.Nombre}\t{a.Nota}");
        }
    }
}
