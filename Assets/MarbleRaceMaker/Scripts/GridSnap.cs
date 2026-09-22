using UnityEngine;

public class GridSnap : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [Header("Connection Snap")]
    [SerializeField] private float connectionSnapDistance = 4f;

    private void OnValidate()
    {
        if (gridManager == null)
        {
            gridManager = FindAnyObjectByType<GridManager>();
        }
    }

    public void Snap()
    {
        if (gridManager == null)
        {
            gridManager = FindAnyObjectByType<GridManager>();
        }

        if (gridManager == null)
        {
            Debug.LogWarning("GridManager non trovato nella scena.");
            return;
        }

        Vector3 snappedPosition =
            gridManager.SnapToGrid(transform.position);

        int yLevel =
            gridManager.GetYLevel(transform.position.y);

        float snappedY =
            gridManager.GetYPosition(yLevel);

        transform.position = new Vector3(
            snappedPosition.x,
            snappedY,
            snappedPosition.z
        );

        ValidateExistingConnections();
    }

    public bool SnapToNearestConnectionPoint()
    {
        TrackPiece ownTrackPiece =
            GetComponent<TrackPiece>();

        if (ownTrackPiece == null)
        {
            Debug.LogWarning(
                "GridSnap richiede un TrackPiece sullo stesso GameObject."
            );

            return false;
        }

        ConnectionPoint[] ownPoints =
            ownTrackPiece.ConnectionPoints;

        if (ownPoints == null || ownPoints.Length == 0)
        {
            Debug.LogWarning(
                "Il TrackPiece non contiene ConnectionPoint."
            );

            return false;
        }

        ConnectionPoint[] allPoints =
            FindObjectsByType<ConnectionPoint>(
                FindObjectsInactive.Exclude
            );

        ConnectionPoint bestTarget = null;
        ConnectionPoint bestOwnPoint = null;

        float bestDistance =
            connectionSnapDistance;

        foreach (ConnectionPoint ownPoint in ownPoints)
        {
            if (ownPoint == null)
                continue;

            foreach (ConnectionPoint targetPoint in allPoints)
            {
                if (targetPoint == null)
                    continue;

                if (targetPoint.transform.IsChildOf(transform))
                    continue;

                if (!ownPoint.IsCompatibleWith(targetPoint))
                    continue;

                float distance =
                    Vector3.Distance(
                        ownPoint.WorldPosition,
                        targetPoint.WorldPosition
                    );

                if (distance > bestDistance)
                    continue;

                bestDistance = distance;
                bestOwnPoint = ownPoint;
                bestTarget = targetPoint;
            }
        }

        if (bestOwnPoint == null || bestTarget == null)
        {
            Debug.Log(
                "Nessun ConnectionPoint compatibile trovato."
            );

            return false;
        }

        AlignToConnectionPoint(
            bestOwnPoint,
            bestTarget
        );

        bestOwnPoint.ConnectTo(bestTarget);

        Debug.Log(
            $"TrackPiece agganciato a {bestTarget.name}."
        );

        return true;
    }

    private void AlignToConnectionPoint(
        ConnectionPoint ownPoint,
        ConnectionPoint targetPoint)
    {
        Vector3 ownDirection =
            ownPoint.WorldDirection;

        Vector3 targetDirection =
            -targetPoint.WorldDirection;

        Quaternion rotationDifference =
            Quaternion.FromToRotation(
                ownDirection,
                targetDirection
            );

        transform.rotation =
            rotationDifference * transform.rotation;

        Vector3 positionOffset =
            targetPoint.WorldPosition -
            ownPoint.WorldPosition;

        transform.position += positionOffset;
    }

    public void ValidateExistingConnections()
    {
        TrackPiece trackPiece =
            GetComponent<TrackPiece>();

        if (trackPiece == null)
            return;

        ConnectionPoint[] ownPoints =
            trackPiece.ConnectionPoints;

        if (ownPoints == null)
            return;

        foreach (ConnectionPoint ownPoint in ownPoints)
        {
            if (ownPoint == null || !ownPoint.IsConnected)
                continue;

            ConnectionPoint other =
                ownPoint.ConnectedTo;

            if (other == null)
            {
                ownPoint.Disconnect();
                continue;
            }

            if (!IsConnectionPositionValid(
                ownPoint,
                other))
            {
                Debug.Log(
                    $"Connessione tra {ownPoint.name} e {other.name} rimossa."
                );

                ownPoint.Disconnect();
            }
        }
    }

    private bool IsConnectionPositionValid(
        ConnectionPoint ownPoint,
        ConnectionPoint otherPoint)
    {
        if (otherPoint == null)
            return false;

        Vector3 ownPosition =
            ownPoint.WorldPosition;

        Vector3 otherPosition =
            otherPoint.WorldPosition;

        Vector2 ownXZ = new Vector2(
            ownPosition.x,
            ownPosition.z
        );

        Vector2 otherXZ = new Vector2(
            otherPosition.x,
            otherPosition.z
        );

        float horizontalDistance =
            Vector2.Distance(
                ownXZ,
                otherXZ
            );

        if (horizontalDistance > 0.05f)
            return false;

        int yDifference =
            Mathf.Abs(
                ownPoint.YLevel -
                otherPoint.YLevel
            );

        return yDifference <= 1;
    }
}
