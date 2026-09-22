using UnityEngine;

public class TrackPiece : MonoBehaviour
{
    [Header("Track Piece")]
    [SerializeField] private string pieceId = "track_piece";
    [SerializeField] private string displayName = "Track Piece";

    [Header("Dimensions")]
    [SerializeField] private Vector3 pieceSize = Vector3.one;

    [Header("Connection Points")]
    [SerializeField] private ConnectionPoint[] connectionPoints;

    public string PieceId => pieceId;

    public string DisplayName => displayName;

    public Vector3 PieceSize => pieceSize;

    public Transform PieceTransform => transform;

    public ConnectionPoint[] ConnectionPoints => connectionPoints;

    private void OnValidate()
    {
        connectionPoints = GetComponentsInChildren<ConnectionPoint>();
    }
}
