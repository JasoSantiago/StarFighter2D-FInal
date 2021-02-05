using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    [SerializeField] private List<Turret1> turretList;
    private Vector2 PercDest1 = new Vector2(0.2f, 0.1f);    // left
    private Vector2 PercDest2 = new Vector2(0.8f, 0.1f);    // right

    private float Speed2 = 50f;
    private float MaxHP = 150;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        FireRate = 0.5f;
        Bounty = 15;
        Speed = 3.0f;
        type = EnemyType.Boss1;
    }

    protected override void Update()
    {
        base.Update();

        ChangeDestination();
        
    }

    protected override void Shoot()
    {
        FireCooldown = 0;

        foreach (Turret1 turret in turretList)
        {
            turret.Shoot(bullet);
        }
    }

    private void ChangeDestination()    // so that the boss moves left to right
    {
        Vector2 destination = CalculatePointDestination(PercentDestination);
        Vector3 destVector3 = new Vector3(destination.x, destination.y, transform.localPosition.z);


        if (Vector3.Distance(transform.localPosition, destVector3) < 5.0f)
        {
            if (PercDest1 != PercentDestination && PercDest2 != PercentDestination)
            {
                PercentDestination = PercDest1;
            }
        }
        if (PercDest1 == PercentDestination && transform.localPosition.x < destination.x)
        {
            PercentDestination = PercDest2;
        }
        else if (PercDest2 == PercentDestination && transform.localPosition.x > destination.x)
        {
            PercentDestination = PercDest1;
        }
    }

    protected override void Movement()
    {
        if (PercDest1 == PercentDestination)
        {
            this.transform.localPosition -= new Vector3(Speed2 * Time.deltaTime, 0);
        }
        else if (PercDest2 == PercentDestination)
        {
            this.transform.localPosition += new Vector3(Speed2 * Time.deltaTime, 0);
        }
        else
        {
            base.Movement();
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
}
