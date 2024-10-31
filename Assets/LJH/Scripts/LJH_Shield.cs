using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("역장 수리 오브젝트")]
    [SerializeField] GameObject shieldRecover;
    [Header("역장 무적 오브젝트")]
    [SerializeField] GameObject invincibility;

    [Header("스크립트")]
    [Header("UIManager 스크립트")]
    [SerializeField] LJH_UIManager uiManagerScript;
    [Header("monsterTest 스크립트")]
    [SerializeField] LJH_monsterTest monsterScript;

    [Header("플레이어 위치")]
    [SerializeField] GameObject playerPos;

    [Header("키입력")]
    [Header("역장 온오프 키입력")]
    [SerializeField] InputActionReference shieldOnOff;
    [Header("데미지 테스트 키입력")]
    [SerializeField] InputActionReference damageTest; // 테스트 끝나고 지워야함
    [Header("총 발사 키입력")]
    [SerializeField] InputActionReference fire;

    [Header("오디오")]
    [Header("역장 피해시 사운드")]
    [SerializeField] AudioSource damaged;
    [Header("역장 파괴시 사운드")]
    [SerializeField] AudioSource breaked;

    [Header("변수")]
    [Header("역장 활성화 여부")]
    public bool isShield;                         // Comment: 역장 활성화 여부   필요없으면 삭제 예정
    [Header("역장 파괴/비파괴 여부")]
    public bool isBreaked;                        // Comment: 역장 파괴 상태
    [Header("역장 수리중 여부")]
    public bool isRecover;                        // Comment: 회복 실행 여부
    [Header("역장 무적 상태 여부")]
    public bool isInvincibility;
    [Header("역장 내구도")]
    public float durability;                      // Comment: 역장 내구도
    [Header("역장 최대 내구도")]
    public const float MAXDURABILITY = 5;         // Comment: 역장 최대 내구도
    [Header("피해량")]
    public float damage = 1;                      // Comment: 받은 피해량 ToDo: 몬스터의 쉴드 데미지 받아와야함
    [Header("몬스터의 공격 알람용 Bool 변수")]
    public bool isNow;


    private void Awake()
    {
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        isInvincibility = false;
        durability = MAXDURABILITY;
    }

    // Comment: 역장이 활성화 될 때
    private void OnEnable()
    {
        durability = MAXDURABILITY;

            isRecover = false;
            // Comment: 트리거 버튼에서 ShieldOn 제거
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 추가
            shieldOnOff.action.performed += ShieldOff;

            // Comment: 역장 활성화될 때 사격 기능 비활성화
            fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;
            fire.action.performed -= GetComponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed += DamagedShieldTest; // 테스트 끝나고 지워야함
        
    }

    // Comment: 역장이 비활성화 될 때
    private void OnDisable()
    {
            // Comment: 트리거 버튼에서 ShieldOn 추가
            shieldOnOff.action.performed += ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 제거
            shieldOnOff.action.performed -= ShieldOff;

            // Comment: 역장 비활성화될 때 사격 기능 활성화
            fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;
            fire.action.performed += GetComponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed -= DamagedShieldTest; // 테스트 끝나고 지워야함


        
    }

    private void Update()
    {
        // Comment: 역장의 위치는 플레이어 위치로 따라다니게
        transform.position = playerPos.transform.position;

        
        
        // Comment: 내구도가 0이 될 때, 역장 파괴
        if (durability <= 0)
        {
            BreakedShield();
        }

    }


    // Comment: 역장 활성화
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        // Comment: 방패 파괴 상태가 아닐때만 해당 함수 불러올 수 있도록
        if (!isBreaked)
        {
            // Comment: 방패 > 활성화 , 방패 수리 > 비활성화, 방패 여부 > 활성화
            gameObject.SetActive(true);
            shieldRecover.SetActive(false);
            isShield = true;
        }
    }

    // Comment: 역장 비활성화
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        // Comment: 방패 > 비활성화, 방패 수리 > 활성화, 방패 여부 > 비활성화 
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: 피격시 역장 내구도 1 감소 (테스트용 함수, 조립시 주석 처리 후 테스트에만 사용하거나 삭제)
    // ToDo:    몬스터의 타격 방식에 따라 내용 변경 필요
    public void DamagedShieldTest(InputAction.CallbackContext obj)// 인수 지워야함
    {
        if (durability > 0)
        {
            // ToDo : 피격시 사운드 구현해야함

            if (isInvincibility)
            {
                // Comment: 무적 상태일 때, 데미지를 0으로 변경
                float zeroDamage = 0;

                durability -= zeroDamage;
            }
            else if (!isInvincibility)
            {

                durability -= 1;
                uiManagerScript.UpdateShieldUI(durability);

                isNow = true;
                Debug.Log(isNow);


                invincibility.SetActive(true);
            }

            damaged.Play();
        }
    }

    // Comment: 역장 파괴, 역장이 비활성화되며 isBreaked 변수에 값 전달
    public void BreakedShield()
    {
        isRecover = true;
        isBreaked = true;
        isShield = false;
        shieldRecover.SetActive(true);
        
        gameObject.SetActive(false);
        

        breaked.Play();
        Debug.Log("역장이 파괴되었습니다.");
        
    }

    


}
