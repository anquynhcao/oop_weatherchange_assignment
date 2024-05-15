using System;
namespace assignment_2_3_vp01o7
{
    internal class Atmosphere
    {
        static Atmosphere atm = null;
        public List<Gas> list;
        

        public int countOzone()
        {
            int counter = 0;
            for(int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Perished() && list[i].type == 'Z')
                {
                    counter++;
                }
            }
            return counter;
        }
        public int countOxygen()
        {
            int counter = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Perished() && list[i].type == 'X')
                {
                    counter++;
                }
            }
            return counter;
        }
        public int countCO2()
        {
            int counter = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Perished() && list[i].type == 'C')
                {
                    counter++;
                }
            }
            return counter;
        }

        public Atmosphere()
        {
            list = new List<Gas>();
        }

        public static Atmosphere Instance()
        {
            if (atm == null)
            {
                atm = new Atmosphere();
            }
            return atm;
        }
        public void reactToEvent(IWeather w)
        {
            // added gas indexes will be higher than Count() so wont react
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Perished())
                {
                    list[i].React(w, i);
                }
            }
        }
        public void adjustment(Gas g, int index)
        {
            for (int i = index+1; i < list.Count(); i ++)
            {
                if (!list[i].Perished() && list[i].type == g.type)
                {
                    list[i].AddThickness(g);
                    return;
                }
            }
            addLayer(g);
        }

        public void addLayer(Gas g)
        {
            list.Add(g);
        }
        public void PrintRound()
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Perished())
                {
                    Console.WriteLine($"{list[i].type} {list[i].thickness:F3}");
                }
            }
        }

    }
}

