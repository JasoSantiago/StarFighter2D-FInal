using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{

    public float Direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Shoot(GameObject bullet)
    {
        GameObject temp = Instantiate(bullet, GameManager.GetInstance().GetMiddlePanel().transform);
        temp.GetComponent<EnemyMissile>().Direction = this.Direction;
        temp.transform.rotation = transform.rotation;
        temp.transform.position = transform.position;
        temp.transform.SetAsFirstSibling();
        EnemyManager.GetInstance().AddBullet(temp);
    }
}