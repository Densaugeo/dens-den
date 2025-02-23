# Den's Den

Den's Den VRChat world

## Notes on Making Various Things

Buttons
- Must have a collider to interact

Video Player
- Built-in default player has no control UI besides a URL input
- Installed with Unity > Assets > Import Package > Custom Package...
  * Selected `USharpVideo_v1.0.1.unitypackage` from https://github.com/MerlinVR/USharpVideo/releases/tag/v1.0.1
  * Unchecked `Examples` folder at import screen. This folder would add ~50 MB to the repo, most of it is 40 MB of pre-baked lighting data for an example scene
