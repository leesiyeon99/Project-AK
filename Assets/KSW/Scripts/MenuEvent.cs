using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEvent : MonoBehaviour
{

    [Header("- 일시정지")]
    [SerializeField] private InputActionReference pause;

    [Header("- 음소거")]
    [SerializeField] private InputActionReference mute;

    PlayerInputWeapon playerInputManager;
    AudioManager audioManager;

    private void Awake()
    {
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
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1f;
            playerInputManager.enabled = true;
        }
        else
        {
            Time.timeScale = 0f;
            playerInputManager.enabled = false;
        }

       
    }

    void OnMute(InputAction.CallbackContext obj)
    {
        audioManager.Mute();

    }
}
