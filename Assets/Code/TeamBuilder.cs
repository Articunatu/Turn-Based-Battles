using UnityEngine;

public class TeamBuilder : MonoBehaviour
{
    [SerializeField] GameObject Main, AllTeams, TeamViewer, MemberView;

    public void Awake()
    {
        Main.SetActive(true);
        AllTeams.SetActive(false);
        TeamViewer.SetActive(false);
        MemberView.SetActive(false);
    }

    public void OpenBuilder()
    {
        Main.SetActive(false);
        AllTeams.SetActive(true);
    }

    public void CloseBuilder()
    {
        AllTeams.SetActive(false);
        Main.SetActive(true);
    }

    public void OpenTeamViewer()
    {
        AllTeams.SetActive(false);
        TeamViewer.SetActive(true);
    }

    public void CloseTeamViewer()
    {
        AllTeams.SetActive(true);
        TeamViewer.SetActive(false);
    }

    public void OpenMemberViewer()
    {
        Main.SetActive(false);
        AllTeams.SetActive(true);
    }

    public void CloseMemberViewer()
    {
        Main.SetActive(true);
        MemberView.SetActive(false);
    }
}