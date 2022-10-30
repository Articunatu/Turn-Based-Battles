using UnityEngine;
using UnityEngine.UI;

public class MenuMemeberUI : MonoBehaviour
{
    [SerializeField] Image image;

    public void SetImage(Character figure)
    {
        image.sprite = figure.Base.FrontSprite;
    }
}
