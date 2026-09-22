using UnityEngine;

public enum ConnectionType
{
    Standard,
    Reduced
}

public class ConnectionPoint : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] private ConnectionType connectionType = ConnectionType.Standard;

    private ConnectionPoint connectedTo;

    public ConnectionType Type => connectionType;

    public bool IsOccupied => connectedTo != null;

    public bool IsConnected => connectedTo != null;

    public ConnectionPoint ConnectedTo => connectedTo;

    public Transform PointTransform => transform;

    public float TrackWidth
    {
        get
        {
            return connectionType switch
            {
                ConnectionType.Standard => 2.8f,
                ConnectionType.Reduced => 1.8f,
                _ => 2.8f
            };
        }
    }

    public Vector3 WorldPosition => transform.position;

    public Vector3 WorldDirection => transform.forward;

    public int YLevel
    {
        get
        {
            GridManager gridManager = FindAnyObjectByType<GridManager>();

            if (gridManager == null)
                return 0;

            return gridManager.GetYLevel(transform.position.y);
        }
    }

    public void ConnectTo(ConnectionPoint other)
    {
        if (other == null || other == this)
            return;

        Disconnect();

        if (other.connectedTo != null)
        {
            other.Disconnect();
        }

        connectedTo = other;
        other.connectedTo = this;
    }

    public void Disconnect()
    {
        ConnectionPoint other = connectedTo;

        connectedTo = null;

        if (other != null && other.connectedTo == this)
        {
            other.connectedTo = null;
        }
    }

    public bool IsCompatibleWith(ConnectionPoint other)
    {
        if (other == null || other == this)
            return false;

        if (IsOccupied || other.IsOccupied)
            return false;

        if (connectionType != other.connectionType)
            return false;

        int yDifference = Mathf.Abs(YLevel - other.YLevel);

        return yDifference <= 1;
    }

    public bool IsStillValidConnection()
    {
        if (connectedTo == null)
            return false;

        if (connectionType != connectedTo.connectionType)
            return false;

        int yDifference = Mathf.Abs(YLevel - connectedTo.YLevel);

        return yDifference <= 1;
    }
}
