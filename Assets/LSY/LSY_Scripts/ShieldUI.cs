using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject lsy_shieldRecover;
    [SerializeField] GameObject lsy_invincibility;

    [Header("플레이어 위치")]
    [SerializeField] GameObject lsy_playerPos;

    [Header("키입력")]
    [SerializeField] InputActionReference lsy_shieldOnOff;
    [SerializeField] InputActionReference lsy_damageTest; // 테스트 끝나고 지워야함
    [SerializeField] InputActionReference lsy_fire;

    [Header("오디오")]
    [SerializeField] AudioSource lsy_damaged;
    [SerializeField] AudioSource lsy_breaked;

    [Header("변수")]
    public bool lsy_isShield;                         // Comment: 역장 활성화 여부   필요없으면 삭제 예정
    public bool lsy_isBreaked;                        // Comment: 역장 파괴 상태
    public bool lsy_isRecover;                        // Comment: 회복 실행 여부
    public bool lsy_isInvincibility;

    public float lsy_durability;                      // Comment: 역장 내구도
    public const float lsy_MAXDURABILITY = 5;         // Comment: 역장 최대 내구도
    public float lsy_damage = 1;                      // Comment: 받은 피해량                                ToDo: 몬스터의 데미지로 구현해야함

    //이시연씀
    [Header("UI")]
    int lsy_shieldCount = 4;
    public Image[] shieldImages = new Image[5];


    private void Awake()
    {
        gameObject.SetActive(false);
        lsy_isRecover = false;
        lsy_isShield = false;
        lsy_isBreaked = false;
        lsy_isInvincibility = false;
        lsy_durability = lsy_MAXDURABILITY;
    }

    // Comment: 역장이 활성화 될 때
    private void OnEnable()
    {
        lsy_durability = lsy_MAXDURABILITY;
        //isBreaked = shieldRecover.GetComponent<LJH_ShieldRecover>().isBreaked;
        //isRecover = shieldRecover.GetComponent<LJH_ShieldRecover>().isRecover;
        //isShield = shieldRecover.GetComponent<LJH_ShieldRecover>().isShield;

        lsy_isRecover = false;
        // Comment: 트리거 버튼에서 ShieldOn 제거
        lsy_shieldOnOff.action.performed -= LSY_ShieldOn;

        // Comment: 트리거 버튼에서 ShiledOff 추가
        lsy_shieldOnOff.action.performed += LSY_ShieldOff;

        // Comment: 역장 활성화될 때 사격 기능 비활성화
        //fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;        총기와 연계 내용이라 머지 이후 주석처리 제거
        //fire.action.performed -= Getcomponent<PlayerInputWeapon>().OffFire;

        lsy_damageTest.action.performed += LSY_DamagedShield; // 테스트 끝나고 지워야함

    }

    // Comment: 역장이 비활성화 될 때
    private void OnDisable()
    {

        // Comment: 트리거 버튼에서 ShieldOn 추가
        lsy_shieldOnOff.action.performed += LSY_ShieldOn;

        // Comment: 트리거 버튼에서 ShiledOff 제거
        lsy_shieldOnOff.action.performed -= LSY_ShieldOff;

        // Comment: 역장 비활성화될 때 사격 기능 활성화
        //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;         총기와 연계 내용이라 머지 이후 주석처리 제거
        //fire.action.performed += Getcomponent<PlayerInputWeapon>().OffFire;

        lsy_damageTest.action.performed -= LSY_DamagedShield; // 테스트 끝나고 지워야함



    }

    private void Update()
    {
        // Comment: 역장의 위치는 플레이어 위치로 따라다니게
        transform.position = lsy_playerPos.transform.position;

        if (lsy_durability <= 0)
        {
            LSY_BreakedShield();
        }

    }


    // Comment: 역장 활성화
    public void LSY_ShieldOn(InputAction.CallbackContext obj)
    {
        if (!lsy_isBreaked)
        {
            gameObject.SetActive(true);
            lsy_shieldRecover.SetActive(false);
            lsy_isShield = true;
        }
    }

    // Comment: 역장 비활성화
    public void LSY_ShieldOff(InputAction.CallbackContext obj)
    {
        lsy_isRecover = true;
        lsy_shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        lsy_isShield = false;
    }

    // Comment: 피격시 역장 내구도 1 감소
    // ToDo:    몬스터의 타격 방식에 따라 내용 변경 필요
    public void LSY_DamagedShield(InputAction.CallbackContext obj)// 인수 지워야함
    {
        if (lsy_durability > 0)
        {
            Debug.Log("역장 피해입음");
            // ToDo : 피격시 사운드 구현해야함

            if (lsy_isInvincibility)
            {
                lsy_damage = 0;
                lsy_durability -= lsy_damage;
            }
            else if (!lsy_isInvincibility)
            {
                lsy_durability -= lsy_damage;
                Instantiate(lsy_invincibility);
            }

            lsy_damaged.Play();
            Debug.Log(lsy_durability);
        }
    }

    // Comment: 역장 파괴, 역장이 비활성화되며 isBreaked 변수에 값 전달
    public void LSY_BreakedShield()
    {
        lsy_isRecover = true;
        lsy_isBreaked = true;
        lsy_isShield = false;
        lsy_shieldRecover.SetActive(true);

        gameObject.SetActive(false);


        lsy_breaked.Play();
        Debug.Log("역장이 파괴되었습니다.");

    }
}
