using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Salary
{
    class SalaryVM : INotifyPropertyChanged
    {
        private string totalDays;
        private string totalSalary;
        private string errorMsg;

        private const int BASE_VALUE = 2;
        private const string FILE_NAME = "salary.txt";
        private const double UNIT = 0.01;

        public string TotalDays {
            get { return totalDays; }
            set { totalDays = value;  OnPropertyChanged(); }
        }

        public string TotalSalary {
            get { return totalSalary; }
            set { totalSalary = value; OnPropertyChanged(); }
        }

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; OnPropertyChanged(); }
        }


        public void calculateSalary() {
            int days;
            reset();
            try
            {
               days = int.Parse(TotalDays);
            }
            catch {
                ErrorMsg = "Days should be Integer!";
                return;
            }
            if (days < 0) {
                ErrorMsg = "Days should be Positive Integer!";
                return;
            }
            double total_salary = calc(days);
            total_salary = total_salary * UNIT;
            TotalSalary = total_salary.ToString("C");
            saveFile(days, total_salary);
        }

        //reset the values before every calculation.
        private void reset() {
            ErrorMsg = null;
            TotalSalary = null;
        }

        //calculate the total salary
        private double calc(int days) {
           return Math.Pow(BASE_VALUE, days) - 1;
        }

        private void saveFile(int days, double totalSalary) {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullname = Path.Combine(path, FILE_NAME);
            StreamWriter sw = File.AppendText(fullname);
            sw.Write("Total days Susan worked:\t" + days+".\t");
            sw.WriteLine("Total amount of salary paied:\t $" + totalSalary);
            sw.WriteLine();
            sw.Close();
        }

        #region Propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(caller));
        }
        #endregion
    }
}
