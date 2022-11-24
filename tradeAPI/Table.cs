using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tableAPI
{
    public class Table
    {
        public Table(int id, string color, int isOccupied, int studentID, string classroomID, string position)
        {
            Id = id;
            Color = color;
            IsOccupied = isOccupied;
            StudentID = studentID;
            ClassroomID = classroomID;
            Position = position;
        }
        public Table()
        {

        }
        public int Id { get; set; }
        public string Color { get; set; }
        public int IsOccupied { get; set; }
        public int StudentID { get; set; }
        public string ClassroomID { get; set; }
        public string Position { get; set; }
    }
}

