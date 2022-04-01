using System.Diagnostics;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//se crea una lista donde se guardarán las palabras que no se van a eliminar y sus reps
List<string> acceptedWords = new List<string>();

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"a9_matricula.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

//se agregan todas las palabras y sus repeticiones al array words
string[] words = File.ReadAllLines(@"a5_matricula.txt".ToString()).ToArray();

//se agregan todas las palabras del stoplist al array deleteWords
string[] deleteWords = File.ReadAllLines(@"stoplist.txt".ToString()).ToArray();

for (int i = 0; i < words.Length; i++)
{
    //se declara un cronometro para el tiempo en el que el programa verifica si cada palabra es aceptada o si se elimina
    Stopwatch chronWords = new Stopwatch();

    //se inicia el cronometro
    chronWords.Start();

    //se separa el contenido actual y se guarda en un array de strings
    String[] str = words[i].Split(' ');

    //se asigna la primer palabra en el string
    String wordToCompare = str[0];

    //se asigna las repeticiones de la primer palabra en el string
    String wordReps = str[1];

    if (!deleteWords.Contains(wordToCompare) & Int32.Parse(wordReps) > 50 & wordToCompare.Length != 1)
    {
        acceptedWords.Add(wordToCompare + " " + wordReps);
    }

    //se detiene el cronometro de la verificación de la palabra
    chronWords.Stop();

    //se agrega al log file
    using (StreamWriter logfile = File.AppendText(@"a9_matricula.txt"))
    {
        logfile.WriteLine("Se revisó palabra " + words[i] + " en " + chronWords.ElapsedMilliseconds + "ms");
    }

    //se imprime en consola
    Console.WriteLine("Se revisó palabra " + words[i] + " en " + chronWords.ElapsedMilliseconds + "ms");
}

acceptedWords.ToArray();

//se elimina el file existente que aun contiene palabras del stoplist
File.Delete(@"a5_matricula.txt".ToString());

//se crea nuevo file con mismo nombre sin las palabras que deseamos eliminar
File.WriteAllLines(@"a5_matricula.txt".ToString(), acceptedWords);

//se detiene el cronometro
chronProgram.Stop();

//se agrega al log file lo que se mostrará en consola y se cierra el identificador de ejecución
using (StreamWriter logfile = File.AppendText(@"a9_matricula.txt"))
{
    logfile.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
    logfile.WriteLine("--------------------------------------------------");
}

//se muestra el tiempo de ejecucíon del programa
Console.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");

