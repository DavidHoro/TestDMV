using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_imp : Ibl
    {
        #region Singleton
        private static readonly BL_imp instance = new BL_imp();

        public static BL_imp Instance => instance;

        private BL_imp() { }
        #endregion

        static Idal myDAL = FactoryDAL.Get_DAL();

        #region Tester
        public bool Add_tester(Tester t)
        {
            if (DateTime.Now.Year - t.Date_of_birth.Year < Configuration.Min_tester_age)
                throw new Exception("Tester under " + Configuration.Min_tester_age);

            try
            {
                return myDAL.Add_tester(t);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete_tester(string ID)
        {
            try
            {
                return myDAL.Delete_tester(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Updete_tester(Tester t)
        {
            if (DateTime.Now.Year - t.Date_of_birth.Year < Configuration.Min_tester_age)
                throw new Exception("Tester under " + Configuration.Min_tester_age);

            try
            {
                return myDAL.Updete_tester(t);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Tester Get_tester(string ID)
        {
            try
            {
                return myDAL.Get_tester(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Tester> Get_all_testers(Func<Tester, bool> predicate = null)
        {
            return myDAL.Get_all_testers(predicate);
        }
        #endregion

        #region Trainee
        public bool Add_trainee(Trainee t)
        {
            if (DateTime.Now.Year - t.Date_of_birth.Year < Configuration.Min_trainee_age)
                throw new Exception("Trainee under " + Configuration.Min_trainee_age);

            try
            {
                return myDAL.Add_trainee(t);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete_trainee(string ID)
        {
            try
            {
                return myDAL.Delete_trainee(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Updete_trainee(Trainee t)
        {
            if (DateTime.Now.Year - t.Date_of_birth.Year < Configuration.Min_trainee_age)
                throw new Exception("Trainee under " + Configuration.Min_trainee_age);

            try
            {
                return myDAL.Updete_trainee(t);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Trainee Get_trainee(string ID)
        {
            try
            {
                return myDAL.Get_trainee(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Trainee> Get_all_trainees(Func<Trainee, bool> predicate = null)
        {
            return myDAL.Get_all_trainees(predicate);
        }
        #endregion

        #region Test
        public bool Add_test(Test t)
        {
            var temp = from item in myDAL.Get_all_tests()
                       where item.Trainee_ID == t.Trainee_ID && item.Car_type == t.Car_type
                       select item;

            foreach (Test item in temp)
            {
                if (item.Grade == null)
                    throw new Exception("You already scheduled  for a test at" + item.Date);

                if (item.Grade == Grade.Pass)
                    throw new Exception("You already have a license for this type car");

                if (item.Date < t.Date && item.Date >= t.Date.AddDays(-Configuration.Waiting_period))
                    throw new Exception("Can't set two tests within a " + Configuration.Waiting_period + "days");
            }

            if (myDAL.Get_trainee(t.Trainee_ID).Lessons < Configuration.Min_lessons)
                throw new Exception("Less than " + Configuration.Min_lessons + " lessons");

            myDAL.Add_Appointment(t.Tester_ID, t.Date);

            return myDAL.Add_test(t);
        }

        public bool Delete_test(int Code)
        {
            try
            {
                return myDAL.Delete_test(Code);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool Updete_test(Test t)
        {
            try
            {
                return myDAL.Updete_test(t);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Test Get_test(int Code)
        {
            try
            {
                return myDAL.Get_test(Code);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion

        public IEnumerable<Test> Get_all_tests(Func<Test, bool> predicate = null)
        {
            return myDAL.Get_all_tests(predicate);
        }

        public IEnumerable<Tester> Close_testers(Address A, int X)
        {
            return myDAL.Get_all_testers(t => A.Distance(t.Address) < X);
        }

        public IEnumerable<Tester> Available_testers(DateTime DT, CarType Car_type, GearType Gear_type)
        {
            return myDAL.Get_all_testers(t => t.Car_type == Car_type && t.Gear_type == Gear_type &&
                                                  t.Appointments.Exists(item => item == DT) == false);
        }

        public int Num_of_tests(Trainee t)
        {
            return myDAL.Get_all_tests(item => item.Trainee_ID == t.ID).Count();
        }

        public bool Entitled_to_license(Trainee t)
        {
            return myDAL.Get_all_tests(item => item.Trainee_ID == t.ID &&
                   item.Car_type == t.Car_type && item.Grade == Grade.Pass) != null;
        }

        #region Grouping
        public IEnumerable<IGrouping<CarType, Tester>> Testers_by_carType(bool sort = false)
        {
            if (sort == true)
                return from item in myDAL.Get_all_testers()
                       orderby item.Last_name, item.First_name
                       group item by item.Car_type;

            return from item in myDAL.Get_all_testers() group item by item.Car_type;
        }

        public IEnumerable<IGrouping<GearType, Tester>> Testers_by_GearType(bool sort = false)
        {
            if (sort == true)
                return from item in myDAL.Get_all_testers()
                       orderby item.Last_name, item.First_name
                       group item by item.Gear_type;

            return from item in myDAL.Get_all_testers() group item by item.Gear_type;
        }

        public IEnumerable<IGrouping<string, Trainee>> Trainees_by_scool(bool sort = false)
        {
            if (sort == true)
                return from item in myDAL.Get_all_trainees()
                       orderby item.Last_name, item.First_name
                       group item by item.School;

            return from item in myDAL.Get_all_trainees() group item by item.School;
        }

        public IEnumerable<IGrouping<string, Trainee>> Trainees_by_teacher(bool sort = false)
        {
            if (sort == true)
                return from item in myDAL.Get_all_trainees()
                       orderby item.Last_name, item.First_name
                       group item by item.Teacher;

            return from item in myDAL.Get_all_trainees() group item by item.Teacher;
        }

        public IEnumerable<IGrouping<int, Trainee>> Trainees_by_num_of_tests(bool sort = false)
        {
            if (sort == true)
                return from item in myDAL.Get_all_trainees()
                       let num = Num_of_tests(item)
                       orderby item.Last_name, item.First_name
                       group item by num;

            return from item in myDAL.Get_all_trainees()
                   let num = Num_of_tests(item)
                   group item by num;
        }
        #endregion

        public IEnumerable<Test> Tests_at_date(DateTime Date)
        {
            return myDAL.Get_all_tests(item => item.Date == Date);
        }
        /// <summary>
        /// This function returns a sorted collection of all the tests between Date1 and Date2
        /// </summary>
        /// <param name="Date1">Start of range</param>
        /// <param name="Date2">End of range</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<DateTime, Test>> Tests_between_dates(DateTime Date1, DateTime Date2)
        {
            return from item in myDAL.Get_all_tests(t => t.Date >= Date1 && t.Date <= Date2)
                   orderby item.Date.Hour
                   group item by item.Date;
        }

        public IEnumerable<Tester> Available_testers_nearby(DateTime DT, Address A, CarType Car_type, GearType Gear_type)
        {
            return myDAL.Get_all_testers(t => t.Car_type == Car_type && t.Gear_type == Gear_type &&
                                     A.Distance(t.Address) <= t.Max_distance && Available_at(t, DT));
        }

        public IEnumerable<Tester> Available_testers_by_day_nearby(DateTime d, Address A, CarType Car_type, GearType Gear_type)
        {
            return myDAL.Get_all_testers(t => t.Car_type == Car_type && t.Gear_type == Gear_type &&
                    A.Distance(t.Address) <= t.Max_distance && Available_at_day(t, d));
        }

        /// <summary>
        /// This function checks if the tester is available at a given date and time
        /// </summary>
        /// <param name="t">The tester</param>
        /// <param name="DT">The given date and time</param>
        public bool Available_at(Tester t, DateTime DT)
        {
            if (DT.DayOfWeek > DayOfWeek.Thursday)
                return false;

            if (t.Schedule[(int)DT.DayOfWeek, DT.Hour - 9] == false)
                return false;

           if (t.Appointments.Exists(item => item == DT))
                return false;

            return true;
        }

        public bool Available_at_day(Tester t, DateTime D)
        {
            if (D.DayOfWeek > DayOfWeek.Thursday)   //אם התאריך הוא בסוף השבוע
                return false;

            int count = 0;
            for (int i = 0; i < 6; i++)             //נמצא את מספר השעות שהוא עובד היום
                if (t.Schedule[(int)D.DayOfWeek, i] == true)
                    count++;

            if (count == 0)         //אם לא עובד היום
                return false;

            if ((t.Appointments.FindAll(item => item.Date.Date == D.Date).Count()) == count)    //אם היומן מלא היום
                return false;

            return true;
        }

    }
}
