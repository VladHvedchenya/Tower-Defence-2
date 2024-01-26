using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float _speed = 1f;

    private List<Node> _path = new List<Node>();
    private Enemy _enemy;
    private GridManager _gridManager;
    private Pathfinder _pathfinder;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindAnyObjectByType<GridManager>();
        _pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void RecalculatePath(bool isResetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (isResetPath)
        {
            coordinates = _pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        _path.Clear();
        _path = _pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathfinder.StartCoordinates);
    }

    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        var wait = new WaitForEndOfFrame();

        for (int i = 1; i < _path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].Coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return wait;
            }
        }

        FinishPath();
    }
}
