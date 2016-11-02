using System.Xml.Serialization;

namespace DynamicFoliage.OptionsSpace.OptionsFramework
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