using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HYJ_EnemyHitPoint : MonoBehaviour
{

    [SerializeField] HYJ_Enemy enemy;
    [SerializeField] bool weak;

    [Header("데미지 텍스트 설정")]
    [SerializeField] public GameObject canvas;
    [SerializeField] public Text damageText;  
 
    private void Awake()
    {
        enemy = GetComponentInParent<HYJ_Enemy>();
    }

    public bool TakeDamage(float damage)
    {
        if (enemy.HitFlag == false)
        {
           
            if (weak)
            {
                Debug.Log("약점");
                enemy.MonsterTakeDamageCalculation(damage * 2f);
            }
            else
            {
                Debug.Log("일반");
                enemy.MonsterTakeDamageCalculation(damage);
            }
            DamageText(weak, damage);
            enemy.HitFlag = true;
            enemy.StartHitFlagCoroutine();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHitFlag()
    {
        return enemy.HitFlag;
    }
    

    public void DamageText(bool isWeak, float damage)
    {
        // 몬스터 위에 데미지 폰트 생성
        // 데미지 set damge로 설정
        // 데미지 color 설정 (약점이면 빨강/아니면 하얀색)
        Debug.Log(isWeak);
        Debug.Log(damage);
        StartCoroutine(OnDamageText(isWeak, damage));
        damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
    }

    public IEnumerator OnDamageText(bool isWeak, float damage)
    {
        if (isWeak)
        {
            damage = damage * 2f;
            damageText.fontSize = 70;
            //damageText 굵게
            damageText.text = "<b>"+damage.ToString()+"</b>";
        }
        else if (!isWeak)
        {
            damageText.fontSize = 60;
            //damageText 굵지 않게
            damageText.text = damage.ToString();
        }
        canvas.SetActive(true);
        float colorHpF = (enemy.monsterNowHp / enemy.monsterSetHp) * 255;
        byte colorHpB = (byte)colorHpF;

        damageText.color = new Color32(255, colorHpB, colorHpB, 255); 

        for (int i = damageText.fontSize ; i >= 30; i--)
        {
            damageText.fontSize = i;
            yield return new WaitForFixedUpdate(); // 다음 FixedUpdte까지 기다림
        }

        yield return new WaitForSeconds(1.5f);

    }
}
