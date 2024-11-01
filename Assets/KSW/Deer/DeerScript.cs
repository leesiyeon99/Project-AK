using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeerScript : MonoBehaviour
{
    [SerializeField] GameObject smokeParticle;
    Animator deerAnim;
    BoxCollider deerCollider;

    Coroutine deerCoroutine;

    public UnityAction<DeerScript> DieEvent;


  

    private void Awake()
    {
        deerAnim = GetComponent<Animator>();
        deerCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        deerCollider.enabled = true;
        smokeParticle.SetActive(true);
    }

    private void OnDisable()
    {
       
        StopAllCoroutines();
    }

    public void StartMoveCheckTime()
    {
        if (deerCoroutine != null)
        {
            StopCoroutine(deerCoroutine);
        }
        deerCoroutine = StartCoroutine(MoveDeer());
    }

    public void DieDeer()
    {
        smokeParticle.SetActive(false);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        float rotation = Random.Range(0, 360);
        transform.Rotate(0f, rotation, 0f);

        deerAnim.SetTrigger("Die");
        deerCollider.enabled = false;

        StopAllCoroutines();
        deerCoroutine = StartCoroutine(FlyDeer());  
       
    }
    IEnumerator MoveDeer()
    {
      
        float time = 3f;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;

        }
      
        DieEvent?.Invoke(this);
        gameObject.SetActive(false);
    }
    IEnumerator FlyDeer()
    {
        float dis = Random.Range(-20, 20f);

        Vector3 vec = transform.position;
        vec.y = 20f;
        vec.x += dis;
        vec.z += dis;

        while (transform.position.y < 20)
        {
            transform.position = Vector3.MoveTowards(transform.position, vec, 17f*Time.deltaTime);
            yield return null;

        }

        DieEvent?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
      
            DieDeer();
        
    }
}
