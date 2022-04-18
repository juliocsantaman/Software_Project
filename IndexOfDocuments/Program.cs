using System.Diagnostics;
using System.Text;
using System.Collections;



//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

//se inicia el cronometro
chronProgram.Start();

//variable para apth de cada file
String path = "";

//variable para contar las pereticiones del token en cada file
int repsInFile = 0;

//lista de apoyo
List<string> postingList = new List<string>();

//se guardan todos los tokens y sus apariciones en total en el string
string[] words = File.ReadAllLines(@"a5_matricula.txt".ToString()).ToArray();

// Lista de documentos.
List<string> documentList = new List<string>();

//se agrega un detalle decorativo al log file para separar una ejecucion de otra
using (StreamWriter logfile = File.AppendText(@"a11_matricula.txt"))
{
    logfile.WriteLine("--------------------------------------------------");
}

for (int i = 1; i <= 3; i++)
{
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

    // Crear un id único.
    string id = string.Empty;
    id = Guid.NewGuid().ToString();

    // Unimos el id con el path.
    string idPath = id + "---" + path;
    // Lo agregamos a la lista.
    documentList.Add(idPath);

    for (int j = 0; j < words.Length; j++)
    {
        //se separa el contenido actual y se guarda en un array de strings
        String[] str = words[j].Split(' ');

        //se guarda en la variable la cantidad de files en el que el token aparece
        int totalWordReps = Int32.Parse(str[1]);

        //se guarda en la variable el token a comparar
        String wordToCompare = str[0];

        //se declara un cronometro para el tiempo en el que el programa verifica si cada palabra es aceptada o si se elimina
        Stopwatch checkFile = new Stopwatch();

        //se inicia el cronometro
        checkFile.Start();

        //se guardan todas las palabras de cada file en el array
        string[] comparedWords = File.ReadAllLines(path.ToString()).ToArray();


        for (int k = 0; k < comparedWords.Length; k++)
        {
            //si la palabra a comparar coincide con la del file se aumenta el contador
            if (wordToCompare == comparedWords[k])
            {
                repsInFile++;
            }
        }

        //si no existe este toquen en el file no se puede realizar el calculo 
        if (repsInFile == 0)
        {
            //se agrega a la lista
            //postingList.Add(path + ";" + totalWordReps + "*100/" + repsInFile + "=0");
        }
        else
        {
            //se calcula el peso
            float weightToken = (totalWordReps * 100) / repsInFile;

            //se agrega a la lista
            postingList.Add(idPath + ";" + totalWordReps + "*100/" + repsInFile + "=" + weightToken);

            checkFile.Stop();

            using (StreamWriter logfile = File.AppendText(@"a11_matricula.txt"))
            {
                logfile.WriteLine("Se ha calculado el peso del token " + wordToCompare + " en el file " + path + " en " + checkFile.ElapsedMilliseconds + "ms");
            }
            Console.WriteLine("Se ha calculado el peso del token " + wordToCompare + " en el file " + path + " en " + checkFile.ElapsedMilliseconds + "ms");
        }

        repsInFile = 0;
    }

}

//se convierte el list a array
postingList.ToArray();

//se crea nuevo file con el array 
File.WriteAllLines(@"posting.txt".ToString(), postingList);

// Se convierte en list a array.
documentList.ToArray();

// Se crea el file con el id y el path.
File.WriteAllLines(@"documentFile.txt".ToString(), documentList);

//se detiene cronometro 
chronProgram.Stop();

//se agrega al log file lo que se mostrará en consola y se cierra el identificador de ejecución
using (StreamWriter logfile = File.AppendText(@"a11_matricula.txt"))
{
    logfile.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
    logfile.WriteLine("--------------------------------------------------");
}

//se muestra el tiempo de ejecucíon del programa
Console.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
