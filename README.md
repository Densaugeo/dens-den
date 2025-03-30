# Den's Den

Den's Den VRChat world

## Setup

Create deploy key on Windows
```
cd ~/.ssh
ssh-keygen -t ed25519 -f id_ed25519_PROJECT_NAME -C $(hostname)
```
SSH agent doesn't work on Windows, so to use the deploy key edit ~/.ssh/config. 

GLTF models require the com.unity.cloud.gltfast package
- Window > Package Manager > + > Add package by name... > com.unity.cloud.gltfast > Add

## Notes on Making Various Things

Buttons
- Must have a collider to interact

Video Player
- Built-in default player has no control UI besides a URL input
- Installed with Unity > Assets > Import Package > Custom Package...
  * Selected `USharpVideo_v1.0.1.unitypackage` from https://github.com/MerlinVR/USharpVideo/releases/tag/v1.0.1
  * Unchecked `Examples` folder at import screen. This folder would add ~50 MB to the repo, most of it is 40 MB of pre-baked lighting data for an example scene

Pickups
- Need VRC Pickup script
- Need to add a collider, which is not created automatically
- Need to add VRC Object Sync script
  * If this causes a build error related to network IDs, use VRChat SDK > Utilities > Network ID ... > Regenerate Scene IDs
