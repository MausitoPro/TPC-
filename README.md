Trabajo Práctico C#

(Perdón profe por los commits posteriores al viernes! Se han encontrado bugs y detalles no desarrollados, por lo que
consideramos pertinente usar github para agregar commits y solucionarlos, además de agregar este README).

Trabajo realizado por Santiago Simón Trifiró y Wallace Mitchell.

Menú:

El menú mostrado en consola dispondrá de dos modos: administrador y cliente, permitiendo así explorar las funcionalidades presentes de ambas partes.

Administrador: será posible gestionar el agregado de productos a la tienda, tendrá acceso a los costos originales de los productos y al dinero en caja.
Cliente: será posible gestionar el carrito, el cual tendrá a disposición los productos presentes en la tienda.

Detalles importantes:
- Como el programa trabaja con un cliente a la vez, el carrito será inicializado junto con la tienda, y cada tienda poseerá un solo carrito que tendrá como atributo principal la misma tienda.
- A la hora de agregar un producto a la tienda como administrador, será posible agregar un stock negativo, como para indicar un faltante (no representa un problema, ya que el programa
no permitirá al cliente adquirir una cantidad negativa de un producto).
- A la hora de agregar un producto a la tienda como administrador, si el nombre del producto ya existe, se denegará la operación sin efectuar cambios.
- A la hora de agregar un producto al carrito como cliente, el stock no se verá afectado en la tienda, cambiando este únicamente luego de efectuar una compra.
- A la hora de agregar un producto al carrito como cliente, se tiene en cuenta el stock que está siendo ocupadoe en el carrito, por lo que nunca será posible comprar más
  productos que los disponibles.
- A la hora de agregar un producto al carrito que ya está presente, este no aparecerá como dos elementos distintos, si no que el stock se sumará.
