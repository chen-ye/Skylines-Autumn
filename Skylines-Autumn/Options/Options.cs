using System.Xml.Serialization;
using DynamicFoliage.OptionsSpace.OptionsFramework;
using System.Collections.Generic;
using DynamicFoliage.OptionsSpace.Profiles;

namespace DynamicFoliage.OptionsSpace
{


    public class Options : IModOptions
    {
        public Options()
        {
            enableSeasons = true;
            enableShore = true;
            enablePollution = true;

            profile = "Default (New York)";
            updateFrequency = "8";
        }

        [DynamicDropDown("Climate profile", nameof(ProfileIO), nameof(ProfileIO.LoadedProfileNames))]
        public string profile { set; get; }        

        [Checkbox("Enable seasonal foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool enableSeasons { set; get; }

        [Checkbox("Enable shoreline foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool enableShore { set; get; }
        [Checkbox("Enable polluted foliage and ground color effect", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool enablePollution { set; get; }

        [TextField("Foliage update frequency (in simulation hours)", nameof(DynamicFoliageEngine), nameof(DynamicFoliageEngine.SetUpdateFrequency))]
        public string updateFrequency { set; get; }

        [XmlIgnore]
        public string FileName => "CSL-DynamicFoliage.xml";
    }
}
