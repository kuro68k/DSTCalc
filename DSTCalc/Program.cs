using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTCalc
{
	class Program
	{
		static readonly uint[] days_in_month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

		static void Main(string[] args)
		{
			// EU
			Console.WriteLine("EU");
			for (int y = 2000; y < 2100; y++)
			{
				// 31 − ((((5 × y) ÷ 4) + 4) mod 7)
				// 31 − ((((5 × y) ÷ 4) + 1) mod 7)
				int startd = 31 - ((((5 * y) / 4) + 4) % 7);
				int endd   = 31 - ((((5 * y) / 4) + 1) % 7);
				//Console.WriteLine(y.ToString() + "\t" + startd.ToString() + "\t" + endd.ToString());
				//Console.WriteLine((y-2000).ToString() + ", " + startd.ToString() + ", " + endd.ToString() + ",");
				Console.WriteLine(startd.ToString() + ", " + endd.ToString() + ",");
			}

			Console.WriteLine();
			Console.WriteLine("US");
			for (uint y = 2000; y < 2100; y++)
			{
				uint startd = 0;
				uint dow;
				do
				{
					startd++;
					dow = DayOfWeek(y, 3, startd);
				} while (dow != 6);     // first Sunday of March
				do
				{
					startd++;
					dow = DayOfWeek(y, 3, startd);
				} while (dow != 6);     // second Sunday of March

				uint endd = 0;
				do
				{
					endd++;
					dow = DayOfWeek(y, 11, endd);
				} while (dow != 6);     // first Sunday of November

				//Console.WriteLine(y.ToString() + "\t" + startd.ToString() + "\t" + endd.ToString());
				//Console.WriteLine((y-2000).ToString() + ", " + startd.ToString() + ", " + endd.ToString() + ",");
				Console.WriteLine(startd.ToString() + ", " + endd.ToString() + ",");
			}

			//Console.WriteLine(DayOfWeek(2000, 1, 1));	// Saturday
			//Console.WriteLine(DayOfWeek(2001, 1, 1));	// Monday
			//Console.WriteLine(DayOfWeek(2019, 5, 10));  // Friday
			//Console.WriteLine(DayOfWeek(2020, 3, 1));	// Sunday
		}

		static bool IsLeapYear(uint year)
		{
			if ((year % 4) != 0)        // if year is not divisible by 4 = NOT leap year
				return false;
			if ((year % 100) != 0)      // if not divisible by 100 = leap year
				return true;
			if ((year % 400) != 0)      // if not divisible by 400 = NOT a leap year
				return false;
			return true;
		}

		// Monday = 0
		static uint DayOfWeek(uint year, uint month, uint day)
		{
			uint m = 1;
			while (m < month)
			{
				day += days_in_month[m-1];
				if ((m == 2) && IsLeapYear(year))
					day++;
				m++;
			}

			uint y = 2000;
			while (y < year)
			{
				day += 365;
				if (IsLeapYear(y))
					day++;
				y++;
			}

			day--;		// 01/01/2000 is day zero
			day += 5;	// 01/01/2000 was a Saturday
			day %= 7;

			return day;
		}

	}
}
