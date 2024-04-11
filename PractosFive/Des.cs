using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace PractosFive
{
    internal class Des
    {
        public static T DeserializeObject<T>()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";
            if(dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
            {
                return default(T);
            }
        }
            
    }
}
