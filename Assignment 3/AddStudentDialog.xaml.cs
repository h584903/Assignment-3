using Assignment_3.Models;
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
    /// Interaction logic for AddStudentDialog.xaml
    /// </summary>
    public partial class AddStudentDialog : Window
    {

        private readonly Dat154Context dx;

        public AddStudentDialog(Dat154Context context)
        {
            InitializeComponent();
            dx = context;

            // Populerer ComboBoxen med studenter og kurs
            studentComboBox.ItemsSource = dx.Students.ToList();
            studentComboBox.DisplayMemberPath = "Studentname";
            studentComboBox.SelectedValuePath = "Id";

            // Populerer ComboBoxen med kurs
            courseComboBox.ItemsSource = dx.Courses.ToList();
            courseComboBox.DisplayMemberPath = "Coursename";
            courseComboBox.SelectedValuePath = "Coursecode";

            // Populerer ComboBoxen med karakterer
            gradeComboBox.ItemsSource = new string[] { "A", "B", "C", "D", "E", "F" };
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Sjekker at alle feltene er fylt ut
            if (studentComboBox.SelectedItem == null || courseComboBox.SelectedItem == null || gradeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a student, course, and grade.");
                return;
            }
            

            // Oppretter et nytt Grade-objekt og legger det til i databasen
            Grade newGrade = new Grade
            {
                Studentid = (int)studentComboBox.SelectedValue,
                Coursecode = (string)courseComboBox.SelectedValue,
                Grade1 = (string)gradeComboBox.SelectedItem
            };

            // Legger til det nye Grade-objektet i databasen
            dx.Grades.Add(newGrade);
            dx.SaveChanges();

            MessageBox.Show("Student added successfully.");
            DialogResult = true;
        }

        // Avbryter og lukker dialogen
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
