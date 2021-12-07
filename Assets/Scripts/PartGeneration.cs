using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartGeneration : MonoBehaviour
{
    public GameObject[] bodyParts;
    public Attribute[] attributes;
    public Type[] types;
    public float spawnRateMin, spawnRateMax;
    public bool canSpawn;
    public float curSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            curSpawnRate -= Time.deltaTime;
            if (curSpawnRate <= 0)
            {
                curSpawnRate = Random.Range(spawnRateMin, spawnRateMax);
                GameObject partClone = Instantiate(bodyParts[Random.Range(0, bodyParts.Length)], transform.position, Quaternion.identity);
                float percentGenerated = Random.Range(0,100);
                float percentGeneratedLeft = 100 - percentGenerated;
                partClone.GetComponent<BodyPart>().SetAttributes(attributes[Random.Range(0, attributes.Length)],
                 percentGenerated, attributes[Random.Range(0, attributes.Length)], percentGeneratedLeft);

            }
        }
    }
}
