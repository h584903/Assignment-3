using Assignment_3.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment_3
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

        public Dat154Context dx { get; set; }

        private readonly LocalView<Student> Students;

        public readonly LocalView<Grade> Grades;

        public readonly LocalView<Course> Courses;
        private string? coursename;

        public Window1(LocalView<Course> cources, string c) {
            
        }

        public Window1(Dat154Context dx, string c)
        {
            this.dx = dx;
            InitializeComponent();

            Grades = dx.Grades.Local;
            Courses = dx.Courses.Local;
            Students = dx.Students.Local;

            var t = from stud in Students
                    join grade in Grades on stud.Id equals grade.Studentid
                    join course in Courses on grade.Coursecode equals course.Coursecode
                    where course.Coursename.Equals(c)
                    select new
                    {
                        Grade = grade.Grade1,
                        Course = course.Coursename,
                        Student = stud.Studentname,
                    };
            courseList.ItemsSource = t;
        }
    }
}
