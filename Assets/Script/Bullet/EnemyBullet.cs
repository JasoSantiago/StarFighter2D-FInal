using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Speed = 8.0f;
    private float damage = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void OnDestroy()
    {
        if (EnemyManager.GetInstance() != null)
            EnemyManager.GetInstance().DeleteBullet(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Starfighter temp = collider.gameObject.GetComponent<Starfighter>();
        if (temp != null)
        {
            Debug.Log("Hit Player");
            temp.Damage(damage);
            Destroy(this.gameObject);
        }
    }

}
