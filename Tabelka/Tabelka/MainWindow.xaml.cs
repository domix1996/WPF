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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tabelka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Person
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Miasto { get; set; }
            public string Ulica { get; set; }
            public string Numer { get; set; }
            public ICollection<string> Zainteresowania { get; set; }

            public Person(string imie, string nazwisko, string miasto, string ulica, string numer, List<string> zainteresowania)
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Miasto = miasto;
                Ulica = ulica;
                Numer = numer;
                Zainteresowania = zainteresowania;
            }
           

        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Person("Kamil", "Mrowiec", "Nowa-Wieś", "Wyspiańskiego", "22",
                new List<string> {"Muzyka", "Gry komputerowe"});
            string test = "ala";
        }
    }
}
