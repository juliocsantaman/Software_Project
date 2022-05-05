using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryController : Controller
    {
        [HttpGet("/")]
        public string Get()
        {
            return "Software Project - Hello, World!";
        }

        [HttpGet("{word}")]
        public MyDictionary Get(string word)
        {
            MyDictionary myDictionary = new MyDictionary(word);
            bool exist = myDictionary.optSearch(myDictionary.Word);
            myDictionary.Exist = exist;
            return myDictionary;
            
        }
    }

    public class MyDictionary
    {
        public string Word { get; set; }    
        public bool Exist { get; set; }
        public List<string> UrlList { get; set; }

        public List<string> top10 = new List<string>();

        public MyDictionary(string word)
        {
            this.Word = word;
        }

        public bool optSearch(string word)
        {
            String path = "";

            word = word.ToLower().Trim();

            int repsInFile = 0;

            List<string> fileRepsList = new List<string>();
            
            for (int i = 1; i <= 503; i++)
            {
                //se declara cronometro para el file
                Stopwatch chronFile = new Stopwatch();

                //se inicia cronometro
                chronFile.Start();

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
                        break;
                    }
                }

                if (repsInFile > 0)
                {
                    if(top10.Count < 10)
                    {
                        chronFile.Stop();
                        top10.Add(path + "?timeMilliSeconds=" + chronFile.ElapsedMilliseconds);
                        repsInFile = 0;

                    }

                }


            }

         

            if (top10.Count >= 1)
            {
                UrlList = top10;
                return true;
            }

            return false;

        }

    }
}
