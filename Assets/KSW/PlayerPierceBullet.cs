using UnityEngine;

public class PlayerPierceBullet : PlayerBullet
{

    [SerializeField] private int defaultPierceCount;
    [SerializeField] private int pierceCount;


    protected override void HitBullet()
    {
      
        pierceCount--;
        if (pierceCount <= 0)
        {
            pierceCount = defaultPierceCount;
            ReturnBullet();

        }
    }

    private void OnEnable()
    {
        pierceCount = defaultPierceCount;
    }
}
