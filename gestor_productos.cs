using System;

struct Producto
{
    public int Codigo;
    public string Nombre;
    public double Precio;
    public int Stock;
}

class GestorProductos
{
    const int CAPACIDAD = 50;

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

    static void OrdenarPorCodigo(Producto[] inventario, int cantidad)
    {
        for (int i = 1; i < cantidad; i++)
        {
            Producto temp = inventario[i];
            int j = i - 1;
            while (j >= 0 && inventario[j].Codigo > temp.Codigo)
            {
                inventario[j + 1] = inventario[j];
                j--;
            }
            inventario[j + 1] = temp;
        }
    }

    static int BuscarIndicePorCodigo(Producto[] inventario, int cantidad, int codigo)
    {
        int inicio = 0;
        int fin = cantidad - 1;
        while (inicio <= fin)
        {
            int medio = (inicio + fin) / 2;
            if (inventario[medio].Codigo == codigo) return medio;
            if (inventario[medio].Codigo < codigo) inicio = medio + 1;
            else fin = medio - 1;
        }
        return -1;
    }

    static void AgregarProducto(Producto[] inventario, ref int cantidad)
    {
        if (cantidad >= CAPACIDAD)
        {
            Console.WriteLine("No hay espacio para más productos.");
            return;
        }

        int codigo;
        do
        {
            codigo = PedirNumeroEnteroPositivo("Código: ");
            if (BuscarIndicePorCodigo(inventario, cantidad, codigo) != -1)
            {
                Console.WriteLine("Ese código ya existe. Ingrese otro.");
            }
        } while (BuscarIndicePorCodigo(inventario, cantidad, codigo) != -1);

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        double precio;
        Console.Write("Precio: ");
        while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
        {
            Console.Write("Precio inválido. Intente nuevamente: ");
        }

        int stock;
        Console.Write("Stock inicial: ");
        while (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
        {
            Console.Write("Stock inválido. Intente nuevamente: ");
        }

        inventario[cantidad] = new Producto { Codigo = codigo, Nombre = nombre, Precio = precio, Stock = stock };
        cantidad++;
        OrdenarPorCodigo(inventario, cantidad);
        Console.WriteLine("Producto agregado.");
    }

    static void BuscarProducto(Producto[] inventario, int cantidad)
    {
        int codigo = PedirNumeroEnteroPositivo("Ingrese el código a buscar: ");
        int indice = BuscarIndicePorCodigo(inventario, cantidad, codigo);
        if (indice == -1)
        {
            Console.WriteLine("Producto no encontrado.");
        }
        else
        {
            Producto p = inventario[indice];
            Console.WriteLine($"Código: {p.Codigo} - Nombre: {p.Nombre} - Precio: {p.Precio} - Stock: {p.Stock}");
        }
    }

    static void ActualizarStock(Producto[] inventario, int cantidad)
    {
        int codigo = PedirNumeroEnteroPositivo("Ingrese el código del producto: ");
        int indice = BuscarIndicePorCodigo(inventario, cantidad, codigo);
        if (indice == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }
        int variacion = PedirVariacionStock();
        if (inventario[indice].Stock + variacion < 0)
        {
            Console.WriteLine("No es posible dejar stock negativo.");
        }
        else
        {
            inventario[indice].Stock += variacion;
            Console.WriteLine($"Nuevo stock: {inventario[indice].Stock}");
        }
    }

    static void ListarProductos(Producto[] inventario, int cantidad)
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay productos cargados.");
            return;
        }
        Console.WriteLine("Código\tNombre\tPrecio\tStock");
        for (int i = 0; i < cantidad; i++)
        {
            Producto p = inventario[i];
            Console.WriteLine($"{p.Codigo}\t{p.Nombre}\t{p.Precio}\t{p.Stock}");
        }
    }

    static void Main()
    {
        Producto[] inventario = new Producto[CAPACIDAD];
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
                    ListarProductos(inventario, cantidad);
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
