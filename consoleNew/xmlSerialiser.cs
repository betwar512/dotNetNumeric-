using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace consoleNew
{
    public class xmlSerialiser
    {
        /*
         * xml serilizer method to create xml file from our text file typeOf (dataModel)
         *methos 2 input 
         *path= text file path
         *xmlPath = xml file path 
         */
        public void seriliser(string path, string xmlPath)
        {

            List<DataModel> myList = new List<DataModel>();
            //int counter = 0;
            //string line;

            // Read the file all the lines
            string[] lines = File.ReadAllLines(path);
            //create file 
            //System.IO.StreamWriter file = new System.IO.StreamWriter(
            //          @"d:\temp\newXml.xml");

            foreach (string s in lines)
            {
                var temp = new List<string>();
                string[] x = s.Split(null);
                foreach (var z in x)
                {
                    //delete space in Array
                    if (!string.IsNullOrEmpty(z))
                        temp.Add(z);
                }
                x = temp.ToArray();


                DataModel myObject = new DataModel();
                //  CID Z ZERR MU MUERR AV AVERR DELTA DELTAERR PKMJD PKMJDERR RV RVERR CHI2 NDOF IDTEL
                myObject.CID = x[1];
                myObject.Z = x[2];
                myObject.ZERR = x[3];
                myObject.MU = x[4];
                myObject.MUERR = x[5];
                myObject.AV = x[6];
                myObject.AVERR = x[7];
                myObject.DELTA = x[8];
                myObject.DELTAERR = x[9];
                myObject.PKMJD = x[10];
                myObject.PKMJDERR = x[11];
                myObject.RV = x[12];
                myObject.RVERR = x[13];
                myObject.CHI2 = x[14];
                myObject.NDOF = x[15];
                myObject.IDTEL = x[16];

                ////write to the file from model 
                //    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(DataModel), new XmlRootAttribute("slabs"));
                //    writer.Serialize(file, myObject);
                myList.Add(myObject);

            }//end of for loop
            //close file 
            //file.Close();
            //create the serialiser to create the xml
            using (var stream = File.Create(xmlPath))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(List<DataModel>));
                // Create the TextWriter for the serialiser to use
                serialiser.Serialize(stream, myList);
            }

        }
        /*
         * Deserializer for DataModel -> xml to Array list in memory 
         *path == xml file path
         */
        public List<DataModel> deserilizer(string path)
        {
            //   DataModel myO;
            List<DataModel> myObjects = null;
            // Construct an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<DataModel>));
            // read file
            StreamReader reader = new StreamReader(path);
            // Call the Deserialize method and cast to the object type.
            myObjects = (List<DataModel>)mySerializer.Deserialize(reader);
            return myObjects;
        }

    }
}
