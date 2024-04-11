using Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        // Må koble til cachen til hoved vinduet for å holde cachen liten
        public Dat154Context dx {  get; set; }
        public Editor()
        {
            InitializeComponent();
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {

            Student s = new()
            {
                Studentname = sName.Text,
                Studentage = int.Parse(sAge.Text),
                // Id må egentlig være unik
                Id = int.Parse(sId.Text)
            };

            dx.Students.Add(s);
            dx.SaveChanges();
        }
    }
}
