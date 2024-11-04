using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    // Comment : 사운드
    [Header("- 재장전 불가 사운드")]
    [SerializeField] private AudioClip reloadDenySound;

    [Header("- UI 관리")]
    [SerializeField] private PlayerWeaponUI weaponUI;

    [Header("- 보유중인 무기")]
    [SerializeField] List<PlayerGun> ownedWeapons;
    [Header("- 사용중인 무기")]
    [SerializeField] PlayerGun currentWeapon;
    [Header("- 탄창 오브젝트")]
    [SerializeField] PlayerMagazine magazine;



    [Header("- 장전 중 확인")]
    [SerializeField] private bool ontGrip;


    public bool OntGrip { get { return ontGrip; } set { ontGrip = value; } }

    [Header("- 슬로우")]
    // Commnet : 1 넣으면 사격 50% 딜레이 증가
    [SerializeField] private float additionalCoolDown;
    public float AdditionalCoolDown { get { return additionalCoolDown; } set { additionalCoolDown = value; } }


    Coroutine slowCoroutine;
    WaitForSeconds slowWaitForSeconds = new WaitForSeconds(3.0f);
    bool disableFlag;

    private void Awake()
    {
        SetWeapons();
        weaponUI.SetUIPos();
    }

    // Comment : 무기 초기화 함수 호출
    public void SetWeapons()
    {
        foreach (PlayerGun weapon in ownedWeapons)
        {

            weapon.InitGun();
        }

    }

    // Comment : 사용중인 무기 반환
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }



    // Comment : 보유중인 무기 반환
    public PlayerGun GetOwnedWeapons(int _index)
    {
        return ownedWeapons[_index];
    }


    // Comment : 보유중인 무기 수 반환
    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count - 1;
    }

    // Comment : 무기 교체
    public void SetCurrentWeapon()
    {

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
        weaponUI.SetUIPos();
    }

    // Comment : 특수 탄환 비어 있을때 호출할 기본 무기 교체 함수
    public void SetDefaultWeapon()
    {

        currentWeapon.gameObject.SetActive(false);

        index = 0;
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.UpdateMagazine();

    }

    // Comment : 재장전
    public void ReloadMagazine()
    {
        currentWeapon.Reload(index - 1);
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck() ||
            index != 0 && PlayerSpecialBullet.Instance.SpecialBullet[index - 1] <= 0 ||
            weaponUI.GetChangeUIActiveSelf())
        {
            AudioManager.Instance.PlaySE(reloadDenySound);
            return;
        }

        magazine.gameObject.SetActive(true);
        ontGrip = true;
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
        ontGrip = false;
    }

    public void SetReloadText()
    {
        magazine.SetTextMagazine();
    }

    // Comment : 슬로우 시작
    public void StartSlow()
    {
        if (disableFlag)
            return;

        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        slowCoroutine = StartCoroutine(SlowEnd());
        additionalCoolDown = 1;
    }

    IEnumerator SlowEnd()
    {
        yield return slowWaitForSeconds;
        additionalCoolDown = 0;
    }

    // 예외 처리
    private void OnEnable()
    {
        disableFlag = false;
    }
    private void OnDisable()
    {
        disableFlag = true;
    }

    private void OnDestroy()
    {
        disableFlag = true;
    }
}
