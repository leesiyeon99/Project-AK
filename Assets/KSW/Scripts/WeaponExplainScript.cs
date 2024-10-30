using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponExplainScript : BaseUI
{
    [SerializeField] TextMeshProUGUI weaponNameUI;
    [SerializeField] TextMeshProUGUI weaponAbilityUI;
    [SerializeField] TextMeshProUGUI weaponAttackUI;
    [SerializeField] TextMeshProUGUI weaponMagazineUI;

    [SerializeField] List<TextMeshProUGUI> explainList;

    [SerializeField] float fadeTime = 2.0f;
    [SerializeField] float fadeDeltaTime;

    Coroutine fadeout;

    void Awake()
    {
        explainList = new List<TextMeshProUGUI>();
        BindAll();
        weaponNameUI = GetUI<TextMeshProUGUI>("Name");
        weaponAbilityUI = GetUI<TextMeshProUGUI>("Ability");
        weaponAttackUI = GetUI<TextMeshProUGUI>("Attack");
        weaponMagazineUI = GetUI<TextMeshProUGUI>("MagazineText");

       

        explainList.Add(weaponNameUI);
        explainList.Add(weaponAbilityUI);
        explainList.Add(weaponAttackUI);
        explainList.Add(weaponMagazineUI);

        
    }



    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetFade()
    {
        StopAllCoroutines();
        fadeDeltaTime = fadeTime;
        foreach (TextMeshProUGUI explainText in explainList)
        {
            explainText.alpha = 1;
        }
    }

    public void StartFadeOut()
    {
        
        SetFade();
        if (!gameObject.activeSelf)
        {
            return;
        }
        fadeout = StartCoroutine(FadeOutCoroutine());
        
    }

    IEnumerator FadeOutCoroutine()
    {
        while (fadeDeltaTime >= 0)
        {
            fadeDeltaTime -= Time.deltaTime;
            FadeOutExplain();
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void FadeOutExplain()
    {
        foreach (TextMeshProUGUI explainText in explainList)
        {
            explainText.alpha = fadeDeltaTime;
        }
    }
    public void SetExplain(string weaponName, GunType gunType, float atk, int magazine )
    {
        weaponNameUI.text = weaponName;
        weaponAbilityUI.text = gunType.ToString();
        weaponAttackUI.text = atk.ToString();
        weaponMagazineUI.text = magazine.ToString();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
