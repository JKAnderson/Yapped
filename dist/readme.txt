
--| Yapped Beta 1.1.2
--| https://www.nexusmods.com/darksouls3/mods/298
--| https://github.com/JKAnderson/Yapped

An editor for Dark Souls 3 and Sekiro param files, which determine the properties of items, attacks, effects, enemies, objects, and a whole lot more.
Requires .NET 4.7.2 - Windows 10 users should already have this.
https://www.microsoft.com/net/download/thank-you/net472
In order to open Sekiro files, you must also copy oo2core_win64.dll from your Sekiro install into Yapped's lib folder.


--| Warning

As far as we know, in DS3 *any* edits to the regulation file (where params are stored) will trigger anticheat, including simply opening it and resaving it.
Only use modified params in offline mode. Back up your save file and restore it before going online again if you're doing anything that could affect it.


--| Instructions

By default, Yapped runs in Sekiro mode; if you want to work with DS3 params, switch to Dark Souls 3 in the File menu.
When you first run Yapped, it will attempt to load the default Steam location for Sekiro's game params. If this isn't correct, use File->Open to find your file.
For DS3, params are stored in Data0.bdt in the main game directory. For Sekiro, params can be found in param\gameparam\gameparam.parambnd.dcx after unpacking the game with UXM.

The different params will be displayed in the left pane. Clicking one will display the rows within it in the center pane. Clicking a row will display its values in the right pane.
Rows can be created or deleted from the Edit menu, or with the hotkeys indicated there. By default you will be prompted when deleting a row; uncheck the option in the Edit menu if you don't want that.
Row ID and name can be edited at will; the name has no functional significance. Vanilla DS3 and Sekiro have their row names stripped out; use Edit->Import Names to populate some of them with predetermined names.

After editing your params, save them with File->Save. The original file will be backed up automatically and can be restored with File->Restore.
For Sekiro, params should be applied with Mod Engine like other mods. Params are only loaded once when the game starts, so you must restart after changing anything.


--| Value Types

Each value in a row has a certain type which determines the kind of data it can hold. If you enter an invalid value you will receive an error message; either correct it, or press Escape to cancel. These types are described below.

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


--| Changelog

1.1.2
	Beta for Sekiro support

1.1.1
	Fix name in create row dialog not doing anything
	Fix duplicated rows being unsaveable sometimes

1.1
	Ctrl+Shift+N: Duplicate selected row
	Ctrl+F: Search for row by name
	Ctrl+G: Go to row by ID
	Ctrl+Shift+F: Search for field by name
	Unused params are hidden by default
	Creating a new row has a nice dialog now
	Updated layouts for several params (thanks Pav)
	Updated names for several params (thanks GiveMeThePowa and Xylozi)
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
GiveMeThePowa, Xylozi - Contributing row names


--| Libraries

Octokit by GitHub
https://github.com/octokit/octokit.net

Semver by Max Hauser
https://github.com/maxhauser/semver

SoulsFormats by Me
https://github.com/JKAnderson/SoulsFormats
