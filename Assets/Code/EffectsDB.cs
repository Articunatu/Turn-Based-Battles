using UnityEngine;
using UnityEngine.UI;

public class EffectsDB : MonoBehaviour
{
    [SerializeField] Hud playerHud, enemyHud;



    public void TurnOffArea(Player player)
    {
        if (player.IsUser)
        {
            playerHud.Area.gameObject.SetActive(false);
        }
        else
        {
            enemyHud.Area.gameObject.SetActive(false);
        }
    }
}