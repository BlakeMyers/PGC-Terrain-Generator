using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]
public class PGCTerrainEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        PGCTerrain PGCTerrain = (PGCTerrain)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Random Terrain"))
        {
            PGCTerrain.ResizeTerrain();
            Debug.Log("Random terrain");
        }
        if (GUILayout.Button("Single Perlin"))
        {
            Debug.Log("Single Perlin");
            PGCTerrain.SinglePerlin();
        }
        GUILayout.EndHorizontal();
    }
}
