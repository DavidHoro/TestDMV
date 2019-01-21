using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
    public class Tester:ICloneable
    {
        public string ID { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Email { get; set; }
        public DateTime Date_of_birth { get; set; }
        public Gender Gender { get; set; }
        public string Phone_number { get; set; }
        public Address Address { get; set; }
        public int Seniority { get; set; }
        public int Max_tests { get; set; }
        public CarType Car_type { get; set; }
        public GearType Gear_type { get; set; }
        public bool[,] Schedule { get; set; } = new bool[6, 5];
        public int Max_distance { get; set; }
        public List<DateTime> Appointments { get; set; }

        public override string ToString()
        {
            return First_name + " " + Last_name + " " + ID + "\n";
        }


        public Tester(string iD, string last_name, string first_name,string email, DateTime date_of_birth, Gender gender, string phone_number, Address address, int seniority, int max_tests, CarType car_type, GearType gear_type, bool[,] schedule, int max_distance, List<DateTime> appointments)
        {
            ID = iD;
            Last_name = last_name != null ? string.Copy(last_name) : null;
            First_name = first_name != null ? string.Copy(first_name) : null;
            Email = string.Copy(email);
            Date_of_birth = date_of_birth;
            Gender = gender;
            Phone_number = string.Copy(phone_number);
            Address = address.Clone() as Address;
            Seniority = seniority;
            Max_tests = max_tests;
            Car_type = car_type;
            Gear_type = gear_type;
            Copy(Schedule, schedule);
            Max_distance = max_distance;
            Appointments = new List<DateTime>(appointments.Count());
            
            foreach (DateTime item in appointments)
                Appointments.Add(item);
        }
        /// <summary>
        /// פונקצייה זו מעתיקה תוכן של מערך 1 למערך שני
        /// </summary>
        /// <param name="arr1">מערך היעד</param>
        /// <param name="arr2">מערך המקור</param>
        public void Copy(bool[,] arr1, bool[,] arr2)
        {
            for (int i = 0; i < arr1.GetLength(0); i++)
                for (int j = 0; j < arr1.GetLength(1); j++)
                    arr1[i, j] = arr2[i, j];
        }

        public object Clone()
        {
            return new Tester(ID, Last_name, First_name,Email, Date_of_birth, Gender, Phone_number, Address, Seniority, Max_tests, Car_type, Gear_type, Schedule, Max_distance, Appointments);
        }

        public Tester()
        {
            Appointments = new List<DateTime>();
        }
    }
}
