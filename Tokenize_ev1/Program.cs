using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //se le imprime al usuario mensaje para solicitarle la palabra
        Console.WriteLine("Ingresa el argumento: ");
        //se lee la palabra ingresada
        string word = Console.ReadLine();
        //si no se ingreso ningun valor se imprime mensaje
        if (word == "")
        {
            Console.WriteLine("Debes ingresar un valor");
        }
        else
        {
            //se llama a la función
            Tokenize(word);
        }
        
    }

    public static void Tokenize(string word)
    {

        String path = "";

        int repsInFile = 0;

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
                String[] str = words[j].Split(' ');
                String wordToCompare = str[0];

                if (word.Equals(wordToCompare))
                {
                    int totalWordReps = Int32.Parse(str[1]);

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
                        Console.WriteLine(path + ";" + "0");
                    } else
                    {
                        float weightToken = (totalWordReps * 100) / repsInFile;
                        Console.WriteLine(path + ";" + weightToken);
                    }
                    repsInFile = 0;
                }
            }

        }
    }
}
