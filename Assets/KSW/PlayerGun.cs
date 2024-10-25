using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private GameObject bullet;

    private void OnEnable()
    {
        shoot.action.performed += OnShoot;
    }
    private void OnDisable()
    {
        shoot.action.performed += OnShoot;
    }
 

    void OnShoot(InputAction.CallbackContext obj)
    {
        
        Instantiate(bullet, muzzle.position, muzzle.rotation);
    }
}
