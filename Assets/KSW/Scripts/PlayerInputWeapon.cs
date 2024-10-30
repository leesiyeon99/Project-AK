using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Comment : 인풋 시스템 관리
    // TODO : 추후 인풋 시스템 수정 합의 필요

    // 일지정지 메뉴 이벤트
    MenuEvent menuEvent;

    private PlayerOwnedWeapons playerOwnedWeapons;
    private PlayerChangeWeapon playerChangeWeapon;

    [Header("- UI 관리")]
    [SerializeField] private PlayerWeaponUI weaponUI;

    [Header("- 발사")]
    [SerializeField] private InputActionReference fire;

    [Header("- 컨트롤러 하단 장전")]
    [SerializeField] private InputActionReference downReload;

    [Header("- 그립 장전")]
    [SerializeField] private InputActionReference gripReload;


    [Header("- 무기교체 UI 토글")]
    [SerializeField] private InputActionReference viewChangeUI;

    [Header("- 무기교체 UI 조작 조이스틱")]
    [SerializeField] private InputActionReference rightJoystcikAxis;

    [Header("- 무기교체 토글 확인")]
    [SerializeField] private bool onToggle;


    private void Awake()
    {
        menuEvent = GameObject.Find("MenuInputManager").GetComponent<MenuEvent>();
        menuEvent.SetPlayerWeaponInput(this);
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();


        downReload.action.performed += OnDownReload;

        gripReload.action.performed += OnGripReload;
        gripReload.action.canceled += OffGripReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

 
        viewChangeUI.action.performed += OnChangeView;
        viewChangeUI.action.canceled += OffChangeView;

        rightJoystcikAxis.action.performed += OnRightJoystick;
        rightJoystcikAxis.action.canceled += OnRightJoystick;
    }
    private void OnDisable()
    {
       
        
        CloseChangeView();

        downReload.action.performed -= OnDownReload;

        gripReload.action.performed -= OnGripReload;
        gripReload.action.canceled -= OffGripReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;


        viewChangeUI.action.performed -= OnChangeView;
        viewChangeUI.action.canceled -= OffChangeView;

        rightJoystcikAxis.action.performed -= OnRightJoystick;
        rightJoystcikAxis.action.canceled -= OnRightJoystick;
    }


    void OnDownReload(InputAction.CallbackContext obj)
    {
        Quaternion quaternion = obj.ReadValue<Quaternion>();


        // Comment : 컨트롤러의 x 좌표 각도가 45~60 사이 일 때 재장전 호출

        if(quaternion.eulerAngles.x > 45f && quaternion.eulerAngles.x < 60f)
        {
            playerOwnedWeapons.ReloadMagazine();
        }
    }

    void OnGripReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadGripOnMagazine();
    }
    void OffGripReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadGripOffMagazine();
    }

    public void OnFire(InputAction.CallbackContext obj)
    {
        if (onToggle)
        {
            playerChangeWeapon.ChangeWeapon();
        }
        else
        {

            playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();
        }

    }
    public void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OnChangeView(InputAction.CallbackContext obj)
    {
     
        weaponUI.OnOffChangeUI(true);
      
        onToggle = true;
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OffChangeView(InputAction.CallbackContext obj)
    {
        CloseChangeView();
    }


    void CloseChangeView()
    {

        playerChangeWeapon.MoveJoystick(Vector2.zero);
        weaponUI.OnOffChangeUI(false);

        onToggle = false;

        playerOwnedWeapons.GetCurrentWeapon().UpdateMagazine();
    }



    void OnRightJoystick(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.MoveJoystick(obj.ReadValue<Vector2>());
    }
    void OffRightJoystick(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.MoveJoystick(Vector2.zero);
    }
}
