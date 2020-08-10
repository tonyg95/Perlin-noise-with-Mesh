using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class meshGen : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 50;
    public int zSize = 50;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        createShape();
        updateMesh();
    }
    private void Update()
    {
        
    }

    void createShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
 
        for (int i = 0, z = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; x++)
            {
                float y = 0;
                if (z < 6)
                {
                    y = 0;
                }
                else
                {
                    y = getNoiseSample(x, z);
                }

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[6*xSize*zSize];

        int tris = 0;
        for (int j = 0; j < (xSize*zSize)+(xSize-1); j++) {
            if (j % (xSize+1) == xSize)
                continue;
            triangles[tris] = j;
            triangles[tris + 1] = xSize + j + 1;
            triangles[tris + 2] = j + 1;
            triangles[tris + 3] = xSize + j + 1;
            triangles[tris + 4] = xSize + j + 2;
            triangles[tris + 5] = j + 1;
            tris += 6;
        }
    }
    void updateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
	
    private void OnDrawGizmos() {
        if (vertices == null)
            return;
        for (int z = 0; z < vertices.Length; z++)
        {
            Gizmos.DrawSphere(vertices[z], 0.1f);
        }
    }
	
    private float getNoiseSample(int x, int z) {
        return Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 2;
    }
}
