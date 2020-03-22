using BusinessLayer.Utility;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;
using XSerializer;

namespace DataAccessLayer.DataAccess
{
    public class Database
    {
        public List<Category> Categories { get; private set; }

        private readonly string DefaultDatabaseFile = Environment.CurrentDirectory + "\\" + "ddb.xml";

        public string CustomDatabaseFile { get; private set; }

        public Database()
        {
            CustomDatabaseFile = Environment.CurrentDirectory + "\\" + "CustomDatabase.xml";
            if (File.Exists(CustomDatabaseFile))
            {
                LoadDatabase(DatabaseType.CustomFile, CustomDatabaseFile);
            }
            else if (File.Exists(DefaultDatabaseFile))
            {
                LoadDatabase(DatabaseType.DefaultFile);
            }
        }

        public Database(DatabaseType type, string customDatabaseFile = null)
        {
            LoadDatabase(type, customDatabaseFile);
        }

        private void LoadDatabase(DatabaseType type, string customDatabaseFile = null)
        {
            Logger Logger = new Logger(Environment.CurrentDirectory + "\\" + "DatabaseLog.txt");
            Logger.LogInformation($"Database loading type: {type}.");
            CustomDatabaseFile = Environment.CurrentDirectory + "\\" + "CustomDatabase.xml";
            if (type == DatabaseType.DefaultFile)
            {
                Categories = GetCategories();
            }
            else if (type == DatabaseType.CustomFile && !string.IsNullOrEmpty(customDatabaseFile))
            {
                CustomDatabaseFile = customDatabaseFile;
                try
                {
                    Categories = Deserialize();
                }
                catch (Exception)
                {
                    Logger.LogInformation($"Database loading failed. Trying to load default database.");
                    Categories = GetCategories();
                }
                Logger.LogInformation($"Deserialization database loading successfully completed.");
            }
            else
            {
                Categories = Deserialize();
            }
            Logger.LogInformation($"Database successfully loaded.");
            Logger.Close();
        }

        public void Serialize()
        {
            XmlSerializer<List<Category>> serializer = new XmlSerializer<List<Category>>();
            FileStream stream = new FileStream(CustomDatabaseFile, FileMode.Create);
            XmlWriterSettings writerSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace   
            };
            XmlWriter xmlWriter = XmlWriter.Create(stream, writerSettings);
            XmlDocument document = new XmlDocument();
            document.LoadXml(serializer.Serialize(Categories));
            document.Save(xmlWriter);
            stream.Close();
        }

        private List<Category> Deserialize()
        {
            List<Category> categories;
            XmlSerializer<List<Category>> serializer = new XmlSerializer<List<Category>>();
            FileStream stream = new FileStream(CustomDatabaseFile, FileMode.Open);
            categories = serializer.Deserialize(stream);
            stream.Close();
            return categories;
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            Category tempCategory = null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(DefaultDatabaseFile);
            XmlElement xmlElement = xmlDoc.DocumentElement;
            foreach (XmlNode xmlNode in xmlElement)
            {
                if (string.Equals(xmlNode.Name, "Category"))
                {
                    XmlNode nameAttribute = xmlNode.Attributes.GetNamedItem("name");
                    XmlNode descriptionAttribute = xmlNode.Attributes.GetNamedItem("description");
                    if (nameAttribute.Value != null && descriptionAttribute.Value != null)
                    {
                        tempCategory = new Category(nameAttribute.Value, descriptionAttribute.Value);
                    }
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        tempCategory.AddProduct(GetProduct(child));
                    }
                }
                categories.Add(tempCategory);
            }
            return categories;
        }

        private Product GetProduct(XmlNode child)
        {
            return new Product(child.ChildNodes[0].InnerText, Convert.ToInt32(child.ChildNodes[1].InnerText),
                Convert.ToDouble(child.ChildNodes[2].InnerText), Convert.ToDouble(child.ChildNodes[3].InnerText),
                Convert.ToDouble(child.ChildNodes[4].InnerText), Convert.ToDouble(child.ChildNodes[5].InnerText));
        }
    }
}
