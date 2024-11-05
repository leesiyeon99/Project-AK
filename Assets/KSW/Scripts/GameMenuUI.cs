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

    public void ToggleMuteUI(bool active)
    {
        muteUI.SetActive(active);
 
    }
    public void TogglePauseUI(bool active)
    {
        pauseUI.SetActive(active);
 
    }

  
}
