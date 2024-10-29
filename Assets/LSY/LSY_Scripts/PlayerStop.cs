using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cinemachineDollyCart;
    [SerializeField] GameObject[] monsters = new GameObject[6];
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StopPoint"))
        {
            cinemachineDollyCart.m_Speed = 0;
            Debug.Log("스피드 0");
        }
    }

    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        //TODO : 적 쪽과 같이 연계해서 해야 할듯? 몬스터 다 처치시 스피드 올라가도록
        //if (monsters.Length == 0)
        //{
        //    cinemachineDollyCart.m_Speed = 2;
        //}
    }
}
