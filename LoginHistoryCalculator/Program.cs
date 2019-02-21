using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHistoryCalculator
{
    class Program
    {
        enum PortalKind
        {
            Unlock,
            Lock
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\KarlMolina\Documents\loginhistory\loginhistory.txt");
            List<PortalDate> portalDates = new List<PortalDate>();
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                // 0 : lock
                // 1 : thu
                // 2 : 2/1/43
                // 3 : 12:21:12
                var dateString = line.Replace(split[0], string.Empty);
                dateString = dateString.Replace(split[1], string.Empty);
                var date = DateTime.Parse(dateString);

                var portalDate = new PortalDate();
                portalDate.Date = date;
                Enum.TryParse(split[0].ToUpperInvariant(), out portalDate.Kind);
                portalDates.Add(portalDate);
            }

            bool onClock = false;
            PortalDate last = portalDates[0];
            foreach (var pd in portalDates)
            {
                Console.WriteLine(pd.Date);
                if (onClock && pd.Kind == PortalKind.Unlock)
                    continue;
                if (!onClock && pd.Kind == PortalKind.Lock)
                    continue;
                if (pd.Kind == PortalKind.Lock)
                {
                    Console.WriteLine($"time diff: {pd.Date.Subtract(last.Date)}");
                    onClock = false;
                }
                else
                {
                    onClock = true;
                    last = pd;
                }

            }

            Console.ReadKey();
        }

        struct PortalDate
        {
            public DateTime Date;

            public PortalKind Kind;
        }
    }
}
