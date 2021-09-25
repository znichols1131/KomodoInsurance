using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApp
{
    public class Developer
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool AccessToPluralsight { get; set; }

        public Developer() { }

        public Developer(string name) 
        {
            Name = name;
        }
        public Developer(string name, bool accessToPluralsight)
        {
            Name = name;
            AccessToPluralsight = accessToPluralsight;
        }
    }
}
