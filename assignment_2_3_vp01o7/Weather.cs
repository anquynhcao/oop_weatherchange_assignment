using System;
using TextFile;
namespace assignment_2_3_vp01o7
{
	interface IWeather
	{
		Gas ChangeOzone(Ozone z);
		Gas ChangeOxygen(Oxygen x);
		Gas ChangeCO2(CO2 c);
	}

	class Thunderstorm : IWeather
	{
		public Gas ChangeOzone(Ozone z)
		{
			return null;
		}
		public Gas ChangeOxygen(Oxygen x)
		{
			double originThickness = x.thickness;
			x.UpdateThickness(0.5);
			return new Ozone(originThickness * 0.5);
		}
        public Gas ChangeCO2(CO2 c)
        {
            return null;
        }

		private Thunderstorm() { }
        private static Thunderstorm instance = null;
        public static Thunderstorm Instance()
        {
            if (instance == null)
            {
                instance = new Thunderstorm();
            }
            return instance;
        }
    }

    class Sunshine : IWeather
    {
        public Gas ChangeOzone(Ozone z)
        {
            return null;
        }
        public Gas ChangeOxygen(Oxygen x)
        {
            double originThickness = x.thickness;
            x.UpdateThickness(0.95);
            return new Ozone(originThickness * 0.05);
        }
        public Gas ChangeCO2(CO2 c)
        {
            double originThickness = c.thickness;
            c.UpdateThickness(0.95);
            return new Oxygen(originThickness * 0.05);
        }

        private Sunshine() { }
        private static Sunshine instance = null;
        public static Sunshine Instance()
        {
            if (instance == null)
            {
                instance = new Sunshine();
            }
            return instance;
        }
    }

    class Other : IWeather
    {
        public Gas ChangeOzone(Ozone z)
        {
            double originThickness = z.thickness;
            z.UpdateThickness(0.95);
            return new Oxygen(originThickness * 0.05);
        }
        public Gas ChangeOxygen(Oxygen x)
        {
            double originThickness = x.thickness;
            x.UpdateThickness(0.9);
            return new CO2(originThickness * 0.1);
        }
        public Gas ChangeCO2(CO2 c)
        {
            return null;
        }

        private Other() { }
        private static Other instance = null;
        public static Other Instance()
        {
            if (instance == null)
            {
                instance = new Other();
            }
            return instance;
        }
    }
}

