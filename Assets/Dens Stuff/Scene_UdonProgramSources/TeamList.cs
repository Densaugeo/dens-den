using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum Team : byte {
    None,
    Carmine,
    Aurum,
    Malachite,
    Amethyst,
}

public class TeamList : UdonSharpBehaviour {
    public TMP_Text[] team_lists = new TMP_Text[4];
    
    [UdonSynced] public int[] player_ids = new int[90];
    [UdonSynced] public Team[] team_ids = new Team[90];
    
    void Start() {}

    public void ToggleTeamAssignment(VRCPlayerApi player, Team team) {
        // This function should only be called by the instance master because it
        // sends out network updates
        if(!Networking.LocalPlayer.isMaster) {
            team_lists[0].text = "ERROR: TeamList.ToggleTeamAssignment() " +
                "should only be called by the instance master.";
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
        // Won't trigger on owner, so needs to be triggered manually here
        OnDeserialization();
    }

    public override void OnDeserialization() {
        string[] texts = { "", "", "", "" };

        var players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];  
        VRCPlayerApi.GetPlayers(players);

        for(int i = 0; i < players.Length; ++i) {
            Team team_id = Team.None;
            for(int j = 0; j < 90; ++j) {
                if(player_ids[j] == players[i].playerId) {
                    team_id = team_ids[j];
                    break;
                }
            }
            
            if(team_id != Team.None) {
                texts[Convert.ToInt32(team_id) - 1] += players[i].displayName +
                    " (id " + players[i].playerId + ")\n";
            }
        }

        for(int i = 0; i < 4; ++i) {
            team_lists[i].text = texts[i];
        }
    }
}
