using System;
using System.Collections;
using System.Collections.Generic;

namespace assignment_2_3_vp01o7
{
    abstract class Gas
    {
        //public Atmosphere atmosphere;
        public char type;
        public double thickness;
        


        public Gas(double thickness)
        {
            //this.atmosphere = atm;
            this.thickness = thickness;
        }

        public void UpdateThickness(double ratio)
        {
            this.thickness = ratio * thickness;
        }

        public void AddThickness(Gas g)
        {
            this.thickness += g.thickness;
        }

        public bool Perished() { return thickness < 0.5; }

        public void React(IWeather weather, int index, ref List<Gas> layers)
        {
            Gas g = this.Traverse(weather);
            if (g != null)
            {
                //Atmosphere atm = Atmosphere.Instance();
                //atm.adjustment(g, index);
                for (int i = index + 1; i < layers.Count(); i++)
                {
                    if (!layers[i].Perished() && layers[i].type == g.type)
                    {
                        layers[i].AddThickness(g);
                        return;
                    }
                }
                layers.Add(g);
            }
        }

        public abstract Gas Traverse(IWeather weather);


    }   

    class Ozone : Gas
    {
        public Ozone( double thickness) : base( thickness)
        {
            type = 'Z';
        }

        public override Gas Traverse(IWeather weather)
        {
            return weather.ChangeOzone(this);
        }
    }



    class Oxygen : Gas
    {
        public Oxygen(double thickness) : base(thickness)
        {
            this.type = 'X';
        }
        public override Gas Traverse(IWeather weather)
        {
            return weather.ChangeOxygen(this);
        }
    }



    class CO2 : Gas
    {
        public CO2(double thickness) : base(thickness)
        {
            this.type = 'C';
        }
        public override Gas Traverse(IWeather weather)
        {
            return weather.ChangeCO2(this);
        }
    }
}


