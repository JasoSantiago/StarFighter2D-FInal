using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class Starfighter : MonoBehaviour
{
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private GameObject shield;
    private float hp = 100.0f;
    public float ShootingSpeed = 0.8f;
    private float TimeLastShoot = 0.0f;
    private float shieldactivetime = 0.0f;
    private bool isshiedactive = false;
    private int APBulletLVL = 0;
    private int MBulletLVL = 0;
    private int LBulletLVL = 0;
    private int HPLvl = 1;
    private float MaxHp = 100;
    [SerializeField] private int currentammo = 0;

    [SerializeField] private Slider slider;
    [SerializeField] private Slider hpbar;

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnPinchSpread += OnPinchSpread;

        hpbar.maxValue = MaxHp;
        hpbar.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLastShoot += Time.deltaTime;
        if (TimeLastShoot > ShootingSpeed)
        {
            if(isshiedactive == false)
            Shoot();
        }

        if (isshiedactive)
        {
            shieldactivetime += Time.deltaTime;
        }

        if (shieldactivetime >= 1.5f)
        {
            isshiedactive = false;
            shieldactivetime = 0.0f;
        }
       
    }

    private void Shoot()
    {
        GameObject temp = null;
        Image image = GetComponent<Image>();
        switch (currentammo)
        {
            case 0:
                temp = Instantiate(Bullet[0], transform.parent);
                temp.transform.localPosition = transform.localPosition + Vector3.up * image.sprite.rect.height / 4;
                ArmorPiercing bullet = temp.GetComponent<ArmorPiercing>();
                bullet.setDamage(APBulletLVL);
                break;
            case 1:
                temp = Instantiate(Bullet[1], transform.parent);
                temp.transform.localPosition = transform.localPosition + Vector3.up * image.sprite.rect.height / 4;
                LAser bullet1 = temp.GetComponent<LAser>();
                bullet1.setDamage(LBulletLVL);
                break;
            case 2:
                temp = Instantiate(Bullet[2], transform.parent);
                temp.transform.localPosition = transform.localPosition + Vector3.up * image.sprite.rect.height / 4;
                Missile bullet2 = temp.GetComponent<Missile>();
                bullet2.setDamage(MBulletLVL);
                break;
        }

        if(temp != null)
            temp.transform.SetAsFirstSibling();

        TimeLastShoot = 0;
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {

        Directions dir = args.directions;
        if (dir == Directions.LEFT)
        {
            currentammo--;
            if (currentammo < 0)
            {
                currentammo = 2;
            }
        }
        else if (dir == Directions.RIGHT)
        {
            currentammo++;
            if (currentammo > 2)
            {
                currentammo = 0;
            }
        }
    }

    public void OnPinchSpread(object sender, SpreadEventArgs args)
    {
        if (args.DistanceDiff > 0)
        {
            if (shield != null)
            {
                Image img = shield.GetComponent<Image>();
                img.enabled = true;
                isshiedactive = true;
            }

        }
        else
        {
            if (slider.value.Equals(100))
            {
                Debug.Log("super");
                slider.value = 0;
                EnemyManager.GetInstance().SuperBullet();
            }
        }
    }
    private void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
        GestureManager.Instance.OnPinchSpread -= OnPinchSpread;
    }

    public void Damage(float damage)
    {
        if(isshiedactive == false)
            hp -= damage;
        if (slider.value < 100)
        {
            slider.value += 10;
            if (slider.value > 100)
            {
                slider.value = 100;
            }
        }

        hpbar.value = hp;

        if (hp <= 0)
        {
            GameManager.GetInstance().Gameover();
        }

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
    }

    public int getApBulletlvl()
    {
        return APBulletLVL;
    }

    public int gatLaserbulletlvl()
    {
        return LBulletLVL;
    }

    public int getmissilebulletlvl()
    {
        return MBulletLVL;
    }

    public void setApBUlletlvl(int num)
    {
        APBulletLVL = num;
    }
    public void setLBUlletlvl(int num)
    {
        LBulletLVL = num;
    }

    public void setMBUlletlvl(int num)
    {
        MBulletLVL = num;
    }

    public float getMaxHp()
    {
        return MaxHp;
    }

    public void setMaxHp(float num)
    {
        MaxHp = num;
    }

    public int getMaxHpLvl()
    {
        return HPLvl;
    }

    public void SetHPLvl(int num)
    {
        HPLvl = num;
    }

    public float GETHP()
    {
        return hp;
    }

    public void reset()
    {
        hp = MaxHp;
        hpbar.maxValue = MaxHp;
        currentammo = 0;
    }
}
