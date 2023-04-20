using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoodinatesLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f);

    private TextMeshPro _label;
    private Vector2Int _coordinates;
    private GridManager _gridManager;

    private void Awake()
    {
        pathColor = new Color(1f, 0.5f, 0f);
        _gridManager = FindObjectOfType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        _label.enabled = true;
        DisplayCoordinates();
    }

    private void Update()
    {
        ColorCoordinates();
        ToggleLabels();
        if (Application.isPlaying)
            return;
        if (_label == null)
            _label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        UpdadeObjectName();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
            _label.enabled = !_label.enabled;
    }

    private void ColorCoordinates()
    {
        if (_gridManager == null) //��������
            return;

        Node node = _gridManager.GetNode(_coordinates);
        // ���� �� �����, ��� ����� �������� null, �� �� ������� ��������� �� null

        if (node == null)
            return;

        if (!node.isWalkable)
            _label.color = blockedColor;
        else if (node.isPath)
            _label.color = pathColor;
        else if (node.isExplored)
            _label.color = exploredColor;
        else
            _label.color = defaultColor;
    }

    private void UpdadeObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void DisplayCoordinates()
    {
        var position = transform.parent.position;
        _coordinates.x = Mathf.RoundToInt(position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(position.z / _gridManager.UnityGridSize);

        _label.text = $"{_coordinates.x}.{_coordinates.y}";
    }
}
