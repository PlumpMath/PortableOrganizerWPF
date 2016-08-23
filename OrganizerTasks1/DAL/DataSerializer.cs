using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OrganizerTasks1.Model;

namespace OrganizerTasks1.DAL
{
    public class DataSerializer
    {
        public static void SerializeData(string fileName, DataModel data)
        {
            var formatter = new BinaryFormatter();
            using (var s = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(s, data);
                s.Close();
            }
        }

        public static DataModel DeserializeItem(string fileName)
        {
            using(var s = new FileStream(fileName, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (DataModel)formatter.Deserialize(s);
            }
        }
    }
}
