using UnityEngine;

public class TeamBuilder : MonoBehaviour
{
    [SerializeField] GameObject MainUI, AllTeamsUI, TeamViewerUI, MemberViewUI;
    [SerializeField] CharacterTeam Currentteam { get; set; }
    [SerializeField] TeamsCollection AllTeams { get; set; }

    public void Awake()
    {
        MainUI.SetActive(true);
        AllTeamsUI.SetActive(false);
        TeamViewerUI.SetActive(false);
        MemberViewUI.SetActive(false);
    }

    public void OpenBuilder()
    {
        MainUI.SetActive(false);
        AllTeamsUI.SetActive(true);
    }

    public void CloseBuilder()
    {
        AllTeamsUI.SetActive(false);
        MainUI.SetActive(true);
    }

    public void OpenTeamViewer()
    {
        var currentTeam = AllTeamsUI.GetComponentInChildren<TeamUI>().Team;
        AllTeamsUI.GetComponentInChildren<TeamUI>().Team = currentTeam;
        AllTeamsUI.SetActive(false);
        TeamViewerUI.SetActive(true);
    }

    public void CloseTeamViewer()
    {
        AllTeamsUI.SetActive(true);
        TeamViewerUI.SetActive(false);
    }

    public void OpenMemberViewer()
    {
        MainUI.SetActive(false);
        AllTeamsUI.SetActive(true);
    }

    public void CloseMemberViewer()
    {
        MainUI.SetActive(true);
        MemberViewUI.SetActive(false);
    }
}