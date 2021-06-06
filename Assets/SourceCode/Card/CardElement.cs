using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardElement : MonoBehaviour , ICard, IClick, IFlip
{
    internal string cardIdentifier;
    public  string CardId
    {
        get => cardIdentifier;
        set => CardId = cardIdentifier;
    }
    public bool isCardFacedUp
    {
        get;
        set;
    }

    public abstract void DoFlip();

    public abstract void Initialse(CardData cardData);

    public abstract void OnClick();

}

