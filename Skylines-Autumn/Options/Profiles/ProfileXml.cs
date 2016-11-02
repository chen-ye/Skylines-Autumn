using System;
using System.Xml.Serialization;

namespace DynamicFoliage.OptionsSpace.Profiles
{
    [XmlRoot("FoliageProfileContainer", IsNullable = false)]
    public class ProfileXml
    {
        [XmlArray("Profiles", IsNullable = false)]
        [XmlArrayItem("Profile", IsNullable = false)]
        public FoliageProfile[] m_containedProfiles = { new FoliageProfile() };

        [XmlIgnore]
        public string FileName => "CSL-DynamicFoliageProfiles.xml";
    }

    public class FoliageProfile
    {
        [XmlAttribute("ProfileName")]
        public string m_name = "Default";

        [XmlElement("DefaultParameters", IsNullable = false)]
        public DefaultParameters m_defaults = new DefaultParameters();

        [XmlArray("StaticAnnualVerdance", IsNullable = false)]
        [XmlArrayItem("Measure", IsNullable = false)]
        public VerdanceMeasure[] m_staticAnnualVerdance =
        { new VerdanceMeasure() };
    }

    public class DefaultParameters
    {
        [XmlElement("SeaLevel", IsNullable = false)]
        public float m_seaLevel = 0F;
    }

    public class VerdanceMeasure
    {
        [XmlElement("gMonthDay", IsNullable = false)]
        public DateTime DayMonth
        {
            get
            {
                DateTime date = new DateTime(2016,1,1);
                date.AddDays(dayOfYear - 1);
                return date;
            }
            set
            {
                dayOfYear = value.DayOfYear;
            }
        }

        [XmlElement("Verdance", IsNullable = false)]
        public float verdance = 1F;

        [XmlIgnore]
        public int dayOfYear;
    }


}
