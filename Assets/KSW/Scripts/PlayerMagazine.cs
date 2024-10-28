using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagazine : MonoBehaviour
{
    GameObject leftController;

    [Header("- 플레이어 소유중 무기 스크립트")]
    [SerializeField] PlayerOwnedWeapons playerOwnedWeapons;
    private void Awake()
    {
        leftController = GameObject.Find("Left Controller");

        transform.parent = leftController.transform;
        transform.localPosition = Vector3.zero;

    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        playerOwnedWeapons.ReloadMagazine();
    }
}
