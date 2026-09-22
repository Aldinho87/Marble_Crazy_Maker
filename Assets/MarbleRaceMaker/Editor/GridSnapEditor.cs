using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridSnap))]
public class GridSnapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridSnap gridSnap = (GridSnap)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Snap to Grid"))
        {
            gridSnap.Snap();

            EditorUtility.SetDirty(gridSnap);
        }

        if (GUILayout.Button("Snap to Connection Point"))
        {
            if (gridSnap.SnapToNearestConnectionPoint())
            {
                EditorUtility.SetDirty(gridSnap);
            }
        }
    }
}
