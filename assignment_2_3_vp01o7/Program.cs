using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using TextFile;

namespace assignment_2_3_vp01o7;

class Program
{
    static void Main(string[] args)
    {
        TextFileReader reader = null;
        bool fileNotFound = false;


        do
        {
            try
            {
                Console.WriteLine("Enter file name: ");
                string filename = Console.ReadLine();
                reader = new TextFileReader(filename);
                fileNotFound = false;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Please enter a valid file name.");
                fileNotFound = true;
            }
        } while (fileNotFound);

        reader.ReadLine(out string line); int n = int.Parse(line);
        //Atmosphere atm = Atmosphere.Instance();
        List<Gas> gasLayers = new();

        for (int i = 0; i < n; i++)
        {
            string[] layerData = reader.ReadLine().Split();
            char type = char.Parse(layerData[0]);
            double thickness = double.Parse(layerData[1]);
            switch (type)
            {
                case 'Z': gasLayers.Add(new Ozone(thickness)); break;
                case 'X': gasLayers.Add(new Oxygen(thickness)); break;
                case 'C': gasLayers.Add(new CO2(thickness)); break;

            }
        }
        List<IWeather> weathers = new List<IWeather>();
        string events = reader.ReadLine();
        for (int i = 0; i < events.Length; i++)
        {
            switch (events[i])
            {
                case 'T': weathers.Add(Thunderstorm.Instance()); break;
                case 'S': weathers.Add(Sunshine.Instance()); break;
                case 'O': weathers.Add(Other.Instance()); break;
            }
        }

        int eventIndex = 0;
        int round = 1;
        //int ozoneNumber = gasLayers.Count(item => item.type == 'Z');
        //int oxygenNumber = gasLayers.Count(item => item.type == 'X');
        //int co2Number = gasLayers.Count(item => item.type == 'C');

        do
        {
            try
            {
                int ozoneNumber = gasLayers.Count(item => item.type == 'Z');
                int oxygenNumber = gasLayers.Count(item => item.type == 'X');
                int co2Number = gasLayers.Count(item => item.type == 'C');

                //atm.reactToEvent(weathers[eventIndex]);
                // added gas indexes will be higher than Count() so wont react

                for (int i = 0; i < gasLayers.Count(); i++)
                {
                    if (!gasLayers[i].Perished())
                    {
                        gasLayers[i].React(weathers[eventIndex], i, ref gasLayers);
                    }
                }

                Console.WriteLine($"Round {round}: After weather variable {events[eventIndex]}");
                eventIndex = (eventIndex + 1) % weathers.Count();
                 

                //atm.PrintRound();
                for (int i = 0; i < gasLayers.Count(); i++)
                {
                    if (!gasLayers[i].Perished())
                    {
                        Console.WriteLine($"{gasLayers[i].type} {gasLayers[i].thickness:F3}");
                    }
                }

                Console.WriteLine();
                round = round + 1;



                if (ozoneNumber != 0 && gasLayers.Count(item => item.type == 'Z' && !item.Perished()) == 0 ||
                oxygenNumber != 0 && gasLayers.Count(item => item.type == 'X' && !item.Perished()) == 0 ||
                co2Number != 0 && gasLayers.Count(item => item.type == 'C' && !item.Perished()) == 0)
                {
                    Console.WriteLine("A component has perished");
                    break;
                }


            }
            catch(Exception e)
            {
                Console.WriteLine("{0}", e.ToString());
            }
            //if (ozoneNumber !=0 && gasLayers.Count(item => item.type == 'Z' && !item.Perished()) == 0 ||
            //    oxygenNumber != 0 && gasLayers.Count(item => item.type == 'X' && !item.Perished()) == 0||
            //    co2Number != 0 && gasLayers.Count(item => item.type == 'C' && !item.Perished()) == 0)
            //{
            //    Console.WriteLine("A component has perished");
            //    break;
            //}

            
        } while (true);
        
    }
}

