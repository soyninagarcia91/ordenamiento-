using System;

struct Alumno
{
    public int Legajo;
    public string Apellido;
    public string Nombre;
    public double Nota;
}

class ProgramaCargaOrdenada
{
    static void InsertarOrdenado(Alumno[] arr, int cantidad, Alumno nuevo)
    {
        int i = cantidad - 1;
        while (i >= 0 && string.Compare(arr[i].Apellido, nuevo.Apellido, StringComparison.OrdinalIgnoreCase) > 0)
        {
            arr[i + 1] = arr[i];
            i--;
        }
        arr[i + 1] = nuevo;
    }

    static void Main(string[] args)
    {
        Console.Write("¿Cuántos alumnos va a cargar? ");
        int total;
        while (!int.TryParse(Console.ReadLine(), out total) || total <= 0)
        {
            Console.Write("Ingrese un número válido: ");
        }

        Alumno[] alumnos = new Alumno[total];
        int cargados = 0;

        while (cargados < total)
        {
            Console.WriteLine($"\nAlumno {cargados + 1}");
            Alumno temp = new Alumno();

            Console.Write("Legajo: ");
            while (!int.TryParse(Console.ReadLine(), out temp.Legajo))
            {
                Console.Write("Legajo inválido. Intente nuevamente: ");
            }

            Console.Write("Apellido: ");
            temp.Apellido = Console.ReadLine();

            Console.Write("Nombre: ");
            temp.Nombre = Console.ReadLine();

            Console.Write("Nota: ");
            while (!double.TryParse(Console.ReadLine(), out temp.Nota) || temp.Nota < 0 || temp.Nota > 10)
            {
                Console.Write("Nota inválida. Intente nuevamente: ");
            }

            InsertarOrdenado(alumnos, cargados, temp);
            cargados++;
        }

        Console.WriteLine("\nListado de alumnos ordenado por apellido:");
        Console.WriteLine("Legajo\tApellido\tNombre\tNota");
        for (int i = 0; i < total; i++)
        {
            Console.WriteLine($"{alumnos[i].Legajo}\t{alumnos[i].Apellido}\t{alumnos[i].Nombre}\t{alumnos[i].Nota}");
        }
    }
}
