# Fundamentos de Desarrollo de Videojuegos

## Trabajo final: Desarrollo de un prototipo en 2D

> 📹 _If you're looking for an english description of this project, feel free to check out [this video](https://youtu.be/u76cGIhHPeQ)._

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

### 🌄 Creación del entorno

Para el diseño de la escena se ha generado una _"Tile Palette"_ con los recursos escogidos.

![Vista previa de la _"Tile Palette"_ generada](./Screenshots/tile-palette.PNG)

Ésta se ha utilizado para generar la estructura de las diferentes secciones de la escena en un conjunto de _tilemaps_. **Cada una las secciones dispone de varios _tilemaps_ en distintas capas de profundidad**, de forma que haya elementos que queden por delante de otros. La nomenclatura usada en estos _tilemaps_ es la siguiente:

- `Ground` contiene únicamente las plataformas o zonas sobre las que el personaje puede caminar. Esta capa tendrá siempre asociado un _"Tilemap Collider 2D"_.
- `Walls` contiene paredes, áreas con las que el jugador podrá colisionar (también incluye un _"Tilemap Collider 2D"_), pero sobre las que no podrá andar.
- `Decoration (front)` es una capa meramente visual de elementos decorativos que se mostrarán por delante del personaje.
- `Decoration (back)` es una capa meramente visual de elementos decorativos que se mostrarán a un nivel de profundidad mayor, dando el efecto de estar más atrás del personaje.

Para completar el diseño del entorno, se ha utilizado un **fondo con efecto parallax** compuesto de 4 capas, donde cada una de ellas se moverá a una velocidad diferente. El script `ParallaxScrollingBackground` es el encargado de simular este movimiento dentro del fondo, aplicando un offset a la textura de las diferentes capas en cada iteración.

Además, se ha usado una **técnica de _background scrolling_** para que el fondo sea visible en todo momento teniendo únicamente dos copias del fondo dispuestas lado a lado. Para ello, el script `BackgroundFollowCamera` actualizará en cada frame la posición del fondo para alinearlo con la cámara.

Además de estos elementos estáticos que conforman el entorno, el mapa contendrá un conjunto de elementos con los que el jugador podrá interactuar (`Interactive Objects`), tales como cajas, botones o las mismas barreras. Dado que este tipo de elementos presentará un comportamiento específico, se han añadido a la escena como nuevos objetos, cada uno con sus scripts y configuración correspondiente.

La siguiente captura muestra la jerarquía de objetos que componen el entorno, agrupados por tipo y por la sección del nivel a la que pertenecen.

![Jerarquía de objetos que componen el entorno](./Screenshots/environment-object-hierarchy.PNG)

El objeto `Map Bounds` que se incluye al final de esta lista es un objeto estático que incluye un _collider_ para limitar el área visible del mapa, impidiendo que los personajes se salgan a partes de la escena en que no hay nada.

### 🔄 Intercambio de personajes

Los dos personajes jugables implementan el mismo comportamiento, definido en el script `PlayableCharacter`. Éste es bastante sencillo, ya que sus posibles acciones están limitadas a caminar y saltar.

**Cada personaje cuenta con sus propios sprites, animaciones y un _"Animation Controller"_** para gestionar cuándo mostrar cada una. Así mismo, los dos **harán uso de las físicas** mediante el componente _"RigidBody2D"_, de forma que aspectos como su masa y gravedad afectarán a su capacidad de saltar o a los objetos que puedan empujar. También dispondrán de un _collider_ que nos permitirá gestionar sus colisiones con otros objetos del entorno.

Es importante mencionar también que el proyecto hace uso del _package_ `Cinemachine`, y que **cada uno de los personajes tiene asociada una cámara virtual que lo seguirá** en todo momento. Estas cámaras se han configurado con unos límites, de forma que se ajusten a la superficie recorrible por cada personaje (por ejemplo, la cámara de la chica sólo puede moverse entre las áreas A1, A2 y GOAL).

Para la implementación de esta funcionalidad de alternar entre los dos personajes durante el juego, se ha creado un script `PlayerController` que será ejecutado por el objeto `Game Controller` de la escena.

Este script mantiene en su estado interno un listado de todos los personajes en escena (etiquetados con el _tag_ _"Player"_), y usará esto para transitar entre ellos en secuencia cada vez que el jugador pulsa la tecla _"Tab"_.

La parte más importante de esta funcionalidad reside en el método `SwitchActiveCharacter`, del cual se muestra un extracto a continuación:

```csharp
public void SwitchActiveCharacter (int index) {
    Debug.Log("Switch to character " + playableCharacters[index].gameObject.name);
    for (int i = 0; i < playableCharacters.Count; i++) {
        bool shouldBeActive = i == index;
        playableCharacters[i].isActiveCharacter = shouldBeActive;
        playableCharacters[i].assignedCamera.enabled = shouldBeActive;
    }
}
```

Este código viene a hacer dos cosas: habilitar la capacidad de movimiento y la cámara asociada al nuevo personaje activo, y desactivar al resto.

Para lo primero se hace uso del flag `isActiveCharacter` del script `PlayerCharacter`. De estar a `false`, esto hará que no se escuche la entrada del jugador durante el método `Update`. Esto es importante, ya que si sólo modificaramos la cámara activa, el otro personaje se estaría moviendo de la misma forma que el activo, aunque no lo viésemos. Esto implicaría que, al cambiar de nuevo al otro personaje, nos lo encontraríamos en una posición completamente diferente de aquella en donde la dejamos.

En cuanto al cambio de cámara, esto se ha hecho usando el flag `enabled` que proveen las cámaras virtuales para desactivarlas o activarlas según haga falta.

### 💎 Gestión de gemas

Como se describió brevemente en el apartado del diseño del nivel, hay una de las mecánicas que requiere del uso de gemas. Estas son pequeños objetos coleccionables que se van a generar de forma dinámica en zonas aleatorias del nivel.

Para implementar la generación de estos objetos **se ha utilizado la técnica de _object pooling_**. En nuestra escena tendremos un objeto `Object Pool` que implementa un script del mismo nombre, que instanciará varias copias de un prefab determinado (llamado `Diamond`, en este caso) y permitirá su reutilización.

Los objetos `Spawner A` y `Spawner B`, por otro lado, implementarán el script `ItemManager` que hará uso de este pool de objetos para hacer _spawn_ de gemas en una posición aleatoria dentro de un rango específico. En concreto, `Spawner A` generará gemas dentro de las secciones A1 y A2, mientras que `Spawner B` lo hará en las áreas B1 y B2.

Ambas instancias de `ItemManager` compartirán el mismo pool de objetos, e intentarán generar nuevas gemas pasado un intervalo de tiempo para reponerlas en caso de que el jugador ya haya recolectado las anteriores.

En la siguiente imagen podemos ver el aspecto del `Object Pool` durante la ejecución del juego:

![Aspecto del pool de objetos durante ejecución](./Screenshots/object-pool-hierarchy.PNG)

Debido a que el tamaño del pool es 10, nunca será posible tener más en escena de forma simultánea. Las instancias que no están en uso (probablemente por haber sido ya recolectadas) aparecen en gris, y eventualmente serán reemplazadas a solicitud de cualquiera de los `Spawner`.

Para dar visibilidad de los objetos que se han recolectado entre ambos personajes, el objeto `Game Controller` implementa un script `GemManager` que mantiene un recuento de las gemas actualmente en el inventario.

Este script hace uso de **eventos personalizados**, suscribiéndose al evento `Item.OnPickUp` (que está definido en la clase `Item` que implementa el prefab `Diamond`) para saber cuándo debe de aumentar el contador.

```csharp
void OnGemPickUp (Item item, GameObject itemPicker) {
    collectedGemCount += 1;
    RefreshCountInUI();
}
```

Se ha incluido también un texto en la UI del juego, que será actualizado cada vez que se recolectan nuevos objetos, tal como se ve en el código anterior.

![Contador de gemas en posición](./Screenshots/gem-counter-ui.PNG)

### 〽 Plataforma móvil

Uno de los elementos del entorno más destacables es la plataforma móvil que hay en el área A2. Ésta **se ha construido utilizando el sistema de _Waypoints_**.

Para esto se han añadido a la escena varios objetos vacíos que identifican los diferentes puntos del circuito, que en este caso es muy sencillo y consta sólo de tres. Se ha añadido también otro objeto vacío guía y que será necesario para que los scripts de `WaypointCircuit` y `WaypointProgressTracer` queden bien configurados.

![Estructura de la plataforma móvil dentro de la escena](./Screenshots/moving-platform-hierarchy.PNG)

Para moverse a lo largo de la ruta, nuestro objeto `Moving Platform` implementa el script `FollowTheGuide` que, como su nombre indica, se encargará de seguir al objeto vacío que creamos antes y que se mueve a lo largo del circuito.

Con esto se logra que la plataforma esté constantemente oscilando entre los tres puntos que hemos definido, tal como se ve en esta imagen:

![Plataforma recorriendo el circuito](./Screenshots/platform-waypoint-circuit.gif)

### 🔘 Activación de botones y apertura de barreras

Como se ha mencionado anteriormente, el nivel dispone de un conjunto de botones que permitirán al jugador abrir las barreras que le impiden el paso.

Todos estos botones implementan un script denominado `ActionableButton`, que lanzan los eventos personalizados `OnButtonRelease` y `OnButtonPress` cuando alguno de los personajes (o una caja) entra en contacto con él. Para lograr esto, los botones tienen activado el flag _"IsTrigger"_ de su _collider_.

A continuación se expone un extracto del código que detecta si el botón está activo:

```csharp
// Validate if the source of the collission is the player or a box the player pushed on top
bool isEventSourceValid (Collider2D collider) {
    GameObject eventSource = collider.gameObject;
    return eventSource.CompareTag("Player") || eventSource.CompareTag("Box");
}

// Trigger event when player collides with the button
private void OnTriggerEnter2D (Collider2D collider)
{
    if (isEventSourceValid(collider)) {
        OnButtonPress(this);
    }
}
```

La segunda pieza del puzzle son las propias barreras, que implementan un script denominado `UnlockableBarrier`. Este script será el que escuche los eventos generados por los botones, y deberá recibir el listado de botones que deben estar activos para su apertura.

En este ejemplo podemos ver que, para la barrera amarilla, el script recibe como parámetro los dos botones amarillos en escena:

![Configuración de un objeto barrera](./Screenshots/unlockable-barrier-config.PNG)

Dentro de `UnlockableBarrier` se llevará una cuenta de los botones requeridos que están activos en cada momento y, en caso de que se dé la condición de que todos activados, ejecutará un método para abrir la barrera:

```csharp
void OnButtonPress (ActionableButton button) {
    if (buttonsRequiredToUnlock.Contains(button)) {
        UpdateCurrentActiveButtonState(button, 1);
        Debug.Log(currentlyActiveButtons.Count + " active buttons for " + this.gameObject.name);
    }

    // Unlock barrier only when all buttons are active at the same time
    if (currentlyLocked && currentlyActiveButtons.Count == buttonsRequiredToUnlock.Count) {
        UnlockBarrier();
    }
}
```

A fin de dar algo de _feedback_ al jugador y hacerle entender que algo ha pasado, este es el punto en que se han usado los impulsos de `Cinemachine`. El método `UnlockBarrier`, aparte de eliminar la barrera de la escena para que no impida más el paso al jugador, se provocará un pequeño efecto de temblor en la cámara.

```csharp
void UnlockBarrier () {
    Debug.Log("Opening barrier " + this.gameObject.name);

    // Apply visual effect so players know something is happening
    CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();
    impulseSource.GenerateImpulse();

    // Disable barrier so it's no longer visible and characters can move forward
    this.gameObject.SetActive(false);
    currentlyLocked = false;
}
```

El resultado de la ejecución de este código se puede observar a continuación:

![Efecto de temblor al desbloquear barrera](./Screenshots/barrier-impulse-preview.gif)

### 🔥 Generación de fuego

Tal y como se mencionó anteriormente, tenemos un obstáculo en el juego (el zombie) que sólo puede ser sorteado mediante el uso del fuego.

Dado que el zombie tiene asociada una masa muy superior a la del personaje, la alternativa de empujarlo para quitarlo de enmedio no funcionará. El zombie ha sido programado de tal forma que lo único que puede derrotarlo es el fuego (véase el script `ZombieController`).

Para hacer esto posible, el área B2 incluye un interruptor que permite generar una llama que sale disparada en dirección al zombie.

Se han incluido **varios elementos de UI** que indican el requisito que el jugador debe de cumplir a fin de poder usar este interruptor. En este caso, cada disparo irá con un coste de 5 gemas.

![Instrucciones de uso del interruptor](./Screenshots/flame-switch-ui.PNG)

La conducta de este interruptor está definida en el script `FlameSwitch`, que ejecutará el siguiente método cuando el personaje si sitúa en una posición en que colisione con el interruptor y disponga de, al menos, 5 gemas en el inventario.

```csharp
// Spawn a flame that will move left until it hits something
void ShootFlame () {
    GameObject flame = Instantiate(flamePrefab, spawnPoint.transform);
    Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
    rb.velocity = Vector2.left * 2.0f;
}
```

Esta función instanciará una nueva llama encima de la tubería que hay en el área B2, y sobreescribirá la propiedad `velocity` de su componente `RigidBody2D` cinemático para lograr que ésta se desplace en línea recta hacia el zombie.

![Uso del interruptor para invocar fuego](./Screenshots/flame-swith-usage.gif)

Como podemos ver en esta demostración, cada disparo consumirá 5 gemas del inventario compartido entre los personajes.

Destacar también que el _prefab_ `Flame` contiene un script que hace que la llama se autodestruya tan pronto como colisione con algo que no sea una gema. En caso de que el jugador la tocara por error en lo que ésta transita hacia el zombie, será posible usar el interruptor para generar una nueva llama tantas veces como se desee (siempre que se disponga de gemas suficientes, claro).

### 🚩 Finalización del nivel

Cuando ambos personajes alcanzan la meta, automáticamente se muestra un mensaje indicando al jugador que ha completado el juego. Para la implementación de este método, han hecho falta varias piezas.

En primer lugar, el script `PlayerCharacter` de los personajes pueden generar los eventos `OnGoalEnter` y `OnGoalExit`. Éstos se lanzarán en cuanto el personaje entra o sale del área marcada como meta, que se ha definido en un objeto vacío llamado `Goal Area` con un _collider_ de tipo _trigger_ y al cual hemos asignado el tag _"Goal"_.

```csharp
private void OnTriggerEnter2D (Collider2D collider)
{
    if (collider.gameObject.tag == "Goal") {
        Debug.Log(this.gameObject.name + " arrived to goal!");
        if (OnGoalEnter != null) {
            OnGoalEnter(this);
        }
    }
}
```

El objeto `Game Controller` de nuestra escena será el responsable de escuchar estos eventos, a fin de detectar cuando los dos personajes han alcanzado la posición destino. Para ello implementa un script `GoalManager` que lleva un recuento de los personajes que ya han alcanzado la meta, y hace algo cuando han llegado todos:

```csharp
void OnCharacterReachedGoal (PlayableCharacter character) {
    charactersInGoal++;

    // If everyone is already in goal, deactivate all characters and show congrats message
    if (charactersInGoal == playerController.GetPlayableCharactersCount()) {
        Debug.Log("Everyone is in goal, so game will end now...");
        playerController.SwitchActiveCharacter(-1);
        congratulationsMessage.SetActive(true);
    }
}
```

A consecuencia de este código, pasan varias cosas. En primer lugar, todos los personajes se marcan como inactivos. Esto implica que el jugador perderá el control sobre ellos, ya que dejarán de escucharse los eventos de teclados asociados al movimiento del personaje. Además, la cámara virtual asociada a cada uno de ellos quedará deshabilitada.

Hay una tercera cámara virtual de baja prioridad ubicada dentro de la escena y que apunta, específicamente, a la zona de meta. Cuando ocurre que se desactiva la cámara asociada a los personajes a nivel individual, ésta pasa a ser la cámara activa y se puede ver la vista centrada en el área destino.

Por último, se mostrarán en pantalla varios componentes de UI que conforman el mensaje que indica al jugador que ha logrado superado al nivel.

![Mensaje de felicitaciones al acabar el juego](./Screenshots/goal-congrats-message.PNG)

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
