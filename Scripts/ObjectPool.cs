using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] [Range(0.1f, 30f)] private float spawnTime = 2f;
    [SerializeField] [Range(1, 15)] private int poolSize = 5;

    private WaitForSecondsRealtime _waitTime;
    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        _pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            _pool[i] = Instantiate(enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    private void Start()
    {
        _waitTime = new WaitForSecondsRealtime(spawnTime);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return _waitTime;
        }
    }

    private void EnableObjectInPool()
    {
        foreach (var enemy in _pool)
        {
            if (enemy.activeInHierarchy)
                continue;
            enemy.SetActive(true);
            return;
        }
    }
}
