using System.Collections;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircleCount = 5;
    public int maxCircleCount = 10;
    public float minCircleSize = 0.5f;
    public float maxCircleSize = 2.0f;

    void Start()
    {
        SpawnCircles();
    }

    void SpawnCircles()
    {
        int circleCount = Random.Range(minCircleCount, maxCircleCount + 1);

        for (int i = 0; i < circleCount; i++)
        {
            float randomSize = Random.Range(minCircleSize, maxCircleSize);
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);

            GameObject newCircle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            newCircle.transform.localScale = Vector3.one * randomSize;
        }
    }
}
