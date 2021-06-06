using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGenerator
{
    // fetch inputs for grid calculation e.g 4 * 4
    // Check if the total number of the cards are pairable from the inputs given above\
    // return false if the cards are odd in numbers

    private int m_value1, m_value2;

    // Lists for storing non repeating indexes
    private List<int> unShuffledArray;
    private List<int> shuffledArrayOfCards;


    internal CardData[] arrayOfCards;
    internal int totalCards = 0;
    internal int totalNumberOfPairs = 0;

    public CardGenerator(int value1, int value2)
    {
        m_value1 = value1;
        m_value2 = value2;
    }

    internal bool RetunCardGeneratorPossibility()
    {
        totalCards = m_value1 * m_value2;
        return (totalCards % 2 == 0 ? true : false);
    }

    internal void InitialiseCardGenerator()
    {
        arrayOfCards = new CardData[totalCards];
        totalNumberOfPairs = totalCards / 2;
        shuffledArrayOfCards = new List<int>();
        unShuffledArray = new List<int>();
        unShuffledArray = Enumerable.Range(1, totalCards).ToList();

    }

    internal void CreateShuffledIndexes()
    {
        for (int i = 1; i <= totalCards; i++)
        {
            int tempRandValue = Random.Range(0, unShuffledArray.Count);
            shuffledArrayOfCards.Add(unShuffledArray[tempRandValue]); // >= 1
            unShuffledArray.RemoveAt(tempRandValue);
        }

    }

    internal void CreateMatchPairs()
    {
        CardData tempData;
        Color tempColor = new Color();

        for (int pairs = 1; pairs <= totalNumberOfPairs; pairs++)
        {
            tempColor = ReturnRandomColor();

            tempData = new CardData();
            tempData.cardId = pairs;
            tempData.cardColor = tempColor;

            arrayOfCards[(shuffledArrayOfCards[(pairs * 2) - 2])-1] = tempData;
            arrayOfCards[(shuffledArrayOfCards[(pairs *2) - 1])-1] = tempData;
        }

    }
    private Color ReturnRandomColor()
    {
        Color randomColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255),255);
        return randomColor;
    }


    ~CardGenerator()
    {
        unShuffledArray.Clear();
        shuffledArrayOfCards.Clear();

    }

}
