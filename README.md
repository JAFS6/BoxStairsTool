# BoxStairs Tool
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://raw.githubusercontent.com/JAFS6/BoxStairsTool/master/LICENSE)
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

<img src="https://cloud.githubusercontent.com/assets/6010819/18685851/103ad5c6-7f7a-11e6-8d41-f749ea4a746d.JPG" alt="BoxStairs custom inspector image" height="300px">

With the object selected, **choose** where do you want to be placed the **pivot** of the object, **edit** the Stairs **Width**, **Height** and **Depth** parameters to set the volume which will contain the stairs, the **Steps Number** parameter to set how many steps will have the stairs and check **Three Sides** parameter if you want stairs on three sides. Finally, **assign** a material to the Stairs **Material** slot. Play with the parameters until you get the desired design. If you want to **finalize the object**'s edition ability, click the finalize button and it will remain on the hierarchy as a group of boxes under a empty root.

This tool accepts multi object editing.

# Compatibility
This tool has been tested on Unity3D version 5.3.6f1 and 5.4.0f3 on Windows and Mac.

# Troubleshooting
If you have any problem, question, suggestion or bug report, [open an issue](https://github.com/JAFS6/BoxStairsTool/issues/new) and we will try to solve it.

# Contributing
If you want to help improve this tool, please test it and [create a new issue](https://github.com/JAFS6/BoxStairsTool/issues/new) if you find any bug.

# License
This tool is licensed under the [MIT license](https://opensource.org/licenses/MIT).

# Author
[Juan Antonio Fajardo Serrano](https://www.linkedin.com/in/jafs6)
