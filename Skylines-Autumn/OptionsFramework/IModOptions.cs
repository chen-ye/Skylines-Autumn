using System.Xml.Serialization;

namespace DynamicFoliage.OptionsFramework
{
    public interface IModOptions
    {
        [XmlIgnore]
        string FileName
        {
            get;
        }
    }
}