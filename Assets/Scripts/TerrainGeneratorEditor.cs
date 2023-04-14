using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TerrainGenerator myTarget = (TerrainGenerator) target;

        if (GUILayout.Button("Reset"))
        {
            myTarget.Reset();
        }

        if (GUILayout.Button("Generate"))
        {
            myTarget.Reset();
            myTarget.Generate();
            myTarget.Focus();
        }
    }
}
