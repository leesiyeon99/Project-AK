using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage1 : MonoBehaviour
{
    [SerializeField] float nowHp;
    [SerializeField] float SetHp;
    //[SerializeField] float 



    private void Start()
    {
        gameObject.tag = "Boss";
    }
}
