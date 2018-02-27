using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp_XMLOperation
{
    public class Model
    {
        public string F1 { get; set; }
        public string F2 { get; set; }
        public List<Child> CList { get; set; }

    }

    public class Child
    {
        public string CF1 { get; set; }
        public string CF2 { get; set; }
 
    }
}
