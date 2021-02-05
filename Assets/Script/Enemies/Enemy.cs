using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Armoured, Shielded, Swarm, Boss1, Boss2, Boss3, None
}

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyType type;
    [SerializeField] protected float HP = 10;
    [SerializeField] public Vector2 PercentDestination = new Vector2(0.5f,0.5f); // range {0, 1}, (0,0) is top left
    [SerializeField] private GameObject ExplosionPrefab;
    
    public GameObject bullet;

    protected float Speed = 1.0f;
    protected float FireRate = 0.9f;
    protected float FireCooldown = 0f;
    protected int Bounty = 1;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        FireCooldown += Time.deltaTime;
        if (FireCooldown >= FireRate)
        {
            Shoot();
        }

       Movement();
       if (HP <= 0)
       {
           EnemyManager.GetInstance().DeleteEnemy(this.gameObject);
           GameManager.GetInstance().Bounties += Bounty;
           Destroy(this.gameObject);
       }
    }

    private void OnDestroy()
    {
        if(EnemyManager.GetInstance() != null)  
            EnemyManager.GetInstance().DeleteEnemy(this.gameObject);
        EnemyManager.GetInstance().PlayEnemyDeath();
        GameManager.GetInstance().score += Bounty;
        if (ExplosionPrefab != null)
        {
            GameObject explosion = GameObject.Instantiate(ExplosionPrefab, transform.parent);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = new Vector3(25,25);
            Destroy(explosion, 1);
            Debug.Log(explosion.name);
        }
    }

    protected virtual void Shoot()
    {
        FireCooldown = 0f;

        if (bullet != null)
        {
            Image image = GetComponent<Image>();

            GameObject temp = Instantiate(bullet, transform.parent);
            temp.transform.Rotate(new Vector3(0, 0, 180));
            temp.transform.localPosition = transform.localPosition + Vector3.down * image.sprite.rect.height/4;
            
           // Debug.Log(temp.transform.localPosition);
           temp.transform.SetAsFirstSibling();

           EnemyManager.GetInstance().AddBullet(temp);
        }
    }

    protected virtual void Movement()
    {
        Vector3 start = transform.localPosition;

        // -0.5 to adjust for pivot at center, 1-y for top left start
        Vector3 destination = CalculatePointDestination(PercentDestination);
        destination.z = start.z;

        Vector3 current = Vector3.Lerp(start, destination, Speed * Time.deltaTime);

        transform.localPosition = current;

        //Debug.Log(destination);

    }

    public Vector3 CalculatePointDestination(Vector2 percDestination)
    {
        RectTransform rectTransform = GameManager.GetInstance().GetMiddlePanel().GetComponent<RectTransform>();

        return new Vector3(rectTransform.rect.width * (percDestination.x - 0.5f),
            rectTransform.rect.height * (1 - percDestination.y - 0.5f));
    }

    public virtual void Damage(float damage)
    {
        HP -= damage;
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
    }

    protected bool HasArrived()
    {
        Vector3 destination = CalculatePointDestination(PercentDestination);
        
        if (Vector3.Distance(destination, transform.localPosition) < 3.0f) //transform.localPosition == destination)
            return true;
        else
            return false;
    }
}
