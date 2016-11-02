using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace DynamicFoliage.OptionsSpace.Profiles
{
    class ProfileIO
    {
        private static Dictionary<string, FoliageProfile> loadedProfiles;

        public const string fileName = "CSL-DynamicFoliageProfiles.xml";
        private const string defaultXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<FoliageProfileContainer xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Profiles>\r\n    <Profile ProfileName=\"Default (New York)\">\r\n      <DefaultParameters>\r\n        <SeaLevel>0</SeaLevel>\r\n      </DefaultParameters>\r\n      <StaticAnnualVerdance>\r\n        <Measure>\r\n          <gMonthDay>--03-01</gMonthDay>\r\n          <Verdance>.2</Verdance>\r\n        </Measure>\r\n        <Measure>\r\n          <gMonthDay>--04-01</gMonthDay>\r\n          <Verdance>1</Verdance>\r\n        </Measure>\r\n        <Measure>\r\n          <gMonthDay>--07-01</gMonthDay>\r\n          <Verdance>1</Verdance>\r\n        </Measure>\r\n        <Measure>\r\n          <gMonthDay>--09-09</gMonthDay>\r\n          <Verdance>.6</Verdance>\r\n        </Measure>\r\n        <Measure>\r\n          <gMonthDay>--09-16</gMonthDay>\r\n          <Verdance>.5</Verdance>\r\n        </Measure>\r\n        <Measure>\r\n          <gMonthDay>--10-01</gMonthDay>\r\n          <Verdance>.4</Verdance>\r\n        </Measure>\r\n          <Measure>\r\n          <gMonthDay>--11-01</gMonthDay>\r\n          <Verdance>.2</Verdance>\r\n        </Measure>\r\n    </StaticAnnualVerdance>\r\n    </Profile>\r\n  </Profiles>\r\n</FoliageProfileContainer>";

        public static Dictionary<string, FoliageProfile> LoadedProfiles
        {
            get
            {
                if (loadedProfiles == null)
                {
                    LoadProfiles();
                }
                return loadedProfiles;
            }
        }

        public static string[] LoadedProfileNames
        {
            get
            {
                var keys = LoadedProfiles.Keys;
                string[] keysArray = new string[keys.Count];
                Debug.Log("Loaded Num Keys: " + keys.Count.ToString());
                keys.CopyTo(keysArray, 0);
                return keysArray;
            }
        }

        public static void LoadProfiles()
        {
            try
            {
                loadedProfiles = new Dictionary<string, FoliageProfile>();
                var xmlSerializer = new XmlSerializer(typeof(ProfileXml));
                ProfileXml profileContainer; // = (ProfileXml)Activator.CreateInstance(typeof(ProfileXml));
                
                xmlSerializer.UnknownAttribute += Serializer_UnknownAttribute;
                xmlSerializer.UnknownElement += Serializer_UnknownElement;
                xmlSerializer.UnknownNode += Serializer_UnknownNode;
                xmlSerializer.UnreferencedObject += Serializer_UnreferencedObject;


                try
                {
                    //var fileName = profileContainer.FileName;
                    //if (!fileName.EndsWith(".xml"))
                    //{
                    //    fileName = fileName + ".xml";
                    //}
                    using (var textReader = new StreamReader(fileName))
                    {

                        profileContainer = xmlSerializer.Deserialize(textReader) as ProfileXml;
                        
                        //foreach (var propertyInfo in typeof(ProfileXml).GetProperties())
                        //{
                        //    if (!propertyInfo.CanWrite)
                        //    {
                        //        continue;
                        //    }
                        //    var value = propertyInfo.GetValue(options, null);
                        //    propertyInfo.SetValue(profileContainer, value, null);
                        //}
                    }
                }
                catch (FileNotFoundException)
                {
                    Debug.LogError("Profile XML not found. Using defaults.");
                    profileContainer = (ProfileXml)xmlSerializer.Deserialize(new StringReader(defaultXml));
                    WriteDefaultProfiles();
                }

                foreach(FoliageProfile foliageProfile in profileContainer.m_containedProfiles)
                {
                    loadedProfiles[foliageProfile.m_name] = foliageProfile;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
        }

        private static void XmlSerializer_Error(object sender, EventArgs e)
        {
            Debug.LogError("XMLSerializer Error: " + e.ToString());
        }

        private static void Serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            Debug.LogError("Unknown Attribute");
            Debug.LogError("\t" + e.Attr.Name + " " + e.Attr.InnerXml);
            Debug.LogError("\t LineNumber: " + e.LineNumber);
            Debug.LogError("\t LinePosition: " + e.LinePosition);
        }

        private static void Serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            Debug.LogError("Unknown Element");
            Debug.LogError("\t" + e.Element.Name + " " + e.Element.InnerXml);
            Debug.LogError("\t LineNumber: " + e.LineNumber);
            Debug.LogError("\t LinePosition: " + e.LinePosition);
        }

        private static void Serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Debug.LogError("UnknownNode Name: " + e.Name);
            Debug.LogError("UnknownNode LocalName: " + e.LocalName);
            Debug.LogError("UnknownNode Namespace URI: " + e.NamespaceURI);
            Debug.LogError("UnknownNode Text: " + e.Text);
        }

        private static void Serializer_UnreferencedObject(object sender, UnreferencedObjectEventArgs e)
        {
            Debug.LogError("UnreferencedObject:");
            Debug.LogError("ID: " + e.UnreferencedId);
            Debug.LogError("UnreferencedObject: " + e.UnreferencedObject);
        }

        private static void WriteDefaultProfiles()
        {
            try
            {
                using (var streamWriter = new StreamWriter(fileName))
                {
                    streamWriter.Write(defaultXml);
                    Debug.Log("New profile xml written");
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
    }
}
