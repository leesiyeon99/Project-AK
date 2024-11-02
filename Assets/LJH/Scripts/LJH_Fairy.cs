using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LJH_Fairy : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("캐릭터 오브젝트")]
    [SerializeField] GameObject character;

    [Header("변수")]
    [Header("페어리 위치 이동 불 변수")]
    [SerializeField] bool fairyWithCharacter;  // 캐릭터에 해당 변수 넣어줘야함, 요정 등장 이벤트 끝날때 트루 이후 요정에서 팔스
    [Header("페어리 살랑거림 불 변수")]
    [SerializeField] bool fairyfixed;  // 캐릭터에 해당 변수 넣어줘야함, 요정 등장 이벤트 끝날때 트루 이후 요정에서 팔스


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fairyfixed = false;
    }
    void Update()
    {
        fairyWithCharacter = character.GetComponent<FairyTest>().fairyWithCharacter;

        if (character.transform.position.z - transform.position.z == 3)
        {
            character.GetComponent<FairyTest>().fairyWithCharacter = true;
        }




        if (fairyWithCharacter)
        {
            this.gameObject.transform.position = character.transform.position + new Vector3(1, 1, 1);
            character.GetComponent<FairyTest>().fairyWithCharacter = false;
            fairyfixed = true;
        }

        if (fairyfixed)
        {
            if (this.transform.position.x - character.transform.position.x > 0)
            {
                rb.AddForce(new Vector3(-2, 0, 0));
            }
            else if (this.transform.position.x - character.transform.position.x < 0)
            {
                rb.AddForce(new Vector3(2, 0, 0));
            }
        }
    }
}
