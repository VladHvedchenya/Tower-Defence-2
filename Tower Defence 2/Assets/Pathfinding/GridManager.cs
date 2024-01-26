using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    
    [Tooltip("World grid size should match UnityEditor snap settings")]
    [SerializeField] private int _unityGridSize = 10;

    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid => _grid;

    public int UnityGridSize => _unityGridSize;

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates) == false)
            return null;

        return _grid[coordinates];
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            _grid[coordinates].IsWalkable = false;
        }
    }

    public void ResetNods()
    {
        foreach (var entry in _grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.IsExplored = false;
            entry.Value.IsPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnityGridSize;
        position.z = coordinates.y * UnityGridSize;

        return position;
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector2Int coordinates = new Vector2Int(i, j);
                _grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}