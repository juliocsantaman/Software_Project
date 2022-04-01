using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//string para el path del file
string path = "";

// Contador que indica el número de
// archivos donde se encuentra la palabra.
int fileCount = 0;

// Contador para la columna de Posting.
int postingCount = 0;

// Lista de posting,
// ruta del archivo y número de repetición
// de la palabra en ese archivo.
List<string> postingList = new List<string>();

// Guarda último índice de una palabra
// encontrada en un archivo.
int lastIndex = -1;

// Un test que hice para ver si daba bien la 
// suma del total de repeticiones de una palabra
// y el total de número de archivos.
// se puede borrar, pero para probarlo.
// int testSum = 0, testSum2 = 0;

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"a7_matricula.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

//se agregan todas las palabras del consolidado en un array de strings
string[] words = File.ReadAllLines(@"a5_matricula.txt".ToString()).ToArray();

for (int i = 0; i < 100; i++)
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
        string[] fileWords = File.ReadAllLines(path.ToString()).ToArray();


        // Guarda último índice de una palabra
        // encontrada en un archivo.
        lastIndex = Array.LastIndexOf(fileWords, wordToCompare);

        // Si se encuentra retorna
        // el índice.
        if (lastIndex > -1)
        {
            // Contador que indica el número de
            // archivos donde se encuentra la palabra.
            fileCount++;
        }

        //se detiene el cronometro
        chronFile.Stop();

        //se agrega al log file
        using (StreamWriter logfile = File.AppendText(@"a7_matricula.txt"))
        {
            logfile.WriteLine("Se revisó file " + path + " en " + chronFile.ElapsedMilliseconds + "ms");
        }

        //se imprime en consola
        Console.WriteLine("Se revisó file " + path + " en " + chronFile.ElapsedMilliseconds + "ms");

        // Aumento en uno porque el índice 
        // de un valor encontrado siempre empieza en 0.
        lastIndex += 1;

        // Si es igual a cero, entonces no
        // lo ponemos en el archivo posting.
        /*if (lastIndex > 0)
        {
            // Se agrega el path donde se encuentra el token
            // y la cantidad de repeticiones.
            postingList.Add(path + ";" + lastIndex + ";");
            // Test.
            // testSum += lastIndex;
            // testSum2 += 1;
        }*/
        postingList.Add(path + ";" + lastIndex + ";");
    }

    // Si es igual a cero,
    // entonces es la primera posición.
    if (postingCount == 0)
    {
        content = str[0] + ";" + fileCount + ";" + postingCount;
        postingCount = fileCount;
    }
    else
    {
        // Si no es igual a cero, 
        // entonces ya empezamos a hacer la suma
        // de count + postingCount para obtener la columna de 
        // postingCount.
        postingCount = fileCount + postingCount;
        content = str[0] + ";" + fileCount + ";" + postingCount;
    }

    //se sobreescribe el contenido de la posicion actual el array
    words[i] = content;

    //se reinicia el contador
    fileCount = 0;

}

//se elimina el file existente
File.Delete(@"dictionary.txt".ToString());

//se crea nuevo file con mismo nombre
File.WriteAllLines(@"dictionary.txt".ToString(), words);

//se elimina el posting existente
File.Delete(@"posting.txt".ToString());

//se crea nuevo posting con mismo nombre
File.WriteAllLines(@"posting.txt".ToString(), postingList);

//se detiene el cronometro del programa
chronProgram.Stop();

//se agrega al log file lo que se mostrará en consola y se cierra el identificador de ejecución
using (StreamWriter logfile = File.AppendText(@"a7_matricula.txt"))
{
    logfile.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
    logfile.WriteLine("--------------------------------------------------");
}

//se muestra el tiempo de ejecucíon del programa
Console.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");

// System.Console.WriteLine("TEST - Suma total de repetición de archivos: " + testSum);
// System.Console.WriteLine("TEST - Suma total de número de documentos: " + testSum2);