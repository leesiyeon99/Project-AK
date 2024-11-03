using UnityEngine;

public class RidingEnemy : MonoBehaviour
{
    [SerializeField] DeerPool pool;
    [SerializeField] Transform ridingEnemy;
    public bool ready;

    private void Start()
    {
        ready = true;
    }

    public void Ride(Transform deerTransform)
    {

        transform.SetParent(deerTransform);

        transform.localPosition = Vector3.zero;
        ridingEnemy.localRotation = Quaternion.Euler(0, 180f, ridingEnemy.localRotation.z);
        gameObject.SetActive(true);

    }

    public void Die()
    {
        gameObject.SetActive(false);
        ready = true;
    }

}
