using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor= Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {

        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        // 시작할 때는 안 보이게
        label.enabled = true;
        DisplayCoordinates();
    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            // 작업 1
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLables();
    }

    void ToggleLables()
    {
        //label.enabled = true;
    }

    private void SetLabelColor()
    {
        if (!gridManager) return;
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
