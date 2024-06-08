using System;

using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;


public class Producto
{

    private string nombre;
    private double costo;
    private int stock;

    public string getNombre()
    {

        return this.nombre;

    }

    public void setNombre(string nombre)
    {

        this.nombre = nombre;

    }

    public double getCosto()
    {

        return this.costo;

    }

    public void setCosto(double costo)
    {

        this.costo = costo * 0.3;

    }

    public double getPrecio_venta()
    {

        return this.costo + this.costo * 3.0 / 10.0;

    }

    public void setStock(int stock) {

        this.stock = stock;

    }

    public void addStock(int stock) {

        this.stock += stock;

    }

    public int getStock() {

        return this.stock;

    }

    public void showProductoTienda()
    {

        Console.WriteLine(
            "Nombre: " + this.getNombre() +
            ", Precio: " + this.getPrecio_venta() +
            ", Stock: " + this.getStock()
        );

    }

    public void showProductoCarrito() {

        Console.WriteLine(
            "Nombre: " + this.getNombre() +
            ", Precio: " + this.getPrecio_venta() +
            ", Cantidad: " + this.getStock()
        );

    }

    public Producto(string nombre, double costo, int stock = 0)
    {

        this.nombre = nombre;
        this.costo = costo;
        this.stock = stock;

    }

}

public class Carrito
{

    private Tienda tienda;

    private List<Producto> productos_carrito = new List<Producto>();

    public void setTienda (Tienda tienda) {

        this.tienda = tienda;

    }

    public Tienda getTienda () {

        return this.tienda;

    }

    public bool producto_carrito_exists(string nombre) 
    {

        foreach (Producto p in this.productos_carrito) {

            if (p.getNombre() == nombre) {

                return true;

            }

        }

        return false;

    }

    public void mostrar_carrito () {

        Console.WriteLine("Carrito:");

        foreach (Producto p in this.productos_carrito) {

            p.showProductoCarrito();

        }

        Console.WriteLine("Total a pagar: " + this.get_total_a_pagar());

    }

    public void añadir_al_carrito (string nombre_producto, int cantidad) {

        Producto? producto = this.tienda.get_producto_byname(nombre_producto);

        if (producto != null) {

            int stock = producto.getStock();

            if (stock >= cantidad) {

                this.productos_carrito.Add(new Producto(
                    producto.getNombre(),
                    producto.getCosto(),
                    cantidad
                ));

            } else {

                Console.WriteLine("No hay suficiente stock de " + nombre_producto);

            }

        } else {

            Console.WriteLine("El producto " + nombre_producto + " no existe\n");

        }

    }

    public void eliminar_del_carrito (String nombre) {

        bool done = false;

        for (int i = 0; i < this.productos_carrito.Count && !done; i++) {

            if (nombre == productos_carrito[i].getNombre())
            {

                this.productos_carrito.RemoveAt(i);
                done = true;

            }

        }

    }

    public double get_total_a_pagar () {

        double total = 0;

        foreach (Producto p in this.productos_carrito) {

            total += p.getPrecio_venta() * p.getStock();

        }

        return total;

    }

    public Carrito (Tienda tienda) {

        this.tienda = tienda;

    }

}

public class Tienda
{

    private double dinero;

    private List<Producto> productos = new List<Producto>();

    private Carrito carrito;

    public Carrito getCarrito () {

        return this.carrito;

    }

    public void addDinero (double dinero) {

        this.dinero += dinero;

    }

    public double getDinero () {

        return this.dinero;

    }

    public void mostrar_productos()
    {

        Console.WriteLine("Tienda:");

        foreach (Producto p in this.productos)
        {

            p.showProductoCarrito();

        }

    }

    public bool producto_exists(string nombre) 
    {

        foreach (Producto p in this.productos) {

            if (p.getNombre() == nombre) {

                return true;

            }

        }

        return false;

    }

    public Producto? get_producto_byname(string nombre)
    {

        for (int i = 0; i < this.productos.Count; i++)
        {

            if ((this.productos)[i].getNombre() == nombre)
            {

                return (this.productos)[i];

            }

        }

        return null;

    }

    public void agregar_producto(Producto producto)
    {

        this.productos.Add(producto);

    }

    public void remover_producto(Producto producto)
    {

        int index = -1;

        bool found = false;

        for (int i = 0; i < this.productos.Count && !found; i++)
        {

            if (producto.getNombre() == (this.productos)[i].getNombre())
            {

                index = i;
                found = true;

            }

        }

        productos.RemoveAt(index);

    }

    public double comprar(double dinero)
    {

        double importe = this.carrito.get_total_a_pagar();

        if (dinero >= importe) {

            this.addDinero(importe);

            double vuelto = dinero - importe;

            return vuelto;

        } else {

            Console.WriteLine("No cuenta con suficiente dinero para efectuar la compra");

        }

        return 0;

    }

    public Tienda () {

        this.carrito = new Carrito(this);

    }

}

class Program
{

    private static readonly string menu_admin = "Menú de administración: "
    + "\n1-) Mostrar productos de la tienda"  
    + "\n2-) Mostrar dinero en caja"  
    + "\n3-) Añadir producto a la tienda"
    + "\n4-) Eliminar producto de la tienda"
    + "\n5-) Agregar stock a producto"
    + "\n6-) Salir";

    private static readonly string menu_cliente = "Menú de cliente: "
    + "\n1-) Mostrar productos de la tienda"
    + "\n2-) Añadir producto al carrito"
    + "\n3-) Eliminar producto del carrito"
    + "\n4-) Mostrar carrito"
    + "\n5-) Comprar"
    + "\n6-) Salir";

    private static void interfaz_administracion (Tienda tienda) {

        bool exit = false;

        while (!exit) {

            Console.WriteLine(menu_admin);
            int? option = getOption(5);

            if (option != null) {

                switch (option)
                {
                    case 1: 
                        tienda.mostrar_productos();
                        break;
                    case 2:
                        Console.WriteLine("Dinero en caja: ");
                        Console.WriteLine(tienda.getDinero());
                        break;
                    case 3:
                        interfaz_agregarProducto(tienda);
                        break;
                    case 4:
                        interfaz_removerProducto(tienda);
                        break;
                    case 5:
                        interfaz_agregarStock(tienda);
                        break;
                    case 6:
                        exit = true;
                        break;
                }

            } else {

                Console.WriteLine("Ha elegido una opción inválida, intente de nuevo");

            }

        }

    }

    private static void interfaz_cliente (Tienda tienda) {

        bool exit = false;

        while (!exit) {

            Console.WriteLine(menu_cliente);
            int? option = getOption(5);

            if (option != null) {

                switch (option)
                {
                    case 1: 
                        tienda.mostrar_productos();
                        break;
                    case 2:
                        interfaz_agregarProducto_carrito(tienda);
                        break;
                    case 3:
                        interfaz_removerProducto_carrito(tienda);
                        break;
                    case 4:
                        tienda.getCarrito().mostrar_carrito();
                        break;
                    case 5:
                        
                        break;
                    case 6:
                        exit = true;
                        break;
                }

            } else {

                Console.WriteLine("Ha elegido una opción inválida, intente de nuevo");

            }

        }

    }

    private static void interfaz_agregarProducto (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            string? nombre;
            double? costo;
            int? stock;

            Console.WriteLine("Ingrese nombre: ");
            nombre = Console.ReadLine();

            if (nombre != null) {

                Console.WriteLine("Ingrese costo: ");
                costo = getDouble();

                if (costo != null) {

                    bool confirmed = false;

                    while (!confirmed) {

                        Console.WriteLine("¿Ingresar stock? y/n");
                        string? sconf = Console.ReadLine();
                        char? conf = checkConf(sconf);

                        if (conf != null) {

                            confirmed = true;

                            if (conf == 'y') {

                                Console.WriteLine("Ingrese stock: (puede agregar negativo)");
                                stock = getInt();

                                if (stock != null) {

                                    tienda.agregar_producto(new Producto(nombre, costo.Value, stock.Value));

                                    Console.WriteLine("Producto añadido");

                                } else {

                                    success = false;
                                    Console.WriteLine("Ha ingresado un stock inválido");

                                }

                            } else {

                                tienda.agregar_producto(new Producto(nombre, costo.Value));

                                Console.WriteLine("Producto añadido");

                            }

                        }

                    }

                } else {

                    success = false;
                    Console.WriteLine("Ha ingresado un precio inválido");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un nombre inválido");

            }

        }

    }

    private static void interfaz_removerProducto (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            string? nombre;

            Console.WriteLine("Ingrese nombre: ");
            nombre = Console.ReadLine();

            if (nombre != null) {

                Producto? p = tienda.get_producto_byname(nombre);

                if (p != null) {

                    tienda.remover_producto(p);
                    Console.WriteLine("Producto eliminado");

                } else {

                    Console.WriteLine("(El nombre ingresado no pertenece a ningún producto, operación finalizada sin cambios)");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un nombre inválido");

            }

        }

    }

    private static void interfaz_agregarStock (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            string? nombre;
            int? stock;

            Console.WriteLine("Ingrese nombre: ");
            nombre = Console.ReadLine();

            if (nombre != null) {

                Console.WriteLine("Ingrese stock: (puede agregar valores negativos para restar stock)");
                stock = getInt();

                if (stock != null) {

                    Producto? p = tienda.get_producto_byname(nombre);

                    if (p != null) {

                        p.addStock(stock.Value);
                        Console.WriteLine("Stock añadido");

                    } else {

                        Console.WriteLine("(El nombre ingresado no pertenece a ningún producto, operación finalizada sin cambios)");

                    }

                } else {

                    success = false;
                    Console.WriteLine("Ha ingresado un stock inválido");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un nombre inválido");

            }

        }

    }

    private static void interfaz_agregarProducto_carrito (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            string? nombre;
            int? cantidad;

            Console.WriteLine("Ingrese nombre: ");
            nombre = Console.ReadLine();

            if (nombre != null) {

                Console.WriteLine("Ingrese cantidad: ");
                cantidad = getInt(true);

                if (cantidad != null) {

                    tienda.getCarrito().añadir_al_carrito(nombre, cantidad.Value);
                    Console.WriteLine("Producto añadido al carrito");

                } else {

                    success = false;
                    Console.WriteLine("Ha ingresado una cantidad inválida");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un nombre inválido");

            }

        }

    }

    private static void interfaz_removerProducto_carrito (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            string? nombre;

            Console.WriteLine("Ingrese nombre: ");
            nombre = Console.ReadLine();

            if (nombre != null) {

                if (tienda.producto_exists(nombre)) {

                    tienda.getCarrito().eliminar_del_carrito(nombre);
                    Console.WriteLine("Producto eliminado del carrito");

                } else {

                    Console.WriteLine("(El nombre ingresado no pertenece a ningún producto, operación finalizada sin cambios)");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un nombre inválido");

            }

        }

    }

    private static void interfaz_pagar (Tienda tienda) {

        bool success = false;

        while (!success) {

            success = true;

            Console.WriteLine("Ingrese pago");

            double? pago = getDouble();

            if (pago != null) {

                double vuelto = tienda.comprar(pago.Value);

                Console.WriteLine("Pago concretado con éxito, vuelto: " + vuelto);

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un pago inválido");

            }

        }

    }

    private static double? getDouble () {

        string? input = Console.ReadLine();

        double? ans;

        if (input == null) {

            ans = null;

        } else {

            try {

                ans = double.Parse(input);

            } catch {

                ans = null;

            }

        }

        return ans;

    }

    private static int? getInt (bool positive = false) {

        string? input = Console.ReadLine();

        int? ans;

        if (input == null) {

            ans = null;

        } else {

            try {

                ans = int.Parse(input);

                if (positive) {

                    if (ans < 0) {

                        ans = null;

                    }

                }

            } catch {

                ans = null;

            }

        }

        return ans;

    }

    private static int? getOption (int options_len) {

        string? input = Console.ReadLine();

        if (input == null) {

            return null;

        }

        int? ans;

        try {

            ans = int.Parse(input);

            if (ans < 0 || ans > options_len) {

                ans = null;

            }

        } catch  {

            ans = null;

        }

        return ans;

    }

    private static char? checkConf (string? input) {

        if (input == null) {

            return null;

        }

        if (input.Length != 1) {

            return null;

        } else {

            char c = input[0];

            if (c != 'y' && c != 'n') {

                return null;

            } else {

                return c;

            }

        }

    }

    private static char? checkType (string? input) {

        if (input == null) {

            return null;

        }

        if (input.Length != 1) {

            return null;

        } else {

            char c = input[0];

            if (c != 'a' && c != 'c') {

                return null;

            } else {

                return c;

            }

        }

    }

    private static bool setUsuario () {

        bool verification = false;

        bool admin = false;

        while (!verification) {

            Console.WriteLine("Ingrese tipo de usuario: (a/c) (a: administración, c: cliente)");

            string? input_type = Console.ReadLine();

            char? type = checkType(input_type);

            if (type != null) {

                admin = type == 'a';
                verification = true;

            } else {

                Console.WriteLine("Ha ingresado una opción inválida, intente de nuevo");

            }

        }

        return admin;

    }

    public static void Main(string[] args)
    {

        Tienda tienda = new Tienda();

        bool program = true;
        
        while (program) {

            bool admin = setUsuario();

            if (admin) {

                interfaz_administracion(tienda);

            } else {

                interfaz_cliente(tienda);

            }

        }

    }
}