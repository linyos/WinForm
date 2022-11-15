using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Text.Json;
using WinForm_Chart.Model;
using System.Windows.Forms;
using System.IO;

namespace WinForm_Chart.service
{
   public static class Method
    {


        public static List<Data> ReadJson(string path )
        {
            List<Data> source = new List<Data>();
            //string path = @"C:\SEN\Coding\C#\WinForm\WinForm_Chart\WinForm_Chart\test.json";
            using (StreamReader reader = new StreamReader(path))
            {
                string jsonText = reader.ReadToEnd();
                source = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(jsonText);
            }
            return source;
        }
    }
}
