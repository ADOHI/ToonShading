using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh;

    public int width;
    public int length;
    public float interval = 1f;
    public float height;
    public float noiseStrength = 1f;
    private void Start()
    {
        //Generate();
    }

    private void Update()
    {
        Generate();
    }

    void Generate()
    {
        mesh = new Mesh();
        var vertices = new List<Vector3>();
        var tris = new List<int>();
        var uvs = new List<Vector2>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                vertices.Add(new Vector3(i * interval, Mathf.PerlinNoise((float)i / width * noiseStrength, (float)j / length * noiseStrength) * height, j * interval));
                uvs.Add(new Vector2((float)i / (width - 1), (float)j / (length - 1)));
            }
        }


        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < length - 1; j++)
            {
                var firstIndex = i * length + j;
                var secondIndex = i * length + j + 1;
                var thirdIndex = (i + 1) * length + j;
                var fourthIndex = (i + 1) * length + j + 1;

                var highestHeight = Mathf.Max(vertices[firstIndex].y, vertices[secondIndex].y, vertices[thirdIndex].y, vertices[fourthIndex].y);
                
                if (highestHeight == vertices[firstIndex].y || highestHeight == vertices[fourthIndex].y)
                {
                    tris.Add(firstIndex);
                    tris.Add(secondIndex);
                    tris.Add(thirdIndex);

                    tris.Add(fourthIndex);
                    tris.Add(thirdIndex);
                    tris.Add(secondIndex);
                }
                else
                {
                    tris.Add(secondIndex);
                    tris.Add(fourthIndex);
                    tris.Add(firstIndex);

                    tris.Add(thirdIndex);
                    tris.Add(firstIndex);
                    tris.Add(fourthIndex);
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
