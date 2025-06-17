using System;
using System.Collections.Generic;

struct Producto
{
    public int Codigo;
    public string Nombre;
    public double Precio;
    public int Stock;
}

class GestorProductos
{
    const int MAX_PRODUCTOS = 50;

    static void MostrarMenu()
    {
        Console.WriteLine("Bienvenido al sistema de gestión de productos");
        Console.WriteLine("1. Agregar producto");
        Console.WriteLine("2. Buscar producto por código");
        Console.WriteLine("3. Actualizar stock de un producto");
        Console.WriteLine("4. Mostrar todos los productos");
        Console.WriteLine("0. Salir");
        Console.Write("opción: ");
    }

    static int PedirVariacionStock()
    {
        int numero;
        do
        {
            Console.Write("Ingrese la variación de stock (número entero positivo o negativo): ");
        } while (!int.TryParse(Console.ReadLine(), out numero));
        return numero;
    }

    static int PedirNumeroEnteroPositivo(string mensaje)
    {
        int numero;
        bool esValido;
        do
        {
            Console.Write(mensaje);
            esValido = int.TryParse(Console.ReadLine(), out numero);
            if (!esValido)
            {
                Console.WriteLine("La entrada no es válida, debe ingresar un número entero positivo.");
            }
            else if (numero <= 0)
            {
                Console.WriteLine("La entrada no es válida, el número debe ser mayor que cero.");
            }
        } while (!esValido || numero <= 0);
        return numero;
    }

    static void AgregarProducto(Producto[] inventario, ref int cantidad)
    {
        if (cantidad >= MAX_PRODUCTOS)
        {
            Console.WriteLine("Inventario lleno. No se pueden agregar más productos.");
            return;
        }

        int codigo;
        bool existe;
        do
        {
            codigo = PedirNumeroEnteroPositivo("Código: ");
            existe = false;
            for (int i = 0; i < cantidad; i++)
            {
                if (inventario[i].Codigo == codigo)
                {
                    existe = true;
                    Console.WriteLine("Ya existe un producto con ese código.");
                    break;
                }
            }
        } while (existe);

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        double precio;
        Console.Write("Precio: ");
        while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
        {
            Console.Write("Ingrese un precio válido: ");
        }

        int stock = PedirNumeroEnteroPositivo("Stock inicial: ");

        inventario[cantidad] = new Producto { Codigo = codigo, Nombre = nombre, Precio = precio, Stock = stock };
        cantidad++;
        Array.Sort(inventario, 0, cantidad, Comparer<Producto>.Create((a, b) => a.Codigo.CompareTo(b.Codigo)));
        Console.WriteLine("Producto agregado.");
    }

    static int BuscarIndicePorCodigo(Producto[] inventario, int cantidad, int codigo)
    {
        int izquierda = 0;
        int derecha = cantidad - 1;
        while (izquierda <= derecha)
        {
            int medio = (izquierda + derecha) / 2;
            if (inventario[medio].Codigo == codigo) return medio;
            if (codigo < inventario[medio].Codigo) derecha = medio - 1;
            else izquierda = medio + 1;
        }
        return -1;
    }

    static void BuscarProducto(Producto[] inventario, int cantidad)
    {
        int codigo = PedirNumeroEnteroPositivo("Ingrese el código del producto: ");
        int indice = BuscarIndicePorCodigo(inventario, cantidad, codigo);
        if (indice >= 0)
        {
            var p = inventario[indice];
            Console.WriteLine($"Código: {p.Codigo}, Nombre: {p.Nombre}, Precio: {p.Precio}, Stock: {p.Stock}");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }

    static void ActualizarStock(Producto[] inventario, int cantidad)
    {
        int codigo = PedirNumeroEnteroPositivo("Código del producto: ");
        int indice = BuscarIndicePorCodigo(inventario, cantidad, codigo);
        if (indice == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        int variacion = PedirVariacionStock();
        if (inventario[indice].Stock + variacion < 0)
        {
            Console.WriteLine("El stock no puede quedar negativo.");
            return;
        }
        inventario[indice].Stock += variacion;
        Console.WriteLine($"Stock actualizado. Nuevo stock: {inventario[indice].Stock}");
    }

    static void MostrarTodos(Producto[] inventario, int cantidad)
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay productos cargados.");
            return;
        }
        Console.WriteLine("Listado de productos:");
        for (int i = 0; i < cantidad; i++)
        {
            var p = inventario[i];
            Console.WriteLine($"{p.Codigo} - {p.Nombre} - Precio: {p.Precio} - Stock: {p.Stock}");
        }
    }

    static void Main()
    {
        Producto[] inventario = new Producto[MAX_PRODUCTOS];
        int cantidad = 0;
        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    AgregarProducto(inventario, ref cantidad);
                    break;
                case "2":
                    BuscarProducto(inventario, cantidad);
                    break;
                case "3":
                    ActualizarStock(inventario, cantidad);
                    break;
                case "4":
                    MostrarTodos(inventario, cantidad);
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
