using UnityEngine;

public class RidingEnemy : MonoBehaviour
{
    [SerializeField] DeerPool pool;

    public bool ready;

    private void Start()
    {
        ready = true;
    }

    public void Ride(Transform deerTransform)
    {

        transform.SetParent(deerTransform);

        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);

    }

    public void Die()
    {
        gameObject.SetActive(false);
        ready = true;
    }

}
