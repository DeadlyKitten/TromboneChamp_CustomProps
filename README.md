# Trombone Champ Custom Props
Simple plugin that allows the addition of hats, masks, and other props to your Tromboner.

# Installation

Manual Installation
If your game isn't modded with BepinEx, DO THAT FIRST! Simply go to the [latest BepinEx release](https://github.com/BepInEx/BepInEx/releases/latest) and extract `BepinEx_x64_<version>.zip` directly into your game's folder, then run the game once to install BepinEx properly.

Then, go to the [latest release](https://github.com/DeadlyKitten/TromboneChamp_CustomProps/releases/latest) of this mod and place the dll into `TromboneChamp/BepInEx/plugins`.

# Usage

This mod has no configuration, simply install props (designated with the `.prop` extension) to `TromboneChamp/BepInEx/CustomProps`.

# For Prop Makers

Download the SDK from the [releases tab](https://github.com/DeadlyKitten/TromboneChamp_CustomProps/releases/latest) to get started creating your own custom props.

You can find instructions on how to do so [here](https://github.com/DeadlyKitten/TromboneChamp_CustomProps/blob/master/Custom%20Props-Project/README.md).

# For Developers

### Contributing to Custom Props
Custom Props is written in C# using .NET Framework 4.7.2

Before building, you'll have to do a bit of setup to the project after cloning.

In the `CustomProps` folder, add a file named `CustomProps.csproj.user` with the following contents.  

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current">
  <PropertyGroup>
    <GameDir>PATH TO YOUR TROMBONECHAMP DIRECTORY</GameDir>
  </PropertyGroup>
</Project>
```

In the `CustomProps-Editor` folder, add a file named `CustomProps-Editor.csproj.user` with the following contents.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current">
  <PropertyGroup>
    <GameDir>PATH TO YOUR TROMBONECHAMP DIRECTORY</GameDir>
    <ProjectDir>PATH TO CUSTOM PROPS-PROJECT DIRECTORY</ProjectDir>
  </PropertyGroup>
</Project>
```

Once you've done this, you should be able to Build (Build > Build Solution or <kbd>CTRL</kbd> + <kbd>SHIFT</kbd> + <kbd>B</kbd>) the solution. This will trigger a build of both projects and copy the plugin dll to `TromboneChamp/BepInEx/plugins` and the editor dll to the Unity Project's SDK folder.

# Disclaimer
This product is not affiliated with [Trombone Champ](https://store.steampowered.com/app/1059990/Trombone_Champ/) or [Holy Wow Studios](http://www.holywowstudios.com/) and is not endorsed or otherwise sponsored by Holy Wow Studios. Portions of the materials contained herein are property of Holy Wow Studios. ©2022 Holy Wow Studios.
