using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string SubjectGroup { get; set; }
        public string TeacherIncharge { get; set; }
        public string Email { get; set; }
        public string Cv { get; set; }
    }
}
