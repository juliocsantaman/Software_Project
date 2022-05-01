using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        String ans;
        do
        {
            Console.WriteLine("Escribe la palabra que deseas buscar: ");
            string word = Console.ReadLine();
            //optSearch(word);
            if (!optSearch(word))
            {
                Console.WriteLine("La palabra no existe en el diccionario");
            }
            Console.WriteLine("Deseas añadir otra? (Y/N): ");
            ans = Console.ReadLine();
        } while (ans == "Y" | ans == "y");
    }

    public static bool optSearch(string word)
    {

        //se declara cronometro para el programa
        Stopwatch chronProgram = new Stopwatch();

        //se inicia cronometro
        chronProgram.Start();

        String path = "";

        word = word.ToLower().Trim();

        int repsInFile = 0;

        List<string> fileRepsList = new List<string>();

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

            string[] fileWords = File.ReadAllLines(path.ToString()).ToArray();

            for (int j = 0; j < fileWords.Length; j++)
            {
                if (fileWords[j].ToLower() == word)
                {
                    repsInFile++;
                }
            }
            fileRepsList.Add(path + ";" + repsInFile);

            repsInFile = 0;

        }

        //st<string> top10 = new List<string>();
        int count = 1;

        for (int i = 0; i < fileRepsList.Count; i++)
        {

            String[] str = fileRepsList[i].Split(';');

            int reps = Int32.Parse(str[1]);
            string file = str[0];

            if (reps >= 1 && count <= 10)
            {
                Console.WriteLine(file+ ";" + reps);
                count++;
            }
        }

        //se detiene el cronometro
        chronProgram.Stop();

        //se agrega el tiempo de ejecucion al log
        using (StreamWriter logfile = File.AppendText(@"log.txt"))
        {
            logfile.WriteLine("Se ejecutó busqueda en " + chronProgram.ElapsedMilliseconds + "ms");
            logfile.WriteLine("---------------------------------------------------------");
        }

        if (count > 1)
        {
            return true;
        } 

        return false;

    } 
}
