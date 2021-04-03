using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_3_Entrer
{
    class Program
    {

        public class Guest
        {
            public string Name { get; set; }
            public List<string> Meals { get; set; }
            public int UnlikeMealCount { get; set; }
            public Guest()
            {
                Meals = new List<string>();
                UnlikeMealCount = 0;
            }
        }
        static void Main(string[] args)
        {
            List<Guest> lstGuest = new List<Guest>();
            List<string> Lines = new List<string>();

            string sLine = Console.ReadLine();
            while (!sLine.Contains("Stop"))
            {
                Lines.Add(sLine);
                sLine = Console.ReadLine();
            }

            foreach (var line in Lines)
            {
                var record = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                string command = string.Empty;
                string guestName = string.Empty;
                string meal = string.Empty;
                if (record.Length > 0)
                {
                    command = record[0];
                    guestName = record[1];
                    meal = record[2];
                }
                if (command.Contains("Like"))
                {
                    var index = lstGuest.FindIndex(a => a.Name == guestName);
                    if (index < 0)
                    {
                        Guest newGuest = new Guest();
                        newGuest.Name = guestName;
                        newGuest.Meals.Add(meal);
                        lstGuest.Add(newGuest);
                    }
                    else
                    {
                        lstGuest[index].Meals.Add(meal);
                    }
                }
                if (command.Contains("Unlike"))
                {
                    var index = lstGuest.FindIndex(a => a.Name == guestName);
                    if (index >= 0)
                    {
                        if (lstGuest[index].Meals.Remove(meal))
                        {
                            Console.WriteLine($"{guestName} doesn't like the {meal}.");
                            lstGuest[index].UnlikeMealCount++;
                        }
                        else
                            Console.WriteLine($"{guestName} doesn't have the {meal} in his/her collection.");
                    }
                    else
                    {
                        Console.WriteLine($"{guestName} is not at the party.");
                    }


                }
            }

            int unlike = 0;
            foreach (var guest in lstGuest.OrderByDescending(a => a.Meals.Count()).ThenBy(a => a.Name))
            {
                Console.WriteLine($"{guest.Name}: {string.Join(", ", guest.Meals)}");
                unlike += guest.UnlikeMealCount;
            }

            Console.WriteLine($"Unliked meals: {unlike}");
        }
    }
}
