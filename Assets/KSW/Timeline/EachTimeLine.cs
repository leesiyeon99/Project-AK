using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EachTimeLine : MonoBehaviour
{


    PlayableDirector director;
    [SerializeField] PlayableAsset[] wave;
    [SerializeField] public int[] waveCount;
    [SerializeField] public int[] keyEnemyCount;
    [SerializeField] int waveIndex;
   
    [SerializeField] StopPoint sp;
    [SerializeField] bool keyEnemyExist;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void Play()
    {
        director.playableAsset = wave[waveIndex];
        director.Play();
    }
    public void DecreaseWaveCount(bool isKey)
    {
        

      

            for (int i = 0; i < waveCount.Length; i++)
            {
                if (waveCount[i] > 0)
                {
                    waveCount[i]--;
                    if (keyEnemyExist && keyEnemyCount[i] > 0 )
                    {
                        if (isKey)
                        {
                            keyEnemyCount[i]--;
                        }
                        if (keyEnemyCount[i] <= 0)
                        {
                            waveIndex++;
                            
                            Play();
                        }
                       
                    }
                    else if (waveCount[i] <= 0)
                    {
                       
                        
                            waveIndex++;
                        
                       
                        if (WaveEndCheck() <= 0)
                        {
                            sp.ResumeDolly();
                            return;
                        }
                        if (waveCount.Length > waveIndex)
                        {
                            Play();
                        }
                    }
                    break;
                }
            }
        

     
       
    }

    int WaveEndCheck()
    {
        int check = 0;
        for (int i = 0; i < waveCount.Length; i++)
        {
            if(waveCount[i] > 0)
            {
                check++;
                break;
            }
            
        }

        return check;
    }
}
