
using System.Linq;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.TextCore.Text;
using VRC.SDKBase;
using VRC.Udon;

public enum Team : byte {
    None,
    Red,
    Blue,
}

public class TeamButton : UdonSharpBehaviour {
    public TMP_Text board;

    string debug = "";

    [UdonSynced] public int[] player_ids = new int[90];
    [UdonSynced] public Team[] team_ids = new Team[90];

    void Start() {}

    public override void Interact() {
        if(Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) {
            ToggleTeamAssignment(Networking.LocalPlayer, Team.Red);
        } else {
            // Ownership request will always be denied. It is only sent as a
            // way of transmitting the player ID
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        base.Interact();
    }

    public override bool OnOwnershipRequest(VRCPlayerApi requester, VRCPlayerApi _newOwner) {
        // OnOwnershipRequest() is called by the local player before the request
        // is sent to the owner. We must return true here or the request will
        // not be sent
        if(requester == Networking.LocalPlayer) return true;
        
        ToggleTeamAssignment(requester, Team.Red);

        // Deny every ownership request -  they're only for transmitting player
        // IDs, we don't actually want to transfer ownership
        return false;
    }

    public void UpdateTeams() {
        string text = "";

        var players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];  
        VRCPlayerApi.GetPlayers(players);

        for(int i = 0; i < players.Length; ++i) {
            Team team_id = Team.None;
            for(int j = 0; j < 90; ++j) {
                if(player_ids[j] == players[i].playerId) {
                    team_id = team_ids[j];
                }
            }

            text += players[i].playerId + " - " + players[i].displayName +
                ": team " + team_id + "\n";
        }

        text += "\n";

        text += "\n";

        for(int i = 0; i < 10; ++i){
            text += player_ids[i] + ": " + team_ids[i] + "\n";
        }

        text += "\n";

        text += debug;

        board.text = text;
    }

    // This function sends out network updates, and should only be called by the
    // instance owner
    void ToggleTeamAssignment(VRCPlayerApi player, Team team) {
        for(int i = 0; i < 90; ++i) {
            if(player_ids[i] == 0 || player_ids[i] == player.playerId) {
                player_ids[i] = player.playerId;
                team_ids[i] = team_ids[i] == team ? Team.None : team;
                break;
            }
        }
        RequestSerialization();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,
            nameof(UpdateTeams));

    }
}
