using SETUtil.SceneUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerMonsterDetection : BaseUI
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;


    public List<GameObject> gameObjects = new List<GameObject>();
    int i = 0;


    public Transform playerforwardtf, monstertf;
     Transform playertf;
    Vector3 v1, v2;
    float dot, mag;
    float engle;
    [SerializeField] TextMeshProUGUI engletext;

    private void Awake()
    {
        Bind();
        playertf = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : 몬스터 감지 범위 기즈모로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : 감지되는 몬스터가 없다면 return 해줌
        if (monstertf == null) return;

        v1 = playerforwardtf.position - playertf.position;
        v2 = monstertf.position - playertf.position;
        dot = Vector3.Dot(v1, v2);
        mag -= Vector3.Magnitude(v1) * Vector3.Magnitude(v2);

        Gizmos.DrawLine(playertf.position, playerforwardtf.position);
        Gizmos.DrawLine(playertf.position, monstertf.position);

        engle = Mathf.Acos(
            Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg;

        engletext.text = engle.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (gameObjects[i] == null) return;
            gameObjects[i].GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            i++;
        }
    }

    void Update()
    {
        // Comment : 플레이어 범위 내의 Enemy레이어를 갖는 오브젝트를 찾아 함수 실행, 몬스터가 감지되지 않는다면 원래 상태로 초기화
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        if (colliders.Length > 0)
        {
            isPlayerMonsterDetection();
        }

        if (colliders.Length == 0)
        {
            PlayerMonsterNonDetection();
        }


        Debug.Log($"길이: {colliders.Length}");
    }

    // Comment : 몬스터 감지시 몬스터와 플레이어의 각도를 구하기 위해 monstertf에 해당 transform을 넣어줌
    private void isPlayerMonsterDetection()
    {
        // 몬스터가 여러 마리 일 때,,,?는 foreach로 모든 행동을 반복해야 하게 되는데...
        monstertf = colliders[0].transform;

        // TODO : 추후 각도에 따라 UI이미지의 변화 구현 예정
        if (engle > 45)
        {
            if (monstertf.position.x > playertf.position.x)
            {
                Debug.Log("오른쪽에 몬스터 존재");
                UpdateScoreUI("RightEnemyCountUI", 1);
                UpdateScoreUI("MonsterCount", colliders.Length);
            }

            if (monstertf.position.x < playertf.position.x)
            {
                Debug.Log("왼쪽에 몬스터 존재");
                UpdateScoreUI("LeftEnemyCountUI", 1);
                UpdateScoreUI("MonsterCount", colliders.Length);
            }
            Debug.Log("몬스터 등장");
        }
        else
        {
            if (monstertf.position.x > playertf.position.x)
            {
                UpdateScoreUI("RightEnemyCountUI", 0);
            }

            if (monstertf.position.x < playertf.position.x)
            {
                UpdateScoreUI("LeftEnemyCountUI", 0);
            }
        }

    }

    // Comment : OverlapSphere에서 enemy레이어를 갖는 오브젝트가 사라지면 초기화
    private void PlayerMonsterNonDetection()
    {
        Debug.Log("주변에 감지되는 몬스터 없음");
        monstertf = null;
    }

    private void UpdateScoreUI(string scoreKey, object score)
    {
        GetUI<TextMeshProUGUI>(scoreKey).text = score.ToString();
    }


}
