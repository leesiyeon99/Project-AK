using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HYJ_DamageText : MonoBehaviour
{
    [SerializeField] TextMeshPro damageText;
    [SerializeField] float textMoveSpeed;
    [SerializeField] float textColorSpeed;
    [SerializeField] float destroyTime;
    private Color damageColor;
    public float damage;

    private void Start()
    {
        damageText = GetComponent<TextMeshPro>();
        damageColor = damageText.color;
        damageText.text = damage.ToString();
        Invoke("DestroyObject",destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0,textMoveSpeed * Time.deltaTime,0));
        damageColor.a = Mathf.Lerp(damageColor.a, 0, Time.deltaTime * textColorSpeed);
        damageText.color = damageColor;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
