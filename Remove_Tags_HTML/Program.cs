using System.Diagnostics;
using System.Text.RegularExpressions;

//se declara un cronometro para la ejecucion del programa
Stopwatch chronProgram = new Stopwatch();

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
for (int i = 1; i <= 503; i++)
{
    //si la vuelta es menor o igual a 10, el nombre del file tendrá dos ceros en el path
    if (i <= 9)
    {
        path = $@"files\00{i}.html";
    }
    //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
    if (i >= 10 && i <= 99)
    {
        path = $@"files\0{i}.html";
    }
    //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
    if (i >= 100 && i <= 503)
    {
        path = $@"files\{i}.html";
    }

    try
    {
        //se declara cronometro para el tiempo en abrir el file
        Stopwatch chronRemoveTags = new Stopwatch();

        //se inicia el cronometro
        chronRemoveTags.Start();

        //string que contiene el nuevo nombre del file a copiar
        string newName = $@"{path}-copy.html";

        //se realiza una copia del file original
        File.Copy(path, newName, true);

        //se guarda en el string source todo el texto que contiene el html original
        string source = File.ReadAllText(path);

        //se guarda en el nuevo file el texto del html original sin las etiquetas
        File.WriteAllText(newName, Regex.Replace(source, "<.*?>", string.Empty));

        //se limpian los saltos de línea
        String[] text = File.ReadAllLines(newName.ToString()).Where(s => s.Trim() != string.Empty).ToArray();

        //se eliminan los espacios
        for (int word = 0; word < text.Length; word++)
        {
            text[word] = text[word].Trim();
        }
        
        //se elimina el file existente
        File.Delete(newName.ToString());

        //se crea nuevo file con mismo nombre
        File.WriteAllLines(newName.ToString(), text);

        //se detiene el cronometro
        chronRemoveTags.Stop();

        //se agrega al log file lo que se mostrará en consola
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine(path + " se modificó en " + chronRemoveTags.ElapsedMilliseconds + " ms");
        }

        //se imprime el tiempo que tardó en abrirse x file
        Console.WriteLine(path + " se modificó en " + chronRemoveTags.ElapsedMilliseconds + " ms");

        //se suma el tiempo que tardó en el file en la variable del tiempo total de todos los files
        totalTimeFiles += chronRemoveTags.ElapsedMilliseconds;
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
