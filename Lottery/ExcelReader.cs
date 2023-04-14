using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class ExcelReader
    {
        public string FileExcel = @"D\table.xlsx";
        List<Person> persons = new List<Person>();

        public ExcelReader(string fileExcel)
        {
            this.FileExcel = fileExcel;
        }



        public List<Person>? Read()
        {
            FileInfo fileInfo = new FileInfo(FileExcel);

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

                        string? content = worksheet.Cells[i, j].Value.ToString();
                        //Console.Write($"{j} {content}\t");

                        switch (j)
                        {
                            case 1: person.Time = content!; break;
                            case 2: person.FirstName = content!; break;
                            case 3: person.LastName = content!; break;
                            case 4: person.Phone = content; break;
                            case 5: person.Email = content; break;
                        }

                    }
                    persons.Add(person);
                }
                return persons;
            }
                
        }
    }
}
