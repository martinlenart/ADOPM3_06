using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ADOPM3_06_02
{
    [XmlInclude(typeof(AUAddress))]
    [XmlInclude(typeof(USAddress))]
    public class Address { public string Street, PostCode; }
    public class USAddress : Address { }
    public class AUAddress : Address { }
    public class Person
    {
        public string Name;

        [XmlElement("pastAddresses")]
        public List<Address> pastAddresses = new List<Address>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person { Name = "Stacey", pastAddresses = new List<Address>
                { new USAddress { Street = "An US Street", PostCode = "An US Zip" },
                  new AUAddress { Street = "An AU Street", PostCode = "An AU Zip" }}};

            var xs = new XmlSerializer(typeof(Person));
            using (Stream s = File.Create(fname("Example8_02.xml")))
                xs.Serialize(s, p);

            Person p2;
            using (Stream s = File.OpenRead(fname("Example8_02.xml")))
                p2 = (Person)xs.Deserialize(s);

            Console.WriteLine($"{p2.Name}"); // Stacy
            Console.WriteLine(p2.pastAddresses[0].GetType());
            Console.WriteLine(p2.pastAddresses[1].GetType());


            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }
    }
    //Exercises
    //1.    Modify the code to inlude a Nested Type "current address" and serialize it maintaining the derived type
    //2.    Modify the code rename the collection of past addresses using XmlArray and XmlArrayItem to rename outer and inner collection elements
    //      To appropirate names
}