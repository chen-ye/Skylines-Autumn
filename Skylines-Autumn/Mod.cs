using ICities;
using DynamicFoliage.OptionsFramework;

namespace DynamicFoliage
{
    public class Mod : IUserMod
    {
        public string Name => "Skylines Dynamic Foliage";

        public string Description => "Allow trees to change color based on the season.";

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddOptionsGroup<Options>();
        }
    }
}
