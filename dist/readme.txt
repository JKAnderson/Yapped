
--| Yapped 1.1
--| https://www.nexusmods.com/darksouls3/mods/298
--| https://github.com/JKAnderson/Yapped

An editor for DS3 game params, which determine the properties of items, attacks, effects, enemies, objects, and a whole lot more.
Requires .NET 4.7.2 - Windows 10 users should already have this.
https://www.microsoft.com/net/download/thank-you/net472


--| Warning

As far as we know, *any* edits to the regulation file (where params are stored) will trigger anticheat, including simply opening it and resaving it.
Only use modified params in offline mode. Back up your save file and restore it before going online again if you're doing anything that could affect it.


--| Instructions

Yapped will try to load the regulation from the default Steam location when it starts. If you have the game installed somewhere else, use File->Open to find Data0.bdt in your game directory.
The different params will be displayed in the left pane. Clicking one will display the rows within it in the center pane. Clicking a row will display its values in the right pane.
Rows can be created or deleted from the Edit menu, or with the hotkeys indicated there. By default you will be prompted when deleting a row; uncheck the option in the Edit menu if you don't want that.
Row ID and name can be edited at will; the name has no functional significance. Vanilla DS3 has its row names stripped out; use Edit->Import Names to populate them with names from the network test (which aren't necessarily accurate) and names inferred from text files (which are).
Be aware of the type when editing values. If you enter an invalid value you will receive an error; either correct it, or press Escape to cancel. Types are described below.

b8, b32 - true or false
fixstr, fixstrW - text
f32 - real number

u8 - unsigned integer, 0 - 255
x8 - hex integer, 0x00 - 0xFF
s8 - signed integer, -128 - 127
u16 - unsigned integer, 0 - 65,535
x16 - hex integer, 0x0000 - 0xFFFF
s16 - signed integer, -32,768 - 32,767
u32 - unsigned integer, 0 - 4,294,967,295
x32 - hex integer, 0x00000000 - 0xFFFFFFFF
s32 - signed integer, -2,147,483,648 - 2,147,483,647

After editing rows or values, save the regulation with File->Save. The original file will be backed up automatically and can be restored with File->Restore.
Because the regulation is only loaded once when the game boots, changes will only take effect after shutting down the game completely and restarting it.


--| Changelog

1.1
	Ctrl+Shift+N: Duplicate selected row
	Ctrl+F: Search for row by name
	Ctrl+G: Go to row by ID
	Ctrl+Shift+F: Search for field by name
	Unused params are hidden by default
	Creating a new row has a nice dialog now
	Updated layouts for several params (thanks Pav)
	Updated names for several params (thanks GiveMeThePowa)
	Added brief descriptions for params on mouse-over (thanks Pav)
	Added support for field descriptions, but didn't actually write any yet

1.0.2
	Locales that use a comma for the decimal point are now supported
	Selected row and visible cells are now remembered for each param separately

1.0.1
	Backup actually works now. If you've already modified something, verify your game files through Steam. Sorry!


--| Credits

Pav - Layouts
TKGP - Application
GiveMeThePowa - Contributing row names


--| Libraries

Costura.Fody by Simon Cropp, Cameron MacFarland
https://github.com/Fody/Costura

Octokit by GitHub
https://github.com/octokit/octokit.net

Semver by Max Hauser
https://github.com/maxhauser/semver

SoulsFormats by Me
https://github.com/JKAnderson/SoulsFormats
