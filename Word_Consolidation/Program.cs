using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

//se crea variable para la suma total de los tiempos que tarda en abrir cada file
long allFileWords = 0;

//se declara cronometro para el tiempo en acomodar todas las palabras consolidadas
Stopwatch orderAllWords = new System.Diagnostics.Stopwatch();

//string para el path del file resultante
string resultantFile = $@"a4_matricula.txt";

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

for (int i = 1; i <= 503; i++)
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
        Stopwatch oneFileWords = new System.Diagnostics.Stopwatch();

        //se inicia el cronometro
        oneFileWords.Start();

        //en cada vuelta se guardan todas las palabras de un file en el array words
        string[] words = File.ReadAllLines(path.ToString()).ToArray();

        //se agregan las palabras al archivo consolidado
        for (int word = 0; word < words.Length; word++)
        {
            //se convierte la palabra a lower case
            words[word] = words[word].ToLower();

            //se agrega la palabra al file de consolidacion
            using (StreamWriter consolidationFile = File.AppendText(resultantFile))
            {
                consolidationFile.WriteLine(words[word]);
            }
        }
        //se detiene el cronometro
        oneFileWords.Stop();

        //se agrega al log file lo que se mostrará en consola
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine(path + " se agregaron en " + oneFileWords.ElapsedMilliseconds + " ms");
        }

        //se imprime el tiempo que tardó en abrirse x file
        Console.WriteLine(path + " se agregaron en " + oneFileWords.ElapsedMilliseconds + " ms");

        //se suma el tiempo que tardó en el file en la variable del tiempo total de todos los files
        allFileWords += oneFileWords.ElapsedMilliseconds;

        //se inicia el cronometro
        orderAllWords.Start();

        //se guardan todas las palabras consolidadas en el array 
        string[] allWords = File.ReadAllLines(resultantFile.ToString()).ToArray();

        //se realiza la comparación para acomodar alfabéticamente
        Comparison<string> comparador = new Comparison<string>((cadena1, cadena2) => cadena1.CompareTo(cadena2));
        Array.Sort<string>(allWords, comparador);

        //se elimina el file existente
        File.Delete(resultantFile.ToString());

        //se crea nuevo file con mismo nombre
        File.WriteAllLines(resultantFile.ToString(), allWords);

        //se detiene el cronometro
        orderAllWords.Stop();

    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}

//se suma el tiempo en agregar todas las palabras al file consolidado + el tiempo en acomodar todas las palabras del archivo consolidado
allFileWords = allFileWords + orderAllWords.ElapsedMilliseconds;

//se agrega al log file lo que se mostrará en consola
using (StreamWriter logfile = File.AppendText(@"log.txt"))
{
    logfile.WriteLine("Todos las palabras de todos los archivos fueron agregadas y acomodadas en " + allFileWords + " ms");
}

//se muestra el tiempo que se tardó en abrir todos los files
Console.WriteLine("Todos las palabras de todos los archivos fueron agregadas y acomodadas en " + allFileWords + " ms");

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
