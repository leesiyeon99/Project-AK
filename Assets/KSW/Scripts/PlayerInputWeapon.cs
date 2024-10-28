using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Comment : 인풋 시스템 관리
    // TODO : 추후 인풋 시스템 수정 합의 필요

    private PlayerOwnedWeapons playerOwnedWeapons;
    private PlayerChangeWeapon playerChangeWeapon;

    [Header("- 보유중 무기 탄창 UI")]
    [SerializeField] private GameObject magazineViewUI;


    [Header("- 발사")]
    [SerializeField] private InputActionReference fire;

    [Header("- 컨트롤러 하단 장전")]
    [SerializeField] private InputActionReference downReload;

    [Header("- 그립 장전")]
    [SerializeField] private InputActionReference gripReload;

    [Header("- 무기 교체")]
    [SerializeField] private InputActionReference changeLeft;
    [SerializeField] private InputActionReference changeRight;

    [Header("- 탄창 UI 토글")]
    [SerializeField] private InputActionReference viewMagazine;

    [Header("- 무기 교체 UI 조작 조이스틱")]
    [SerializeField] private InputActionReference rightJoystcikAxis;

    private void Awake()
    {
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
      
        downReload.action.performed += OnDownReload;

        gripReload.action.performed += OnGripReload;
        gripReload.action.canceled += OffGripReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

        changeLeft.action.performed += OnChangeLeft;
        changeRight.action.performed += OnChangeRight;

        viewMagazine.action.performed += OnMagazineView;
    }
    private void OnDisable()
    {
        downReload.action.performed -= OnDownReload;

        gripReload.action.performed -= OnGripReload;
        gripReload.action.canceled -= OffGripReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;

        changeLeft.action.performed -= OnChangeLeft;
        changeRight.action.performed -= OnChangeRight;

        viewMagazine.action.performed -= OnMagazineView;
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
        playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();

    }
    public void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }
    void OnChangeLeft(InputAction.CallbackContext obj)
    {
       // playerChangeWeapon.ChangeWeapon(true);

    }
    void OnChangeRight(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.ChangeWeapon(false);

    }

    void OnMagazineView(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.MagazineUIUpdate();
        magazineViewUI.SetActive(!magazineViewUI.activeSelf);
        

    }

}
