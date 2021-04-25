using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField]
    PlayerControls player;

    [SerializeField]
    GameObject[] fishLowPrefab;
    [SerializeField]
    GameObject[] fishMidPrefab;
    [SerializeField]
    GameObject[] fishHighPrefab;

    [SerializeField]
    GameObject[] trashPrefab;

    [SerializeField]
    float spawnDistance;

    [SerializeField]
    float midDeepSea;

    [SerializeField]
    float highDeepSea;

    int fishProbability;
    int trashProbability;

    public static Core Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fishProbability = 40;
        trashProbability = 40;
        InvokeRepeating("SpawnThings", 0f, 0.5f);
    }

    void Update()
    {
        if (player.gameObject.transform.position.y < highDeepSea)
        {
            fishProbability = 30;
            trashProbability = 65;
        }
        else if (player.gameObject.transform.position.y < midDeepSea)
        {
            fishProbability = 40;
            trashProbability = 45;
        }
        else
        {
            fishProbability = 50;
            trashProbability = 25;
        }
    }

    // Update is called once per frame
    void SpawnThings()
    {
        if (player.started && player.transform.position.y < 4f)
        {
            spawnDistance = Mathf.Abs(spawnDistance);
            if (player.rb.gravityScale < 0)
            {
                spawnDistance *= -1;
            }

            int rand = Random.Range(1, 101);
            if (rand < fishProbability)
            {
                Vector3 fishPos = new Vector3(Random.Range(-1f, 1f), player.gameObject.transform.position.y - spawnDistance, 0f);
                SpawnFish(fishPos);
            }
            else if (rand < fishProbability + trashProbability)
            {
                Vector3 trashPos = new Vector3(Random.Range(-1f, 1f), player.gameObject.transform.position.y - spawnDistance, 0f);
                if (trashPos.y > -155f)
                {
                    int trashIndex = Random.Range(0, trashPrefab.Length);
                    Instantiate(trashPrefab[trashIndex], trashPos, Quaternion.identity);
                }
            }
        }
    }

    void SpawnFish(Vector3 fishPos)
    {
        if (fishPos.y > -155f)
        {
            int randomFish;
            if (player.gameObject.transform.position.y < highDeepSea)
            {
                randomFish = Random.Range(0, fishHighPrefab.Length);
                Instantiate(fishHighPrefab[randomFish], fishPos, Quaternion.identity);
            }
            else if (player.gameObject.transform.position.y < midDeepSea)
            {
                randomFish = Random.Range(0, fishMidPrefab.Length);
                Instantiate(fishMidPrefab[randomFish], fishPos, Quaternion.identity);
            }
            else
            {
                randomFish = Random.Range(0, fishLowPrefab.Length);
                Instantiate(fishLowPrefab[randomFish], fishPos, Quaternion.identity);
            }
        }
    }
}
