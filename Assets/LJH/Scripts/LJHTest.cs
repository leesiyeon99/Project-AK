using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJHTest : MonoBehaviour
{
    float ljh_durability; // 내구도 UI용
    [SerializeField] GameObject[] ljh_shieldImages;     // 내구도 UI용

    [SerializeField] LJH_Shield ljh;


    public void Update()
    {
    }


    public void UpdateShieldUI(float durability)
    {
        durability = Mathf.Clamp(durability, 0, ljh_shieldImages.Length);
        for (int i = 0; i < ljh_shieldImages.Length; i++)
        {
            ljh_shieldImages[i].gameObject.SetActive(i < durability);
        }
    }

    
}
