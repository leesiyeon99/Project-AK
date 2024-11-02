using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EachTimeLine : MonoBehaviour
{


    PlayableDirector director;
    [SerializeField] PlayableAsset[] wave;
    [SerializeField] public int[] waveCount;
    [SerializeField] int waveIndex;

    [SerializeField] StopPoint sp;

    public void Play()
    {
        director.playableAsset = wave[waveIndex];
        director.Play();
    }
    public void DecreaseWaveCount()
    {
        waveCount[waveIndex]--;

        if (waveCount[waveIndex] <= 0)
        {

            waveIndex++;
            if (waveCount.Length <= waveIndex)
            {
                Debug.Log("³¡");
                sp.ResumeDolly();
                return;
            }
            Play();
        }
    }
}
