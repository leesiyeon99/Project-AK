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
    public LJH_DamageManager damageManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        curState = GameState.Ready;
        playerTransform = GameObject.FindWithTag("Player").transform;
        damageManager = FindObjectOfType<LJH_DamageManager>();
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
        damageManager.ljh_bloodImage.gameObject.SetActive(false);
    }
    public void GameClear()
    {
        curState = GameState.GameClear;
        if (WHS_StageIndex.curStage == 1)
        {
            PlayerRespawnStage1.Instance.lsy_isdie = true;
        }
        ScoreUIManager.Instance.WinScoreLine();
        nextTextButton.action.Enable();
        nextTextButton.action.performed += NextRoad;
    }
    public void ReStart()
    {
        if (WHS_StageIndex.curStage == 1)
        {
            SceneManager.LoadScene("KSJ1Stage");
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            SceneManager.LoadScene("KYH_Stage2");
        }
    }
    public void PlayerDied()
    {
        GameOver();
        if (WHS_StageIndex.curStage == 1)
        {
            PlayerRespawnStage1.Instance.lsy_isdie = true;
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            PlayerRespawnStage1.Instance.lsy_isdie = false;
        }
        ScoreUIManager.Instance.LoseScoreLine();
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
        if (WHS_StageIndex.curStage == 1)
        {
            if (curState == GameState.GameOver)
            {
                SceneManager.LoadScene("KSJ1Stage");
                GameStart();
            }
            else
            {
                SceneManager.LoadScene("KYH_Stage2");
            }
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            SceneManager.LoadScene("KYH_Stage2");
        }
        nextTextButton.action.Disable();
        nextTextButton.action.performed -= NextRoad;
    }
}