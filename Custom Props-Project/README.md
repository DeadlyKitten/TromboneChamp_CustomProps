# Custom Props Unity Project
A unity project to create your own props for the [Custom Props](https://github.com/deadlykitten/TromboneChamp_CustomProps) mod for Trombone Champ.

## Usage
This project is made with Unity version `2019.3.11f1`. Higher or lower Unity versions may not work properly, so make sure to download it from the [Unity Archive](https://unity3d.com/get-unity/download/archive) if you don't have it already. It's recommended to use Unity Hub to make managing versions easier.

To use this SDK you can either unzip and open the project or create a new project and install the unitypackage. Both can be found under the [Releases tab](https://github.com/deadlykitten/TromboneChamp_CustomProps/releases/latest).

Once you've opened the unity project, open the sample scene located at `SDK/Scenes/Prop Editor`.

## Creating a Prop
Creating a prop is pretty simple, but does required a bit of setup to make sure it fits all 4 character models used by the game. 

1. Create an empty GameObject, and add your model(s) as children. They should be aranged as such that the root object is at their collective pivot point. If you're unsure, just zero out their transforms (`1,1,1` for scale) and adjust to make them fit right together. 

2. Add a `Custom Prop` component to your root object. This component will be responsible for storing metadata about your prop as well as information on how to position, scale, and rotate it in-game.

3. Once you have your prop looking good on its own, it's time to get it looking good with the Tromboner. On the `Tromboners` object, make sure the `Active Tromboner` is set to Male 0. Then, open up the hierarchy for the active tromboner and drag your prop under the bone you want to attach it to.

4. Once your prop is set as a child of your desired bone, move, rotate, and scale it so that it looks good.

5. Select the `Tromboners` object again, and select the next `Active Tromboner` in the dropdown. Repeat steps 3 and 4 with each of the remaining tromboners.

6. Once you've finished with that, fill out the empty fields at the top of the `Custom Prop` component. A description of each field can be found below in the next section.

7. Once you have everything just the way you want it, click on the `Export Prop` button at the bottom of the `Custom Prop` component. Place it in your `BepInEx/CustomProps` directory and try it out in game!

## The `Custom Prop` Component

### These are the fields you will need to fill out yourself:
- **Prop Name** - The name of your prop. This will be the default filename that your prop exports with.
- **Author Name** - The name of the prop's creator. This should be your name!
- **License** - If you got your model(s) online and they came with an open-source license, be sure to include it here. You can also include your own license if you made them yourself!

### These fields should be filled out automatically as you put your prop together.
*Only modify them if you know what you're doing!*
- **Attach Bone** - This is the bone that your custom prop will parent itself to when spawned. This is the name of the GameObject, without the full path.
- **Position Offsets** - This is an array of **Vector3**'s storing localPosition offsets per tromboner.
- **Rotation Offsets** - This is an array of **Vector3**'s storing localRotation offsets per tromboner.
- **Scale Offsets** - This is an array of **Vector3**'s storing localScale offsets per tromboner.
