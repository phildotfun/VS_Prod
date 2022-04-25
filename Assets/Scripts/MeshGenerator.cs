using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    public int Worldx;
    public int Worldz;

    [Header("Wave Controller")]
    public float waveOffset;
    public float amp;
    public float freq;

    private Mesh mesh;

    private int[] triangles;
    private Vector3[] verticies;

    enum waveStyle
    {
        Cos,
        Sin,
        PerlinNoise
    }
    

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;



    
    }

    private void Update()
    {
        GenerateMesh();
        UpdateMesh();
    }


    //create the mesh
    void GenerateMesh()
    {

        float zPos = Mathf.Cos(Time.time * 30) * 30;

        triangles = new int[Worldx * Worldz * 6];
        verticies = new Vector3[(Worldx + 1) * (Worldz + 1)];

        //set the world position of the verts
        for (int i = 0, z = 0, y = 0; z <= Worldz; z++)
        {
            for(int x = 0; x <= Worldx; x++)
            {
                verticies[i] = new Vector3(x, Mathf.Sin((Time.time - (waveOffset * y)) * amp) * freq, z);
                y++;
                i++;
            }
        }


        //create the tris that that verts will be assigned to
        int tris = 0;
        int verts = 0;

        for (int z = 0; z < Worldz; z++)
        {
            for(int x = 0; x < Worldx; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + Worldz + 1;
                triangles[tris + 2] = verts + 1;

                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + Worldz + 1;
                triangles[tris + 5] = verts + Worldz + 2;

                verts++;
                tris += 6;
                
            }
        verts++;
        }
    }


    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
