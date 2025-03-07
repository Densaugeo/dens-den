using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum Team : byte {
    None,
    Red,
    Blue,
}

public class TeamList : UdonSharpBehaviour {
    public TMP_Text board;
    
    public string debug = "";

    [UdonSynced] public int[] player_ids = new int[90];
    [UdonSynced] public Team[] team_ids = new Team[90];
    
    void Start() {}

    public void ToggleTeamAssignment(VRCPlayerApi player, Team team) {
        // This function should only be called by the instance owner because it
        // sends out network updates
        if(!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) {
            board.text = "ERROR: TeamList.ToggleTeamAssignment() should only " +
                "be called by the instance owner.";
            return;
        }

        for(int i = 0; i < 90; ++i) {
            if(player_ids[i] == 0 || player_ids[i] == player.playerId) {
                player_ids[i] = player.playerId;
                team_ids[i] = team_ids[i] == team ? Team.None : team;
                break;
            }
        }
        RequestSerialization();
        SendCustomNetworkEvent(
            VRC.Udon.Common.Interfaces.NetworkEventTarget.All,
            nameof(UpdateTeams));
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
}
