using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp_XMLOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "AppConfig\\XMLFile.xml";
            XmlHelper helper = new XmlHelper(path);

            //helper.ModifyNodeText("./root/user/name", "bobo1");

            //helper.RemoveNode("./root/user[@id=0]");
            //var node = helper.GetSingleNode("./root/user[@id=0]");
            //var s = helper.GetAttributeValue(node, "./name", "code");


            //var str = XmlHelper.Serializer<Model>(new Model { F1 = "字段一", F2 = "2", CList = new List<Child> { new Child { CF1 = "c1", CF2 = "c2" }, new Child { CF1 = "c11", CF2 = "c21" } } });

            //var m = XmlHelper.Deserialize<Model>(str);

            //var x = XmlHelper.Transform(@"E:\XML.xml", @"E:\XSLT.xsl", new System.Xml.Xsl.XsltArgumentList());

            //XmlHelper.Transform(@"E:\XML.xml", @"E:\XSLT.xsl", @"E:\newXml.xml", true); @"E:\XML.xml"
            var doc = new XmlDocument();
            doc.Load(@"E:\projgxz_myself\ConsoleApp_XMLOperation\ConsoleApp_XMLOperation\AppConfig\XMLFile.xml");
            var x = XmlHelper.Transform(doc, @"E:\projgxz_myself\ConsoleApp_XMLOperation\ConsoleApp_XMLOperation\AppConfig\XSLTFile.xslt", new System.Xml.Xsl.XsltArgumentList());
        }
    }
}
