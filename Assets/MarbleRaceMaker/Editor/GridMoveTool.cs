using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class GridMoveTool
{
    private static Transform trackedTransform;

    private static Vector3 lastPosition;
    private static Vector3 lastValidPosition;

    private static bool undoRecorded;

    static GridMoveTool()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        Selection.selectionChanged += OnSelectionChanged;
    }

    private static void OnSelectionChanged()
    {
        ResetTracking();
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            ResetTracking();
            return;
        }

        GridSnap gridSnap =
            selectedObject.GetComponent<GridSnap>();

        if (gridSnap == null)
        {
            ResetTracking();
            return;
        }

        Transform currentTransform =
            selectedObject.transform;

        if (trackedTransform != currentTransform)
        {
            trackedTransform = currentTransform;

            lastPosition =
                currentTransform.position;

            lastValidPosition =
                currentTransform.position;

            undoRecorded = false;
        }

        if (currentTransform.position != lastPosition)
        {
            if (!undoRecorded)
            {
                Undo.RecordObject(
                    currentTransform,
                    "Move Track Piece"
                );

                undoRecorded = true;
            }

            Vector3 requestedPosition =
                currentTransform.position;

            if (WouldBreakVerticalConnection(
                currentTransform,
                requestedPosition))
            {
                currentTransform.position =
                    lastValidPosition;

                lastPosition =
                    lastValidPosition;

                EditorUtility.SetDirty(
                    currentTransform
                );

                return;
            }

            lastValidPosition =
                requestedPosition;

            lastPosition =
                requestedPosition;

            gridSnap.ValidateExistingConnections();

            EditorUtility.SetDirty(
                currentTransform
            );
        }

        if (Event.current.type == EventType.MouseUp)
        {
            undoRecorded = false;

            lastPosition =
                currentTransform.position;

            lastValidPosition =
                currentTransform.position;
        }
    }

    private static bool WouldBreakVerticalConnection(
        Transform movedTransform,
        Vector3 requestedPosition)
    {
        TrackPiece trackPiece =
            movedTransform.GetComponent<TrackPiece>();

        if (trackPiece == null)
            return false;

        ConnectionPoint[] connectionPoints =
            trackPiece.ConnectionPoints;

        if (connectionPoints == null)
            return false;

        Vector3 positionDelta =
            requestedPosition -
            movedTransform.position;

        foreach (ConnectionPoint ownPoint in connectionPoints)
        {
            if (ownPoint == null ||
                !ownPoint.IsConnected)
            {
                continue;
            }

            ConnectionPoint otherPoint =
                ownPoint.ConnectedTo;

            if (otherPoint == null)
                continue;

            float requestedWorldY =
                ownPoint.WorldPosition.y +
                positionDelta.y;

            GridManager gridManager =
                Object.FindAnyObjectByType<GridManager>();

            if (gridManager == null)
                continue;

            int requestedYLevel =
                gridManager.GetYLevel(
                    requestedWorldY
                );

            int otherYLevel =
                otherPoint.YLevel;

            int yDifference =
                Mathf.Abs(
                    requestedYLevel -
                    otherYLevel
                );

            if (yDifference > 1)
            {
                return true;
            }
        }

        return false;
    }

    private static void ResetTracking()
    {
        trackedTransform = null;

        lastPosition = Vector3.zero;
        lastValidPosition = Vector3.zero;

        undoRecorded = false;
    }
}
