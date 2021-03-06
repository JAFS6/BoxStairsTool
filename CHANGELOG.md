# Change Log
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [1.4.0] - 2016-12-01
### Added
- Ability to override individual step height.
- Ability to override individual step depth.
- Ability to override individual step material.
- Ability to modify Stairs Height in base to steps height.
- Ability to modify Stairs Depth in base to steps depth.
- "Selection box", a disabled box collider to represent the overall volume of the object determined by the values of Stairs Width, Height and Depth.
- Package manual to the repo.
- Explanation of the new features on the README.

### Fixed
- Explanation on the README file about how set the editor properly to see the pivot handle correctly.

## [1.3.1] - 2016-10-06
### Added
- Version file to the package.

## [1.3.0] - 2016-10-01
### Added
- Undo for the finalize box stairs feature.
- Now the object maintain its position when changing the pivot.

### Changed
- Substituted foreach loops by for loops due to the first ones are slower than the second ones.

## [1.2.1] - 2016-09-26
### Added
- Motivation section to README file.

## [1.2.0] - 2016-09-20
### Added
- CHANGELOG file.
- Image of the tool on the README file.
- Option to create BoxStairs with 3 sides.

### Changed
- Tool moved to the GameObject/3D Object menu. Now it appears on the hierarchy's context menu too.
- README file updated according to the version features.

### Fixed
- Undo after change some parameter didn't update the BoxStairs object.

## [1.1.0] - 2016-08-29
### Fixed
- While the script is on the root GameObject, select any child on the scene cause select the root.
- If any GameObject is selected on the hierarchy on the moment of creation of a BoxStairs object, it will have a new BoxStairs child.

## [1.0.0] - 2016-08-28
### Added
- First version of the BoxStairs Tool.
