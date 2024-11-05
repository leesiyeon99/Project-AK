using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("쉴드 오브젝트")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject damageManager;

    [Header("변수")]
    [Header("역장 무적 여부")]
    public bool isInvincibility;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(!isInvincibility)
        isInvincibility = true;
        damageManager.GetComponent<LJH_DamageManager>().isInvincibility = isInvincibility;
        Invoke("ObjOff", 0.2f);
    }

    void OnDisable()
    {
        isInvincibility = false;
        if (damageManager == null)
        {
            return;
        }
        else
        {
            damageManager.GetComponent<LJH_DamageManager>().isInvincibility = isInvincibility;
        }
    }

    void ObjOff()
    {
        gameObject.SetActive(false);
    }
}
