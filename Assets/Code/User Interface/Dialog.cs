using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] int letterVelocity;

    [SerializeField] Text dialogText, energy;
    [SerializeField] GameObject attackSelectionUI;
    [SerializeField] List<Text> attackText;
    [SerializeField] List<Image> attackUI;
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

    public void SetAttackNames(List<Attack> attacks)
    {
        for (int i = 0; i < attackText.Count; ++i)
        {
            if (i < attacks.Count)
            {
                attackText[i].text = attacks[i].Base.Name;
                attackUI[i].sprite = attacks[i].Base.UI;
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

