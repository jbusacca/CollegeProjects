using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDataDLL
{
    public class TimeData
    {
        // makes data serializeable so it can be sent
        [Serializable]
        public struct StructTimeData
        {
            public int hour, minute, second;
            public bool isAlarmTime;
            public bool is24HourTime;

            public StructTimeData(int h = 12, int m = 0, int s = 0, bool a = false, bool t = true)
            {
                hour = h;
                minute = m;
                second = s;
                isAlarmTime = a;
                is24HourTime = t;
            }
        }
    }
}