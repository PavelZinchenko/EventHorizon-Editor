# EventHorizon Editor

## How to use:

1. Download and extract Release.zip.
2. Run GameDatabase.exe and open the database.
3. Click Create Mod and enter the name.
4. Copy the Mod file to the folder "\<Game Installation Folder\>/Mods" for PC or "Android/data/com.ZipasGames.EventHorizon/files/Mods" for Android.
5. Open the game settings and select your Mod.

Since 0.15.3 you can also put the whole database into the Mods folder. This allows you to edit your ship builds in game editor.

## Images

Images can be placed anywere in the /Database/ folder. They must be a square size - 128x128, 256x256, 512x512, 1024x1024 etc. Only PNG and JPEG formats are supported.

## Localization

Localization files should be named according to the language - <language>.xml (e.g. English.xml, Russian.xml)

```
<?xml version="1.0" encoding="UTF-8"?>
<resources>
<string name="Module1">Small bomb</string>
<string name="Module2">Big bomb</string>
</resources>
```
