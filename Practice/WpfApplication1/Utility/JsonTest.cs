using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Utility
{
    public class JsonTest
    {
        public JObject MainJson;

        public void Save()
        {
            MainJson = new JObject();
            MainJson.Add(new JProperty("version", "1"));
            var sectorObj = new JObject();
            sectorObj.Add("center_x", "125");
            sectorObj.Add("center_y", "350");

            var districtObj = new JObject();

            var districtList = new JArray();
            districtList.Add("102931");
            districtList.Add("394835");
            districtList.Add("549845");
            districtList.Add("294393");

            districtObj.Add("list", districtList);

            MainJson.Add("sectors", sectorObj);
            MainJson.Add("districts", districtObj);

            var saveFileDialog = new SaveFileDialog() { Filter = "JSON File|*.json", Title = "Save JSON" };
            if (saveFileDialog.ShowDialog() == true)
            {
                var fileName = saveFileDialog.FileName;
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                var streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
                using (JsonTextWriter writer = new JsonTextWriter(streamWriter)
                {
                    Indentation = 2,
                    Formatting = Formatting.Indented
                })
                {
                    MainJson.WriteTo(writer);
                }
            }
        }

        public void Read()
        {
            var openFileDialog = new OpenFileDialog() { Filter = "JSON File|*.json", Title = "Load JSON" };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    MainJson = (JObject)JToken.ReadFrom(reader);
                }
                var sectorObj = MainJson["sectors"];
                var centerX = (int)sectorObj["center_x"];
                var centerY = (int)sectorObj["center_y"];

                var districtObj = MainJson["districts"];
                var r_list = (JArray)districtObj["list"];
                var list =  r_list.Select(c => (int)c).ToArray();

                var sectorObj0 = MainJson["sectors0"];
            }
        }
    }
}
