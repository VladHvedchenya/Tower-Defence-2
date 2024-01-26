using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _blockedColor = Color.gray;
    [SerializeField] private Color _exploredColor = Color.red;
    [SerializeField] private Color _pathColor = Color.blue;

    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private GridManager _gridManager;

    private void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        _label.enabled = true;
        DisplayCoordinates();
    }

    private void Update()
    {
        if (Application.isPlaying == false)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.enabled;
        }
    }

    private void DisplayCoordinates()
    {
        if (_gridManager == null)
            return;

        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);
        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if (_gridManager == null)
            return;

        Node node = _gridManager.GetNode(_coordinates);

        if (node == null)
            return;

        if (!node.IsWalkable)
            _label.color = _blockedColor;
        else if (node.IsPath)
            _label.color = _pathColor;
        else if (node.IsExplored)
            _label.color = _exploredColor;
        else
            _label.color = _defaultColor;
    }
}
