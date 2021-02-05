using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAser : Bullet
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null)
        {
            enemy = (Enemy) other.GetComponent<Boss1>();
        }

        if (enemy.type == EnemyType.Shielded)
        {
            MultiplyDamage(2);
        }
        else if (enemy.type == EnemyType.Swarm)
        {
            MultiplyDamage(0.5f);
        }
        base.OnTriggerEnter2D(other);
    }
}
