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
    /// Interaction logic for Window2.xaml
    /// </summary>
    /// 
    public partial class Window2 : Window
    {
        public Dat154Context dx { get; set; }

        private readonly LocalView<Student> Students;

        public readonly LocalView<Grade> Grades;

        public readonly LocalView<Course> Courses;

        private string? studentname;

        public Window2()
        {
            InitializeComponent();
        }

        public Window2(Dat154Context dx, string? studentname)
        {
            this.dx = dx;
            this.studentname = studentname;
        }
    }
}
