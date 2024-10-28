using UnityEngine;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [SerializeField] public Image UIImage;
    [SerializeField] public bool isActiveUI = false;
    [SerializeField] Image image;

    private void Update()
    {
       if (isActiveUI)
        {
           UIMovement();
        }
    }

    public void UIMovement()
    {
        //Comment : WorldToScreenPoint로 몬스터의 움직임에 따라 UI의 위치도 스크린포인트로 변환하여 계산, 변환된 pos로 ui이  동
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)
        {
            UIImage.gameObject.SetActive(true);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, image.rectTransform.rect.width );
            pos.y = Mathf.Clamp(pos.y, 0, image.rectTransform.rect.height / 2);
            UIImage.rectTransform.anchoredPosition = pos;

            //몬스터가 오른쪽 밖으로 나갔을 때
            if (pos.x == image.rectTransform.rect.width || pos.y == 0 || pos.y == image.rectTransform.rect.width || pos.x == 0)
            {
                SetActiveFalse();
            }
        }
    }

    public void SetActiveFalse()
    {
        UIImage.gameObject.SetActive(false);
    }

}