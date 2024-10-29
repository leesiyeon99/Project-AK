using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [SerializeField] GameObject shield;
    public bool isInvincibility;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(!isInvincibility)
        Debug.Log("公利 惯悼");
        isInvincibility = true;
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
        Invoke("ObjOff", 0.2f);
    }

    void OnDisable()
    {
        Debug.Log("公利 秦力");
        isInvincibility = false;
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
    }

    void ObjOff()
    {
        gameObject.SetActive(false);
    }
}
