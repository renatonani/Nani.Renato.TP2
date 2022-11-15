# MagicTruco.

## Sobre mí:
Me gustó la idea del trabajo de dejar a libertad de cada uno decidir qué juego realizar. Si bien el juego que más conozco es el uno, elegí hacer el truco para que sea un desafío (después me arrepentí, pero bueno). A pesar de que pensar la lógica y el funcionamiento del juego llevó bastante tiempo, fue divertido e interesante de desarrollar. Asímismo considero que al igual que el trabajo anterior, este trabajo es una buena herramienta para unificar los temas vistos durante la segunda mitad de la cursada. Sinceramente, estoy bastante contento con el resultado de mi trabajo y con haber podido desarrollar una interfaz de usuario medianamente aceptable.

## Resumen:
- La aplicación MagicTruco cuenta con una base de datos donde los usuarios pueden registrarse (siempre y cuando el nombre de usuario no haya sido utilizado anteriormente) y logearse una vez creada su cuenta correctamente. Al ingresar, el usuario podrá elegir si ver información relacionadas con las estadísticas de las partidas jugadas por cada jugador registrado en la aplicación o crear una nueva sala de juego donde podrá empezar una partida de truco.
Al ser offline o local, dentro de la aplicación se da por hecho que el usuario logeado **SIEMPRE SERÁ EL JUGADOR 1, mientras que el jugador 2 será un invitado que jugará de manera local sin estar relacionado con un usuario.**
Al ganar o perder una partida, automáticamente se actualizarán las partidas jugadas, ganas o perdidas, del usuario logeado.

## Diagrama de clases:

![image](https://user-images.githubusercontent.com/98593040/202044151-daf7c0e3-608a-4283-8999-8ca0bbf6fc12.png)

## Justificación técnica:

- Tema 10:
El manejo de excepciones en bloques try-catch-finally fue útil mayormente a la hora de realizar la conexión a la base de datos, ya que en caso de haber un error o excepción, la misma queda contenida y mediante una depuración podemos saber de qué tipo de excepción se trata.

- Tema 11:
Las pruebas unitarias las realicé al finaliza por completo la funcionalidad del programa y fueron útiles para encontrar pequeños errores en las distintas entidades creadas. Por ejemplo, gracias a las pruebas unitarias pude detectar un error en la sobrecarga del != de la entidad Carta. 

- Tema 12 y 13:
Los tipos genéricos los utilicé en conjunto con las interfaces para desarrolar una interfaz que sirva de base para trabajar con conexiones a bases de datos sin importar el tipo de datos que deseemos trabajar. Esto habilita a que en un futuro, si se desea crear una base de datos que trabaje con otro tipo de dato dentro de la aplicación, la interfaz pueda dar las indicaciones necesarias de los métodos fundamentales a desarrollar.


- Tema 14 y 15:
La serialización y el manejo de archivos fueron útiles para generar un método que permita crear un archivo ".json" que guarde los datos de un mazo de truco. De esta manera, no sería necesario que al crear un mazo de cartas nuevo, el mismo se cree de cero, sino que podría ser creado a partir de los datos leídos del archivo "Cartas.json".

- Tema 16 y 17:
La conexión a una base de datos SQL permitió guardar los datos de los usuarios registrados en la aplicación, a diferecia de la aplicación desarrollada anteriormente donde la permanencia de datos no era una opción. Esto me permitió trabajar de manera mucho más eficiente y práctica con los usuarios registrados en la aplicación, permitiendo así realizar un menú de login que permita tanto iniciar sesión como registrarse en la aplicación, además de guardar las estadísticas de cada usuario en partícular contando con la posibilidad de modificar esos datos.

- Tema 18, 19 y 20:
Tanto los eventos, delegados, expresiones lambda e hilos permitieron agregar funcionalidades y mejorar el funcionamiento del programa. En el caso de los delegados y los eventos, los utilicé particularmente para desarrollar un contador dentro de la partida que permita tener consciencia de la duración de la misma y que se desarrolle de manera paralela (en un hilo aparte) mientras se ejecuta la partida. Los hilos también permitieron que cada partida pueda encontrarse corriendo en un proceso diferente de manera independiente.


## Reglas del juego y aclaraciones.
Al ser esta una versión acotada del truco (donde las partidas se juegan por puntos y no por cantidad de rondas), se modificaron algunas de las reglas del juego.
Al momento de cantar envido, el jugador contrario al que lo haya cantado, tendrá la posibilidad de aceptarlo, rechazarlo o cantar falta envido. En caso de darse esta última opción, el ganador de la partida se definirá en base al jugador que mayor tanto tenga, ya que sólo contamos con 4 rondas.
