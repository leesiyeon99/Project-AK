using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDetection : MonoBehaviour
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;

    public Image leftImage; 
    public Image rightImage; 
    public Transform playerforwardtf, monstertf;
    Transform playertf;
    public TextMeshProUGUI text;

    private void Awake()
    {
        playertf = transform;
    }

    Vector3 v1, v2;
    float dot, mag;
    float engle;

    // Comment : 플레이어와 몬스터 사이의 각도를 구해서 기즈모로 그려준 후 텍스트로 각도 표시/ 범위에 따라 기즈모 색상 다르게 표시
    private void OnDrawGizmos()
    {
        // Comment : 몬스터 감지 범위 기즈모로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : 감지되는 몬스터가 없다면 return 해줌
        if (monstertf == null) return;

        v1 = playerforwardtf.position - playertf.position;
        v2 = monstertf.position - playertf.position;
        dot = Vector3.Dot(v1, v2);
        mag -= Vector3.Magnitude(v1) * Vector3.Magnitude(v2);

        if (dot == mag)
        {
            Gizmos.color = Color.white;
        }
        else if (dot == 0)
        {
            Gizmos.color = Color.red;
        }
        else if (dot == -mag)
        {
            Gizmos.color = Color.gray;
        }
        else if (dot < 0)
        {
            Gizmos.color = Color.yellow;
        }
        else if (dot > 0 && dot < mag)
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawLine(playertf.position, playerforwardtf.position);
        Gizmos.DrawLine(playertf.position, monstertf.position);

        engle = Mathf.Acos(
            Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg;

        text.text = engle.ToString();
    }

    void Update()
    {
        // Comment : 플레이어 범위 내의 Enemy레이어를 갖는 오브젝트를 찾아 함수 실행, 몬스터가 감지되지 않는다면 원래 상태로 초기화
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        if (colliders.Length > 0)
        {
            PlayerMonsterDetection();
        }

        if (colliders.Length == 0)
        {
            PlayerMonsterNonDetection();
        }
    }

    // Comment : 몬스터 감지시 몬스터와 플레이어의 각도를 구하기 위해 monstertf에 해당 transform을 넣어줌
    private void PlayerMonsterDetection()
    {
        monstertf = colliders[0].transform;

        // TODO : 추후 각도에 따라 UI이미지의 변화 구현 예정
        if (engle > 45)
        {
            if (monstertf.position.x > playertf.position.x)
            {
                Debug.Log("오른쪽에 몬스터 존재");
                rightImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("왼쪽에 몬스터 존재");
                leftImage.gameObject.SetActive(true);
            }
            Debug.Log("몬스터 등장");
        }
        else
        {
            Debug.Log("몬스터 보임");
            rightImage.gameObject.SetActive(false);
            leftImage.gameObject.SetActive(false);
        }
    }

    // Comment : OverlapSphere에서 enemy레이어를 갖는 오브젝트가 사라지면 초기화
    private void PlayerMonsterNonDetection()
    {
        Debug.Log("주변에 감지되는 몬스터 없음");
        monstertf = null;
        rightImage.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
    }


}
