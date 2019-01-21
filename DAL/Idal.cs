using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        #region Tester
        bool Add_tester(Tester t);
        bool Delete_tester(string ID);
        bool Updete_tester(Tester t);
        Tester Get_tester(string ID);
        IEnumerable<Tester> Get_all_testers(Func<Tester, bool> predicate = null);
        #endregion

        #region Trainee
        bool Add_trainee(Trainee t);
        bool Delete_trainee(string ID);
        bool Updete_trainee(Trainee t);
        Trainee Get_trainee(string ID);
        IEnumerable<Trainee> Get_all_trainees(Func<Trainee, bool> predicate = null);
        #endregion

        #region Test
        bool Add_test(Test t);
        bool Delete_test(int Code);
        bool Updete_test(Test t);
        Test Get_test(int Code);
        IEnumerable<Test> Get_all_tests(Func<Test, bool> predicate = null);
        #endregion

        void Add_Appointment(string ID, DateTime DT);

    }
}
