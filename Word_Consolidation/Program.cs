using System.Diagnostics;

//string para el path del file
string path = "";

for (int i = 0; i < 2; i++)
{
    //si la vuelta es menor o igual a 10, el nombre del file tendrá dos ceros en el path
    if (i <= 9)
    {
        path = $@"files\00{i}.html-copy.html-ordered";
    }
    //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
    if (i >= 10 && i <= 99)
    {
        path = $@"files\0{i}.html-copy.html-ordered";
    }
    //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
    if (i >= 100 && i <= 503)
    {
        path = $@"files\{i}.html-copy.html-ordered";
    }

    //en cada vuelta se guardan todas las palabras de un file en el array words
    string[] words = File.ReadAllLines(path.ToString()).ToArray();

    //se agregan las palabras al archivo consolidado
    for (int word = 0; word < words.Length; word++)
    {
        using (StreamWriter consolidationFile = File.AppendText(@"consolidation.txt"))
        {
            consolidationFile.WriteLine(words[word]);
        }
    }

}
