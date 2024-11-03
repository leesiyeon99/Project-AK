using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioMixer audioMixer;


    public void Mute(bool mute)
    {
        if (mute)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
         

        }
        else
        {
            audioMixer.SetFloat("MasterVolume", 0f);

        }
    }

    public void PlaySE(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }


    // ΩÃ±€≈Ê
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;

        }
    }

    void Awake()
    {
        if (instance == null)
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
        if (WHS_StageIndex.curStage == 0)
        {
            Debug.Log("CURStage0");
        }
        else if (WHS_StageIndex.curStage == 1)
        {
            Debug.Log("CURStage1");
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            Debug.Log("CURStage2");
        }
    }

}
