using UnityEngine;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [SerializeField] RectTransform ui;
    [SerializeField] Image image;
    private void Update()
    {
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)  // 몬스터가 앞에 있으면
        {
            image.gameObject.SetActive(true);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
            ui.anchoredPosition = pos;

            Debug.Log(pos.x);
            Debug.Log(pos.y);

        }
        else    // 몬스터가 뒤에 있으면
        {

        }
    }
}