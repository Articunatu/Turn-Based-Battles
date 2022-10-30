using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HealthBar healthBar;

    [SerializeField] public Image Area;

    Character _figure;

    //public static void AreaImage(Color color, BattleUnit unit)
    //{
    //    if (unit.IsPlayerUnit)
    //    {
    //        AreaPlayer.gameObject.SetActive(true);
    //        AreaPlayer.color = color;
    //    }
    //    else
    //    {
    //        AreaEnemy.gameObject.SetActive(true);
    //        AreaEnemy.color = color;
    //    }
    //}

    public void SetData(Character figure)
    {
        _figure = figure;

        nameText.text = figure.Base.Name;
        healthBar.SetHealth((float)_figure.Health / _figure.MaxHealth);
    }

    public IEnumerator UpdateHP()
    {
        if (_figure.HealthChanged)
        {
            yield return healthBar.SetHealthSmooth((float)_figure.Health / _figure.MaxHealth);
            _figure.HealthChanged = false;
        }
    }

    public IEnumerator UpdateDrainHP()
    {
        if (_figure.HealthChanged)
        {
            yield return healthBar.SetDrainHealthSmooth((float)_figure.Health / _figure.MaxHealth);
            _figure.HealthChanged = false;
        }
    }
}