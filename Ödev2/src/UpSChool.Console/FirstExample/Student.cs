using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSChool.Console.FirstExample
{
    public class Student : PersonBase, ITurkishPerson, IAge
    {

        public string SchoolNumber { get; set; }
        public int Score1 { get; set; }
        public int score2 { get; set; }
        public int score3 { get; set; }
        public string TCID { get; set; }
        public int Age { get; set; }
    }
}