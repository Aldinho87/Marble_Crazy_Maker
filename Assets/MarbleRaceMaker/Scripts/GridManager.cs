using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int gridSize = 20;
    [SerializeField] private float cellSize = 4f;

    [Header("Vertical Levels")]
    [SerializeField] private int minYLevel = -2;
    [SerializeField] private int maxYLevel = 2;
    [SerializeField] private float yLevelStep = 2f;

    [Header("Active Construction Level")]
    [SerializeField, Range(-2, 2)] private int activeYLevel = 0;

    [Header("Visual")]
    [SerializeField] private Color gridColor = new Color(0.1f, 0.8f, 1f, 1f);
    [SerializeField] private float lineThickness = 3f;
    [SerializeField] private float adjacentGridAlpha = 0.25f;

    public int GridSize => gridSize;
    public float CellSize => cellSize;

    public int MinYLevel => minYLevel;
    public int MaxYLevel => maxYLevel;
    public float YLevelStep => yLevelStep;

    public int ActiveYLevel => activeYLevel;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        DrawConstructionGrids();
    }

    private void DrawConstructionGrids()
    {
        if (gridSize <= 0 || cellSize <= 0f)
            return;

        DrawGridAtLevel(
            activeYLevel,
            gridColor,
            lineThickness
        );

        int upperLevel = activeYLevel + 1;

        if (upperLevel <= maxYLevel)
        {
            Color adjacentColor = gridColor;
            adjacentColor.a *= adjacentGridAlpha;

            DrawGridAtLevel(
                upperLevel,
                adjacentColor,
                lineThickness
            );
        }

        int lowerLevel = activeYLevel - 1;

        if (lowerLevel >= minYLevel)
        {
            Color adjacentColor = gridColor;
            adjacentColor.a *= adjacentGridAlpha;

            DrawGridAtLevel(
                lowerLevel,
                adjacentColor,
                lineThickness
            );
        }

        Handles.color = Color.red;

        Handles.SphereHandleCap(
            0,
            new Vector3(
                transform.position.x,
                GetYPosition(activeYLevel),
                transform.position.z
            ),
            Quaternion.identity,
            0.3f,
            EventType.Repaint
        );
    }

    private void DrawGridAtLevel(
        int yLevel,
        Color color,
        float thickness)
    {
        Handles.color = color;

        float totalSize = gridSize * cellSize;
        float halfSize = totalSize * 0.5f;
        float y = GetYPosition(yLevel);

        for (int i = 0; i <= gridSize; i++)
        {
            float x = -halfSize + i * cellSize;

            Vector3 start =
                transform.position +
                new Vector3(
                    x,
                    y - transform.position.y,
                    -halfSize
                );

            Vector3 end =
                transform.position +
                new Vector3(
                    x,
                    y - transform.position.y,
                    halfSize
                );

            Handles.DrawLine(
                start,
                end,
                thickness
            );
        }

        for (int i = 0; i <= gridSize; i++)
        {
            float z = -halfSize + i * cellSize;

            Vector3 start =
                transform.position +
                new Vector3(
                    -halfSize,
                    y - transform.position.y,
                    z
                );

            Vector3 end =
                transform.position +
                new Vector3(
                    halfSize,
                    y - transform.position.y,
                    z
                );

            Handles.DrawLine(
                start,
                end,
                thickness
            );
        }
    }
#endif

    public void SetActiveYLevel(int yLevel)
    {
        activeYLevel =
            Mathf.Clamp(
                yLevel,
                minYLevel,
                maxYLevel
            );
    }

    public Vector3 SnapToGrid(Vector3 position)
    {
        Vector3 localPosition =
            position - transform.position;

        float x =
            Mathf.Round(
                localPosition.x / cellSize
            ) * cellSize;

        float z =
            Mathf.Round(
                localPosition.z / cellSize
            ) * cellSize;

        return transform.position +
               new Vector3(
                   x,
                   position.y,
                   z
               );
    }

    public float GetYPosition(int yLevel)
    {
        yLevel =
            Mathf.Clamp(
                yLevel,
                minYLevel,
                maxYLevel
            );

        return transform.position.y +
               yLevel * yLevelStep;
    }

    public int GetYLevel(float worldY)
    {
        float localY =
            worldY - transform.position.y;

        return Mathf.RoundToInt(
            localY / yLevelStep
        );
    }
}
