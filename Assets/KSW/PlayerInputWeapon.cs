using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Commnet : 인풋 시스템 관리
    // TODO : 추후 인풋 시스템 수정 합의 필요


    [SerializeField] private PlayerOwnedWeapons playerOwnedWeapons;
    [SerializeField] private PlayerChangeWeapon playerChangeWeapon;

    [SerializeField] private InputActionReference fire;
    [SerializeField] private InputActionReference changeLeft;
    [SerializeField] private InputActionReference changeRight;
    private void Awake()
    {
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

        changeLeft.action.performed += OnChangeLeft;
        changeRight.action.performed += OnChangeRight;
    }
    private void OnDisable()
    {
        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;

        changeLeft.action.performed -= OnChangeLeft;
        changeRight.action.performed -= OnChangeRight;
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
