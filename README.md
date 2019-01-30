# Yapped
An editor for DS3 game params, which determine the properties of items, attacks, effects, enemies, objects, and a whole lot more. For detailed instructions, please refer to the included readme.  
Requires [.NET 4.7.2](https://www.microsoft.com/net/download/thank-you/net472) - Windows 10 users should already have this.  
[Nexus Page](https://www.nexusmods.com/darksouls3/mods/298)  

# Warning
As far as we know, *any* edits to the regulation file (where params are stored) will trigger anticheat, including simply opening it and resaving it.  
Only use modified params in offline mode. Back up your save file and restore it before going online again if you're doing anything that could affect it.  

# Changelog
### 1.1
* Ctrl+Shift+N: Duplicate selected row
* Ctrl+F: Search for row by name
* Ctrl+G: Go to row by ID
* Ctrl+Shift+F: Search for field by name
* Unused params are hidden by default
* Creating a new row has a nice dialog now
* Updated layouts for several params (thanks Pav)
* Updated names for several params (thanks GiveMeThePowa and Xylozi)
* Added brief descriptions for params on mouse-over (thanks Pav)
* Added support for field descriptions, but didn't actually write any yet

### 1.0.2
* Locales that use a comma for the decimal point are now supported
* Selected row and visible cells are now remembered for each param separately

### 1.0.1
* Backup actually works now. If you've already modified something, verify your game files through Steam. Sorry!

# Credits
**Pav** - Layouts  
**TKGP** - Application  
**GiveMeThePowa, Xylozi** - Contributing row names

# Libraries
[Costura.Fody](https://github.com/Fody/Costura) by Simon Cropp, Cameron MacFarland

[Octokit](https://github.com/octokit/octokit.net) by GitHub

[Semver](https://github.com/maxhauser/semver) by Max Hauser

[SoulsFormats](https://github.com/JKAnderson/SoulsFormats) by Me