using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerRespawnStage1.Instance == null) return;
        if (PlayerRespawnStage1.Instance.lsy_isdie == true)
        {
            gameObject.transform.position = new Vector3(0, 1, 0);
            WaveTimeline.Instance.Play();
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.position = new Vector3(0, 1, 0);
            WaveTimeline.Instance.Play();
        }
    }
}
