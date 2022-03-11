﻿using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

//se crea variable para la suma total de los tiempos que tarda en abrir cada file
long totalTime = 0;

//variable para el contador de repeticiones
int reps = 0;

//se declara cronometro para el tiempo en acomodar todas las palabras consolidadas
Stopwatch orderAllWords = new System.Diagnostics.Stopwatch();

//string para el path del file resultante
string resultantFile = $@"a5_matricula.txt";

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

for (int i = 1; i <= 4; i++)
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

        //el primer for asigna la palabra al string thisWord
        for (int word = 0; word < words.Length; word++)
        {
            String thisWord = words[word];
            //el segundo for compara la palabra thisWord con todas las palabras en el array 
            for (int next = 0; next < words.Length; next++)
            {
                //si la palabra es igual, aumenta el contador
                if (thisWord == words[next])
                {
                    reps += 1;
                }
            }

            //ahora necesito agregar un condicion para poder agregar lo de abajo al nuevo file. Si no agrego una
            //condición, agrega el texto por cada vuelta y necesitamos que lo agregue una sola vez

            using (StreamWriter newFile = File.AppendText($@"{path}-count.txt"))
            {
                newFile.WriteLine(words[word] + ";" + reps);
            }
            reps = 0;
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

