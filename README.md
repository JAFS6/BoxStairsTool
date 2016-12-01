# BoxStairs Tool
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/JAFS6/BoxStairsTool/blob/master/LICENSE.txt)
![](https://img.shields.io/badge/Unity3D%20version-5.4.0-lightgrey.svg)

Unity3D editor tool to create simple stairs made out of boxes.

--------

# Motivation
Have you ever need to create some stairs made out of boxes for a level mockup or for a simple scene? With this lightweight tool you will create stairs in no time with several options like number of steps, height, width, depth and you could assign a material with the same ease.

# Setup
**Download** the latest version of **BoxStairs Tool** from the [releases page](https://github.com/JAFS6/BoxStairsTool/releases). You can choose between the zip file or the unity package.

## Setup from zip file
Unzip it and **put** the **BoxStairsTool folder on** your Unity3D **project's assets folder**.

## Setup from unity package
Import the package: **Assets > Import Package > Custom Package**, select the file and **import** all.

## Result
Whatever form you use, you will see a **new entry on the GameObject/3D Object menu** called **BoxStairs**.

# Usage
Click the **GameObject/3D Object > BoxStairs** button on the menu, or in the hierarchy's context menu, to create a default GameObject called BoxStairs on the (0,0,0) position. If a GameObject is selected on the Hierarchy, the new GameObject will be created as a child and on the local position (0,0,0). The same applies to several GameObjects selected, each of them will have a child BoxStairs GameObject.

<img src="https://cloud.githubusercontent.com/assets/6010819/20762274/212667a4-b726-11e6-8455-2322a6d6654e.JPG" alt="BoxStairs custom inspector image" width="500px">

With the object selected, **choose** where do you want to be placed the **pivot** of the object. In order to see the selected handle correctly (Downstairs or Upstairs), you must select pivot option on the editor Gizmo Display Toggles.

<img src="https://docs.unity3d.com/uploads/Main/HandlePositionButtons.png" alt="Unity Gizmo Display Toggles" height="30px">

**Edit** the Stairs **Width**, **Height** and **Depth** parameters to set the volume which will contain the stairs (this is represented with a green box when you select the GameObject), the **Steps Number** parameter (clicking over +1,+10,-1,-10 buttons) to set how many steps will have the stairs and check **Three Sides** parameter if you want stairs on three sides (Note that this option will fix the relation between Width and Depth, first one will be double of the second one). Finally, **assign** a material to the Stairs **Material** slot. Play with the parameters until you get the desired design. If you want to **finalize the object**'s edition ability, click the finalize button and it will remain on the hierarchy as a group of boxes under a empty root.

This tool doesn't accepts multi object editing.

# Advanced Usage

This tool introduces on the 1.4.0 version advanced features to allow more customization of the object.

<img src="https://cloud.githubusercontent.com/assets/6010819/20762278/274c1016-b726-11e6-8ef8-07dc7de9b023.JPG" alt="BoxStairs custom inspector image" width="500px">

## Steps Height and Depth

You can set the _Height/Depth_ of each step individually setting its property on the specific input field, the element 0 is the bottom most step. The steps will "push" the others to match the space you introduce.

The last step will have its dimension supedited to the space remaining between the sum of the previous steps and the _Stairs Height/Depth_, unless the _Stairs Height/Depth Drived By Steps_ is on, in that case, you will be able to edit all the steps and you will not be able to edit the _Stairs Height/Depth_ property which will be modified by the sum of the steps.

Each action which modifies the _Stairs Height/Depth_ property will redistribute the space on that dimension among all the steps, to prevent this and to conserve your custom values you must enable the _Keep Custom Height/Depth Values_ option.

## Materials

When assigning a material to the Stairs Material slot, all steps will take this one, but if you want an specific material for some specific steps you only need to drag the desired material onto the specific step material slot.

# Troubleshooting
If you have any problem, question, suggestion or bug report, [open an issue](https://github.com/JAFS6/BoxStairsTool/issues/new) and we will try to solve it.

# Contributing
If you want to help improve this tool, please test it and [create a new issue](https://github.com/JAFS6/BoxStairsTool/issues/new) if you find any bug.

# License
This tool is licensed under the [MIT license](https://opensource.org/licenses/MIT).

# Author
[Juan Antonio Fajardo Serrano](https://www.linkedin.com/in/jafs6)
