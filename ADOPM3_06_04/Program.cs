﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
//using System.Text.Json.Serialization;

namespace ADOPM3_06_04
{
    public class USAddress : Address { }
    public class AUAddress : Address { }
    public class Address 
    { 
        public string Street { get; set; }
        public string PostCode { get; set; } 
    }
    public class Person
    {
        public string Name { get; set; }
        public Address currentAddress { get; set; }
        public List<Address> pastAddresses { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person
            {
                Name = "Stacey",
                currentAddress = new Address { Street = "An Address", PostCode = "A Zip Code" },
                pastAddresses = new List<Address>
                { new USAddress { Street = "An US Street", PostCode = "An US Zip" },
                  new AUAddress { Street = "An AU Street", PostCode = "An AU Zip" },
                  new Address { Street = "A Generic Street", PostCode = "A Generic Zip" }}
            };

            Console.WriteLine("Serialized");
            Console.WriteLine($"{p.Name}"); // Stacy
            foreach (var item in p.pastAddresses)
            {
                Console.WriteLine(item.GetType());
            }

            //Console.WriteLine();
            //Console.WriteLine(JsonSerializer.Serialize<Person>(p, new JsonSerializerOptions() { WriteIndented = true }));
            
            using (Stream s = File.Create(fname("Example8_04.json")))
            using (TextWriter writer = new StreamWriter(s))
                writer.Write(JsonSerializer.Serialize<Person>(p /*, new JsonSerializerOptions() { WriteIndented = true }*/));
            


            
            Person p2;
            using (Stream s = File.OpenRead(fname("Example8_04.json")))
            using (TextReader reader = new StreamReader(s))

                p2 = JsonSerializer.Deserialize<Person>(reader.ReadToEnd());

            Console.WriteLine();
            Console.WriteLine("Serialized");
            Console.WriteLine($"{p2.Name}"); // Stacy
            foreach (var item in p2.pastAddresses)
            {
                Console.WriteLine(item.GetType());      //Note the difference
            }
            

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }
    }
    //Exercise:
    //1.    Experiment with property decorator [JsonIgnore], [JsonPropertyName()]
    //2.    Make all members of type Person and type Address public fields instead of properties. Seriealize and deserialize. What Happens?
}
