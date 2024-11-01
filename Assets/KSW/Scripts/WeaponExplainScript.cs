using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponExplainScript : BaseUI
{

    private static WeaponExplainScript instance;


    public static WeaponExplainScript Instance
    {
        get
        {
            return instance;

        }
    }

    [SerializeField] TextMeshProUGUI weaponNameUI;
    [SerializeField] TextMeshProUGUI weaponAbilityUI;
    [SerializeField] TextMeshProUGUI weaponAttackUI;
    [SerializeField] TextMeshProUGUI weaponMagazineUI;

    [SerializeField] List<TextMeshProUGUI> explainList;

    [SerializeField] float fadeTime = 2.0f;
    [SerializeField] float fadeDeltaTime;

    Coroutine fadeout;
    bool enableCheck;
    bool destroyCheck;
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
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

    private void OnDestroy()
    {
        destroyCheck = true;
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        enableCheck = true;
    }

    private void OnEnable()
    {
        enableCheck = false;
    }


    public void SetFade()
    {
        if (destroyCheck)
            return;
        StopAllCoroutines();
        fadeDeltaTime = fadeTime;
        foreach (TextMeshProUGUI explainText in explainList)
        {
            explainText.alpha = 1;
        }
    }

    public void StartFadeOut()
    {
        if (destroyCheck)
            return;
        SetFade();
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (enableCheck)
            return;
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
    public void SetExplain(string weaponName, string gunType, string atk, string magazine )
    {
        if (destroyCheck)
            return;
        weaponNameUI.text = weaponName;

        weaponAbilityUI.text = gunType;
        
        
        weaponAttackUI.text = atk.ToString();
        weaponMagazineUI.text = magazine.ToString();
    }


}
