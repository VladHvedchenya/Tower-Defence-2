using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower _ballistaPrefab;
    [SerializeField] private bool _isPlaceable;

    private GridManager _gridManager;
    private Vector2Int _coordinates = new Vector2Int();
    private Pathfinder _pathfinder;

    private void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
        _pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if (!IsPlaceable)
                _gridManager.BlockNode(_coordinates);
        }
    }

    public bool IsPlaceable => _isPlaceable;

    private void OnMouseDown()
    {
        if (_gridManager.GetNode(_coordinates).IsWalkable && !_pathfinder.WillBlockPath(_coordinates))
        {
            bool isSuccessful = _ballistaPrefab.CreateTower(_ballistaPrefab, transform.position);

            if (isSuccessful)
            {
                _gridManager.BlockNode(_coordinates);
                _pathfinder.NotifyReceivers();
            }
        }
    }
}
