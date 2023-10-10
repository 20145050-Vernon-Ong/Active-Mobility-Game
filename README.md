# Active Mobility Web App

---

## Installation of Necessary Softwares/Toolkits/Packages/Dependencies/SDK

### Getting the required Assets
Git Clone / Pull or Download as Zip in the following GitHub Repository

### Installation of Unity Hub
Unity Hub is required to open or run any of the Unity Projects. You can install them by clicking [here](https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe) if not visit the official website [here](https://unity3d.com/get-unity/download)

### Choose your appropriate Unity Version that you need.
For this case we're using **2021.3.36f1** where you can find all the archive [here](https://unity3d.com/get-unity/download/archive)

### Choose the Visual Studio you prefer to use
Tested with VS Community 2022 and VS Commmunity 2019. You can find these [here](https://visualstudio.microsoft.com/downloads/)

Go to Visual Studio Installer and Download the Necessary Workloads Stated Below

### Unity 3rd Party Packages
-[SuperTiled2Unity](https://seanba.itch.io/supertiled2unity)

### Installation of Map Editor and Pixel Art Creation Software
- [Tiled](https://www.mapeditor.org/)
- [Aseprite](https://www.aseprite.org/) or [pixel art](https://www.pixilart.com/draw) online

### Convert Image to Pixel Art
- [Pixel It](https://giventofly.github.io/pixelit/)

---
a.	Objective of the game

- The Active Mobility Game is designed to offer users invaluable insights into the nuances of road safety as they manoeuvre through the intricate web of lanes and navigate the ever-changing traffic lights in the vibrant streets of Singapore.

-	Within this immersive gaming experience, your mission is to safely reach your desired destination by adhering to a set of foundational traffic rules. Deviating from these rules will result in the loss of one precious life, so stay alert and follow the road code diligently.


-	Our goal for the game is to provide an engaging journey through a series of meticulously designed levels or stages. Each stage is designed to present you with distinct learning points and scenarios, ensuring that you acquire a comprehensive understanding of road safety as you progress.

---
## Setting up the Working Environment from Downloaded Asset

### Opening Unity Project

1) Open Unity Hub
2) Open Project -> Select Downloaded Project

## Setting up the Working Environment from Scratch

### Creating the Project

1) Open Unity Hub
2) New Project -> 2D (Core)

### Installing and Importing Packages in Unity

Install the [SuperTiled2Unity](https://seanba.itch.io/supertiled2unity) for Unity by Assets -> Import Package -> Custom Package -> Select [SuperTiled2Unity](https://seanba.itch.io/supertiled2unity)

### Linking up Visual Studio and Unity for Auto Complete statements when coding / developing in Visual Studio

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

### Testing & Deploying

For Testing, just press play in Unity.
![image](https://user-images.githubusercontent.com/25051402/209916155-7e40f7d6-c903-48ad-be9b-ffde3bf23a8d.png)
To test how it looks on WebPage, Click File -> Build Setting -> Select WebGL -> Build and Run
![image](https://user-images.githubusercontent.com/25051402/209916109-94901c68-c116-451c-970f-a46e2f650f19.png)

For Deploying, Zip the file and upload to [itch.io](itch.io)

## Tutorial

- [Tiled](https://www.youtube.com/watch?v=ZwaomOYGuYo&list=PL6wuv1YGOTFfxi8pdN2ghWmDqZqy3_XA7)
- [Aseprite](https://www.youtube.com/watch?v=tFsETEP01k8)
- [Unity Web Game](https://youtube.com/playlist?list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu)
- To use tilemap in unity directly instead of using [Tiled](https://www.mapeditor.org/). Refer [here](https://www.youtube.com/watch?v=ryISV_nH8qw&t=627s)
- Collison and Layer Sorting using [Tiled](https://www.mapeditor.org/) and Unity. Refer [here](https://www.youtube.com/watch?v=iJINzMUxlkA&t=220s)

## Quick Guide on Creating Boundaries

![image](https://user-images.githubusercontent.com/25051402/210027918-6126524c-4a9e-40b3-8784-d9bde1885c7e.png)
![image](https://user-images.githubusercontent.com/25051402/210028050-8cb97f71-7022-463b-a803-9bd616c1ed7a.png)

Remember to save after adding borders to your tileset Asset
