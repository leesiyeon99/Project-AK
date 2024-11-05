using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnStage1 : MonoBehaviour
{
    static public PlayerRespawnStage1 Instance { get; private set; }

    public bool lsy_isdie = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

}
