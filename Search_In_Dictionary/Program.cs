using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //se le imprime al usuario mensaje para solicitarle la palabra
        Console.WriteLine("Que palabra deseas buscar?: ");
        //se lee la palabra ingresada
        string word = Console.ReadLine();
        //si no se ingreso ningun valor se imprime mensaje
        if (word == "")
        {
            Console.WriteLine("Debes ingresar un valor");
        }
        //se llama a la función
        FindWord(word);
    }

    public static void FindWord(string word)
    {
        //se declara cronometro para el programa
        Stopwatch chronProgram = new Stopwatch();

        //se inicia cronometro
        chronProgram.Start();

        //se convierte la palabra a lowercase para que coincida con las palabras
        word = word.ToLower();

        //string para el path
        String path = "";

        for (int j = 1; j <= 503; j++)
        {
            if (j <= 9)
            {
                path = $@"files\00{j}.html-copy.html-ordered.txt";
            }
            //si la vuelta es mayor que 10 y menor o igual a 100, el nombre del file tendrá un cero en el path
            if (j >= 10 && j <= 99)
            {
                path = $@"files\0{j}.html-copy.html-ordered.txt";
            }
            //si la vuelta es mayor a 100 y menor o igual a 503, el path ya no tendrá ceros, unicamente el numero de la vuelta
            if (j >= 100 && j <= 503)
            {
                path = $@"files\{j}.html-copy.html-ordered.txt";
            }

            //se crea un array para guardar todo el contenido del file
            string[] words2 = File.ReadAllLines(path.ToString()).ToArray();


            for (int k = 0; k < words2.Length; k++)
            {
                //si la palabra que el usuario busca es igual a la palabra del file en la posicion actual se continua
                if (word == words2[k])
                {
                    //se imprime en pantalla el file que contiene la palabra
                    Console.WriteLine(path);
                    //se guarda en el log file
                    using (StreamWriter logfile = File.AppendText(@"log.txt"))
                    {
                        logfile.WriteLine("Se encontro " + word + " en " + path);
                    }
                    break;

                }
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

    }

}