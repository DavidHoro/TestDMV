using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Tools
    {
        public static CarType ToCarType(object ob)
        {
            switch (ob.ToString())
            {
                case "Private":
                    return CarType.Private;
                case "Motorcycle":
                    return CarType.Motorcycle;
                case "Truck":
                    return CarType.Truck;
                case "Semi_trailer":
                    return CarType.Semi_trailer;
                default:
                    return default(CarType);
            }
        }

        public static GearType ToGearType(object ob)
        {
            switch (ob.ToString())
            {
                case "Auto":
                    return GearType.Auto;
                case "Manual":
                    return GearType.Manual;
                default:
                    return default(GearType);
            }
        }

        public static int ToInt(object ob)
        {
            switch (ob.ToString())
            {
                case "9:00":
                    return 9;
                case "10:00":
                    return 10;
                case "11:00":
                    return 11;
                case "12:00":
                    return 12;
                case "13:00":
                    return 13;
                case "14:00":
                    return 14;
                default:
                    return 0;
            }
        }
    }
}
