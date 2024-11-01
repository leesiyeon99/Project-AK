using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class WaveTimeline : MonoBehaviour
{
    PlayableDirector director;
    [SerializeField] PlayableAsset[] wave;
    [SerializeField] int waveIndex; 
    private void Awake()
    {
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
