using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static Class1 C1 = new Class1();
        public const int nItem = 5000;
        public const int nPoint = 250;
        public static List<double[,]> list = new List<double[,]>();

        static void Write()
        {
            var fileStream = new FileStream("INPUT", FileMode.Create, FileAccess.Write);
            var bs = new BinaryWriter(fileStream);

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            
            for (int i = 0; i < nItem; i++)
            {
                var size = (int)(random.NextDouble() * nPoint + nPoint);
                bs.Write(size);
                for (int j = 0; j < size; j++)
                {
                    bs.Write(random.NextDouble());
                    bs.Write(random.NextDouble());
                }
            }

            bs.Close();
        }

        static void Read()
        {
            var fileStream = new FileStream("INPUT", FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fileStream);

            var jsonFile = new FileStream("JSON.json", FileMode.Create, FileAccess.Write);
            var streamWriter = new StreamWriter(jsonFile);
            var jsonWriter = new JsonTextWriter(streamWriter);
            var serializer = new JsonSerializer();

            C1.raw = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();
            var byteStream = new MemoryStream(C1.raw);
            br = new BinaryReader(byteStream);
            for (int i = 0; i < nItem; i++)
            {
                var npoint = br.ReadInt32();
                var data = new double[npoint, 2];
                for (int j = 0; j < npoint; j++)
                {
                    data[j, 0] = br.ReadDouble();
                    data[j, 1] = br.ReadDouble();
                }
                list.Add(data);
            }

            serializer.Serialize(jsonWriter, C1);

            jsonWriter.Close();
            br.Close();
        }

        static void JsonRead()
        {
            var fileStream = new FileStream("JSON.json", FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream);
            var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();

            C1 = (Class1)serializer.Deserialize(jsonReader, typeof(Class1));

            jsonReader.Close();
        }

        static void Main(string[] args)
        {
<<<<<<< HEAD
            Write();
            Read();
            JsonRead();
            /*
=======
>>>>>>> 21530afdf9f358aba027133dc30c0b34a2a89f72
            for (int i = 0; i < 100000000; i++)
            {
                Console.WriteLine("TEST STRING " + (i + 1));
            }
<<<<<<< HEAD
            */
=======
>>>>>>> 21530afdf9f358aba027133dc30c0b34a2a89f72
            //Console.WriteLine("계속하려면 아무 키나 누르십시오.");
            //Console.ReadKey(true);
        }
    }
}
