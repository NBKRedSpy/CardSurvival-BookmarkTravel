
![Alt text](media/Card%20Image.png)

# Bookmark Travel
Quickly transition to a location or shelter by double clicking the bookmark for that card.  Combine with "Instant Transition" mod to move in and out of shelters instantly with a double tap.

Normally the user must find a location card, then click on that card, then confirm that dialog.  This mod does the same thing with a double tap of a bookmark key.

For example: To enter a shed quickly, set the shed to bookmark 3.  When tapping 3 twice quickly, the game will move into that shed.  

A single key press for the bookmark moves the view to the card, which is the normal game's bookmark operation.

The following options can be configured:
* Double press, single press, or holding down a bookmark key to travel.
* Immediately transit or show the card's dialog instead.
* Activation time
    * For double click, how quickly a key must be pressed twice to activate.
    * For hold mode, how long a key must be pressed to activate.

# Settings

|Name|Default|Description|
|--|--|--|
|DoubleClickMilliseconds|250|The maximum milliseconds for a key press to be considered a double click.  If the TravelMode is set to Hold, then this is how long the key must be held down to activate.|
|TravelMode|DoubleClick|Determines if a single click, double click, or holding a key will invoke a travel.  See Travel Mode Options below.|
|ShowCardDialog|false|If true, will show the information dialog for the card instead of traveling.  If false, will instantly travel.|


## Travel Mode Options

|Name|Description|
|--|--|
|SingleClick|Pressing a key will immediately travel.|
|DoubleClick|Double pressing a key within the DoubleClickMilliseconds value will travel.|
|Hold|Continuously holding a key for DoubleClickMilliseconds will travel.|


# Changing the Configuration
All options are contained in the config file which is located at ```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx\config\BookmarkTravel.cfg```.

The .cfg file will not exist until the mod is installed and then the game is run.

To reset the config, delete the config file.  A new config will be created the next time the game is run.

# Installation 
This section describes how to manually install the mod.

If using the Vortex mod manager from NexusMods, these steps are not needed.  

## Overview
This mod requires the BepInEx mod loader.

## BepInEx Setup
If BepInEx has already been installed, skip this section.

Download BepInEx from https://github.com/BepInEx/BepInEx/releases/download/v5.4.21/BepInEx_x64_5.4.21.0.zip

* Extract the contents of the BepInEx zip file into the game's directory:
```<Steam Directory>\steamapps\common\Card Survival Tropical Island```

    __Important__:  The .zip file *must* be extracted to the root folder of the game.  If BepInEx was extracted correctly, the following directory will exist: ```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx```.  This is a common install issue.

* Run the game.  Once the main menu is shown, exit the game.
    
* In the BepInEx folder, there will now be a "plugins" directory.

## Mod Setup
* Download the CardSurvival-BookmarkTravel.zip.  
    * If on Nexumods.com, download from the Files tab.
    * Otherwise, download from https://github.com/NBKRedSpy/CardSurvival-CardSurvival-BookmarkTravel/releases/

* Extract the contents of the zip file into the ```BepInEx/plugins``` folder.

* Run the Game.  The mod will now be enabled.

# Uninstalling

## Uninstall
This resets the game to an unmodded state.

Delete the BepInEx folder from the game's directory
```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx```

## Uninstalling This Mod Only

This method removes this mod, but keeps the BepInEx mod loader and any other mods.

Delete the ```CardSurvival-BookmarkTravel.dll``` from the ```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx\plugins``` directory.

# Compatibility
Safe to add and remove from existing saves.

# Credits
<a href="https://www.flaticon.com/free-icons/number" title="number icons">Number icons created by Freepik - Flaticon</a>

# Change Log 

## 1.2.0
* Supports holding a key.
* Supports upgrading previous config to new TravelMode option.

## 1.1.0
* Fixed bookmark allowing travel when not valid.  At night with no light, overloaded, etc.

## 1.0.0
* Release
