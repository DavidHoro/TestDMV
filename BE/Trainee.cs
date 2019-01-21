using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee:ICloneable
    {
        public string ID{ get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Phone_number { get; set; }
        public Address Address { get; set; }
        public DateTime Date_of_birth { get; set; }
        public CarType Car_type { get; set; }
        public GearType Gear_type { get; set; }
        public String School { get; set; }
        public String Teacher { get; set; }
        public int Lessons { get; set; }
        public Grade? Status { get; set; }

        
        public override string ToString()
        {
            if (Status != null)
                return First_name + " " + Last_name + " " + ID + "" + Status + "\n";
            else
                return First_name + " " + Last_name + " " + ID + "\n";
        }

        public object Clone()
        {
            return new Trainee(ID, Last_name, First_name, Email, Gender, Phone_number, Address, Date_of_birth, Car_type, Gear_type, School, Teacher, Lessons, Status);
        }

        public Trainee(string Fn, string Ln, string id)
        {
            First_name = Fn;
            Last_name = Ln;
            ID = id;
            Lessons = 20;
        }

        public Trainee(string iD, string last_name, string first_name,string email ,Gender gender, string phone_number, Address address, DateTime date_of_birth, CarType car_type, GearType gear_type, string school, string teacher, int lessons, Grade? status = null)
        {
            ID = iD;
            Last_name = string.Copy(last_name);
            First_name = string.Copy(first_name);
            Email = string.Copy(email);
            Gender = gender;
            Phone_number = string.Copy(phone_number);
            Address = address.Clone() as Address;
            Date_of_birth = date_of_birth;
            Car_type = car_type;
            Gear_type = gear_type;
            School = string.Copy(school);
            Teacher = string.Copy(teacher);
            Lessons = lessons;
            Status = status;
        }

        public Trainee()
        {

        }
    }
}
