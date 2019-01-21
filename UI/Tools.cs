using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

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

        #region Validation test
        public static bool IsValidEmail(string Email)
        {
            try
            {
                var addr = new MailAddress(Email);
                return Email == addr.Address;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool IsValidAddress(string Address)
        {
            // פורמט כתובת תקין הוא כזה שבו מופיע שם הרחוב (אותיות ומספרים בלבד) ולאחריו פסיק
            //אחרי הפסיק יופיע מספר הבית (ספרות בלבד) ולאחריו פסיק נוסף
            //אחרי הפסיק השני יופיע שם העיר (אותיות בלבד
            int count = 0;

            foreach (var s in Address)
                if (s == ',')
                    count++;

            if (count != 2)                   //count < 2 ???
                return false;

            int i = 0;

            while (true)
            {
                if (Address[i] == ',')
                    break;
                if (!char.IsLetterOrDigit(Address[i]) && Address[i] != ' ')
                    return false;
                i++;
            }
            if (i == 0) return false;        //מינימום רחוב באורך אות אחת

            while (true)
            {
                if (Address[i] == ',')
                    break;

                if (!char.IsNumber(Address[i]) && Address[i] != ' ')
                    return false;
                i++;
            }
            if (i == Address.Length)       //מינימום עיר באורך אות אחת
                return false;
            return true;
        }

        public static bool IsValidPhoneNumber(string Number)
        {
            if (Number.Count() < 10)
                return false;

            if (Number[0] != '0' || Number[1] != '5')
                return false;

            return true;
        }
        #endregion

    }
}
