using System.Linq;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.TextCore.Text;
using VRC.SDKBase;
using VRC.Udon;

public class TeamButton : UdonSharpBehaviour {
    public TeamList team_list;
    public Team team;

    void Start() {}

    public override void Interact() {
        if(Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) {
            team_list.ToggleTeamAssignment(Networking.LocalPlayer, team);
        } else {
            // Ownership request will always be denied. It is only sent as a
            // way of transmitting the player ID
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        base.Interact();
    }

    public override bool OnOwnershipRequest(VRCPlayerApi requester,
    VRCPlayerApi _newOwner) {
        // OnOwnershipRequest() is called by the local player before the request
        // is sent to the owner. We must return true here or the request will
        // not be sent
        if(requester == Networking.LocalPlayer) return true;
        
        team_list.ToggleTeamAssignment(requester, team);

        // Deny every ownership request -  they're only for transmitting player
        // IDs, we don't actually want to transfer ownership
        return false;
    }
}
