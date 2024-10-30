using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LJH_ScoreManager : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] public int scoreP;


    void Start()
    {
        score = 0;
        scoreP = score;
    }

    void Update()
    {
        if (score != scoreP)
        {
            score = scoreP;
        }
    }
}
