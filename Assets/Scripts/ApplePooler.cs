using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePooler : MonoBehaviour
{
    [SerializeField] GameObject applePrefab = null;
    [SerializeField] int appleStartCreation = 10;
    [SerializeField] Transform[] spawnPoints = null;
    [SerializeField] float spawnRate = 2;
    [SerializeField] float minimumSpawnRate = 0.5f;
    [SerializeField] GameObject appleCrushParticles = null;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip appleDestroySound = null;

    Queue<GameObject> applesQueue = new Queue<GameObject>();

    private void Awake()
    {
        CreateInitialApples();
        StartCoroutine(SpawnApple());
    }

    private void CreateInitialApples()
    {
        for (int i = 0; i < appleStartCreation; i++)
        {
            EnqueueApple(Instantiate(applePrefab));
        }
    }

    private IEnumerator SpawnApple()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if (applesQueue.Count <= 0)
            {
                EnqueueApple(Instantiate(applePrefab));
            }

            GameObject apple = applesQueue.Dequeue();

            int randomPoint = Random.Range(0, spawnPoints.Length - 1);
            apple.transform.position = spawnPoints[randomPoint].position;
            apple.SetActive(true);

            spawnRate = Mathf.Clamp(spawnRate - 0.05f, minimumSpawnRate, 2);
        }
    }

    public void EnqueueApple(GameObject appleToEnqueue)
    {
        appleToEnqueue.SetActive(false);
        applesQueue.Enqueue(appleToEnqueue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Apple"))
        {
            EnqueueApple(collision.gameObject);
            Instantiate(appleCrushParticles, collision.GetContact(0).point, appleCrushParticles.transform.rotation);
            audioSource.PlayOneShot(appleDestroySound);
        }
    }
}
