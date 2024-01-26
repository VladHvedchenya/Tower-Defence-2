using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 30f)] private float _delay = 1f;
    [SerializeField] [Range (0,50)] private int _poolSize = 5;
    [SerializeField] private GameObject _enemyPrefab;

    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            EnableObjectInPool();
            yield return wait;
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i].activeInHierarchy == false)
            {
                _pool[i].SetActive(true);
                return;
            }
        }
    }

    private void PopulatePool()
    {
        _pool = new GameObject[_poolSize];

        for (int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }
}
