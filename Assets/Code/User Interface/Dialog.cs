using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] int letterVelocity;

    [SerializeField] Text dialogText, energy;
    [SerializeField] GameObject attackSelectionUI;
    [SerializeField] Text[] attackText;
    [SerializeField] Image[] attackUI;
    //[SerializeField] Sprite PlantUI, FireUI, WaterUI, IceUI, LightningUI, AirUI, EarthUI, MetalUI, ChemicalUI, SpaceUI, RayUI;

    private void Start()
    {
        attackSelectionUI.SetActive(false);
    }
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public void SetEnergy(string energy_)
    {
        energy.text = energy_;
    }

    public IEnumerator WriteDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterVelocity);
        }

        yield return new WaitForSeconds(1f);
    }

    public void ActivateAttackSelectionUI(bool enabled)
    {
        attackSelectionUI.SetActive(enabled);
    }

    public void SetAttackNames(Character character)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < character.Attacks.Count)
            {
                attackText[i].text = character.Attacks[i].Base.Name;
                attackUI[i].sprite = character.Attacks[i].Base.UI;
            }
            else
            {
                attackText[i].text = " ";
                attackUI[i].enabled = false;
            }
        }
    }

    public void UpdateEnergy(Character character, Attack attack)
    {
        energy.text = $"{character.Energy - attack.Cost}";
    }
}

