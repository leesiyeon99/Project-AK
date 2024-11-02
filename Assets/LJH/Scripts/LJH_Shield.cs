using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject shieldRecover;
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject invincibility;

    [Header("��ũ��Ʈ")]
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager uiManagerScript;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] GameObject playerPos;

    [Header("Ű�Է�")]
    [Header("���� �¿��� Ű�Է�")]
    [SerializeField] InputActionReference shieldOnOff;
    [Header("�� �߻� Ű�Է�")]
    [SerializeField] InputActionReference fire;

    [Header("�����")]
    [Header("���� ���ؽ� ����")]
    [SerializeField] AudioSource damaged;
    [Header("���� �ı��� ����")]
    [SerializeField] AudioSource breaked;

    [Header("����")]
    [Header("���� Ȱ��ȭ ����")]
    public bool isShield;                         // Comment: ���� Ȱ��ȭ ����   �ʿ������ ���� ����
    [Header("���� �ı�/���ı� ����")]
    public bool isBreaked;                        // Comment: ���� �ı� ����
    [Header("���� ������ ����")]
    public bool isRecover;                        // Comment: ȸ�� ���� ����
    [Header("���� ���� ���� ����")]
    public bool isInvincibility;
    [Header("���� ������")]
    public float durability;                      // Comment: ���� ������
    [Header("������ ���� �˶��� Bool ����")]
    public bool isNow;




    private void Awake()
    {
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        isInvincibility = false;
    }

    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {

            isRecover = false;
            // Comment: Ʈ���� ��ư���� ShieldOn ����
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff �߰�
            shieldOnOff.action.performed += ShieldOff;

        // Comment: ���� Ȱ��ȭ�� �� ��� ��� ��Ȱ��ȭ
        //fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;
        //fire.action.performed -= GetComponent<PlayerInputWeapon>().OffFire;

    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
            // Comment: Ʈ���� ��ư���� ShieldOn �߰�
            shieldOnOff.action.performed += ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff ����
            shieldOnOff.action.performed -= ShieldOff;

        // Comment: ���� ��Ȱ��ȭ�� �� ��� ��� Ȱ��ȭ
        //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;
        //fire.action.performed += GetComponent<PlayerInputWeapon>().OffFire;



    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;

        
        
        // Comment: �������� 0�� �� ��, ���� �ı�
        if (durability <= 0)
        {
            BreakedShield();
        }

    }


    // Comment: ���� Ȱ��ȭ
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        // Comment: �Ͻ������� ��� �Ұ�
       // if (MenuEvent.Instance.IsPause)
       //     return;

        // Comment: ���� �ı� ���°� �ƴҶ��� �ش� �Լ� �ҷ��� �� �ֵ���
        if (!isBreaked)
        {
            // Comment: ���� > Ȱ��ȭ , ���� ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ
            gameObject.SetActive(true);
            shieldRecover.SetActive(false);
            isShield = true;


            // Comment: �ѱ� ��ǲ ����
          // PlayerInputWeapon.Instance.enabled = false;
          // PlayerInputWeapon.Instance.IsShield = isShield;
        }
    }

    // Comment: ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        // Comment: �Ͻ������� ��� �Ұ�
       // if (MenuEvent.Instance.IsPause)
       //     return;

        // Comment: ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ, ���� ���� > ��Ȱ��ȭ 
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;

        // Comment:�ѱ� ��ǲ �ѱ�

       // PlayerInputWeapon.Instance.enabled = true;
       // PlayerInputWeapon.Instance.IsShield = isShield;
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void BreakedShield()
    {
        isRecover = true;
        isBreaked = true;
        isShield = false;
        shieldRecover.SetActive(true);
        
        gameObject.SetActive(false);
        breaked.Play();
        
    }

}
