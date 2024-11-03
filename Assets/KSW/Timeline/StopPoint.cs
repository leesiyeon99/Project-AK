using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart dolly;
    [SerializeField] EachTimeLine timeLine;
    [SerializeField] WaveDialogue2Manager waveDialogue;
  
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player")
        {
            dolly.m_Speed = 0;
            timeLine.Play();
        }
    }

    public void ResumeDolly()
    {
        dolly.m_Speed = 20;
        waveDialogue.StartWave();
    }
}
