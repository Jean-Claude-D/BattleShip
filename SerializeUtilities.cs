using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp1
{
    class SerializeUtilities<T>
    {
        private static IFormatter formatter = new BinaryFormatter();

        //Prevent instantiation since SerializeUtilities is a utilities class
        private SerializeUtilities() { }

        //Serialize the given object obj of type T into an overwritten file fileName
        public static void Serialize(T obj, string fileName)
        {
            if(obj == null)
            {
                throw new ArgumentException("The object parameter obj must be non-null");
            }
            else if(fileName == null || fileName.Trim().Length == 0)
            {
                throw new ArgumentException("The string parameter fileName must be non-null");
            }

            if(!obj.GetType().IsSerializable)
            {
                throw new ArgumentException("The object parameter " + obj.ToString() + " must be Serializable");
            }

            //Create the stream indicating the location where the serializaed obj should be saved
            Stream fileStream = new FileStream
                (fileName, FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                //Serialize
                formatter.Serialize(fileStream, obj);
            }
            //close the stream to avoid resource leak
            finally
            {
                if(fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        //Deserialize an object of type T from file fileName
        public static T Deserialize(string fileName)
        {
            if (fileName == null || fileName.Trim().Length == 0)
            {
                throw new ArgumentException("The string parameter fileName must be non-null");
            }

            //Create the stream indicating the location from where to deserialize
            Stream fileStream = new FileStream
                (fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            T toReturn;

            try
            {
                //Deserialize
                object obj = formatter.Deserialize(fileStream);

                //ensure deserialized object is of type T
                if(obj.GetType() != typeof(T))
                {
                    throw new ArgumentException("The deserialized object " + obj.ToString() + " is not of type T : " + typeof(T).ToString());
                }

                toReturn = (T)obj;

            }
            //close the stream to avoid resource leak
            finally
            {
                if(fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return toReturn;
        }
    }
}
