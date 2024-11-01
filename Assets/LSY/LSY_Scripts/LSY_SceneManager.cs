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
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
        ReStart();
    }

    public void GameClear()
    {
        curState = GameState.GameClear;
        ReStart();
    }

    public void ReStart()
    {

    }

    public void PlayerDied()
    {
        ReStart();
        // TODO : »õ ¾À ·Îµå
    }

    public void LoadScene(int index)
    {
        LoadScene(index);
    }
}
