using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [SerializeField] GameObject ObjShieldPrefab;
    [SerializeField] GameObject playerPos;
    [SerializeField] InputActionReference shieldOn;

    bool isBreaked;
    float durability = 5;

    private void Start()
    {
        ObjShieldPrefab = gameObject;
        //shieldOn.action.performed += ShieldOn;

        isBreaked = false;
        durability = 5;
    }
    
    private void Update()
    {
        transform.position = playerPos.transform.position; // 쉴드의 위치는 플레이어 위치로 따라다니게
        
    }

   

    public void ShieldOn()                                  // 방패 활성화
    {
        ObjShieldPrefab.SetActive(true);
    }

    public void ShieldOff()                                 // 방패 비활성화
    {
        ObjShieldPrefab.SetActive(false);
    }

    public void BreakedShield()
    {
        ObjShieldPrefab.SetActive(false);
        isBreaked = true;
    }

    /*
    IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSecond(2.0f);               // 2초간 방패 들기 불가
        Coroutine recovery = StartCoroutine("RecoveryShield");
    }

    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecond(3.0f);               // 방패 들기 불가 + 3초 후 방패 수리 완료
        isBreaked = false;
    }
    */
}
