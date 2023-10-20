# Active Mobility Web App

---

## Installation of Necessary Softwares/Toolkits/Packages/Dependencies/SDK

### Getting the required Assets
Git Clone / Pull or Download as Zip in the following GitHub Repository

### Installation of Unity Hub
Unity Hub is required to open or run any of the Unity Projects. You can install them by clicking [here](https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe) if not visit the official website [here](https://unity3d.com/get-unity/download)

### Choose your appropriate Unity Version that you need.
For this case we're using **2022.3.11f1** where you can find all the archive [here](https://unity3d.com/get-unity/download/archive)

### Choose the Visual Studio you prefer to use
Tested with VS Community 2022 and VS Commmunity 2019. You can find these [here](https://visualstudio.microsoft.com/downloads/)

Go to Visual Studio Installer and Download the Necessary Workloads Stated Below

### Unity 3rd Party Packages
- [SuperTiled2Unity](https://seanba.itch.io/supertiled2unity)
- [Leaderboard Package](https://danqzq.itch.io/leaderboard-creator/download/eyJleHBpcmVzIjoxNjk2OTExNzc2LCJpZCI6MTE1NDU0OX0%3d.6wJHD42kz3l3krzHELR%2bb38XtUE%3d)

### Installation of Map Editor and Pixel Art Creation Software
- [Tiled](https://www.mapeditor.org/)
- [Aseprite](https://www.aseprite.org/) or [pixel art](https://www.pixilart.com/draw) online

### Convert Image to Pixel Art
- [Pixel It](https://giventofly.github.io/pixelit/)

---
## Objective of the game

- The Active Mobility Game is designed to offer users invaluable insights into the nuances of road safety as they manoeuvre through the intricate web of lanes and navigate the ever-changing traffic lights in the vibrant streets of Singapore.

-	Within this immersive gaming experience, your mission is to safely reach your desired destination by adhering to a set of foundational traffic rules. Deviating from these rules will result in the loss of one precious life, so stay alert and follow the road code diligently.

-	Our goal for the game is to provide an engaging journey through a series of meticulously designed levels or stages. Each stage is designed to present you with distinct learning points and scenarios, ensuring that you acquire a comprehensive understanding of road safety as you progress.
  
---
## Architecture diagram
![Untitled Diagram drawio (4)](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/66571854-a017-46a6-ae1e-2121911cc501)

---
## Modules of the games

-	Menu Module
-	Objective Module
-	Map Module
-	Score Module
-	Gameplay Module
-	Leader board Module

---
## Setting up the Working Environment from Downloaded Asset

### Opening Unity Project

1) Open Unity Hub
2) Open Project -> Select Downloaded Project

## Setting up the Working Environment from Scratch

### Creating the Project

1) Open Unity Hub
2) New Project -> 2D (Core)

## Installing and Importing Packages in Unity

Install the [SuperTiled2Unity](https://seanba.itch.io/supertiled2unity) for Unity by Assets -> Import Package -> Custom Package -> Select [SuperTiled2Unity](https://seanba.itch.io/supertiled2unity)

If you've imported the game and encounter any errors related to the scene order, you can resolve them by navigating to Unity's Build Settings. There, you should ensure that the scene numbers are configured to load the first scene of the game, which in this case is the MainScene. Adjusting this setting correctly will help resolve any scene-related issues that may arise during setup.

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/f53b2d04-8eb4-4bb1-886b-1807c08cebe3)

---
## Code walkthrough / explanation 

GC.cs 
- Within GC mostly contain codes that affect the gameobjects that the player comes in contact with. you'll primarily find codes responsible for handling interactions between the player and various game objects, such as coins and roads. Additionally, the script includes functions that enable the player to lose health upon collision with roads or vehicles. Adding of points and what happens when the game ends.

Playermovement.cs
- The script contains codes that affect player movement. It contains the code of not allowing players to go backwards within the game. It also contains the code which disables/enables the gamepad when using either the web/mobile to play the game.

TileManager.cs
- The script contains codes that generates out the maps in the list in a random order. The object that has the script attached to has the list of maps that are in random order. If more new maps are being created, you can add them into the list.

TrafficLightCar.cs & TrafficSystem.cs
- Both scripts contains codes that changes the animator for the traffic lights based on a specific duration. The value can be changed in the code. 

VehicleMovement.cs
- the script contains codes that changes the car movement and positioning on the road. Changing of speed can also be changed from the script.

Readxml.cs
- within Readxml are codes that specifically to manage text associated with popups and notifications. 'ReadXML' provides a convenient way to modify these texts without the need for direct in-game editing. You can effortlessly upload this code to GitHub and customize it to display your preferred text variations."

PopupManager.cs
- The script contains the behaviour of the popups when it is being toggled on or off. 

ButtonToggleScript.cs
- The script is linked to popupmanager and what it does is that is saves the toggle state of the toggle and how the toggle button behaves.

HealthManager.cs 
- The HealthManager manages the hearts of the game which is located at the top left of the player information. It contains code that shows the hearts as well as the the additional points which will be added accordingly once the player ends the game with x number of hearts left.

DistanceCalculator.cs
- The distance calculator checks the distance which the player has covered, distance between gems and the player as well as the behaviour of how the compass reacts when the player is near a gem.

Scoremanager.cs
- The scoremanager will invoke and parse the score that is saved in the end game screen which will allow you to submit the values into the leaderboard and display them.

Leaderboard.cs
- The leaderboard script makes use of an api key that uses a leaderboard plugin which can be accessed by us to manage the leader board. Uploading an entry or deleting an entry can be done via code as well. 

---
## Linking up Visual Studio and Unity for Auto Complete statements when coding / developing in Visual Studio

Go to Edit -> Preferences -> External Tools

Select the Visual Studio Version you prefer for the External Script Editor 

Under the 'Generate .csproj files for' 
Check the following
- Embedded packages
- Local packages
- Registry packages
- Git packages
- Built-in packages
- Local tarball
- Packages from unknown source
- Player projects

![image](https://user-images.githubusercontent.com/25051402/201814555-b883820b-f0c9-43b9-8ba7-52a8ad66a7fb.png)
![image](https://user-images.githubusercontent.com/25051402/201815209-163efeb2-6fe6-4a0c-a076-237235f14db8.png)

### Switching Platform to WebGL

Go to File -> Build Setting -> WebGL

If not Downloaded, Download in Unity Editor and Restart the Project

### Disable Compression Format

1) Go to File -> Build Settings -> Player Settings
![image](https://user-images.githubusercontent.com/25051402/209916303-b1eee72e-7d6f-4247-b259-fe19e5b264ec.png)
2) Go to Player -> Publishing Settings
3) Under Compression Format -> Select Disabled
![image](https://user-images.githubusercontent.com/25051402/209916468-305da456-01d2-469c-bf6a-e2f6e21415a0.png)

---
## Testing & Deploying

For Testing, just press play in Unity.

![image](https://user-images.githubusercontent.com/25051402/209916155-7e40f7d6-c903-48ad-be9b-ffde3bf23a8d.png)

To test how it looks on WebPage, 
Click File -> Build Setting -> Select WebGL -> Build and Run

![image](https://user-images.githubusercontent.com/25051402/209916109-94901c68-c116-451c-970f-a46e2f650f19.png)

For Deploying, Zip the file and upload to [itch.io](itch.io)

1) Log-in with the following email and password, taamgame@gmail.com | ActiveMobility123
2) Navigate to the drop down arrow located at the top right and upload project

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/95601ba0-a4d2-496c-991e-4dd13db3e99f)

3) You can then fill up the basic information of the game and upload the project onto itch for testing

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/b09616bd-5646-41ce-a82a-729396a9ce33)

---
## Update or manage Leaderboard

You can manage the leaderboard by accessing it through this link: [Leaderboard Creator by Danial Jumagaliyev (itch.io)](https://danqzq.itch.io/leaderboard-creator). You can then choose to either Create a new leaderboard or make use of existing leaderboards. 

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/df1ef533-ab82-41fe-bef0-a0e9c48dc63c)

If you would like to add the existing leaderboard into the site, you just have to copy the string of key located in leaderboard.cs. and paste it in.

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/23e903b1-dea5-49d4-aace-5a618dc0fd6e)

If you choose to add a new leaderboard you can do so but if you want the game to make use of the newly created leaderboard, you would have to copy the secret key and replace the string of key located in leaderboard.cs

private string publicLeaderboardKey = "9adf1b039fb92837c0a268411dd78ff70a7820d1734da55b9640cdcf9f7561fa";

---
## Update or Create new maps through the 2DTiledMap application 

1) 	In Tiled, click File located at the top-left corner, hover over New and select New Map.
   
![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/ec075be5-c259-4cda-b543-bb3af3859e03)

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/043d69e0-43fe-4386-8792-aab08eac3d59)

2) 	Map properties should be based on the image shown and save the map into "MapWithColliders".
   
![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/a3b29bd5-f04d-48ec-a0a7-257fa5be3162)

### Adding a new scene for the new level and inserting the newly created map.

1) In Unity, under Project located at bottom left corner, proceed into the Scene folder. Once in it, right-click and hover over Create. Select Scene to create a new scene. This can be called “Level 2” or any suitable names.
   
![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/6c528572-0071-4ebd-bf49-40257b25dd76)

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/8cae9d75-a63a-4cd6-a2fc-c3b0e3ab8c7a)

2) To add a new map from Tiled, double click the newly created scene to be in that specific scene. Once done, find the map created and saved previously into the folder and drag it into the scene.
   
![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/ba448f65-2f0a-4dd9-9cda-3d231e40375d)

3) To update the map, follow along the Demo1 video.

### Stage/Levels, Moving from one scene to another with the use of buttons.

1) To create a button in Unity, under the GameObject menu, hover to UI and click Button – TextMeshPro.
2) For the scene that wants to be displayed, ensure it is added to “Scenes In Build” located in Build Settings.
3) Using the MapLoaderScript.loadScene, input the scene number assigned in the Build Settings to load the respective map/level.

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/01a5c1bb-5d01-49cb-898c-9d50660cbf5e)

Refer to demo2 video.

### Updating UI with new graphics.

1) To update the UI for the game, head over to the Hierarchy of the unity project and look for canvas.
   
![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/060fc08d-cd85-4a58-9ce4-51cc4285a329)

2) To change the graphics for the player information at the top left, you can edit the playerinfo prefab located in the hierachy.
3) you can then edit any of the prefab with images under the inspector.

![image](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104333224/29ceef97-fbbe-4688-889f-06f25f398db1)

4) The notable UI are PlayerInfo, NotifPop, TutorialPop, Popup, PauseMenu, D-Pad and any buttons or texts found in the canvas hierarchy.
   
---
## Extra guide

- [Tiled](https://www.youtube.com/watch?v=ZwaomOYGuYo&list=PL6wuv1YGOTFfxi8pdN2ghWmDqZqy3_XA7)
- [Aseprite](https://www.youtube.com/watch?v=tFsETEP01k8)
- [Unity Web Game](https://youtube.com/playlist?list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu)
- [Leaderboard Guide](https://youtu.be/-O7zeq7xMLw?si=kUbx0BF7NEU8RBM_)
- To use tilemap in unity directly instead of using [Tiled](https://www.mapeditor.org/). Refer [here](https://www.youtube.com/watch?v=ryISV_nH8qw&t=627s)
- Collison and Layer Sorting using [Tiled](https://www.mapeditor.org/) and Unity. Refer [here](https://www.youtube.com/watch?v=iJINzMUxlkA&t=220s)
- [Level framework](https://forum.unity.com/threads/progression-xp-points-leveling-framework.428087/)
- [Level/Stage selection](https://youtu.be/YAHFnF2MRsE?si=r_Z3f4p57ePkSTWq) and [here](https://youtu.be/vpbPd6jNEBs?si=snZCrgQ_oCNPmw6k)

## Quick Guide on Creating Boundaries

![image](https://user-images.githubusercontent.com/25051402/210027918-6126524c-4a9e-40b3-8784-d9bde1885c7e.png)
![image](https://user-images.githubusercontent.com/25051402/210028050-8cb97f71-7022-463b-a803-9bd616c1ed7a.png)

Remember to save after adding borders to your tileset Asset

## UI preview on different devices

From top to bottom:
Google Pixel > Huawei > Ipad Mini > Ipad Pro > Iphone 13 > Samsung Galaxy Note 20

![Google Pixel XL](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/a8c6e18d-7d5f-47f9-9bad-33dff5c0dd32)
![Huawei P40 Pro](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/35a48795-a605-46a2-baf6-69cc40bd1e95)
![Ipad Mini](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/5b005d78-0064-4fe2-90e0-e82c7eb037ed)
![Ipad Pro](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/969c2dbd-1bea-4524-853e-7b4bda5e3001)
![Iphone 13 Pro max](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/c9dda881-3ba8-45ce-a4c8-e268210bed99)
![Samsung Galaxy Note20 Ultra 5](https://github.com/20145050-Vernon-Ong/Active-Mobility-Game/assets/104403770/c8ae958a-4abd-4ece-9f26-bb6f78e6cf2d)
