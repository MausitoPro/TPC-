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
            ", Costo: " + this.getCosto() + 
            ", Precio final: " + this.getPrecio_venta() +
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

    public void resetCarrito () {

        productos_carrito = new List<Producto>();

    }

    public bool productoCarritoExists(string nombre) 
    {

        foreach (Producto p in this.productos_carrito) {

            if (p.getNombre() == nombre) {

                return true;

            }

        }

        return false;

    }

    public Producto? getProductoByName_carrito(string nombre) {

        for (int i = 0; i < this.productos_carrito.Count; i++)
        {

            if ((this.productos_carrito)[i].getNombre() == nombre)
            {

                return (this.productos_carrito)[i];

            }

        }

        return null;

    }

    public void mostrarCarrito () {

        Console.WriteLine("Carrito:");

        foreach (Producto p in this.productos_carrito) {

            p.showProductoCarrito();

        }

        Console.WriteLine("Total a pagar: " + this.getTotalAPagar());

    }

    public void añadirAlCarrito (string nombre_producto, int cantidad) {

        Producto? producto = this.tienda.getProductoByName(nombre_producto);

        if (producto != null) {

            int stock = producto.getStock();

            if (stock >= cantidad) {

                this.productos_carrito.Add(new Producto(
                    producto.getNombre(),
                    producto.getCosto(),
                    cantidad
                ));

            } 

        } 

    }

    public void eliminarDelCarrito (String nombre) {

        bool done = false;

        for (int i = 0; i < this.productos_carrito.Count && !done; i++) {

            if (nombre == productos_carrito[i].getNombre())
            {

                this.productos_carrito.RemoveAt(i);
                done = true;

            }

        }

    }

    public double getTotalAPagar () {

        double total = 0;

        foreach (Producto p in this.productos_carrito) {

            total += p.getPrecio_venta() * p.getStock();

        }

        return total;

    }

    public void actualizarStock () {

        foreach (Producto p in productos_carrito) {

            Producto? aux = this.tienda.getProductoByName(p.getNombre());

            if (aux != null) {

                aux.addStock(-p.getStock());

            }

        }

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

    public void mostrarProductos()
    {

        Console.WriteLine("Tienda:");

        foreach (Producto p in this.productos)
        {

            p.showProductoTienda();

        }

    }

    public void mostrarProductosCarrito()
    {

        Console.WriteLine("Tienda:");

        foreach (Producto p in this.productos)
        {

            p.showProductoCarrito();

        }

    }

    public bool productoExists(string nombre) 
    {

        foreach (Producto p in this.productos) {

            if (p.getNombre() == nombre) {

                return true;

            }

        }

        return false;

    }

    public Producto? getProductoByName(string nombre)
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

    public void agregarProducto(Producto producto)
    {

        this.productos.Add(producto);

    }

    public void removerProducto(Producto producto)
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

        double importe = this.carrito.getTotalAPagar();

        if (dinero >= importe) {

            this.addDinero(importe);

            this.carrito.actualizarStock();

            this.carrito.resetCarrito();

            double vuelto = dinero - importe;

            return vuelto;

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
            int? option = getOption(6);

            if (option != null) {

                switch (option)
                {
                    case 1: 
                        tienda.mostrarProductos();
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
            int? option = getOption(6);

            if (option != null) {

                switch (option)
                {
                    case 1: 
                        tienda.mostrarProductosCarrito();
                        break;
                    case 2:
                        interfaz_agregarProducto_carrito(tienda);
                        break;
                    case 3:
                        interfaz_removerProducto_carrito(tienda);
                        break;
                    case 4:
                        tienda.getCarrito().mostrarCarrito();
                        break;
                    case 5:
                        interfaz_pagar(tienda);
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

                                    tienda.agregarProducto(new Producto(nombre, costo.Value, stock.Value));

                                    Console.WriteLine("Producto añadido");

                                } else {

                                    success = false;
                                    Console.WriteLine("Ha ingresado un stock inválido");

                                }

                            } else {

                                tienda.agregarProducto(new Producto(nombre, costo.Value));

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

                Producto? p = tienda.getProductoByName(nombre);

                if (p != null) {

                    tienda.removerProducto(p);
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

                Producto? p = tienda.getProductoByName(nombre);

                if (p != null) {

                    Console.WriteLine("Ingrese stock: (puede agregar valores negativos para restar stock)");
                    stock = getInt();

                    if (stock != null) {

                        p.addStock(stock.Value);
                        Console.WriteLine("Stock añadido");

                    } else {

                        success = false;
                        Console.WriteLine("Ha ingresado un stock inválido");

                    }

                        
                } else {

                    Console.WriteLine("El producto no existe");

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

                Producto? p = tienda.getProductoByName(nombre);

                if (p != null) {

                    Producto? p_actual = tienda.getCarrito().getProductoByName_carrito(nombre);

                    int cantidad_actual = 0;

                    if (p_actual != null) {

                        cantidad_actual = p_actual.getStock();

                    }

                    Console.WriteLine("Ingrese cantidad: (stock disponible: " + (p.getStock() - cantidad_actual) + ")");
                    cantidad = getInt(true);

                    if (cantidad != null) {

                        if (cantidad + cantidad_actual <= p.getStock()) {

                            tienda.getCarrito().eliminarDelCarrito(nombre);
                            tienda.getCarrito().añadirAlCarrito(nombre, cantidad.Value + cantidad_actual);
                            Console.WriteLine("Producto añadido al carrito");

                        } else {

                            Console.WriteLine("No hay suficiente stock");

                        }

                    } else {

                        success = false;
                        Console.WriteLine("Ha ingresado una cantidad inválida");

                    }

                } else {

                    Console.WriteLine("Ese producto no existe");

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

                if (tienda.getCarrito().productoCarritoExists(nombre)) {

                    tienda.getCarrito().eliminarDelCarrito(nombre);
                    Console.WriteLine("Producto eliminado del carrito");

                } else {

                    Console.WriteLine("El producto está en el carrito o no existe");

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

            double precio_total = tienda.getCarrito().getTotalAPagar();

            Console.WriteLine("Precio total: " + precio_total);
            Console.WriteLine("Ingrese pago: ");

            double? pago = getDouble();

            if (pago != null) {

                if (pago >= precio_total) {

                    double vuelto = tienda.comprar(pago.Value);

                    Console.WriteLine("Pago concretado con éxito, vuelto: " + vuelto);

                } else {

                    Console.WriteLine("El monto abonado no es suficiente");

                }

            } else {

                success = false;
                Console.WriteLine("Ha ingresado un pago inválido");

            }

        }

    }

    private static double? getDouble (bool positive = false) {

        string? input = Console.ReadLine();

        double? ans;

        if (input == null) {

            ans = null;

        } else {

            try {

                ans = double.Parse(input);

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

            if (c != 'a' && c != 'c' && c != 's') {

                return null;

            } else {

                return c;

            }

        }

    }

    private static char setUsuario () {

        while (true) {

            Console.WriteLine("Ingrese tipo de usuario: (a/c/s) (a: administración, c: cliente, s: salir)");

            string? input_type = Console.ReadLine();

            char? type = checkType(input_type);

            if (type != null) {

                return type.Value;

            } else {

                Console.WriteLine("Ha ingresado una opción inválida, intente de nuevo");

            }

        }

    }

    public static void Main(string[] args)
    {

        Tienda tienda = new Tienda();

        bool program = true;
        
        while (program) {

            char type = setUsuario();

            switch (type) {

                case 'a': 
                    interfaz_administracion(tienda);
                    break;
                case 'c':
                    interfaz_cliente(tienda);
                    break;
                case 's' :
                    program = false;
                    break;
            }

        }

    }

}