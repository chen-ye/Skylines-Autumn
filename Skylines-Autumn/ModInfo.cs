using DynamicFoliage.OptionsSpace.OptionsFramework;
using DynamicFoliage.OptionsSpace.Profiles;
using ICities;
using DynamicFoliage.OptionsSpace;
using UnityEngine;

namespace DynamicFoliage
{
    public class ModInfo : IUserMod
    {
        public string Name => "Skylines Dynamic Foliage";

        public string Description => "Allow trees to change color based on the season.";

        public void OnSettingsUI(UIHelperBase helper)
        {
            ProfileIO.LoadProfiles(); //test
            helper.AddOptionsGroup<Options>();

#if DEBUG
            Debug.Log(OptionsWrapper<Options>.Options.profile);
#endif
        }
    }
}
