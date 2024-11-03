using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource titleSource;
    [SerializeField] AudioSource stage1Source;
    [SerializeField] AudioSource stage2Source;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioClip titleClip;
    [SerializeField] AudioClip stage1Clip;
    [SerializeField] AudioClip stage2Clip;


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
        //seSource.PlayOneShot(clip);
    }

    public void PlayTitle(AudioClip clip)
    {
        titleSource.clip = clip;
        titleSource.Play();
    }

    public void PlayStage1(AudioClip clip)
    {
        stage1Source.clip = clip;
        stage1Source.Play();
    }

    public void PlayStage2(AudioClip clip)
    {
        stage2Source.clip = clip;
        stage2Source.Play();
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
            PlayTitle(titleClip);
        }
        else if (WHS_StageIndex.curStage == 1)
        {
            PlayStage1(stage1Clip);
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            PlayStage2(stage2Clip);
        }
    }

}
