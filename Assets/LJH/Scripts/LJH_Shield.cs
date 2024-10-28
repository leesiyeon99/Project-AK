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
    [SerializeField] GameObject invincibility;

    [Header("플레이어 위치")]
    [SerializeField] GameObject playerPos;

    [Header("키입력")]
    [SerializeField] InputActionReference shieldOnOff;
    [SerializeField] InputActionReference damageTest; // 테스트 끝나고 지워야함
    [SerializeField] InputActionReference fire;

    [Header("오디오")]
    [SerializeField] AudioSource damaged;
    [SerializeField] AudioSource breaked;

    [Header("변수")]
    public bool isShield;                         // Comment: 역장 활성화 여부   필요없으면 삭제 예정
    public bool isBreaked;                        // Comment: 역장 파괴 상태
    public bool isRecover;                        // Comment: 회복 실행 여부
    public bool isInvincibility;

    public float durability;                      // Comment: 역장 내구도
    public const float MAXDURABILITY = 5;         // Comment: 역장 최대 내구도
    public float damage = 1;                      // Comment: 받은 피해량                                ToDo: 몬스터의 데미지로 구현해야함


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
       //isBreaked = shieldRecover.GetComponent<LJH_ShieldRecover>().isBreaked;
       //isRecover = shieldRecover.GetComponent<LJH_ShieldRecover>().isRecover;
       //isShield = shieldRecover.GetComponent<LJH_ShieldRecover>().isShield;

            isRecover = false;
            // Comment: 트리거 버튼에서 ShieldOn 제거
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 추가
            shieldOnOff.action.performed += ShieldOff;

            // Comment: 역장 활성화될 때 사격 기능 비활성화
            //fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;        총기와 연계 내용이라 머지 이후 주석처리 제거
            //fire.action.performed -= Getcomponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed += DamagedShield; // 테스트 끝나고 지워야함
        
    }

    // Comment: 역장이 비활성화 될 때
    private void OnDisable()
    {
        
            // Comment: 트리거 버튼에서 ShieldOn 추가
            shieldOnOff.action.performed += ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 제거
            shieldOnOff.action.performed -= ShieldOff;

            // Comment: 역장 비활성화될 때 사격 기능 활성화
            //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;         총기와 연계 내용이라 머지 이후 주석처리 제거
            //fire.action.performed += Getcomponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed -= DamagedShield; // 테스트 끝나고 지워야함


        
    }

    private void Update()
    {
        // Comment: 역장의 위치는 플레이어 위치로 따라다니게
        transform.position = playerPos.transform.position;

        if (durability <= 0)
        {
            BreakedShield();
        }

    }


    // Comment: 역장 활성화
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        if (!isBreaked)
        {
            gameObject.SetActive(true);
            shieldRecover.SetActive(false);
            isShield = true;
        }
    }

    // Comment: 역장 비활성화
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: 피격시 역장 내구도 1 감소
    // ToDo:    몬스터의 타격 방식에 따라 내용 변경 필요
    public void DamagedShield(InputAction.CallbackContext obj)// 인수 지워야함
    {
        if (durability > 0)
        {
            Debug.Log("역장 피해입음");
            // ToDo : 피격시 사운드 구현해야함

            if (isInvincibility)
            {
                damage = 0;
                durability -= damage;
            }
            else if (!isInvincibility)
            {
                durability -= damage;
                Instantiate(invincibility);
            }

            damaged.Play();
            Debug.Log(durability);
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
