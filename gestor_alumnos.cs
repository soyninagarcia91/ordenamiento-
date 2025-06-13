using System;
using System.Collections.Generic;
using System.Linq;

struct Alumno
{
    public int Legajo;
    public string Apellido;
    public string Nombre;
    public bool EstaActivo;
}

class GestorAlumnos
{
    static void MostrarMenu()
    {
        Console.WriteLine("Menú de opciones:");
        Console.WriteLine("1 - Cargar alumnos");
        Console.WriteLine("2 - Mostrar listado de alumnos");
        Console.WriteLine("3 - Ordenar por número de legajo");
        Console.WriteLine("4 - Ordenar por apellido");
        Console.WriteLine("5 - Mostrar cantidad de alumnos activos");
        Console.WriteLine("6 - Editar alumno por legajo");
        Console.WriteLine("0 - Salir");
        Console.Write("Seleccione una opción: ");
    }

    static void CargarAlumnos(List<Alumno> lista)
    {
        Console.Write("¿Cuántos alumnos desea cargar? ");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
        {
            Console.Write("Ingrese un número válido: ");
        }

        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"\nAlumno {i + 1}");
            Alumno nuevo = new Alumno();

            Console.Write("Legajo: ");
            while (!int.TryParse(Console.ReadLine(), out nuevo.Legajo))
            {
                Console.Write("Legajo inválido. Intente nuevamente: ");
            }

            Console.Write("Apellido: ");
            nuevo.Apellido = Console.ReadLine();

            Console.Write("Nombre: ");
            nuevo.Nombre = Console.ReadLine();

            Console.Write("¿Está activo? (s/n): ");
            string activo = Console.ReadLine().Trim().ToLower();
            nuevo.EstaActivo = activo == "s" || activo == "si";

            lista.Add(nuevo);
        }
    }

    static void MostrarAlumnos(List<Alumno> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No hay alumnos cargados.");
            return;
        }
        Console.WriteLine("Legajo\tApellido\tNombre\tEstado");
        foreach (var a in lista)
        {
            string estado = a.EstaActivo ? "Activo" : "Inactivo";
            Console.WriteLine($"{a.Legajo}\t{a.Apellido}\t{a.Nombre}\t{estado}");
        }
    }

    static void EditarAlumno(List<Alumno> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No hay alumnos cargados.");
            return;
        }
        Console.Write("Ingrese el número de legajo del alumno a editar: ");
        int legajo;
        if (!int.TryParse(Console.ReadLine(), out legajo))
        {
            Console.WriteLine("Legajo inválido.");
            return;
        }

        int indice = lista.FindIndex(a => a.Legajo == legajo);
        if (indice == -1)
        {
            Console.WriteLine("Alumno no encontrado.");
            return;
        }

        Alumno editado = lista[indice];
        Console.WriteLine($"Editando datos de {editado.Apellido}, {editado.Nombre}");

        Console.Write($"Nuevo apellido ({editado.Apellido}): ");
        string nuevoApellido = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nuevoApellido))
        {
            editado.Apellido = nuevoApellido;
        }

        Console.Write($"Nuevo nombre ({editado.Nombre}): ");
        string nuevoNombre = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nuevoNombre))
        {
            editado.Nombre = nuevoNombre;
        }

        Console.Write($"\u00bfEstá activo? (s/n) [{(editado.EstaActivo ? "s" : "n")}] : ");
        string activo = Console.ReadLine().Trim().ToLower();
        if (activo == "s" || activo == "si") editado.EstaActivo = true;
        else if (activo == "n" || activo == "no") editado.EstaActivo = false;

        lista[indice] = editado;
        Console.WriteLine("Alumno actualizado.");
    }

    static void Main()
    {
        List<Alumno> alumnos = new List<Alumno>();
        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    CargarAlumnos(alumnos);
                    break;
                case "2":
                    MostrarAlumnos(alumnos);
                    break;
                case "3":
                    alumnos.Sort((a, b) => a.Legajo.CompareTo(b.Legajo));
                    Console.WriteLine("Listado ordenado por legajo.");
                    break;
                case "4":
                    alumnos.Sort((a, b) => string.Compare(a.Apellido, b.Apellido, StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine("Listado ordenado por apellido.");
                    break;
                case "5":
                    int activos = alumnos.Count(a => a.EstaActivo);
                    Console.WriteLine($"Cantidad de alumnos activos: {activos}");
                    break;
                case "6":
                    EditarAlumno(alumnos);
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

