using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart dolly;
    [SerializeField] EachTimeLine timeLine;
  
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player")
        {
            dolly.m_Speed = 0;
            timeLine.Play();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dolly.m_Speed = 5;
        }
    }

    public void ResumeDolly()
    {
        dolly.m_Speed = 5;
    }
}
