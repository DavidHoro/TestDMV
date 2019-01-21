using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Criteria// : ICloneable
    {
        public int? Safe_distance { get; set; }
        public int? Parking { get; set; }
        public int? Mirrors_observing { get; set; }
        public int? Signals { get; set; }
        public int? Priority { get; set; }
        public bool? Expropriation_of_control { get; set; }

        public Criteria(int? safe_distance, int? parking, int? mirrors_observing, int? signals, int? priority, bool? expropriation_of_control)
        {
            Safe_distance = safe_distance;
            Parking = parking;
            Mirrors_observing = mirrors_observing;
            Signals = signals;
            Priority = priority;
            Expropriation_of_control = expropriation_of_control;
        }

        public Criteria(int x = 0) { Safe_distance = Parking = Mirrors_observing = Signals = Priority = 0; Expropriation_of_control = true; }
        //public object Clone()
        //{
        //    if(Expropriation_of_control != null)
        //        return new Criteria(Safe_distance, Parking, Mirrors_observing, Signals, Priority, Expropriation_of_control);
        //    return null;
        //}

        /// <summary>
        /// פונקציה זו מחשבת האם התלמיד עבר את מבחן הנהיגה או לא. על מנת לעבור יש להשיג ציון 8.5 לפחות
        /// </summary>
        /// <returns> את סטטוס התלמיד</returns>
        public Grade grade()
        {
            if (Expropriation_of_control == true)
                return Grade.Fail;

            if (0.25 * Safe_distance + 0.1 * Parking + 0.15 * Mirrors_observing + 0.1 * Signals + 0.4 * Priority < 8.5)
                return Grade.Fail;

            return Grade.Pass;
        }


    }

}