using SETUtil.SceneUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMonsterDetection : BaseUI
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;

    [SerializeField] TextMeshProUGUI leftMonsterCount;
    [SerializeField] TextMeshProUGUI rightMonsterCount;


    public List<GameObject> gameObjects = new List<GameObject>();
    int i = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObjects[i].GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            i++;
        }
    }

    private void Update()
    {
        
    }

    private void UpdateScoreUI(string scoreKey, object score)
    {
        GetUI<TextMeshProUGUI>(scoreKey).text = score.ToString();
    }
}
