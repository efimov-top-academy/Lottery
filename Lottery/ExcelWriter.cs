using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class ExcelWriter
    {
        string fileExcel = @"D:\remove.xlsx";
        List<Person> persons = new List<Person>();

        public ExcelWriter(List<Person> persons)
        {
            this.persons = persons;
        }

        public void Write()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("PemovePersons");

                int row = 1;
                foreach (Person person in persons)
                {
                    sheet.Cells[row, 1].Value = person.Time;
                    sheet.Cells[row, 2].Value = person.FirstName;
                    sheet.Cells[row, 3].Value = person.LastName;
                    sheet.Cells[row, 4].Value = person.Phone;
                    sheet.Cells[row, 5].Value = person.Email;
                }

                sheet.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(fileExcel));
            }
        }
    }
}
