using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUIShow : MonoBehaviour
{
    public Image[] shieldImages; 
    //public ShieldUI shieldUI; // ShieldUI 컴포넌트의 참조
    public float lsy_durability = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            lsy_durability--;
            UpdateShieldUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IncreaseDurability();
        }
    }

    public void IncreaseDurability()
    {
        lsy_durability++;
        UpdateShieldUI();
    }

    private void UpdateShieldUI()
    {
        float durability = Mathf.Clamp(lsy_durability, 0, shieldImages.Length);

        for (int i = 0; i < shieldImages.Length; i++)
        {
            shieldImages[i].gameObject.SetActive(i < durability);
        }
    }
}
