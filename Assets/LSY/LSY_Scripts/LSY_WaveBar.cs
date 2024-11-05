using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSY_WaveBar : MonoBehaviour
{
    static public LSY_WaveBar instance {  get; private set; }

    [SerializeField] Image waveBarImage;

    public float maxCount = 0;
    public float curCount = 0;
    public float wavePercent;

    private void Start()
    {
        MaxCount();
        CurCount();
    }
    private void Update()
    {
        CurCount();

        wavePercent = curCount / maxCount;
        waveBarImage.fillAmount = 1 - wavePercent;
    }

    private float MaxCount()
    {
        maxCount = 0;

        for (int i = 0; i < WaveTimeline.Instance.waveCount.Length; i++)
        {
            maxCount += WaveTimeline.Instance.waveCount[i];
        }

        return maxCount;
    }

    private float CurCount()
    {
        curCount = 0;

        for (int i = 0; i < WaveTimeline.Instance.waveCount.Length; i++)
        {
            curCount += WaveTimeline.Instance.waveCount[i];
        }

        return curCount;
    }
}
