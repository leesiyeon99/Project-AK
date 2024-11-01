using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class WaveTimeline : MonoBehaviour
{
    private static WaveTimeline instance;


    public static WaveTimeline Instance
    {
        get
        {
            return instance;

        }
    }
  
    PlayableDirector director;
    [SerializeField] PlayableAsset[] wave;
    [SerializeField] int waveIndex;
    [SerializeField] int waveCount;
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
        waveIndex++;
        director.Play();
    }
}
