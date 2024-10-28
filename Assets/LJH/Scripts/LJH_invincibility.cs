using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    GameObject shield;

    private void Start()
    {
        shield = GameObject.Find("Shield");
    }

    void OnEnable()
    {
        Debug.Log("公利 惯悼");
        shield.GetComponent<LJH_Shield>().isInvincibility = true;

        Destroy(gameObject, 0.2f);
    }

    void OnDisable()
    {
        Debug.Log("公利 秦力");
        shield.GetComponent<LJH_Shield>().isInvincibility = false;
    }
}
