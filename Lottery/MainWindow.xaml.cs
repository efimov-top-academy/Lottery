using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lottery
{
    class BlockParams
    {
        TextBlock block;
        string text;
        public BlockParams(TextBlock block, string text)
        {
            this.block = block;
            this.text = text;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        List<TextBlock> txtPersonsRandom = new List<TextBlock>();
        int maxRandomCount = 8;
        //Timer timer = new();

        List<Person>? persons = new List<Person>();
        List<Person>? personsRemove = new List<Person>();
        List<Person>? personsSelected = new List<Person>();
        CurrentPerson personCurrent = new CurrentPerson();

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            //var reader = new ExcelReader(@"D:\RPO\table.xlsx");
            persons = Read(@"D:\RPO\table.xlsx");
            personCurrent.Person = persons.FirstOrDefault();

            stackCurrentPerson.DataContext = personCurrent;

            //string pstr = "";

            //foreach (var p in persons)
            //    pstr += p.FirstName + " " + p.LastName + "\n";
            //MessageBox.Show(pstr);

            if (File.Exists(@"D:\RPO\remove.xlsx"))
            {
                personsRemove = Read(@"D:\RPO\remove.xlsx");
                personsRemove = personsRemove?.Distinct(new PersonComparer()).ToList();
                persons = persons?.Except(personsRemove!, new PersonComparer()).ToList();
            }
            
            

            //timer.Interval = 100;
            //timer.Elapsed += Timer_Start!;

            for (int i = 0; i < maxRandomCount; i++)
            {
                TextBlock txtPerson = new TextBlock();
                txtPerson.Text = "Person " + i.ToString();
                txtPerson.HorizontalAlignment = HorizontalAlignment.Center;
                txtPerson.VerticalAlignment = VerticalAlignment.Center;
                txtPerson.TextAlignment = TextAlignment.Center;
                txtPerson.TextWrapping = TextWrapping.Wrap;
                txtPerson.FontSize = 20;
                gridPersonRandom.Children.Add(txtPerson);
                txtPersonsRandom.Add(txtPerson);
            }

            
                
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int index = random.Next(0, persons.Count - 1);
            personCurrent.Person = persons[index];
        }

        private static void Timer_Start(Object source, ElapsedEventArgs e)
        {

        }

        private List<Person>? Read(string fileExcel)
        {
            List<Person> people = new List<Person>();

            FileInfo fileInfo = new FileInfo(fileExcel);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage package = new ExcelPackage(fileInfo);

            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null) return null;
            else
            {
                int rows = worksheet.Dimension.Rows;
                int columns = worksheet.Dimension.Columns;

                for (int i = 2; i <= rows; i++)
                {
                    Person person = new();
                    for (int j = 1; j <= columns; j++)
                    {
                        string content = "";
                        if (worksheet.Cells[i, j].Value != null)
                            content = worksheet.Cells[i, j].Value.ToString();

                        switch (j)
                        {
                            case 1: person.Time = content; break;
                            case 2: person.FirstName = content; break;
                            case 3: person.LastName = content; break;
                            case 4: person.Phone = content; break;
                            case 5: person.Email = content; break;
                        }

                    }
                    people.Add(person);
                }
                return people;
            }

        }

        private void ShowPersonRandom()
        {

        }

    }
}
