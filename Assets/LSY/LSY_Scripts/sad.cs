using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sad : MonoBehaviour
{
    public Transform tf1, tf2;
    Transform tf;
    public TextMeshProUGUI text;

    private void Awake()
    {
        tf = transform;
    }

    Vector3 v1, v2;
    float dot, mag;

    private void OnDrawGizmos()
    {
        v1 = tf1.position - tf.position;
        v2 = tf2.position - tf.position;
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

        Gizmos.DrawLine(tf.position, tf1.position);
        Gizmos.DrawLine(tf.position, tf2.position);

        text.text = (Mathf.Acos(
            Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg).ToString();
    }
}
