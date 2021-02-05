using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSteppingEnemy : Enemy
{
    private bool arrived = false;
    private float time_elapsed;
    private Vector3 point_destination;
    [SerializeField] private float change_direction;

    protected override void Start()
    {
        base.Start();
        point_destination = CalculatePointDestination(PercentDestination);
        
        change_direction = Random.Range(0, 2) == 1 ? -1.0f : 1.0f;
    }

    protected override void Movement()
    {
        if (!arrived)
        {
            base.Movement();
            arrived = HasArrived();
            if (arrived)
            {
                point_destination = CalculatePointDestination(PercentDestination);
                SetSecondSpeed();
            }
        }
        else
        {
            if (change_direction > 0)
            {
                if (point_destination.x + 100 < transform.localPosition.x)
                {
                    change_direction *= -1;
                }
            }
            else if(change_direction < 0)
            {
                if (point_destination.x - 100 > transform.localPosition.x)
                {
                    change_direction *= -1;
                }
            }

            Vector3 tempPos = transform.localPosition;
            tempPos.x += change_direction * Speed * Time.fixedDeltaTime;
            transform.localPosition = tempPos;
        }
    }

    protected virtual void SetSecondSpeed()
    {
        Speed = 20.0f;
    }
}
