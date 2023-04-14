using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private bool hasSolidGround;
    [SerializeField] private GameObject cellPrefab;

    [Space]
    [Header("Size")]
    [Min(5)]
    [SerializeField] private int xSize = 20;
    [Min(5)]
    [SerializeField] private int zSize = 20;
    [Range(0, 10)]
    [SerializeField] private int height = 4;
    [Range(-10, 10)]
    [SerializeField] private int entropy = 10;

    [Space]
    [Header("Color")]
    [SerializeField] private Gradient gradient;
    [SerializeField] private List<Material> colors;

    public void Generate()
    {
        CreateMaterialList();

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                float noiseX = x / (float)xSize * entropy;
                float noiseZ = z / (float)zSize * entropy;
                int noiseY = (int) Mathf.Round(Mathf.PerlinNoise(noiseX, noiseZ) * height);

                if (hasSolidGround)
                {
                    for (int i = 0; i < noiseY; i++)
                        CreateCell(x, i, z);
                }
                else
                {
                    CreateCell(x, noiseY, z);
                }
            }
        }
    }

    private void CreateMaterialList()
    {
        colors.Clear();
        for (int i = 0; i <= height; i++)
        {
            Material material = new Material(Shader.Find("Standard"))
            {
                color = gradient.Evaluate(i / (float) height)
            };
            colors.Add(material);
        }
    }

    private void CreateCell(int x, int y, int z)
    {
        Vector3 position = new Vector3(x, y, z);
        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.transform.parent = transform;
        if (colors[y]) cell.GetComponent<MeshRenderer>().sharedMaterial = colors[y];
    }

    public void Reset()
    {
         for(int i = transform.childCount - 1; i >= 0; i--)
         {
             DestroyImmediate(transform.GetChild(i).gameObject);
         }
    }

    public void Focus()
    {
        Selection.activeTransform = transform;
        SceneView.lastActiveSceneView.FrameSelected();
    }
}
