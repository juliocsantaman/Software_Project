using System.Diagnostics;


//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new System.Diagnostics.Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

//se crea variable para la suma total de los tiempos que tarda en abrir cada file
long totalTimeFiles = 0;

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

//loop que corre 503 veces
for (int i = 1; i <= 1; i++)
{
    //si la vuelta es menor o igual a 10, el nombre del file tendrá dos ceros en el path
    if (i <= 9)
    {
        path = $@"files\00{i}.html-copy.html";
    }
    //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
    if (i >= 10 && i <= 99)
    {
        path = $@"files\0{i}.html-copy.html";
    }
    //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
    if (i >= 100 && i <= 503)
    {
        path = $@"files\{i}.html-copy.html";
    }

    try
    {
        //se declara cronometro para el tiempo en separar palabras y ordenarlas
        Stopwatch chronSeparateSort = new System.Diagnostics.Stopwatch();

        //se inicia el cronometro
        chronSeparateSort.Start();

        //string que contiene el nuevo nombre del file a generar
        string newName = $@"{path}-ordered.txt";

        //se guarda en el string source todo el texto que contiene el html sin etiquetas
        string source = File.ReadAllText(path);

        //se guarda en el array las palabras separadas por espacio y caracteres especiales no omitiendo numeros
        string[] valores = source.Split('@', '/', '-', '.', ':', ';', ')', '(', '#', '$', '%', '_', ' ', ',', '|', '&', '[', ']', '*', '\n');

        //se realiza la comparación para acomodar alfabéticamente
        Comparison<string> comparador = new Comparison<string>((cadena1, cadena2) => cadena1.CompareTo(cadena2));
        Array.Sort<string>(valores, comparador);

        //loop que recorre el array
        foreach (string value in valores)
        {
            //se agrega al nuevo file la palabra
            using (StreamWriter file = File.AppendText($@"{newName}"))
            {
                file.WriteLine(value);
            }
        }

        //se detiene el cronometro
        chronSeparateSort.Stop();

        //se agrega al log file lo que se mostrará en consola
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine(path + " se modificó en " + chronSeparateSort.ElapsedMilliseconds + " ms");
        }

        //se imprime el tiempo que tardó en abrirse x file
        Console.WriteLine(path + " se modificó en " + chronSeparateSort.ElapsedMilliseconds + " ms");

        //se suma el tiempo que tardó en el file en la variable del tiempo total de todos los files
        totalTimeFiles += chronSeparateSort.ElapsedMilliseconds;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}

//se agrega al log file lo que se mostrará en consola
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("Todos los archivos se modificaron en " + totalTimeFiles + " ms");
}

//se muestra el tiempo que se tardó en abrir todos los files
Console.WriteLine("Todos los archivos se modificaron en " + totalTimeFiles + " ms");

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
