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

    public Image image; public Transform playerforwardtf, monstertf;
    Transform playertf;
    public TextMeshProUGUI text;

    private void Awake()
    {
        playertf = transform;
    }

    Vector3 v1, v2;
    float dot, mag;
    float engle;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
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
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        if (colliders.Length > 0)
        {
            Debug.Log("몬스터 찾음");
            PlayerMonsterDetection();
        }

        if (colliders.Length == 0)
        {
            PlayerMonsterNonDetection();
        }
    }

    private void PlayerMonsterDetection()
    {
        monstertf = colliders[0].transform;
        if (engle > 90)
        {
            Debug.Log("몬스터 안보임");
            image.color = Color.white;
        }
        else
        {
            Debug.Log("몬스터 찾음");
            image.color = Color.red;
        }
    }

    private void PlayerMonsterNonDetection()
    {
        monstertf = null;
        image.color = Color.white;
    }


}
