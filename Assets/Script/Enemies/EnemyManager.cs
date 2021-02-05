using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager Instance;

    public static EnemyManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new EnemyManager();
            return Instance;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public GameObject Armoured;
    public GameObject Shielded;
    public GameObject Swarm;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    public List<EnemySpawn> EnemyList;

    private int currentIndex = 0;
    private int currentSpawnIndex = 0;
    private List<int> spawnIndex;
    [SerializeField] private List<GameObject> spawnedEnemies;
    [SerializeField] private GameObject BossHPBar;
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private AudioSource deathEffect;

    public Starfighter player;

    private void Update()
    {
        if (spawnedEnemies.Count == 0 && GameManager.GetInstance().GetGamePanel().activeSelf)
        {
            SpawnNextBatch();
        }
    }

    public void SetStage(int stageNum)
    {
        Reset();

        switch (stageNum)
        {
            case 1:
                Stage1();
                break;
            case 2:
                Stage2();
                break;
            case 3:
                Stage3();
                break;
            default:
                break;
        }
    }

    private void SpawnNextBatch()
    {
        if (currentSpawnIndex < spawnIndex.Count)
        {
            int increment = currentIndex;
            for (; currentIndex <  increment + spawnIndex[currentSpawnIndex]; currentIndex++)
            {
                SpawnEnemy(EnemyList[currentIndex]);
            }
            currentSpawnIndex++;
        }
        else if (currentIndex < EnemyList.Count)
        {//spawn boss
            SpawnEnemy(EnemyList[currentIndex]);
            currentIndex++;
            MusicManager.GetInstance().PlayBoss();
        }
    }

    private void Stage1()
    {
          
        //All spawnable Enemies
        {// Batch 1
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.25f, -0.1f), new Vector2(0.25f, .2f) ));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.45f, .2f) ));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .2f) ));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.85f, -0.1f), new Vector2(0.85f, .2f) ));
            spawnIndex.Add(4);
        }///*
        {// Batch 2
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .22f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.45f, -0.1f), new Vector2(0.25f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.55f, -0.1f), new Vector2(0.75f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .22f)));
            spawnIndex.Add(4);
        }
        {// Batch 3
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.35f, -0.1f), new Vector2(0.85f, .3f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.25f, -0.1f), new Vector2(0.75f, .25f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.15f, -0.1f), new Vector2(0.65f, .2f)));
            spawnIndex.Add(3);
        }
        {// Batch 4
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.15f, .3f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.75f, -0.1f), new Vector2(0.25f, .25f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.85f, -0.1f), new Vector2(0.35f, .2f)));
            spawnIndex.Add(3);
        }
        {// Batch 5
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.10f, -0.45f), new Vector2(0.65f, .25f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.15f, -0.4f), new Vector2(0.75f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.85f, -0.2f), new Vector2(0.25f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.95f, -0.2f), new Vector2(0.35f, .25f)));
            spawnIndex.Add(4);
        }
        {// Batch 6
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.25f, -0.1f), new Vector2(0.25f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.45f, -0.1f), new Vector2(0.45f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.85f, -0.1f), new Vector2(0.85f, .2f)));
            spawnIndex.Add(4);
        }
        {// Batch 7
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.4f, -0.2f), new Vector2(0.35f, .3f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.5f, -0.1f), new Vector2(0.5f, .25f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.6f, -0.2f), new Vector2(0.65f, .3f)));
            spawnIndex.Add(3);
        }
        //*/


        {// Boss
            EnemyList.Add(new EnemySpawn(EnemyType.Boss1, new Vector2(0.5f, -10.3f), new Vector2(0.5f, 0.1f)));
        }
    }

    private void Stage2()
    {
        //All spawnable Enemies
        {// Batch 1
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.25f, -0.1f), new Vector2(0.25f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.45f, -0.1f), new Vector2(0.45f, .15f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .15f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.85f, -0.1f), new Vector2(0.85f, .2f)));
            spawnIndex.Add(4);
        }///*
        {// Batch 2
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.35f, -0.1f), new Vector2(0.25f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.45f, -0.1f), new Vector2(0.405f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.55f, -0.1f), new Vector2(0.60f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.65f, -0.1f), new Vector2(0.75f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 3
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.25f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.55f, -0.1f), new Vector2(0.75f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 4
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.35f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.55f, -0.1f), new Vector2(0.65f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 5
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.35f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.55f, -0.1f), new Vector2(0.50f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .2f)));
            spawnIndex.Add(5);
        }
        {// Boss
            EnemyList.Add(new EnemySpawn(EnemyType.Boss2, new Vector2(0.5f, -10.3f), new Vector2(0.5f, 0.1f)));
        }
    }

    private void Stage3()
    {
        //All spawnable Enemies
        {// Batch 1
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.25f, -0.1f), new Vector2(0.25f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.45f, -0.1f), new Vector2(0.45f, .15f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .15f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.85f, -0.1f), new Vector2(0.85f, .2f)));
            spawnIndex.Add(4);
        }///*
        {// Batch 2
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.35f, -0.1f), new Vector2(0.25f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.45f, -0.1f), new Vector2(0.405f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.55f, -0.1f), new Vector2(0.60f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.75f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 3
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.25f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.55f, -0.1f), new Vector2(0.75f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 4
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.45f, -0.1f), new Vector2(0.35f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.55f, -0.1f), new Vector2(0.65f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .12f)));
            spawnIndex.Add(4);
        }
        {// Batch 5
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.35f, -0.1f), new Vector2(0.15f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.45f, -0.1f), new Vector2(0.35f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.55f, -0.1f), new Vector2(0.50f, .12f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Armoured, new Vector2(0.65f, -0.1f), new Vector2(0.65f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Swarm, new Vector2(0.65f, -0.1f), new Vector2(0.85f, .2f)));
            EnemyList.Add(new EnemySpawn(EnemyType.Shielded, new Vector2(0.80f, -0.1f), new Vector2(0.95f, .2f)));
            spawnIndex.Add(6);

        }
        {
            EnemyList.Add(new EnemySpawn(EnemyType.Boss3, new Vector2(0.5f, -10.3f), new Vector2(0.5f, 0.1f)));
        }
    }
    private void SpawnEnemy(EnemySpawn spawn)
    {
        GameObject temp = null;
        switch (spawn.Type)
        {
            case EnemyType.Armoured:
                temp = Instantiate(Armoured, GameManager.GetInstance().GetMiddlePanel().transform);
                break;
            case EnemyType.Shielded:
                temp = Instantiate(Shielded, GameManager.GetInstance().GetMiddlePanel().transform);
                break;
            case EnemyType.Swarm:
                temp = Instantiate(Swarm, GameManager.GetInstance().GetMiddlePanel().transform);
                break;
            case EnemyType.Boss1:
                temp = Instantiate(Boss1, GameManager.GetInstance().GetMiddlePanel().transform);
                if(BossHPBar != null)
                {
                    BossHPBar.SetActive(true);
                    Slider slider = BossHPBar.GetComponent<Slider>();
                    slider.maxValue = 150;
                    slider.value = 150;
                    BossHPBar.GetComponentInChildren<Text>().text = "Piaza";
                }
                MusicManager.GetInstance().PlayBoss();
                break;
            case EnemyType.Boss2:
                temp = Instantiate(Boss2, GameManager.GetInstance().GetMiddlePanel().transform);
                if (BossHPBar != null)
                {
                    BossHPBar.SetActive(true);
                    Slider slider = BossHPBar.GetComponent<Slider>();
                    slider.maxValue = 300;
                    slider.value = 300;

                    BossHPBar.GetComponentInChildren<Text>().text = "Farloun";
                }
                MusicManager.GetInstance().PlayBoss();
                break;
            case EnemyType.Boss3:
                temp = Instantiate(Boss3, GameManager.GetInstance().GetMiddlePanel().transform);
                if (BossHPBar != null)
                {
                    BossHPBar.SetActive(true);
                    Slider slider = BossHPBar.GetComponent<Slider>();
                    slider.maxValue = 500;
                    slider.value = 500;
                    BossHPBar.GetComponentInChildren<Text>().text = "Tarion";
                }
                MusicManager.GetInstance().PlayBoss();
                break;
            default:
                Debug.Log("Enemy Type not set");
                break;
        }

        if (temp != null)
        {
            spawnedEnemies.Add(temp);
            Enemy enemy = temp.GetComponent<Enemy>();
            temp.transform.localPosition = enemy.CalculatePointDestination(spawn.StartLoc);
            enemy.PercentDestination = spawn.Destination;
            temp.transform.SetAsFirstSibling();
        }
        else
        {
            Debug.LogError("Enemyspawn is null!!!");
        }
    }

    public void DeleteEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }

    public void SuperBullet()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            Enemy enemy = spawnedEnemies[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(10);
            }
            else if (enemy == null)
            {
                Boss1 boss = GetComponent<Boss1>();
                boss.Damage(10);
            }
        }

        DeleteAllBullet();
    }

    public void Reset()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        DeleteAllBullet();

        currentSpawnIndex = 0;
        currentIndex = 0;
        spawnIndex = new List<int>();
        spawnedEnemies = new List<GameObject>();
        EnemyList = new List<EnemySpawn>();
        bulletList = new List<GameObject>();

        BossHPBar.SetActive(false);
    }

    public void SetBossSlider(float hp)
    {
        Slider slider = BossHPBar.GetComponent<Slider>();
        
        slider.value = hp;
    }

    public void AddBullet(GameObject bullet)
    {
        bulletList.Add(bullet);
    }

    public void DeleteBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
    }
    public void DeleteAllBullet()
    {
        foreach (GameObject bullet in bulletList)
        {
            Destroy(bullet);
        }
    }

    public void PlayEnemyDeath()
    {
        if (deathEffect != null)
        {
            deathEffect.Play();
        }
    }
}
