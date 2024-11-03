using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WHS_DollyProgress : MonoBehaviour
{
    // 맵 진행상황
    // DollyCart의 진행도에 따라 0에서 1까지 슬라이더 이동

    private static WHS_DollyProgress instance;

    public static WHS_DollyProgress Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] Image progressBar;

    private CinemachinePathBase path;

    public float progress;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        path = dollyCart.m_Path; 
    }


    private void Update()
    {
        float progress = Mathf.Clamp01(dollyCart.m_Position / path.PathLength); // 카트의 위치 / 트랙의 길이
        progressBar.fillAmount = progress;
    }

}
