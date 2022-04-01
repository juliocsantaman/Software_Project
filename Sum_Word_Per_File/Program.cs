using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

//contador para repeticiones de palabra en todos los files
int count = 0;

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

//se agregan todas las palabras del consolidado en un array de strings
string[] words = File.ReadAllLines(@"a5_matricula.txt".ToString()).ToArray();

for (int i = 0; i < 503; i++)
{
    //se agrega al string content el contenido de la posición actual
    String content = words[i];

    //se separa el contenido actual y se guarda en un array de strings
    String[] str = words[i].Split(' ');

    //se asigna la primer palabra en el string
    String wordToCompare = str[0];

    for (int i2 = 1; i2 <= 503; i2++)
    {
        //se declara un cronometro para el tiempo en revisar cada file con cada palabra
        Stopwatch chronFile = new Stopwatch();

        //se inicia el cronometro
        chronFile.Start();

        if (i2 <= 9)
        {
            path = $@"files\00{i2}.html-copy.html-ordered.txt";
        }
        //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
        if (i2 >= 10 && i2 <= 99)
        {
            path = $@"files\0{i2}.html-copy.html-ordered.txt";
        }
        //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
        if (i2 >= 100 && i2 <= 503)
        {
            path = $@"files\{i2}.html-copy.html-ordered.txt";
        }

        //se crea un array para guardar todo el contenido del file
        string[] words2 = File.ReadAllLines(path.ToString()).ToArray();

        int index = Array.IndexOf(words2, wordToCompare);
        if (index > -1)
        {
            count++;
        }

        //se detiene el cronometro
        chronFile.Stop();

        //se agrega al log file
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine("Se revisó file " + path + " en " + chronFile.ElapsedMilliseconds + "ms");
        }

        //se imprime en consola
        Console.WriteLine("Se revisó file " + path + " en " + chronFile.ElapsedMilliseconds + "ms");

    }
    //se agrega a la variable content el string que queremos como resultado
    content = str[0] + ";" + str[1] + ";" + count;

    //se sobreescribe el contenido de la posicion actual el array
    words[i] = content;

    //se reinicia el contador
    count = 0;

}

//se elimina el file existente
File.Delete(@"a5_matricula.txt".ToString());

//se crea nuevo file con mismo nombre
File.WriteAllLines(@"a5_matricula.txt".ToString(), words);

//se detiene el cronometro del programa
chronProgram.Stop();

//se agrega al log file lo que se mostrará en consola y se cierra el identificador de ejecución
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
    logfile.WriteLine("--------------------------------------------------");
}

//se muestra el tiempo de ejecucíon del programa
Console.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
