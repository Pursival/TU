using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSecuritySystem 
{
    [Serializable]
    public class ButtonSerialisation:Button
    {
        
       
            public string BtnName;
            public Point BtnLocation;
            public Color BtnColor;
            public Size BtnSize;

            public static void Save(ButtonSerialisation button, string filePath)
            {
                using (StreamWriter file = File.CreateText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, button);//if file not exists create one empty else
                                                       //load data from file append at end then save
                }
            }
            //do a deletion of a button function
            public static Button Load(string filePath)
            {
            ButtonSerialisation button = null;

                using (StreamReader file = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    button = (ButtonSerialisation)serializer.Deserialize(file, typeof(ButtonSerialisation));
                }
            var btt = (Button)button;
            return btt;
            
            }
            void button_Click(object sender, System.EventArgs e)
            {
            //your stuff...
            }
    }
}