using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance = null;
    
    // Actions
    public static Action StartCardGenerator;
    public static Action GameOverCalled;

    internal CardGenerator currentCardGenerator;


    public static GameManager ReturnInstance()
    {
        return Instance;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // restricting contructor to create another instance
    private GameManager() { }

    [SerializeField] private GameObject screenGamePlay, screenMainMenu, screenResult;
    [SerializeField] private Text txtTimeTaken, txtGameScreenTimeTaken, txtGameScreenCounts,txtPauseBtn;
    [SerializeField] private MatchLogicController matchLogicController;
    [SerializeField] internal bool isGameSarted = false;
    private double timeInSeconds = 0;
    private string minutes="", seconds= "";

    private void OnEnable()
    {
        GridController.GridGenerated += EnableGamePlay;
    }

    public void OnStartClicked()
    {
        StartCardGenerator.Invoke();   
    }

    private void EnableGamePlay(CardGenerator cardGeneratorOBJ)
    {
        Debug.Log(cardGeneratorOBJ);
        screenGamePlay.SetActive(true);
        screenMainMenu.SetActive(false);
        currentCardGenerator = cardGeneratorOBJ;
        isGameSarted = !isGameSarted;
        timeInSeconds = 0;
    }

    private void Update()
    {
        if (!isGameSarted) return;

        timeInSeconds += Time.deltaTime;
        minutes = Mathf.Floor((float)(timeInSeconds / 60)).ToString("00");
        seconds = Mathf.Floor((float)(timeInSeconds % 60)).ToString("00");
        txtTimeTaken.text = String.Format("Time Taken - {0}:{1}", minutes, seconds);

    }

    public void HandlePauseAndResumeGame()
    {
        if(isGameSarted)
        {
            Time.timeScale = 0.0f;
            isGameSarted = false;
            txtPauseBtn.text = "Play";
            return;
        }
        if(!isGameSarted)
        {
            Time.timeScale = 1.0f;
            isGameSarted = true;
            txtPauseBtn.text = "Pause";
            return;
        }
    }

    internal void GameOver()
    {
        GameOverCalled.Invoke();
        screenResult.SetActive(true);
        screenGamePlay.SetActive(false);
        isGameSarted = false;
        txtGameScreenCounts.text = "Tries : " + matchLogicController.ReturnTotalCounts();
        txtGameScreenTimeTaken.text = String.Format("Time Taken - {0}:{1}", minutes, seconds);
    }

    private void OnDisable()
    {
        GridController.GridGenerated -= EnableGamePlay;
    }

}
