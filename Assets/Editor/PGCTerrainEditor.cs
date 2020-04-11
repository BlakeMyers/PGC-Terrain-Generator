using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]
public class PGCTerrainEditor : Editor
{
    bool showPer = false;
    bool showMPO = false;
    int mapsize;
    public override void OnInspectorGUI() {

        PGCTerrain PGCTerrain = (PGCTerrain)target;

        EditorGUILayout.LabelField("Map Options");

        mapsize = EditorGUILayout.IntField("Map Width (2^n)", mapsize);

        if (Mathf.Floor(Mathf.Log(mapsize, 2)) == Mathf.Ceil(Mathf.Log(mapsize, 2)))
        {
            PGCTerrain.mapHeight = mapsize;
            PGCTerrain.mapWidth = mapsize;
        }
        else {
            mapsize = 512;
            PGCTerrain.mapHeight = mapsize;
            PGCTerrain.mapWidth = mapsize;
        }

        PGCTerrain.mapDepth = EditorGUILayout.IntField("Map Depth", PGCTerrain.mapDepth);

        if (GUILayout.Button("Random Terrain"))
        {
            PGCTerrain.RandomTerrain();
            Debug.Log("Random terrain");
        }

        showPer = EditorGUILayout.Foldout(showPer, "Perlin Noise Options");

        if (showPer)
        {
            PGCTerrain.scale = EditorGUILayout.Slider("Scale", PGCTerrain.scale, 0.001f, 500);
            PGCTerrain.Offsets = EditorGUILayout.Vector2Field("Offsets", PGCTerrain.Offsets);

            if (GUILayout.Button("Single Perlin"))
            {
                Debug.Log("Single Perlin");
                PGCTerrain.SinglePerlin();
            }

            showMPO = EditorGUILayout.Foldout(showMPO, "Multiple Perlin Options");

            if (showMPO)
            {
                PGCTerrain.seed = EditorGUILayout.IntField("Map Seed", PGCTerrain.seed);
                PGCTerrain.numOctaves = EditorGUILayout.IntSlider("Octaves", PGCTerrain.numOctaves, 0, 12);
                PGCTerrain.persistance = EditorGUILayout.Slider("Persistance", PGCTerrain.persistance, 0.0f, 1);
                PGCTerrain.lacunarity = EditorGUILayout.Slider("Lacunarity", PGCTerrain.lacunarity, 0.0f, 1);
                if (GUILayout.Button("Multiple Perlin"))
                {
                    PGCTerrain.MultiplePerlin();
                }
            }
        }
    }
}
