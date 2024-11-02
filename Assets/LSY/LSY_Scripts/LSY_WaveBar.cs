using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSY_WaveBar : MonoBehaviour
{
    [SerializeField] Image waveBarImage;

    int maxCount = 0;
    int curCount = 0;

    private void Start()
    {
        MaxCount();
        CurCount();
    }
    private void Update()
    {
        CurCount();

        float wavePercent = (float)curCount / maxCount;
        waveBarImage.fillAmount = 1 - wavePercent;
    }

    private int MaxCount()
    {
        maxCount = 0;

        for (int i = 0; i < WaveTimeline.Instance.waveCount.Length; i++)
        {
            maxCount += WaveTimeline.Instance.waveCount[i];
        }

        return maxCount;
    }

    private int CurCount()
    {
        curCount = 0;

        for (int i = 0; i < WaveTimeline.Instance.waveCount.Length; i++)
        {
            curCount += WaveTimeline.Instance.waveCount[i];
        }

        return curCount;
    }
}
