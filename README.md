# Skylines-Autumn

Source code for a Dynamic Foliage Mod for Cities Skylines.

Allow trees to change color based on the season and other factors.

[Source code](https://github.com/chen-ye/Skylines-Autumn)
[Workshop link](http://steamcommunity.com/sharedfiles/filedetails/?id=791968744)

## Configuration
In this release, custom static seasonal profiles are supported (verdance is specified at specific times of year, and is interpolated between these values, and is unaffected by altitude and temperature). Add them by editing `%programfiles(x86)%\Steam\steamapps\common\Cities_Skylines\CSL-DynamicFoliageProfiles.xml`, which is generated after the mod is first run.

This mod does not affect saves. To uninstall the mod, simply disable it.

## Compatibility and Companion Mods
### Trees
All default trees are supported. Some custom trees in the workshop are supported, and some don't exhibit any change, though I haven't yet found a technical reason for why or why not. As in the example screenshots, this effect can be exploited to make mixed evergreen-deciduous forests - certain conifers, like [this one](http://steamcommunity.com/sharedfiles/filedetails/?id=449133254) will not be impacted by seasonal changes.
### Mods
This is obviously not an exhaustive list.
#### Compatible
This mod is compatible with [Rush Hour's](http://steamcommunity.com/sharedfiles/filedetails/?id=605590542) modified time progression.

This mod is a great companion to [Climate Control](http://steamcommunity.com/sharedfiles/filedetails/?id=629713122), which adds more seasonal gameplay effects (such as temperature), allows using rainfall profiles, and changes the rain effect to snow when it drops below 0 (no snow accumulation though :-( ). Be warned that the workshop version of Climate Control is not compatible with Rush Hour's modified time progression however!

#### Incompatible
This mod is incompatible with [No Radioactive Desert and More](http://steamcommunity.com/sharedfiles/filedetails/?id=666425898) mod, because it overrides the same codepaths. But settings for disabling the polluted tree and shoreline tree effect are available. If there is sufficient demand, I can also include the other settings, but you should consider alternatives such as [No More Purple Pollution](http://steamcommunity.com/workshop/filedetails/?id=407865240).


## Changelog
### v1.0:
Lots of various bugfixes. Support static seasonal change profiles.

### v0.1:
Initial functional build. Supports hardcoded seasonal change.

## Credits
Code from [Climate Control](https://steamcommunity.com/sharedfiles/filedetails/?id=629713122) and [No Radioactive Desert and More](https://steamcommunity.com/sharedfiles/filedetails/?id=666425898) is used.

[Fall Tree icon](https://thenounproject.com/dobrik/collection/seasons/?oq=season&cidx=0&i=315008) by Eugene Dobrik from the Noun Project.