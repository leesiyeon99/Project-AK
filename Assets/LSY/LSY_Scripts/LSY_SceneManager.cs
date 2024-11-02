using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LSY_SceneManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static LSY_SceneManager Instance { get; private set; }

    [SerializeField] GameState curState;

    public InputActionReference nextTextButton;

    public Transform playerTransform;

    public bool lsy_isdie = false;

    private void Awake()

    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        curState = GameState.Ready;
    }


    private void Update()
    {
        if (curState == GameState.Ready)
        {
            GameReady();
        }
        if (curState == GameState.Running)
        {
            GameStart();
        }
        else if (curState == GameState.GameOver)
        {
            PlayerDied();
            curState = GameState.Running;
        }
        else if (curState == GameState.GameClear)
        {
            curState = GameState.Ready;
        }

    }

    public void GameReady()
    {
        curState = GameState.Ready;
    }

    public void GameStart()
    {
        curState = GameState.Running;
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
    }

    public void GameClear()
    {
        curState = GameState.GameClear;
        lsy_isdie = true;
        ScoreUIManager.Instance.WinScoreLine();
        DisplayScoreScreen(); 
    }

    public void ReStart()
    {   
        //if (WHS_StageSelectScene.Instance.curStage == 1)
        //{
        //    SceneManager.LoadScene("KSJ1Stage");
        //}
        //else if (WHS_StageSelectScene.Instance.curStage == 2)
        //{
            
        //}
    }

    public void PlayerDied()
    {
        GameOver();
        lsy_isdie = true;
        ScoreUIManager.Instance.LoseScoreLine();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("KSJ1Stage");
        }
        DisplayScoreScreen();

    }

    private void DisplayScoreScreen()
    {
        nextTextButton.action.Enable();
        nextTextButton.action.performed += NextRoad;
        ScoreUIManager.Instance.LoseScoreLine();

    }

    void NextRoad(InputAction.CallbackContext obj)
    {
        //if (WHS_StageSelectScene.Instance.curStage == 1)
        //{
        //    SceneManager.LoadScene("KSJ1Stage");
        //}
        //else if (WHS_StageSelectScene.Instance.curStage == 2)
        //{

        //}
    }

}
