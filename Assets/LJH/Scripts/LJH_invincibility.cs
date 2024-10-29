using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [SerializeField] GameObject shield;
    public bool isInvincibility;

    

    void OnEnable()
    {
        if(!isInvincibility)
        Debug.Log("公利 惯悼");
        isInvincibility = true;
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
        Destroy(gameObject, 2f);
    }

    void OnDisable()
    {
        Debug.Log("公利 秦力");
        isInvincibility = false;
    }
}
