using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : Idal
    {
        #region Singleton
        private static readonly Dal_imp instance = new Dal_imp();

        public static Dal_imp Instance => instance;

        private Dal_imp() { }
        //static Dal_imp() { }
#endregion

        #region Add
        public bool Add_test(Test t)
        {
            t.Test_code = Configuration.Test_Code++;
            DataSource.Tests.Add(t);

            return true;
        }

        public bool Add_tester(Tester t)
        {
            foreach (Tester item in DataSource.Testers)
                if (item.ID == t.ID)
                    throw new Exception("Tester already in the system");

            DataSource.Testers.Add(t);

            return true;
        }

        public bool Add_trainee(Trainee t)
        {
            foreach (Trainee item in DataSource.Trainees)
                if (item.ID == t.ID)
                    throw new Exception("Trainee is already in the system");

            DataSource.Trainees.Add(t);

            return true;
        }
        #endregion

        #region Delete
        public bool Delete_test(int Code)
        {
            int i;
            i = DataSource.Tests.RemoveAll(item => item.Test_code == Code);

            if (i != 0)
                return true;
            else
                throw new Exception("The test doesn't exist");
        }

        public bool Delete_tester(string ID)
        {
            int i;
            i = DataSource.Testers.RemoveAll(item => item.ID == ID);

            if (i != 0)
                return true;
            else
                throw new Exception("The tester doesn't exist");
        }

        public bool Delete_trainee(string ID)
        {
            int i;
            i = DataSource.Trainees.RemoveAll(item => item.ID == ID);

            if (i != 0)
                return true;
            else
                throw new Exception("The trainee doesn't exist");
        }
        #endregion

        #region Get all
        public IEnumerable<Tester> Get_all_testers(Func<Tester, bool> predicate = null)
        {
            if (predicate == null)
                return from item in DataSource.Testers select item.Clone() as Tester;

            return from item in DataSource.Testers
                   where predicate(item)
                   select item.Clone() as Tester;        
          }

        public IEnumerable<Test> Get_all_tests(Func<Test, bool> predicate = null)
        {
            if (predicate == null)
                return from item in DataSource.Tests select item.Clone() as Test;

            return from item in DataSource.Tests
                       where predicate(item)
                       select item.Clone() as Test;
        }

        public IEnumerable<Trainee> Get_all_trainees(Func<Trainee, bool> predicate = null)
        {
            if (predicate == null)
                return from item in DataSource.Trainees select item.Clone() as Trainee;

            return from item in DataSource.Trainees
                   where predicate(item)
                   select item.Clone() as Trainee;
        }
        #endregion

        #region Get
                public Tester Get_tester(string ID)
                {
                    foreach (Tester item in DataSource.Testers)
                        if (item.ID == ID)
                            return item;

                    throw new Exception("The tester doesn't exist");
                }

                public Test Get_test(int Code)
                {
                    foreach (Test item in DataSource.Tests)
                        if (item.Test_code == Code)
                            return item;

                    throw new Exception("The test doesn't exist");
                }

                public Trainee Get_trainee(string ID)
                {
                    foreach (Trainee item in DataSource.Trainees)
                        if (item.ID == ID)
                            return item;

                    throw new Exception("The trainee doesn't exist");
                }
        #endregion

        #region Update

        public bool Updete_test(Test t)
        {
            int i;
            i = DataSource.Tests.FindIndex(item => item.Test_code == t.Test_code);

            if (i == -1)
                throw new Exception("Test not found");

            DataSource.Tests[i] = t;

            return true;
        }

        public bool Updete_tester(Tester t)
        {
            int i;
            i = DataSource.Testers.FindIndex(item => item.ID == t.ID);

            if (i == -1)
                throw new Exception("Tester not found");

            DataSource.Testers[i] = t;
        
            return true;
        }

        public bool Updete_trainee(Trainee t)
        {
            int i;
            i = DataSource.Trainees.FindIndex(item => item.ID == t.ID && item.Car_type == t.Car_type);

            if (i == -1)
                throw new Exception("Trainee not found");

            DataSource.Trainees[i] = t;

            return true;
        }
        #endregion

        public void Add_Appointment(string ID, DateTime DT)
        {
            DataSource.Testers.Find(item => item.ID == ID).Appointments.Add(DT);
        }

    }
}
