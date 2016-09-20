# BoxStairs Tool
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://raw.githubusercontent.com/JAFS6/BoxStairsTool/master/LICENSE)
![](https://img.shields.io/badge/Unity3D%20version-5.4.0-lightgrey.svg)

Unity3D editor tool to create simple stairs made out of boxes.

--------

# Setup
**Download** the latest version of **BoxStairs Tool** from the [releases page](https://github.com/JAFS6/BoxStairsTool/releases), unzip it and **put** the **BoxStairsTool folder on** your Unity3D **project's assets folder**. This creates a new menu on your Unity Editor called BoxStairs Tool.

# Usage
Click the **BoxStairs Tool menu > Create BoxStairs** button to create a default GameObject called BoxStairs on the (0,0,0) position. If a GameObject is selected on the Hierarchy, the new GameObject will be created as a child and on the local position (0,0,0). The same applies to several GameObjects selected, each of them will have a child BoxStairs GameObject.

With the object selected, **choose** where do you want to be placed the **pivot** of the object, **edit** the Stairs **Width**, **Height** and **Depth** parameters to set the volume which will contain the stairs, and the **Steps Number** parameter to set how many steps will have the stairs. Finally, **assign** a material to the Stairs **Material** slot. Play with the parameters until you get the desired design. If you want to **finalize the object**'s edition ability, click the finalize button and it will remain on the hierarchy as a group of boxes under a empty root.

This tool accepts multi object editing.

# Compatibility
This tool has been developed and tested on Unity3D version 5.4.0.

# Troubleshooting
If you have any problem, question, suggestion or bug report, [open an issue](https://github.com/JAFS6/BoxStairsTool/issues/new) and we will try to solve it.

# License
This tool is licensed under the [MIT license](https://opensource.org/licenses/MIT).

# Author
[Juan Antonio Fajardo Serrano](https://es.linkedin.com/in/jafs6)
