using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject shieldRecover;

    [Header("플레이어 위치")]
    [SerializeField] GameObject playerPos;

    [Header("키입력")]
    [SerializeField] InputActionReference shieldOnOff;
    [SerializeField] InputActionReference damageTest; // 테스트 끝나고 지워야함

    [Header("변수")]
    public bool isShield;          // Comment: 역장 활성화 여부
    public bool isBreaked;                // Comment: 역장 파괴 상태
    public float durability;       // Comment: 역장 내구도

    private void Start()
    {
        

        gameObject.SetActive(false);

        isShield = false;
        isBreaked = false;
        durability = 5;
    }
    // Comment: 역장이 활성화 될 때
    private void OnEnable()
    {
        // Comment: 트리거 버튼에서 ShieldOn 제거
        shieldOnOff.action.performed -= ShieldOn;
        // Comment: 트리거 버튼에서 ShiledOff 추가
        shieldOnOff.action.performed += ShieldOff;

        damageTest.action.performed += DamagedShield; // 테스트 끝나고 지워야함
    }

    // Comment: 역장이 비활성화 될 때
    private void OnDisable()
    {
        // Comment: 트리거 버튼에서 ShieldOn 추가
        shieldOnOff.action.performed += ShieldOn;
        // Comment: 트리거 버튼에서 ShiledOff 제거
        shieldOnOff.action.performed -= ShieldOff;

        damageTest.action.performed -= DamagedShield; // 테스트 끝나고 지워야함
    }

    private void Update()
    {
        // Comment: 역장의 위치는 플레이어 위치로 따라다니게
        transform.position = playerPos.transform.position;

            if (durability < 1)
            {
                BreakedShield();
            }
        

       // if () 
       // {
       //     DamagedShield();
       // }

        
    }


    // Comment: 역장 활성화
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(true);
        isShield = true;
    }

    // Comment: 역장 비활성화
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: 피격시 역장 내구도 1 감소
    // ToDo:    몬스터의 타격 방식에 따라 내용 변경 필요
    public void DamagedShield(InputAction.CallbackContext obj)// 인수 지워야함
    {
        // ToDo : 피격시 사운드 구현해야함
        durability -= 1;
        Debug.Log(durability);
    }

    // Comment: 역장 파괴, 역장이 비활성화되며 isBreaked 변수에 값 전달
    public void BreakedShield()
    {
        gameObject.SetActive(false);
        shieldRecover.SetActive(true);
        isBreaked = true;
        Debug.Log("역장이 파괴되었습니다.");
        
    }

    
}
