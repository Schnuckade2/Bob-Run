using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    // Dein 2D-Prefab
    public Vector2 spawnPosition;
    public GameObject bomb;
    public LogicScript logic;


    // 9.62; 3.56


    void Start()
    {
       StartCoroutine(MultiBomb());
       SpawnBomb();
        
    }


    void SpawnObject()
    {
        if (logic.count < 40)
        {
            SpawnObjectEasy();
        }
        else if (logic.count >= 40 && logic.count < 180)
        {
            SpawnObjectMid();
        }
        else if (logic.count >= 180)
        {
            SpawnObjectHard();
        }
    }

    void SpawnObjectEasy()
    {
        int num = Random.Range(1, 15);
        if (num == 10 || num == 1)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 12)
        {
            StartCoroutine(MultiBomb());

        }
        else if (num == 13)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }

         
        
    }
    
    void SpawnObjectMid() 
    {
        int num = Random.Range(1, 22);
        if (num == 10 || num == 2)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 1)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 12)
        {
            StartCoroutine(MultiBomb());

        }
        else if (num == 13 || num == 4)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
    }


    void SpawnObjectHard()
    {
        int num = Random.Range(1, 18);
        if (num == 11)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 1 || num == 2 || num == 3)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 12 || num == 14)
        {
            StartCoroutine(MultiBomb());

        }
        else if (num == 13 || num == 10)
        {
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        else if (num == 4)
        {
            StartCoroutine(MultiBombHard());
        }
    }

    IEnumerator MultiBomb()
    {
        int counter = 0;
        while (counter < 3)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            counter++;
        }

    }

    IEnumerator MultiBombHard()
    {
        int counter = 0;
        while (counter < 10)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(bomb, spawnPosition, Quaternion.identity);
            counter++;
        }

    }


    void SpawnBomb()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, 1f);
    }
}
