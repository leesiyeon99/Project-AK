using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [Header("몬스터 인디케이터 배경 이미지")]
    [SerializeField] public Image image;
    [Header("몬스터 인디케이터 아이콘 이미지")]
    [SerializeField] public Image UIImage;

    [Header("몬스터 인디케이터 아이콘 활성화 여부")]
    [SerializeField] public bool isActiveUI = false;


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

            // Comment : 몬스터 인디케이터 배경 위치를 넘어가면 비활성화 시켜줌
            if (pos.x == image.rectTransform.rect.width || pos.y == 0 || pos.y == image.rectTransform.rect.width || pos.x == 0)
            {
                SetActiveFalse();
            }
        }

        if (gameObject.GetComponent<HYJ_Enemy>().isDie == true)
        {
            isActiveUI = false;
            SetActiveFalse();
        }
    }

    public void SetActiveFalse()
    {
        UIImage.gameObject.SetActive(false);
    }

}