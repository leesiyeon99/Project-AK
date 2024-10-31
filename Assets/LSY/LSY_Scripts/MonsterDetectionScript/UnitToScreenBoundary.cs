using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [SerializeField] public Image UIImage;
    [SerializeField] public bool isActiveUI = false;
    [SerializeField] Image image;
    [SerializeField] MonsterCount monsterCount;


    private void Update()
    {
       if (isActiveUI)
        {
           UIMovement();
        }
    }

    public void UIMovement()
    {
        //Comment : WorldToScreenPoint로 몬스터의 움직임에 따라 UI의 위치도 스크린포인트로 변환하여 계산, 변환된 pos로 ui이동
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)
        {
            if (UIImage == null)return;
            UIImage.gameObject.SetActive(true);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, image.rectTransform.rect.width);
            pos.y = Mathf.Clamp(pos.y, 40, image.rectTransform.rect.height / 2);
            UIImage.rectTransform.anchoredPosition = pos;


            if (pos.x == image.rectTransform.rect.width || pos.y == 0 || pos.y == image.rectTransform.rect.width || pos.x == 0)
            {
                SetActiveFalse();
            }
        }

        if (gameObject.GetComponent<HYJ_Enemy>().isDie == true)
        {
            Debug.Log("ui코루틴시작");
            StartCoroutine(MonsterDiedScoreMinus());
        }
    }

    public void SetActiveFalse()
    {
        UIImage.gameObject.SetActive(false);
    }


    IEnumerator MonsterDiedScoreMinus()
    {
        yield return new WaitForSeconds(1.9f);
        Debug.Log("몬스터 죽으면 ui 사라지도록");
        if (UIImage != null)
        Destroy(UIImage.gameObject);
    }

}