using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSY_SceneManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static LSY_SceneManager Instance { get; private set; }

    [SerializeField] GameState curState;


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
        //if (아무키나 누르면)
        //다음씬으로
    }

    public void ReStart()
    {   
        //만약 현재씬이 스테이지 1이라면
        SceneManager.LoadScene("KSJ1Stage");
        //만약 현재씬이 스테이지 2라면
    }

    public void PlayerDied()
    {
        GameOver();
        lsy_isdie = true;
        ScoreUIManager.Instance.WinScoreLine();
        //if (아무키나 누르면)
        ReStart();
    }

    //public void LoadScene(int index)
    //{
    //    LoadScene(index);
    //}
}
