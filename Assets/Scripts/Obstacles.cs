using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacles : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnPositionX = 11.0f;
    public float objectSpeed = 2.0f;
    public float initialWaitTime = 3.0f;
    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 5.0f;
    public int maxObjectOnScreen = 2;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnObjectsCoroutine());
    }

    IEnumerator SpawnObjectsCoroutine() {

        yield return new WaitForSeconds(initialWaitTime);

        while (true) {
            if (spawnedObjects.Count < maxObjectOnScreen) {
                SpawnObject();
            }

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnObject() {
        int randomIndex = Random.Range(0, obstacles.Length);
        Vector2 spawnPosition = new Vector2(spawnPositionX, 0);

        if (obstacles[randomIndex].CompareTag("Cloud")) {

            spawnPosition.y = Random.Range(1.5f, 4.3f);
        } else {

            spawnPosition.y = Random.Range(-4f, -2.5f);
        }
        GameObject spawnedObject = Instantiate(obstacles[randomIndex], spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<ObjectMovement>().speed = objectSpeed;
        spawnedObjects.Add(spawnedObject);

        spawnedObject.GetComponent<ObjectMovement>().OnObjectDestroyed += () => {
            spawnedObjects.Remove(spawnedObject);
        };
    }
}
