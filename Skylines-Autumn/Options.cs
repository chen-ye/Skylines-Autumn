using System.Xml.Serialization;
using DynamicFoliage.OptionsFramework;

namespace DynamicFoliage
{

    class Options : IModOptions
    {
        public Options()
        {
            season = true;
            shore = true;
            pollution = true;

            updateFrequency = 8;

            //seasonalCalcMethod = SeasonalCalcOption.Static;
        }

        public enum SeasonalCalcOption { Static }

        [Checkbox("Enable seasonal foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool season { set; get; }
        //public SeasonalCalcOption seasonalCalcMethod { set; get; }

        [Checkbox("Enable shoreline foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool shore { set; get; }
        [Checkbox("Enable polluted foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool pollution { set; get; }

        [TextField("Foliage update frequency (in simulation hours)", nameof(DynamicFoliageEngine), nameof(DynamicFoliageEngine.SetUpdateFrequency))]
        public double updateFrequency { set; get; }

        [XmlIgnore]
        public string FileName => "CSL-DynamicFoliage.xml";
    }
}
