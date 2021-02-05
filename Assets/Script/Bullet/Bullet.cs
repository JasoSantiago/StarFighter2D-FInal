using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Speed = 12.0f;

    public float DespawnTime = 4.0f;

    private float damage;

    private int[] DamageperLevel = new[] { 1, 2, 3, 4, 5, 6 };

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, DespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        transform.Translate(new Vector3(0, Speed * Time.deltaTime, 0));
    }

    protected float getDamage()
    {
        return this.damage;
    }

    public virtual void setDamage(int num)
    {
        this.damage = DamageperLevel[num];
    }

    protected virtual void MultiplyDamage(float num)
    {
        this.damage *= num;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        Boss1 boss1 = other.gameObject.GetComponent<Boss1>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null)
            enemy.Damage(damage);
        else if (boss1 != null)
        {
            boss1.Damage(damage);
        }

        if(boss1 != null || enemy != null)
            Destroy(this.gameObject);
    }
}
