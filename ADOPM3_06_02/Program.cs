using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ADOPM3_06_02
{
    [XmlInclude(typeof(AUAddress))]
    [XmlInclude(typeof(USAddress))]
    public class Address { public string Street, PostCode; }
    public class USAddress : Address { }
    public class AUAddress : Address { }
  
    [XmlInclude(typeof(Person))]
    public class Person
    {
        public string Name;
        public int Age;

        [XmlElement("pastAddresses")]
        //[XmlArray("BestAdresses")]
        //[XmlArrayItem ("GoodAdress")]
        public List<Address> pastAddresses = new List<Address>();

        public Person BestFriend;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person
            {
                Name = "Stacey", Age = 25,
                pastAddresses = new List<Address>
                { new USAddress { Street = "An US Street", PostCode = "An US Zip" } as Address,
                  new AUAddress { Street = "An AU Street", PostCode = "An AU Zip" }as Address,
                  new Address { Street = "A Generic Street", PostCode = "A Generic Zip" }},
                BestFriend = new Person { Name = "Bob", Age = 30}
            };

            Console.WriteLine("Serialized");
            Console.WriteLine($"{p.Name}"); // Stacy
            foreach (var item in p.pastAddresses)
            {
                Console.WriteLine(item.GetType());
            }

            var xs = new XmlSerializer(typeof(Person));
            using (Stream s = File.Create(fname("Example8_02.xml")))
                xs.Serialize(s, p);
 
            Person p2;
            using (Stream s = File.OpenRead(fname("Example8_02b.xml")))
                p2 = (Person)xs.Deserialize(s);

            Console.WriteLine();
            Console.WriteLine("DeSerialized");
            Console.WriteLine($"{p2.Name}"); // Stacy
            foreach (var item in p2.pastAddresses)
            {
                Console.WriteLine(item.GetType());
            }

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