# Fundamentos de Desarrollo de Videojuegos

## Trabajo final: Desarrollo de un prototipo en 2D

Este proyecto contiene un prototipo de juego 2D que se ha desarrollado aplicando todos los conocimientos adquiridos a lo largo de la asignatura.

En este juego dispondremos de un total de dos personajes jugables, y el objetivo final será reunirlos en la meta. Cada uno de los personajes empieza en un lado diferente del mapa, y será necesaria la cooperación entre ellos para alcanzar el centro y completar el nivel.

A continuación se incluye una imagen que muestra el prototipo en acción, de principio a fin.

![Demostración del prototipo](./Screenshots/2d-prototype-demo.gif)

## Diseño del nivel

### 🗺 Atonomía del mapa

En la siguiente imagen se muestra una vista completa de la escena, que se divide en 5 secciones diferentes:

![Vista de la escena al completo](./Screenshots/level-section-overview.png)

Se ha aplicado un nombrado a cada una de las secciones para facilitar su identificación en los siguientes párrafos. También encontraremos estos nombres en la jerarquía de objetos de la escena, donde se ha usado esta nomenclatura para agrupar algunos elementos.

### 🚧 Abriendo las barreras

Como podemos ver, las secciones están separadas por una barrera que impide el paso a los personajes hacia el centro. Abrir una de estas barreras requerirá la activación de los botones del color correspondiente.

A fin de establecer un mecanismo para la cooperación, ninguno de los personajes tiene posibilidad de abrir estas barreras desde su lado del mapa, ya que los botones se han dispuesto de tal forma que un personaje sólo pueda abrir el camino del otro.

En este caso, podemos ver que la barrera azul del área A1 sólo puede ser abierta desde el botón en el área B1. Del mismo modo, la barrera naranja en el área B1 requiere de que se pulse el botón correspondiente en el área A1.

![Abriendo la barrera naranja](./Screenshots/opening-red-barrier.gif)

La misma mecánica se aplica a las áreas A2 y B2, donde los botones que abren el camino están en el área contraria.

El caso de la barrera amarilla es algo especial, ya que tenemos dos botones del mismo color. Para superar esta sección, será necesario que los dos sean pulsados de forma simultánea. Resolver este puzzle requerirá el uso de alguna de las cajas en escena para mantener activo uno de los botones mientras que la chica pulsa el otro.

![Activando los botones amarillos](./Screenshots/opening-yellow-barrier.gif)

### 🔥 Usando el fuego

El botón verde del área B2 está custodiado por un zombie que impide el paso al jugador. Éste atacará al personaje en caso de acercarse, impulsándolo hacia detrás.

![Zombie impidiendo el paso](./Screenshots/zombie-attack-preview.gif)

La única manera de pasar es eliminar este obstáculo, y para ello juegan un papel las diferentes gemas que pueden recolectar los personajes.

Al inicio de la escena se ha incluido un interruptor que se activará en caso de que la cantidad de gemas en el inventario sea, al menos, 5. Si el personaje se coloca en el área del interruptor cuando dicha condición se cumpla, consumirá 5 de sus gemas y se generará una llama que permitirá acabar con el zombie.

![Usando la llama para liberar el camino](./Screenshots/flame-usage-preview.gif)

## Detalles de implementación

:memo: TODO

## Información adicional del proyecto

### 🖥️ Especificaciones

- **Unity:** 2021.3.26f1 Personal
- **Sistema operativo:** Windows 10, 64 bits

### 🎨 Recursos utilizados

- [Open Game Art: Cute Girl](https://opengameart.org/content/cute-girl-free-sprites)
- [Open Game Art: The Boy](https://opengameart.org/content/the-boy-free-sprites)
- [Open Game Art: The Zombie](https://opengameart.org/content/the-zombie-free-sprites)
- [Kenney: Platformer Art Pixel Redux](https://kenney.nl/assets/platformer-art-pixel-redux)
- [Free 2D Cartoon Parallax Background](https://assetstore.unity.com/packages/2d/environments/free-2d-cartoon-parallax-background-205812)
