﻿using Assignment_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Controls;
using Window = System.Windows.Window;
﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.Logging;

namespace Assignment_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Dat154Context dx = new();

        // Lokalt cachet data, kan opprette flere for forskjellige models
        private readonly LocalView<Student> Students;

        public readonly LocalView<Grade> Grades;

        public readonly LocalView<Course> Courses;

        public MainWindow()
        {
            Students = dx.Students.Local;
            Grades = dx.Grades.Local;
            Courses = dx.Courses.Local;

            // Henter alt fra databasen
            dx.Students.Load();
            dx.Courses.Load();
            dx.Grades.Load();

            InitializeComponent();
        }
        // Viser alle studiene
        private void courseButton_Click(object sender, RoutedEventArgs e)
        {
            var t = from courses in Courses
                    select new
                    {
                        CourseID = courses.Coursecode,
                        Course = courses.Coursename,
                        Semester = courses.Semester,
                        Teacher = courses.Teacher,
                    };
            studentView.ItemsSource = t;
        }
        // Viser alle studentene med en F
        private void failButton_Click(object sender, RoutedEventArgs e)
        {
            var t = from stud in Students
                    join grad in Grades on stud.Id equals grad.Studentid
                    join c in Courses on grad.Coursecode equals c.Coursecode
                    where grad.Grade1 == ("F")
                    select new
                    {
                        Name = stud.Studentname,
                        Course = c.Coursename,
                        Grade = grad.Grade1,
                    };
            studentView.ItemsSource = t;
        }

        // Søkefunksjon
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string search = textBox.Text.ToLower();
            var t = from s in Students
                    where s.Studentname.ToLower().Contains(search)
                    select new
                    {
                        ID = s.Id,
                        Name = s.Studentname,
                        Age = s.Studentage,
                    };

            studentView.ItemsSource = t;
        }

         
        // Vise alle studentene på et studie
        private void studentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = studentView.SelectedItem;

            if (c != null && c.GetType().GetProperty("CourseID") != null)
            {
                System.Type type = c.GetType();
                string coursename = (string)type.GetProperty("Course").GetValue(c, null);
                Window1 win = new Window1(dx, coursename)
                {
                    dx = dx
                };
                win.ShowDialog();
            }
        }

        // sorterer studentene etter karakter
        private void gradeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = gradeList.SelectedIndex;
            switch (value)
            {
                case 1:
                    var t = from stud in Students
                            join grade in Grades on stud.Id equals grade.Studentid
                            join c in Courses on grade.Coursecode equals c.Coursecode
                            where grade.Grade1.CompareTo("A") <= 0
                            select new
                            {
                                Grade = grade.Grade1,
                                Course = c.Coursename,
                                Student = stud.Studentname,
                            };
                    studentView.ItemsSource = t;
                    break;
                case 2:
                    t = from stud in Students
                        join grade in Grades on stud.Id equals grade.Studentid
                        join c in Courses on grade.Coursecode equals c.Coursecode
                        where grade.Grade1.CompareTo("B") <= 0
                        select new
                        {
                            Grade = grade.Grade1,
                            Course = c.Coursename,
                            Student = stud.Studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 3:
                    t = from stud in Students
                        join grade in Grades on stud.Id equals grade.Studentid
                        join c in Courses on grade.Coursecode equals c.Coursecode
                        where grade.Grade1.CompareTo("C") <= 0
                        select new
                        {
                            Grade = grade.Grade1,
                            Course = c.Coursename,
                            Student = stud.Studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 4:
                    studentView.ClearValue(ItemsControl.ItemsSourceProperty);
                    t = from stud in Students
                        join grade in Grades on stud.Id equals grade.Studentid
                        join c in Courses on grade.Coursecode equals c.Coursecode
                        where grade.Grade1.CompareTo("D") <= 0
                        select new
                        {
                            Grade = grade.Grade1,
                            Course = c.Coursename,
                            Student = stud.Studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 5:
                    t = from stud in Students
                        join grade in Grades on stud.Id equals grade.Studentid
                        join c in Courses on grade.Coursecode equals c.Coursecode
                        where grade.Grade1.CompareTo("E") <= 0
                        select new
                        {
                            Grade = grade.Grade1,
                            Course = c.Coursename,
                            Student = stud.Studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                default:
                    t = from stud in Students
                        join grade in Grades on stud.Id equals grade.Studentid
                        join c in Courses on grade.Coursecode equals c.Coursecode
                        select new
                        {
                            Grade = grade.Grade1,
                            Course = c.Coursename,
                            Student = stud.Studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
            }

        }

        // enkel metode for å oppdatere viewet
        private void refreshView()
        {
            var t = from stud in Students
                    join grade in Grades on stud.Id equals grade.Studentid
                    join c in Courses on grade.Coursecode equals c.Coursecode
                    select new
                    {
                        Grade = grade.Grade1,
                        Course = c.Coursename,
                        Student = stud.Studentname,
                    };
            studentView.ItemsSource = t;
        }

        // Fjerner en karakter fra databasen
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Remove button clicked");
            Console.WriteLine(studentView.SelectedItem);

            if (studentView.SelectedItem is Grade selectedGrade)
            {
                dx.Grades.Remove(selectedGrade);
                dx.SaveChanges();
            }
            var c = studentView.SelectedItem;
            if (c != null && c.GetType().GetProperty("Grade") != null)
            {
                try
                {
                    System.Type type = c.GetType();
                    string coursename = (string)type.GetProperty("Course").GetValue(c, null);
                    string student = (string)type.GetProperty("Student").GetValue(c, null);
                    string grade = (string)type.GetProperty("Grade").GetValue(c, null);

                    // Finner graden som tilsvarer verdiene i c
                    Grade g = dx.Grades.Where(g => g.Student.Studentname == student && g.Grade1 == grade).First();

                    if (g != null)
                    {
                        dx.Grades.Remove(g);
                        dx.SaveChanges();
                        MessageBox.Show("Grade removed successfully.");
                        refreshView();

                        gradeList.SelectedIndex = 0;
                    }
                }
                catch (Exception)
                {

                    throw;  
                }
            }
        }

        // Legger til en ny karakter
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentDialog dialog = new AddStudentDialog(dx);
            if (dialog.ShowDialog() == true)
            {
                refreshView();
            }
        }
    }
}