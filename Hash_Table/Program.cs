using System.Diagnostics;
using System.Text;
using System.Collections;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //se declara un cronometro para la ejecucion del programa
            Stopwatch chronProgram = new Stopwatch();

            //se inicia el cronometro
            chronProgram.Start();

            //se agrega un detalle decorativo al log file para separar una ejecucion de otra
            using (StreamWriter logfile = File.AppendText(@"a8_matricula.txt"))
            {
                logfile.WriteLine("--------------------------------------------------");
            }

            // Arreglo que contienen los valores del diccionario sin la estructura HashTable.
            string[] dictionaryElements = File.ReadAllLines(@"dictionary.txt".ToString()).ToArray();
            // Este contador es para guardar los valores ASCII.
            int count = 0;

            // Arreglo de valores ASCII.
            string[] wordsAscii = new string[dictionaryElements.Length];

            // Obteniendo los valores ASCII de las palabras.
            // se suman los valores ASCII de cada letra de la
            // palabra para hacer el hash y conseguir 
            // la posición más adecuada.
            for (int i = 0; i < wordsAscii.Length; i++)
            {
                // Se separa el contenido actual y se guarda en un array de strings.
                string[] arrContent = dictionaryElements[i].Split(';');
                // Este es el token.
                string str = arrContent[0];

                // Obtener el valor en ascii de cada palabra en forma de arreglo
                // de mi arreglo dictionaryElements.
                byte[] ASCIIvalues = Encoding.ASCII.GetBytes(str);
                foreach (var value in ASCIIvalues)
                {
                    // Se va sumando cada valor ASCII de la letra.
                    count += value;
                }

                // Concatenamos la suma total ASCII con nuestro valor de diccionario.
                wordsAscii[i] = count.ToString() + "-" + dictionaryElements[i];

                // Reinicio del contador.
                count = 0;
            }

            // HashTable.
            int hashTableSize = 10000;
            MyHashTable hash = new MyHashTable(hashTableSize);
            hash.functionHash(wordsAscii, hash.arr);
            System.Console.WriteLine("Número de colisiones: " + hash.collisions);

            //se elimina el file existente
            File.Delete(@"hashtable.txt".ToString());

            //se crea nuevo file con mismo nombre
            File.WriteAllLines(@"hashtable.txt".ToString(), hash.arr);

            //se detiene el cronometro del programa
            chronProgram.Stop();

            //se agrega al log file lo que se mostrará en consola y se cierra el identificador de ejecución
            using (StreamWriter logfile = File.AppendText(@"a8_matricula.txt"))
            {
                logfile.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
                logfile.WriteLine("--------------------------------------------------");
            }

            System.Console.WriteLine();
            //se muestra el tiempo de ejecucíon del programa
            Console.WriteLine("El programa se ejecutó en " + chronProgram.ElapsedMilliseconds + " ms");
        }
    }

    // Clase HashTable.
    class MyHashTable
    {
        // Atributos.
        public string[] arr;
        public int collisions = 0;
        int size;

        // Método constructor.
        public MyHashTable(int size)
        {
            this.size = size;
            arr = new string[this.size];
            Array.Fill(arr, "-1");
        }

        // Método para obtener el hash,
        // la posición adecuada en el arreglo.
        public void functionHash(string[] strArr, string[] arr)
        {
            for (int i = 0; i < strArr.Length; i++)
            {
                // Se separa el contenido actual y se guarda en un array de strings.
                string[] elements = strArr[i].Split('-');
                // Este es el valor ASCII total del token (es un número para hacer operaciones de hash).
                string element = elements[0];
                int arrIndex = (Int32.Parse(element)) % (this.size - 1);
                //System.Console.WriteLine("El índice es: " + arrIndex + " para el elemento " + elements[1]);

                // Tratando las colisiones.
                while (arr[arrIndex] != "-1")
                {
                    collisions++;
                    arrIndex++;
                    //System.Console.WriteLine("Ocurrió una colisión en el índice " + (arrIndex - 1) + " cambiar al índice " + arrIndex);
                    arrIndex %= this.size;
                }

                arr[arrIndex] = elements[1];
            }
        }

    }

}
