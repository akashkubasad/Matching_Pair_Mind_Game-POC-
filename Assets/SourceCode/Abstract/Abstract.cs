using System;
using UnityEngine;
public interface ICard
{
    string CardId { get; set; }
    bool isCardFacedUp { get; set; }
    void Initialse(CardData data);

}
public interface IClick
{
    void OnClick();
}

public interface IFlip
{
    void DoFlip();
}

public class CardData
{
    public int cardId;
    public Color cardColor;
}
