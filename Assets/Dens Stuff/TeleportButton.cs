
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

public class TeleportButton : UdonSharpBehaviour {
    public override void Interact() {
        //VRCPlayerApi
        //VRCPlayerObject
        Networking.LocalPlayer.TeleportTo(new Vector3(0, 0, -25), Quaternion.Euler(0, 0, 0));


        base.Interact();
    }
}
