using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tradeAPI
{
    public class Classroom
    {
        public Classroom(string name, string subject, string teacher)
        {
            Name = name;
            Subject = subject;
            Teacher = teacher;
     
        }
        public Classroom()
        {

        }

        public string Name { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
    }
}
