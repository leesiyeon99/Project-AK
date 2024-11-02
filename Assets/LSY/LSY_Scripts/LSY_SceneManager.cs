using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LSY_SceneManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static LSY_SceneManager Instance { get; private set; }

    [SerializeField] GameState curState;

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
        ScoreUIManager.Instance.ResetScore();
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
        //점수판 나오는 곳으로 씬을 로딩하거나, 씬을 보여줌
        ScoreUIManager.Instance.LoseScoreLine(ScoreUIManager.Instance.score);
    }

    public void GameClear()
    {
        curState = GameState.GameClear;
        ScoreUIManager.Instance.WinScoreLine(ScoreUIManager.Instance.score);
        // 다음씬으로
    }

    public void ReStart()
    {

    }

    public void PlayerDied()
    {
        GameOver();
    }

    public void LoadScene(int index)
    {
        LoadScene(index);
    }
}
