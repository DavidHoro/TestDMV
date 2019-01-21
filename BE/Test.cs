using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test : ICloneable
    {
        public int Test_code { get; set; }
        public string Tester_ID { get; set; }
        public string Tester_name { get; set; }
        public string Trainee_ID { get; set; }
        public string Trainee_name { get; set; }
        public DateTime Date { get; set; }
        public Address Starting_point { get; set; }
        public CarType Car_type { get; set; }
        public GearType Gear_type { get; set; }
        public Criteria? Criteria { get; set; }
        public Grade? Grade { get; set; }
        public String Notes { get; set; }
     

        public object Clone()
        {
            return new Test(Test_code, Tester_ID, Tester_name, Trainee_ID, Trainee_name, Date, Starting_point, Criteria, Grade, Car_type, Gear_type, Notes);
        }
        public List<Test> GetAllTest() {
            return null;
        }
        public override string ToString()
        {
            if(Grade == null)
                return Test_code + " " + Trainee_ID + "\n";

            return Test_code + " " + Trainee_ID + " " + Grade + "\n";
        }

        public Test(string TraineeId, string TesterId, string TraineeName, string TesterName, DateTime date, Address address, CarType carType, GearType gearType)
        {
            Trainee_ID = TraineeId;
            Tester_ID = TesterId;
            Trainee_name = string.Copy(TraineeName);
            Tester_name = string.Copy(TesterName);
            Date = date;
            Starting_point = address.Clone() as Address;
            Car_type = carType;
            Gear_type = gearType;
            Grade = null;
            Notes = null;
        }

        public Test(int test_code, string tester_ID, string tester_name, string trainee_ID, string trainee_name, DateTime date, Address starting_point,Criteria? criteria, Grade? grade, CarType car_type, GearType gear_type, string notes)
        {
            Test_code = test_code;
            Tester_ID = tester_ID;
            Tester_name = string.Copy(tester_name);
            Trainee_ID = trainee_ID;
            Trainee_name = string.Copy(trainee_name);
            Date = date;
            Starting_point = starting_point.Clone() as Address;
            Criteria = criteria;//.Clone() as Criteria;
            Grade = grade;
            Car_type = car_type;
            Gear_type = gear_type;
            Notes = notes != null ? string.Copy(notes) : null;
        }
    }
}
