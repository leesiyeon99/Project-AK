using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WHS_DollyProgress : MonoBehaviour
{
    // 맵 진행상황
    // DollyCart의 진행도에 따라 0에서 1까지 슬라이더 이동
    // UI 배치 논의 필요

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] Slider progressBar;

    private CinemachinePathBase path;

    private void Start()
    {
        path = dollyCart.m_Path;

        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }

    private void Update()
    {
        float progress = Mathf.Clamp01(dollyCart.m_Position / path.PathLength); // 카트의 위치 / 트랙의 길이
        progressBar.value = progress;
    }
}
