using System;
using System.Configuration;
using System.IO;

namespace TransformationFichier
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Traitement en cours...");

            var skipX = false;
            var skipY = false;
            string lastY = null;
            var isFirstLine = true;

            var delimiter = Convert.ToChar(ConfigurationManager.AppSettings.Get("Delimiter"));
            var positionY = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PositionY"));

            //Get all the records from the input file
            using (var reader = new StreamReader(ConfigurationManager.AppSettings.Get("InputFileName")))
            using (var writer = new StreamWriter(ConfigurationManager.AppSettings.Get("OutputFileName")))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();
                    var y = line.Split(delimiter)[positionY];

                    if (isFirstLine)
                    {
                        lastY = y;
                        isFirstLine = false;
                    }

                    if (y == lastY && !skipY)
                    {
                        if (!skipX)
                        {
                            writer.WriteLine(line);
                            skipX = true;
                        }
                        else
                        {
                            skipX = false;
                        }
                    }
                    else
                    {
                        if (!skipY)
                        {
                            skipY = true;
                        }
                        else if (y == lastY && skipY)
                        {
                            // do nothing
                        }
                        else
                        {
                            writer.WriteLine(line);

                            skipY = false;
                            skipX = true;
                        }
                    }

                    lastY = y;
                }
            }

            System.Console.WriteLine("Entrer une touche pour fermer.");
            System.Console.ReadLine();
        }

        public class Line
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }
    }
}
