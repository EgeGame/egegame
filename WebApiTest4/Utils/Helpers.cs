using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest4.Util
{
    public class Helpers
    {
        static Helpers()
        {
            EgePointsMap = new Dictionary<int, int>();
            EgePointsMap.Add(0, 0);
            EgePointsMap.Add(1, 7);
            EgePointsMap.Add(2, 14);
            EgePointsMap.Add(3, 20);
            EgePointsMap.Add(4, 27);
            EgePointsMap.Add(5, 34);
            EgePointsMap.Add(7, 42);
            EgePointsMap.Add(8, 44);
            EgePointsMap.Add(9, 46);
            EgePointsMap.Add(10, 48);
            EgePointsMap.Add(11, 50);
            EgePointsMap.Add(12, 51);
            EgePointsMap.Add(13, 53);
            EgePointsMap.Add(14, 55);
            EgePointsMap.Add(15, 57);
            EgePointsMap.Add(16, 59);
            EgePointsMap.Add(17, 61);
            EgePointsMap.Add(18, 62);
            EgePointsMap.Add(19, 64);
            EgePointsMap.Add(20, 66);
            EgePointsMap.Add(21, 68);
            EgePointsMap.Add(22, 70);
            EgePointsMap.Add(23, 72);
            EgePointsMap.Add(24, 73);
            EgePointsMap.Add(25, 75);
            EgePointsMap.Add(26, 77);
            EgePointsMap.Add(27, 79);
            EgePointsMap.Add(28, 81);
            EgePointsMap.Add(29, 83);
            EgePointsMap.Add(31, 88);
            EgePointsMap.Add(32, 91);
            EgePointsMap.Add(33, 94);
            EgePointsMap.Add(34, 97);
            EgePointsMap.Add(35, 100);
        }

        private static Dictionary<int, int> EgePointsMap { get; set; }

        public static int GetEgePoints(int points)
        {
            try
            {
                return EgePointsMap[points];
            }
            catch (Exception exception)
            {
                return 0;
            }
            
        }
    }
}