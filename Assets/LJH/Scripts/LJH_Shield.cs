using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [SerializeField] GameObject playerPos;
    [SerializeField] InputActionReference shieldOnOff;

    bool isSheild;
    bool isBreaked;
    float durability = 5;

    private void Start()
    {
        gameObject.SetActive(false);

        isSheild = false;
        isBreaked = false;
        durability = 5;
    }
    // 방패가 활성화 될 때
    private void OnEnable()
    {
        // 트리거 버튼에서 ShieldOn 제거
        shieldOnOff.action.performed -= ShieldOn;
        // 트리거 버튼에서 ShiledOff 추가
        shieldOnOff.action.performed += ShieldOff;
    }

    // 방패가 비활성화 될 때
    private void OnDisable()
    {
        // 트리거 버튼에서 ShieldOn 추가
        shieldOnOff.action.performed += ShieldOn;
        // 트리거 버튼에서 ShiledOff 제거
        shieldOnOff.action.performed -= ShieldOff;
    }

    private void Update()
    {
        // 쉴드의 위치는 플레이어 위치로 따라다니게
        transform.position = playerPos.transform.position;
        
    }


    // 방패 활성화
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(true);
        isSheild = true;
    }

    // 방패 비활성화
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(false);
        isSheild = false;
    }

    public void BreakedShield()
    {
        gameObject.SetActive(false);
        isBreaked = true;
    }

    
    IEnumerator ShieldCoolDown()
    {
        // 2초간 방패 들기 불가
        yield return new WaitForSeconds(2.0f);

        // 2초 뒤 RecoveryShield 코루틴 실행
        Coroutine recovery = StartCoroutine("RecoveryShield");
    }

    IEnumerator RecoveryShield()
    {
        // 방패 들기 불가 + 3초 후 방패 수리 완료
        yield return new WaitForSeconds(3.0f);
        isBreaked = false;
    }
    
}
