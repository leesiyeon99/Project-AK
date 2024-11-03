using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollySpeed : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart dolly;
    [SerializeField] float speed;
    [SerializeField] float pos;
    [SerializeField] Transform cam;
    [SerializeField]  float angle;
    private void Awake()
    {
        
        dolly= GetComponent<CinemachineDollyCart>();
    }
   
    void Update()
    {
        if (pos < dolly.m_Position && dolly.m_Speed != 0)
        {
            dolly.m_Speed = speed;
        }
        if(695 < dolly.m_Position && dolly.m_Position< 710)
        {
            if(Mathf.Abs(angle)<20)
                angle -= Time.deltaTime*4;
           
            cam.transform.localRotation = Quaternion.Euler(angle, 16.7f, 0f);
        }
      
    }
}
