using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [SerializeField] private int m_Xvalue = 4, m_Yvalue = 4;
    [SerializeField] private Text txtResult;
    [SerializeField] private GameObject cardOBJ, gridPanel;
    [SerializeField] private List<GameObject> cardsGenerated;

    public static Action<CardGenerator> GridGenerated;

    private List<CardData> scrumbledCards;

    internal CardGenerator cardGenerator;
    private void OnEnable()
    {
        GameManager.StartCardGenerator += CreateGenerator;
        cardsGenerated = new List<GameObject>();

    }
    private void CreateGenerator()
    {
        txtResult.text = "";

        if (cardGenerator != null) cardGenerator = null;

        cardGenerator = new CardGenerator(m_Xvalue, m_Yvalue);

        if (!cardGenerator.RetunCardGeneratorPossibility())
        {
            txtResult.text = "Cannot generate cards with " + m_Xvalue + "*" + m_Yvalue;
            return;
        }

        cardsGenerated = new List<GameObject>();
        StartCoroutine(StartGeneratingGrid());
    }

    IEnumerator StartGeneratingGrid()
    {
        cardGenerator.InitialiseCardGenerator();
        yield return new WaitForEndOfFrame();
        cardGenerator.CreateShuffledIndexes();
        yield return new WaitForEndOfFrame();
        cardGenerator.CreateMatchPairs();
        yield return new WaitForEndOfFrame();

        scrumbledCards = new List<CardData>();

        scrumbledCards = cardGenerator.arrayOfCards.ToList();

        yield return new WaitForEndOfFrame();

        DisplayGrid();
    }


    private void DisplayGrid()
    {
        Debug.Log(cardsGenerated.Count + " " + cardGenerator.totalCards);
        for (int i = 0; i < cardGenerator.totalCards; i++)
        {
                GameObject card = Instantiate(cardOBJ);
                card.transform.SetParent(gridPanel.transform);
                card.GetComponent<ICard>().Initialse(scrumbledCards[i]);
                cardsGenerated.Add(card);
        }
        GridGenerated.Invoke(cardGenerator);
    }

    private void OnDisable()
    {
        GameManager.StartCardGenerator -= CreateGenerator;
    }
}
