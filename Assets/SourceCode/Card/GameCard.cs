using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameCard : CardElement
{
    [SerializeField] private GameObject imageWithData, imageHide;
    [SerializeField] private int speed = 7;
    [SerializeField] private Text textCardId;
    [SerializeField] private Image cardBG;
    private bool isSwappingInProgress = false;
    private GameManager gameManager;

    private Button btnCard;
    private int fromScale = 0, toScale = 1;

    private void Awake()
    {
        btnCard = gameObject.GetComponent<Button>();
        gameManager = GameManager.ReturnInstance();
    }
    private void OnEnable()
    {
        btnCard.onClick.AddListener(OnClick);
        GameManager.GameOverCalled += DestroyObject;
    }
    public override void Initialse(CardData cardData)
    {
        cardIdentifier = cardData.cardId.ToString();
        isCardFacedUp = false;
        textCardId.text = cardIdentifier;
        cardBG.color = cardData.cardColor;

    }

    public override void OnClick()
    {
        if (!gameManager.isGameSarted) return;

        if (isSwappingInProgress) return;

        if(!isCardFacedUp)
        {
            DoFlip();
            MatchLogicController.RegisterClicks.Invoke(this);
            isSwappingInProgress = true;
        }
            
    }
    public override void DoFlip()
    {
        StartCoroutine(StartFlipping());
    }

    private void SaveFlippedState()
    {
        isCardFacedUp = !isCardFacedUp;
    }

    private void SwapImages()
    {
        imageWithData.SetActive(!imageWithData.activeSelf);
        imageHide.SetActive(!imageHide.activeSelf);
    }

    IEnumerator StartFlipping()
    {
        float amount = 1f;

        yield return new WaitForEndOfFrame();
        while (transform.localScale.y > 0.03f)
        {
            amount -= (speed * Time.deltaTime);
            transform.localScale = new Vector3(1, amount, 1);
            yield return new WaitForEndOfFrame();

        }

        amount = 0;

        SwapImages();

        yield return new WaitForEndOfFrame();

        while (transform.localScale.y < 1)
        {
            amount += (speed * Time.deltaTime);
            amount = Mathf.Clamp(amount, fromScale, toScale);
            transform.localScale = new Vector3(1, amount, 1);
            yield return new WaitForEndOfFrame();
        }
        SaveFlippedState();
        isSwappingInProgress = false;

    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        btnCard.onClick.RemoveListener(OnClick);
        GameManager.GameOverCalled -= DestroyObject;

    }
}
