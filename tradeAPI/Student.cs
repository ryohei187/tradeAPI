using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tradeAPI
{
    public class Student
    {
        public Student(int id, string unilogin, string firstname, string lastname, int isSeated, int role)
        {
            Id = id;
            Unilogin = unilogin;
            Firstname = firstname;
            Lastname = lastname;
            IsSeated = isSeated;
            Role = role;
        }
        public Student()
        {

        }
        public int Id { get; set; }
        public string Unilogin { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int IsSeated { get; set; }
        public int Role { get; set; }
    }
}
