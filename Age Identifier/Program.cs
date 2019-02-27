using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_Identifier
{
    class InvalidDateException : Exception
    {
        public InvalidDateException(String ExceptionString)
        {
            Console.WriteLine(ExceptionString);
        }
    }
    class AgeIdentifier
    {
        DateTime BirthDate;
        DateTime TodaysDate;
        public AgeIdentifier()
        {
        }
        public void AcceptDate()
        {
            Console.WriteLine("Enter Your BirthDate in Format YYYY MM DD HH MM SS");
           int Year = Convert.ToInt32(Console.ReadLine());
           int Month = Convert.ToInt32(Console.ReadLine());
           int Day = Convert.ToInt32(Console.ReadLine());
           int Hour = Convert.ToInt32(Console.ReadLine());
           int Minute = Convert.ToInt32(Console.ReadLine());
           int Second = Convert.ToInt32(Console.ReadLine());

            if (!Validation(Year,Month,Day,Hour,Minute,Second))
            {
                throw new InvalidDateException("Invalid Date");
            }

           BirthDate = new DateTime(Year,Month,Day,Hour,Minute,Second);
           TodaysDate = DateTime.Now.ToLocalTime();
        }
        private bool IsLeapYear(int Year)
        {
            bool bRet = true;

            if (Year % 4 == 0)
            {
                if (Year % 100 == 0)
                {
                    if (!(Year % 400 == 0))
                    {
                        bRet = false;
                    }
                }
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }
        private bool Validation(int year,int month,int day,int hour,int minute,int second)
        {
            bool bRet = true;

            if (year < 0 || year > DateTime.Now.Year || month < 0 || month > 12)  // Validation for Year and month
            {                                          
                bRet = false;                           
            }                                         
          
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) // Validation for Day
            {
                if (day > 31)
                {
                    bRet = false;
                }
            }
            else
            {
                if (IsLeapYear(year) && month == 2 && day > 29)
                {
                    bRet = false;
                }
                else if (month == 2)
                {
                    if (day > 28)
                    {
                        bRet = false;
                    }
                }

                if (day > 30)
                {
                    bRet = false;
                }

            }

            if (hour < 0 || hour > 60 || minute < 0 || minute > 60 || second < 0 || second > 60)
            {
                bRet = false;
            }

            return bRet;
        }
        public void DisplayAge()
        {
            var totalSec = (TodaysDate - BirthDate).TotalSeconds;
            var totalYears = Math.Truncate((totalSec / (60 * 60 * 24)) / 365);
            var totalMonths = Math.Truncate(((totalSec / (60 * 60 * 24)) % 365) / 30);
            var remainingDays = Math.Truncate(((totalSec / (60 * 60 * 24)) % 365) % 30);
            var hours = Math.Truncate((((totalSec / (60 * 60)) % (365 * 24)) % (30 * 24)) % 24);
            var minutes = Math.Truncate(((((totalSec / 60) % (365 * 24 * 60)) % (30 * 24 * 60)) % (24 * 60)) % 60);
            var seconds = Math.Truncate(((((totalSec % (365 * 24 * 60 * 60)) % (30 * 24 * 60 * 60)) % (24 * 60 * 60)) % (60 * 60)) % 60);
            Console.WriteLine("Estimated duration is {0} year(s), {1} month(s) ,{2} day(s),{3} Hours(s) , {4} Minute(s) , {5} Second(s)", totalYears, totalMonths, remainingDays, hours, minutes, seconds);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AgeIdentifier aobj = new AgeIdentifier();
            aobj.AcceptDate();
            aobj.DisplayAge();
        }
    }
}
