using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface Ibl
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

        IEnumerable<Tester> Close_testers(Address A, int X);
        bool Available_at(Tester t, DateTime DT);
        bool Available_at_day(Tester t, DateTime D);
        IEnumerable<Tester> Available_testers(DateTime DT,CarType Car_type, GearType Gear_type);
        IEnumerable<Tester> Available_testers_nearby(DateTime DT,Address A, CarType Car_type, GearType Gear_type);
        IEnumerable<Tester> Available_testers_by_day_nearby(DateTime DT, Address A, CarType Car_type, GearType Gear_type);
        int Num_of_tests(Trainee t);
        bool Entitled_to_license(Trainee t);
        IEnumerable<Test> Tests_at_date(DateTime Date);
        IEnumerable<IGrouping<DateTime, Test>> Tests_between_dates(DateTime Date1, DateTime Date2);
        IEnumerable<IGrouping<CarType, Tester>> Testers_by_carType(bool sort = false);
        IEnumerable<IGrouping<GearType, Tester>> Testers_by_GearType(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> Trainees_by_scool(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> Trainees_by_teacher(bool sort = false);
        IEnumerable<IGrouping<int, Trainee>> Trainees_by_num_of_tests(bool sort = false);
    }
}
