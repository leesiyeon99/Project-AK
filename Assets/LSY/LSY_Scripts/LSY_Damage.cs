using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LSY_Damage : MonoBehaviour
{
    private float lsy_HP = 100;
    private Color curColor;
    private readonly Color initColor = Color.green;

    public float curHp = 100;

    public TextMeshProUGUI curHPUI;

    public Image hpBar;

    public Image bloodImage;
    private Coroutine bloodCoroutine;

    public Image shieldImage;
    private Coroutine shieldCoroutine;

    private void Start()
    {
        curColor = initColor;
        hpBar.color = initColor;
        curHPUI.text = $"{curHp}";
    }

    // Todo : 임의로 적과 충돌했을 때 UI활성화 되게 해놓음
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DisplayHpBar();
            curHPUI.text = $"{curHp}";

            // Todo : 이 부분은 방어가 없을 때 피격이 들어왔을 경우 실행되도록
            // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
            if (bloodCoroutine != null)
            {
                StopCoroutine(bloodCoroutine);
            }
            bloodCoroutine = StartCoroutine(ShowBloodScreen());

            // Todo : 이 부분은 방어가 켜졌을 때 피격 받으면 실행되도록
            // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
            if (shieldCoroutine != null)
            {
                StopCoroutine (shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }
    }

    // Comment : hp에 따른 체력바UI 이미지 변경
    private void DisplayHpBar()
    {
        float hpPercentage = curHp / lsy_HP;

        if (hpPercentage > 0.5f)
        {
            curColor = Color.green;
        }
        else if (hpPercentage > 0.3f)
        {
            curColor = Color.yellow;
        }
        else
        {
            curColor = Color.red;
        }

        hpBar.color = curColor;
        hpBar.fillAmount = hpPercentage;
    }

    // Comment : 플레이어가 피격 당할 시 보여지는 피격효과
    IEnumerator ShowBloodScreen()
    {
        bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.9f, 1f));

        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = bloodImage.color;

        // Commet : 1.5초 동안 점차 이미지가 투명해지도록 설정
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null; 
        }

        bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    IEnumerator ShowShieldScreen()
    {
        shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.9f, 1f));

        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = shieldImage.color;

        // Commet : 1.5초 동안 점차 이미지가 투명해지도록 설정
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

}
