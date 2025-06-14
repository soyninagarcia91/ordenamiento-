using System;
using System.Collections.Generic;

struct Libro
{
    public string Titulo;
    public string Autor;
    public int AnioPublicacion;
    public int Stock;
}

class GestorLibros
{
    static void MostrarMenu()
    {
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1 - Agregar libro");
        Console.WriteLine("2 - Buscar libros por autor");
        Console.WriteLine("3 - Buscar libros por año de publicación");
        Console.WriteLine("4 - Mostrar todos los libros ordenados por año");
        Console.WriteLine("0 - Salir");
    }

    static void AgregarLibro(List<Libro> lista)
    {
        Libro nuevo = new Libro();

        Console.Write("Título: ");
        nuevo.Titulo = Console.ReadLine();

        Console.Write("Autor: ");
        nuevo.Autor = Console.ReadLine();

        Console.Write("Año de publicación: ");
        while (!int.TryParse(Console.ReadLine(), out nuevo.AnioPublicacion))
        {
            Console.Write("Ingrese un año válido: ");
        }

        Console.Write("Stock: ");
        while (!int.TryParse(Console.ReadLine(), out nuevo.Stock) || nuevo.Stock < 0)
        {
            Console.Write("Ingrese un número de stock válido: ");
        }

        lista.Add(nuevo);
        Console.WriteLine("Libro agregado correctamente.\n");
    }

    static void BuscarPorAutor(List<Libro> lista)
    {
        Console.Write("Ingrese el autor a buscar: ");
        string autor = Console.ReadLine().Trim();
        var encontrados = lista.FindAll(l => l.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase));
        if (encontrados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros de ese autor.\n");
        }
        else
        {
            Console.WriteLine("Libros encontrados:");
            foreach (var l in encontrados)
            {
                Console.WriteLine($"{l.Titulo} ({l.AnioPublicacion}) - Stock: {l.Stock}");
            }
            Console.WriteLine();
        }
    }

    static void BuscarPorAnio(List<Libro> lista)
    {
        Console.Write("Ingrese el año a buscar: ");
        int anio;
        if (!int.TryParse(Console.ReadLine(), out anio))
        {
            Console.WriteLine("Año inválido.\n");
            return;
        }
        var encontrados = lista.FindAll(l => l.AnioPublicacion == anio);
        if (encontrados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros de ese año.\n");
        }
        else
        {
            Console.WriteLine("Libros publicados en ese año:");
            foreach (var l in encontrados)
            {
                Console.WriteLine($"{l.Titulo} por {l.Autor} - Stock: {l.Stock}");
            }
            Console.WriteLine();
        }
    }

    static void MostrarOrdenadosPorAnio(List<Libro> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No hay libros cargados.\n");
            return;
        }
        var copia = new List<Libro>(lista);
        copia.Sort((a, b) => a.AnioPublicacion.CompareTo(b.AnioPublicacion));
        Console.WriteLine("Listado de libros:");
        foreach (var l in copia)
        {
            Console.WriteLine($"{l.AnioPublicacion} - {l.Titulo} por {l.Autor} (Stock: {l.Stock})");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        List<Libro> libros = new List<Libro>();
        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    AgregarLibro(libros);
                    break;
                case "2":
                    BuscarPorAutor(libros);
                    break;
                case "3":
                    BuscarPorAnio(libros);
                    break;
                case "4":
                    MostrarOrdenadosPorAnio(libros);
                    break;
                case "0":
                    Console.WriteLine("Fin del programa.");
                    break;
                default:
                    Console.WriteLine("Opción inválida.\n");
                    break;
            }
        } while (opcion != "0");
    }
}
