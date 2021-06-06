using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchLogicController : MonoBehaviour
{
    [SerializeField] private Text txtCounts;
    public static Action<CardElement> RegisterClicks;

    private static int totalNumberOfClicks = 0;
    [SerializeField]private int pairsMatched = 0;
    [SerializeField] private int totalPairs = 0;
    private bool toBeDequeued = false;
    private static string currentFlippedID = "";

    private Queue<CardElement> flippedCards = new Queue<CardElement>();

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.ReturnInstance();
    }

    private void OnEnable()
    {
        RegisterClicks += HandleClicks;
        GridController.GridGenerated += GamePlayStarted;
    }

    private void GamePlayStarted(CardGenerator obj)
    {
        totalNumberOfClicks = 0;
        totalPairs = obj.totalNumberOfPairs;
        pairsMatched = 0 ;
        Debug.Log(pairsMatched);
        flippedCards.Clear();
        currentFlippedID = "";
        toBeDequeued = false;
        txtCounts.text = "Tries : " + 0;
    }

    private void HandleClicks(CardElement cardFlipped)
    {
        
        flippedCards.Enqueue(cardFlipped);
        totalNumberOfClicks++;
        txtCounts.text = "Tries : " + totalNumberOfClicks ;


        if (toBeDequeued)
        {

            CardElement ele1 = flippedCards.Dequeue();
            CardElement ele2 = flippedCards.Dequeue();
            Debug.Log(ele1.CardId + " wrong answwers " + ele2.CardId);
            toBeDequeued = false;
            ele1.DoFlip();
            ele2.DoFlip();
            currentFlippedID = cardFlipped.CardId;
        }
            
        if(totalNumberOfClicks % 2 == 0 && totalNumberOfClicks !=0)
        {
            if(cardFlipped.CardId.Equals(currentFlippedID))
            {
                // dequeue both 2
                CardElement ele1 = flippedCards.Dequeue();
                CardElement ele2 = flippedCards.Dequeue();
                
                Debug.Log(ele1.CardId + " " + ele2.CardId);

                pairsMatched++;
                Debug.Log("<color=red>MATCHED</color>" + ele1.CardId + " " + ele2.CardId);
                if(pairsMatched == totalPairs)
                {
                    // GameOver
                    Debug.Log("GAMEOVER");
                    gameManager.GameOver();
                }
            }
            else
            {
                    // dequeue on next click;
                    toBeDequeued = true;
            }
        }
        else
        {
            currentFlippedID = cardFlipped.CardId;
        }
    }

    internal int ReturnTotalCounts()
    {
        return totalNumberOfClicks;
    }

    private void OnDisable()
    {
        RegisterClicks -= HandleClicks;
        GridController.GridGenerated -= GamePlayStarted;
    }

}
