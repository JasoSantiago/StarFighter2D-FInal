using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : SideSteppingEnemy
{
    [SerializeField] private List<Turret1> normalTurret;
    [SerializeField] private List<Turret2> missileTurret;


    private float MaxHP = 300;
    private float missileCooldown = 15.0f;
    private float missileLastShot = 0.0f;
    private int missileShootCount = 3;
    private float missileInterval = 1.0f;

    public GameObject Missile;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        FireRate = 0.5f;
        Bounty = 35;
        Speed = 3.0f;
        type = EnemyType.Boss1;
    }

    protected override void Update()
    {
        base.Update();
        missileLastShot += Time.deltaTime;
        if (missileLastShot >= missileCooldown)
        {
            StartCoroutine(ShootMissiles());
            missileLastShot = 0;
        }
    }

    protected override void Shoot()
    {
        FireCooldown = 0;

        foreach (Turret1 turret in normalTurret)
        {
            turret.Shoot(bullet);
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        EnemyManager.GetInstance().SetBossSlider(HP);

        if (HP <= 0)
        {
            GameManager.GetInstance().Victory();
        }
    }

    IEnumerator ShootMissiles() // coroutine for shooting multiple missiles at fixed intervals
    {
        for (int i = 0; i < missileShootCount; i++)
        {
            foreach (Turret2 turret2 in missileTurret)
            {
                turret2.Shoot(Missile);
            }
            yield return new WaitForSeconds(missileInterval);
        }

        missileLastShot = 0;
    }

    protected override void SetSecondSpeed()
    {
        Speed = 40.0f;
    }
}
