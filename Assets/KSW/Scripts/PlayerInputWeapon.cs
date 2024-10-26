using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Comment : 인풋 시스템 관리
    // TODO : 추후 인풋 시스템 수정 합의 필요


    [SerializeField] private PlayerOwnedWeapons playerOwnedWeapons;
    [SerializeField] private PlayerChangeWeapon playerChangeWeapon;

    [SerializeField] private InputActionReference fire;
    [SerializeField] private InputActionReference reload;
    [SerializeField] private InputActionReference downReload;
    [SerializeField] private InputActionReference changeLeft;
    [SerializeField] private InputActionReference changeRight;
    private void Awake()
    {
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
        reload.action.performed += OnReload;
        downReload.action.performed += OnDownReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

        changeLeft.action.performed += OnChangeLeft;
        changeRight.action.performed += OnChangeRight;
    }
    private void OnDisable()
    {
        reload.action.performed -= OnReload;
        downReload.action.performed -= OnDownReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;

        changeLeft.action.performed -= OnChangeLeft;
        changeRight.action.performed -= OnChangeRight;
    }


    void OnReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadMagazine();

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

    void OnFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();

    }
    void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }
    void OnChangeLeft(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.ChangeWeapon(true);

    }
    void OnChangeRight(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.ChangeWeapon(false);

    }



}
