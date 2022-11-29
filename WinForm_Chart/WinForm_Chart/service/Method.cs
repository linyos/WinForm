using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Text.Json;
using WinForm_Chart.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.IO;

namespace WinForm_Chart.service
{
    /// <summary>
    ///  之後改成DI 方式注入
    /// </summary>
   public static class Method
    {
        private static string path = Directory.GetCurrentDirectory().ToString() +"\\test.json";
        
        /// <summary>
        /// 單純Read json file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Data> ReadJson(string path)
        {
            List<Data> data1;
            using (StreamReader file = File.OpenText(path))
            {
                Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                data1 = (List<Data>)jsonSerializer.Deserialize(file, typeof(List<Data>));
            }
            
            return data1;
        }


        public static void  ReviseJson(int idx, int id)
        {

           
            string json = File.ReadAllText(path);
            //dynamic jsonObj = new Data();
            dynamic jsonObj = new List<Data>();
            JsonConvert.PopulateObject(json, jsonObj);
            jsonObj[idx].Id = id;
            using (StreamWriter file = File.CreateText(path))
            {

                Newtonsoft.Json.JsonSerializer jsonSerializer1 = new Newtonsoft.Json.JsonSerializer();
                jsonSerializer1.Serialize(file, jsonObj);

            }
        }

 

        public static List<T> ReadJsonType <T>(string path)
        {
            string fileName = path;
            string jsonString= File.ReadAllText(fileName);
            var objResponse1 = JsonDeserialize<T>(jsonString);
            return objResponse1;
            
        }

        public static List<T> JsonDeserialize<T>(this string SerializedJSONString)
        {
            var stuff = JsonConvert.DeserializeObject<List<T>>(SerializedJSONString);
            return stuff;
        }


    }
}
