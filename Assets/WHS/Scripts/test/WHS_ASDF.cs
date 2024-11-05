using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_ASDF : MonoBehaviour
{

    [SerializeField] private CinemachineDollyCart dollyCart;

    private void Update()
    {
        float curPos = dollyCart.m_Position;

        //WHS_DollyProgress.Instance.UpdateProgress(curPos);
    }
}
