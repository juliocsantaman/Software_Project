using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

//se crea variable para la suma total de los tiempos que tarda en abrir cada file
long totalTime = 0;

//se declara cronometro para el tiempo en acomodar todas las palabras consolidadas
Stopwatch orderAllWords = new System.Diagnostics.Stopwatch();

//string para el path del file resultante
string resultantFile = $@"a5_matricula.txt";

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

for (int i = 1; i <= 1; i++)
{
    //si la vuelta es menor o igual a 10, el nombre del file tendrá dos ceros en el path
    if (i <= 9)
    {
        path = $@"files\00{i}.html-copy.html-ordered.txt";
    }
    //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
    if (i >= 10 && i <= 99)
    {
        path = $@"files\0{i}.html-copy.html-ordered.txt";
    }
    //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
    if (i >= 100 && i <= 503)
    {
        path = $@"files\{i}.html-copy.html-ordered.txt";
    }

    try
    {
        //se declara cronometro para el tiempo en agregar todas las palabras de un file
        Stopwatch countWords = new System.Diagnostics.Stopwatch();

        //se inicia el cronometro
        countWords.Start();

        //en cada vuelta se guardan todas las palabras de un file en el array words
        string[] words = File.ReadAllLines(path.ToString()).ToArray();

        // LINQ. Se hace el filtro de los grupos, es decir, las palabras repetidas
        // mostrando palabra alfabéticamente y repetición.
        foreach (var repeatedWords in words.GroupBy(word => word).Where(word => word.Count() >= 0))
        {
            using (StreamWriter newFile = File.AppendText($@"{path}-count.txt"))
            {
                // Accedemos a Key = palabra y a Count = veces repetidas.
                newFile.WriteLine("{0} {1}", repeatedWords.Key, repeatedWords.Count());
            }
        }

        // LINQ. Se hace el filtro de los grupos, es decir, las palabras repetidas
        // y de forma descendente con respecto a las frecuencias de las palabras.
        foreach (var repeatedWords in words.GroupBy(word => word).Where(word => word.Count() >= 0).OrderByDescending(word => word.Count()))
        {
            using (StreamWriter newFile = File.AppendText(resultantFile))
            {
                // Accedemos a Key = palabra y a Count = veces repetidas.
                newFile.WriteLine("{0} {1}", repeatedWords.Key, repeatedWords.Count());
            }
        }

        //se detiene el cronometro
        countWords.Stop();

        //se agrega al log file lo que se mostrará en consola
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine(path + " se contaron en " + countWords.ElapsedMilliseconds + " ms");
        }

        //se imprime el tiempo que tardó en abrirse x file
        Console.WriteLine(path + " se contaron en " + countWords.ElapsedMilliseconds + " ms");

        //se suma el tiempo que tardó en el file en la variable del tiempo total de todos los files
        totalTime += countWords.ElapsedMilliseconds;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}
//se agrega al log file lo que se mostrará en consola
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("Todos las palabras de todos los archivos fueron contadas en " + totalTime + " ms");
}

//se muestra el tiempo que se tardó en abrir todos los files
Console.WriteLine("Todos las palabras de todos los archivos fueron contadas en " + totalTime + " ms");

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


