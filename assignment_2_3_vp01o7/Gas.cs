using System;
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

        public abstract void React(IWeather weather, int index);

        
    }   



    class Ozone : Gas
    {
        public Ozone( double thickness) : base( thickness)
        {
            type = 'Z';
        }
        public override void React(IWeather weather, int index)
        {
            Gas g = weather.ChangeOzone(this);
            if (g != null)
            {
                Atmosphere atm = Atmosphere.Instance();
                atm.adjustment(g,index);
            }
        }
    }



    class Oxygen : Gas
    {
        public Oxygen(double thickness) : base(thickness)
        {
            this.type = 'X';
        }
        public override void React(IWeather weather, int index)
        {
            Gas g = weather.ChangeOxygen(this);
            if (g != null)
            {
                Atmosphere atm = Atmosphere.Instance();
                atm.adjustment(g, index);
            }
        }
    }



    class CO2 : Gas
    {
        public CO2(double thickness) : base(thickness)
        {
            this.type = 'C';
        }
        public override void React(IWeather weather, int index)
        {
            Gas g = weather.ChangeCO2(this);
            if (g != null)
            {
                Atmosphere atm = Atmosphere.Instance();
                atm.adjustment(g, index);
            }
        }
    }
}


