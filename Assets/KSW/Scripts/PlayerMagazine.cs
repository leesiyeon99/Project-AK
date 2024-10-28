using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagazine : MonoBehaviour
{
    GameObject leftController;

    [Header("- �÷��̾� ������ ���� ��ũ��Ʈ")]
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