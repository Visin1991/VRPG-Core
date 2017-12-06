using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour {

    public int verticesPerRow = 255;

    [ContextMenu("Generate Plane Mesh")]
    public void GenerateAPlane()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = Wei1_1.PlaneMeshGenerator.PlaneMash2(verticesPerRow);
        meshFilter.sharedMesh = mesh;
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
}
