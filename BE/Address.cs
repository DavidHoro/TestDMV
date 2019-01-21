using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address:ICloneable
    {

        private string street;
        private string building_number;
        private string city;

        public Address(string street, string building_number, string city)
        {
            this.street = string.Copy(street);
            this.building_number = string.Copy( building_number);
            this.city = string.Copy(city);
        }

        public object Clone()
        {
            return new Address(street, building_number, city);
        }

        public override string ToString()
        {
            return street + ", " + building_number + ", " + city +  "\n";
        } 

        public int Distance(Address A)
        {
            Random r = new Random();
            return r.Next(2, 10);
        }

    }

    public static class Extesions
    {
        public static Address ToAddress(this string str)
        {
            int i = 0;
            string street, num, city;

            while (str[i] != ',')
                i++;
            street = str.Substring(0, i);

            do { i++; }
            while (str[i] != ',');

            num = str.Substring(street.Length + 1, i - street.Length - 1);

            city = str.Substring(i + 1);

            return new Address(street, num, city);
        }
    }
}
