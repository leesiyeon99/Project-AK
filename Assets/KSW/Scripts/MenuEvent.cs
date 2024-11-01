using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEvent : MonoBehaviour
{

    [Header("- 일시정지")]
    [SerializeField] private InputActionReference pause;

    [Header("- 음소거")]
    [SerializeField] private InputActionReference mute;

    [Header("- 메뉴 UI")]
    [SerializeField] private GameMenuUI menu;


    AudioManager audioManager;

    private bool isPause;
    private bool isMute;


    // 싱글톤
    private static MenuEvent instance;
    public static MenuEvent Instance
    {
        get
        {
            return instance;

        }
    }
    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {

        pause.action.performed += OnPause;
        mute.action.performed += OnMute;


    }
    private void OnDisable()
    {
        pause.action.performed -= OnPause;
        mute.action.performed -= OnMute;

    }

   

    void OnPause(InputAction.CallbackContext obj)
    {
        if (isPause)
        {
         
            Time.timeScale = 1f;
            PlayerInputWeapon.Instance.enabled = true;
            isPause = false;
         
        }
        else
        {
           
            Time.timeScale = 0f;
            PlayerInputWeapon.Instance.enabled = false;
            isPause = true;
           
        }

        menu.TogglePauseUI(isPause);
    }

    void OnMute(InputAction.CallbackContext obj)
    {
        if (isMute)
        {
            isMute = false;
          
        }
        else
        {
            isMute = true;
            
        }

        audioManager.Mute(isMute);
        menu.ToggleMuteUI(isMute);
    }





}
