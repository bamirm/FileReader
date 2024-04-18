using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiParks
{
    public class DataProcessing
    {
        public static List<Park> BubbleSort(List<Park> parks)
        {
            for (int i = 0; i < parks.Count - 1; i++)
            {
                for (int j = 0; j < parks.Count - 1 - i; j++)
                {
                    if (int.Parse(parks[j].Id) > int.Parse(parks[j + 1].Id))
                    {
                        Park p = parks[j];
                        parks[j] = parks[j + 1];
                        parks[j + 1] = p;
                    }
                }
            }

            return parks;
        }

        public static List<Park> Selection(List<Park> parks, int n)
        {
            for (var i = 0; i < parks.Count; i++)
            {
                if (int.Parse(parks[i].Id) == n)
                {
                    return (new List<Park>() { parks[i] });
                }
            }
            return (new List<Park>());
        }
    }
}
