using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Mileshin_L1
{
    class Comparer : IComparer<Building>
        {
            public int Compare(Building tmp1, Building tmp2)
            {
                if(tmp1.Get_rsdnt() == tmp2.Get_rsdnt())
                {
                    if (tmp1.Get_type() > tmp2.Get_type())
                        return 1;
                    else if (String.Compare(tmp1.Get_adrss(), tmp2.Get_adrss(), true) < 0)
                        return 1;
                    else if (String.Compare(tmp1.Get_adrss(), tmp2.Get_adrss(), true) > 0)
                        return -1;
                    else return 0;
                }
                else
                {
                    if (tmp1.Get_rsdnt() > tmp2.Get_rsdnt())
                        return 1;
                    else return -1;
                }
            }
        }
    [XmlInclude(typeof(House))]
    [XmlInclude(typeof(Office))]
    [KnownType(typeof(House))]
    [KnownType(typeof(Office))]
    public abstract class Building
        {
            [DataMember]
            public uint residents;
            [DataMember]
            public int type;
            [DataMember]
            public string adress;

            public Building() { }

            public uint Get_rsdnt()
            {
                return residents;
            }

            public int Get_type()
            {
                return type;
            }

            public string Get_adrss()
            {
                return adress;
            }

            public abstract void Set_rsdnt(uint value);

            public abstract void Info();
        }
    
    public class House : Building
    {
        public House() { }

        public House(uint flt, uint rms, string adrs)
        {
            type = 1;
            adress = adrs;
            uint val = flt * rms;
            Set_rsdnt(val);
        }

        public override void Set_rsdnt(uint value)
        {
            residents = (UInt32)(value * 1.3);
        }

        public override void Info()
        {
            Console.WriteLine("House: {0} withs {1} residents", adress, residents);
        }
    }
  
    public class Office : Building
            {
                public Office() { }

                public Office(uint sqware, string adrs)
                {
                    type = 0;
                    adress = adrs;
                    Set_rsdnt(sqware);
                }

                public override void Set_rsdnt(uint value)
                {
                    residents = (UInt32)(value * 0.2);
                }

                public override void Info()
                {
                    Console.WriteLine("Office: {0} withs {1} residents", adress, residents);
                }
    }

    [XmlInclude(typeof(Building))]
    [KnownType(typeof(Building))]
    public class Company
     {
        [DataMember]
        public uint total_residents;
        [DataMember]
        public uint middle_residents;
        [DataMember]
        public uint count;
        [DataMember]
        public List<Building> buildings = new List<Building>();

                public uint Get_mid_rsdnts()
                {
                    return middle_residents;
                }

                public void Add_new_build(uint flt, uint rms, string adrs)
                {
                    Building tmp;

                    tmp = new House(flt, rms, adrs);

                    buildings.Add(tmp);
                    count++;
                    total_residents += tmp.Get_rsdnt();
                    middle_residents = total_residents / count;
                }

                public void Add_new_build(uint sqware, string adrs)
                {
                    Building tmp;

                    tmp = new Office(sqware, adrs);

                    buildings.Add(tmp);
                    count++;
                    total_residents += tmp.Get_rsdnt();
                    middle_residents = total_residents / count;
        }

                public void Get_Info(bool type)
                {
                    if(type)
                    {
                        if (count < 3) return;

                        for(int i = 0; i < 3; i++)
                        {
                            buildings[i].Info();
                        }
                    }
                    else
                    {
                        if (count < 4) return;

                        for(int i = 0; i < 4; i++)
                        {
                            buildings[(int)count - i].Info();
                        }
                    }
                }
                public void Sort()
                {
                    if (count < 2) return;

                    Comparer sort = new Comparer();
                    buildings.Sort(sort);
                }
                
            }
        
        class Menu
        {
            char chose;
            Company Lets_build = new Company();

            public void Start()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1: Add building");
                    Console.WriteLine("2: Sort buildings");
                    Console.WriteLine("3: Show 3 first buildings");
                    Console.WriteLine("4: Show 4 last building");
                    Console.WriteLine("5: Get middle residents");
                    Console.WriteLine("6: To XML");
                    Console.WriteLine("7: To JSON");
                    Console.WriteLine("8: Load class from XML or JSON");

                    var press = Console.ReadKey();
                    chose = press.KeyChar;

                    switch (chose)
                    {
                        case '1':
                        Console.Clear();
                        Console.WriteLine("Type building adress");
                        string adr = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Type building (House or Office) 1/0");
                            string chouse = Console.ReadLine();
                        if (chouse == "house")
                        {
                            Console.Clear();
                            Console.WriteLine("Type count of rooms in flat");
                            uint rms = Convert.ToUInt32(Console.ReadLine());
                            Console.Clear();
                            Console.WriteLine("Type count of flats");
                            uint flts = Convert.ToUInt32(Console.ReadLine());
                            Lets_build.Add_new_build(flts, rms, adr);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Type size of area");
                            uint ar = Convert.ToUInt32(Console.ReadLine());
                            Lets_build.Add_new_build(ar, adr);
                        }
                            
                            break;

                        case '2':
                            Console.Clear();
                            Lets_build.Sort();
                            break;

                        case '3':
                            Console.Clear();
                            Lets_build.Get_Info(true);
                            break;

                        case '4':
                            Console.Clear();
                            Lets_build.Get_Info(false);
                            break;

                        case '5':
                            Console.Clear();
                            Console.WriteLine("Middle residents is {0}", Lets_build.Get_mid_rsdnts());
                            break;

                        case '6':
                            Console.Clear();

                            XmlSerializer typer = new XmlSerializer(typeof(Company));

                            using (Stream writer = File.Create("put.xml"))
                                typer.Serialize(writer, Lets_build);

                            Console.WriteLine("File is create!");
                            break;

                        case '7':
                            Console.Clear();

                            DataContractJsonSerializer typerJ = new DataContractJsonSerializer(typeof(Company));

                            using (Stream writerJ = File.Create("put.json"))
                                typerJ.WriteObject(writerJ, Lets_build);

                            Console.WriteLine("File is create!");
                            break;

                        case '8':
                            Console.Clear();
                            if (!File.Exists("put.json"))
                            {
                                if (!File.Exists("put.xml"))
                                {
                                    Console.WriteLine("File not found!");
                                    break;
                                }

                                XmlSerializer outX = new XmlSerializer(typeof(Company));

                                using (Stream reader = File.OpenRead("put.xml"))
                                Lets_build = (Company)outX.Deserialize(reader);

                                Console.WriteLine("Read is done!");
                                break;
                            }

                            DataContractJsonSerializer outJ = new DataContractJsonSerializer(typeof(Company));

                            using (Stream readerJ = File.OpenRead("put.json"))
                            Lets_build = (Company)outJ.ReadObject(readerJ);

                            Console.WriteLine("Read is done!");
                            break;
                    }

                    Console.WriteLine("\nPress any key");
                    Console.ReadKey();
                }
            }

        }

    class Program
    {
        static void Main()
        {
            Menu men = new Menu();
            men.Start();
        }
    }
       
    
}
