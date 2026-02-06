using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] float cellSize = 1f;
    private List<GameObject> enemyList = new List<GameObject>();
    GameManager gameManager;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnEnemy();
        }
        renderEnemy();
        
    }

    void Update()
    {
        
    }

    void spawnEnemy()
    {
        int ranPosY = Random.Range(0, 9);
        int ranPosX = Random.Range(0, 5);

        Vector2 pos = new Vector2(ranPosX, ranPosY);
        // var all_pos = getAllEnemyPosition();

        GameObject newEnemy = Instantiate(enemyPrefabs, pos,  Quaternion.identity);
        newEnemy.GetComponent<Enemy>().enemyPos = new Vector2(ranPosX, ranPosY);
        enemyList.Add(newEnemy);
    }

    void renderEnemy()
    {
        foreach (GameObject e in enemyList)
        {
            // Get pos
            Vector2 v = e.GetComponent<Enemy>().enemyPos;
            int x = (int)v.x;
            int y = (int)v.y;

            // Translate to actual pos
            Vector3 pos = new Vector3(x * cellSize, y * cellSize, 0f);

            // Set object to pos
            e.GetComponent<Enemy>().transform.position = pos;
        }
        
    }

    List<Vector2> getAllEnemyPosition()
    {
        List<Vector2> r = new List<Vector2>();
        foreach (GameObject v in enemyList)
        {
            r.Add(v.GetComponent<Enemy>().enemyPos);
        }
        return r;
    }

    public void ememiesMove()
    {
        foreach (GameObject v in enemyList)
        {
            v.GetComponent<Enemy>().enemyPos.x += 1.0f;
        }
        renderEnemy();
    }

    // void takeDamage()
    // {
    //     for (int y = 0; y < 10; y++)
    //     {
    //         for (int x = 0; x < 10; x++)
    //         {
    //             foreach (GameObject v in enemyList)
    //             {
    //                 var vX = v.GetComponent<Enemy>().enemyPos.x;
    //                 var vY = v.GetComponent<Enemy>().enemyPos.y;
    //                 if (gameManager.status[y][x] == )
    //             }

    //         }
    //     }
        
    // }


    // public Transform randomEnemyPosition()
    // {

    // }
}
