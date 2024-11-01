using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class WaveTimeline : MonoBehaviour
{
  
    PlayableDirector director;
    [SerializeField] PlayableAsset[] wave;
    [SerializeField] public int[] waveCount;
    [SerializeField] int waveIndex;
   

    private static WaveTimeline instance;


    public static WaveTimeline Instance
    {
        get
        {
            return instance;

        }
    }

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
        director = GetComponent<PlayableDirector>();
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }

    }

    public void Play()
    {
        director.playableAsset = wave[waveIndex];
        director.Play();
    }
    public void DecreaseWaveCount()
    {
        waveCount[waveIndex]--;

        if(waveCount[waveIndex] <= 0)
        {
            
            waveIndex++; 
            if (waveCount.Length <= waveIndex)
            {
                Debug.Log("³¡");
                return;
            }
            Play();
        }
    }

}
