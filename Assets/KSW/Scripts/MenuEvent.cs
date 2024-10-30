using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEvent : MonoBehaviour
{

    [Header("- ¿œΩ√¡§¡ˆ")]
    [SerializeField] private InputActionReference pause;

    [Header("- ¿Ωº“∞≈")]
    [SerializeField] private InputActionReference mute;

    PlayerInputWeapon playerInputManager;
    AudioManager audioManager;

    private bool isPause;
    private bool isMute;


    // ΩÃ±€≈Ê
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

    public void SetPlayerWeaponInput(PlayerInputWeapon input)
    {
        playerInputManager = input;
    }

    void OnPause(InputAction.CallbackContext obj)
    {
        if (isPause)
        {
            Time.timeScale = 1f;
            playerInputManager.enabled = true;
            isPause = false;
        }
        else
        {
            Time.timeScale = 0f;
            playerInputManager.enabled = false;
            isPause = true;
        }

       
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

    }





}
