using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuUI : BaseUI
{
    private GameObject muteUI;
    private GameObject pauseUI;

    private void Awake()
    {
        
        Bind();
        pauseUI = GetUI("Pause");
        muteUI = GetUI("Mute");
    }

    private void Start()
    {
        CameraOff();
    }

    public void ToggleMuteUI(bool active)
    {
        muteUI.SetActive(active);
        CameraOff();
    }
    public void TogglePauseUI(bool active)
    {
        pauseUI.SetActive(active);
        CameraOff();
    }

    void CameraOff()
    {
        if(!muteUI.activeSelf && !pauseUI.activeSelf)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            transform.parent.gameObject.SetActive(true);
        }
    }
}
