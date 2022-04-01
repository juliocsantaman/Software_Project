String path = "";

int repsInFile = 0;

List<string> postingList = new List<string>();

string[] words = File.ReadAllLines(@"a5_matricula.txt".ToString()).ToArray();

for (int i = 1; i <= 503; i++)
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

    for (int j = 0; j < words.Length; j++)
    {
        //se separa el contenido actual y se guarda en un array de strings
        String[] str = words[j].Split(' ');

        //
        int totalWordReps = Int32.Parse(str[1]);

        String wordToCompare = str[0];

        string[] comparedWords = File.ReadAllLines(path.ToString()).ToArray();

        for (int k = 0; k < comparedWords.Length; k++)
        {
            if (wordToCompare == comparedWords[k])
            {
                repsInFile++;
            }
        }

        if (repsInFile == 0)
        {
            //postingList.Add(path + ";" + "0");
            //Console.WriteLine(path + ";" + "0");
        } else
        {
            float weightToken = (totalWordReps * 100) / repsInFile;

            postingList.Add(path + ";" + totalWordReps);

            Console.WriteLine(path + ";" + totalWordReps + "*100/" + repsInFile + "=" + weightToken);
        }

        repsInFile = 0;
    }

}
