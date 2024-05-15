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
        Atmosphere atm = Atmosphere.Instance();
        for(int i = 0; i < n; i++)
        {
            string[] layerData = reader.ReadLine().Split();
            char type = char.Parse(layerData[0]);
            double thickness = double.Parse(layerData[1]);
            switch (type)
            {
                case 'Z': atm.addLayer(new Ozone(thickness)); break;
                case 'X': atm.addLayer(new Oxygen(thickness)); break;
                case 'C': atm.addLayer(new CO2(thickness)); break;

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
        int ozoneNumber = atm.countOzone();
        int oxygenNumber = atm.countOxygen();
        int co2Number = atm.countCO2();
        do
        {
            try
            {
                atm.reactToEvent(weathers[eventIndex]);
                Console.WriteLine($"Round {round}: After weather variable {events[eventIndex]}");
                eventIndex = (eventIndex + 1) % weathers.Count();
                atm.PrintRound();
                Console.WriteLine();
                round = round + 1;
            }
            catch(Exception e)
            {
                Console.WriteLine("{0}", e.ToString());
            }
            if (ozoneNumber !=0 && atm.countOzone() == 0 ||
                oxygenNumber != 0 && atm.countOxygen() == 0||
                co2Number != 0 && atm.countCO2() == 0)
            {
                Console.WriteLine("A component has perished");
                break;
            }

            
        } while (true);
        
    }
}

