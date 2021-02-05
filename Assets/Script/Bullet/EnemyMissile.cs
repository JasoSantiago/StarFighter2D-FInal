using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyMissile : MonoBehaviour
{
    public float Direction;
    private float Speed = 6.0f;
    private float RotationSpeed = 35.0f;
    private float ChargeCount = 1.0f;
    private float timePassed = 0.0f;

    private float damage = 6.0f;


    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed <= ChargeCount)
        {
            MovementPhase1();
        }
        else
        {
            MovementPhase2();
        }
    }

    void Start()
    {
        Destroy(this.gameObject, 7.0f);
    }

    private void MovementPhase1()
    {
        transform.Translate((Direction == 1.0f ? Vector2.right : Vector2.left) * Speed / 2 * Time.deltaTime);
    }

    private void MovementPhase2()
    {

        Vector2 direction = (Vector2) EnemyManager.GetInstance().player.transform.localPosition - (Vector2)transform.localPosition;
        //direction = direction.normalized;
        float zRotation = transform.localRotation.eulerAngles.z;
        //float uAngle = Vector2.Angle((Vector2) transform.localPosition, (Vector2) EnemyManager.GetInstance().player.transform.localPosition);
        //float sAngle = Vector2.SignedAngle((Vector2)transform.localPosition, (Vector2)EnemyManager.GetInstance().player.transform.localPosition);
        //Debug.Log($"Angle = {uAngle} : SignedAngle = {sAngle} : Direction = {direction}");
        zRotation += 90;

        float angle = Mathf.Atan2(direction.y, direction.x);
        angle *= 180 / Mathf.PI;
        if (angle < 0)
        {
            angle += 360;
        }

        if (Mathf.Abs(zRotation - angle) > 1)
        {
            float multiplier = zRotation - angle >= 0 ? -1 : 1;
            transform.Rotate(0, 0 , RotationSpeed * Time.deltaTime * multiplier);
        }

        Vector3 newPosition = Vector3.MoveTowards(transform.localPosition,
            EnemyManager.GetInstance().player.transform.localPosition, Speed * 40 * Time.deltaTime);

        transform.localPosition = newPosition;
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
