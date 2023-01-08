using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] bool isUser;
    [SerializeField] Hud hud;
    public int AttackSpeed { get; set; }
    public Character CurrentMember { get; set; }
    public AreaID Area { get; set; }
    public bool entryDamage, ///Hazard
         armor, barrier; ///Boosts

    public Hud Hud
    {
        get
        {
            return hud;
        }
    }

    public bool IsUser
    {
        get
        {
            return isUser;
        }
    }

    public Character _character { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>(); 
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Character character)
    {
        _character = character;
        if (isUser)
        {
            image.sprite = _character.Base.BackSprite;
        }
        else
        {
            image.sprite = _character.Base.FrontSprite;
        }

        hud.SetData(character);

        image.color = originalColor;
        image.transform.position = originalPos;
        PlayEntranceAnimation();
    }

    public void PlayEntranceAnimation()
    {
        if (isUser)
        {
            image.transform.localPosition = new Vector3(-750f, originalPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(750f, originalPos.y);
        }
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlaySwapAnimation()
    {
        image.transform.localPosition = new Vector3(originalPos.x, originalPos.y);
        if (isUser)
        {
            image.transform.DOLocalMoveX(-750f, 1.5f);
        }
        else
        {
            image.transform.DOLocalMoveX(750f, 1.5f);
        }       
    }

    public void PlayAttackMovementAnimation()
    {
        Sequence animation = DOTween.Sequence();
        if (isUser)
        {
            animation.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
            animation.Append(image.transform.DOLocalMoveX(originalPos.y + 50f, 0.25f));
        }
        else
        {
            animation.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
            animation.Append(image.transform.DOLocalMoveX(originalPos.y - 50f, 0.25f));
        }
        animation.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayAttackStationaryAnimation()
    {
        Sequence animation = DOTween.Sequence();
        if (isUser)
        {
            animation.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));           
        }
        else
        {
            animation.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));           
        }
        animation.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayDefeatAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));  
    }
}
