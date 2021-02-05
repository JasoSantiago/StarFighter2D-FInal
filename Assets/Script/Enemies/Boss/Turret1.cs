using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    public int direction = 1;

    private float turnAngle = 60.0f;
    private float turnSpeed = 5.0f;

    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float increment = turnSpeed * Time.fixedDeltaTime * direction;
        currentRotation += increment;

        transform.Rotate(new Vector3(0,0, increment));

        if (direction > 0)
        {
            if (currentRotation > turnAngle / 2)
                direction *= -1;
        }
        else if (direction < 0)
        {
            if (currentRotation < -turnAngle / 2)
                direction *= -1;
        }

    }

    public void Shoot(GameObject bullet)
    {
        GameObject temp = Instantiate(bullet, GameManager.GetInstance().GetMiddlePanel().transform);
        temp.transform.rotation = transform.rotation;
        temp.transform.position = transform.position;
        temp.transform.SetAsFirstSibling();
        EnemyManager.GetInstance().AddBullet(temp);
    }
}
