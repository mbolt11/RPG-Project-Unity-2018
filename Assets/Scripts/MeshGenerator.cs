using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public Vector3 vertLeftTopFront = new Vector3(-1, 1, 1);
    public Vector3 vertRightTopFront = new Vector3(1, 1, 1);
    public Vector3 vertRightTopBack = new Vector3(1, 1, -1);
    public Vector3 vertLeftTopBack = new Vector3(-1, 1, -1);

    private void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh; 

    //Verctices
    Vector3[] vertices = new Vector3[]
        {
            //front face
            //lefttopfront 0
            vertLeftTopFront,
            //righttopfront 1
            vertRightTopFront,
            //leftbottomfront 2
            new Vector3(-1, -1, 1),
            //rightbottomfront 3
            new Vector3(1, -1, 1),

            //back face
            //righttopback 4
            vertRightTopBack,
            //lefttopback 5
            vertLeftTopBack,
            //rightbottomback 6
            new Vector3(1, -1, -1),
            //leftbottomback 7
            new Vector3(-1, -1, -1),

            //left face
            //lefttopback 8
            vertLeftTopBack,
            //lefttopfront 9
            vertLeftTopFront,
            //leftbottomback 10
            new Vector3(-1, -1, -1),
            //leftbottomfront 11
            new Vector3(-1, -1, 1),

            //right face
            //righttopfront 12
            vertRightTopFront,
            //righttopback 13
            vertRightTopBack,
            //rightbottomfront 14
            new Vector3(1, -1, 1),
            //rightbottomback 15
            new Vector3(1, -1, -1),

            //top face
            //lefttopback 16
            vertLeftTopBack,
            //righttopback 17
            vertRightTopBack,
            //lefttopfront 18
            vertLeftTopFront,
            //righttopfront 19
            vertRightTopFront,

            //bottom face
            //leftbottomfront 20
            new Vector3(-1,-1,1),
            //rightbottomfront 21
            new Vector3(1,-1,1),
            //leftbottomback 22
            new Vector3(-1, -1, -1),
            //rightbottomback 23
            new Vector3(1, -1, -1)
        };

        //triangles clockwise
        int[] triangles = new int[]
        {
            //front face
            //first triangle
            0,2,3,
            //second triangle
            3,1,0,

            //back face
            //first triangle
            4,6,7,
            //second triangle
            7,5,4,

            //left face
            //first triangle
            8,10,11,
            //second triangle
            11,9,8,

            //right face
            //first triangle
            12,14,15,
            //second triangle
            15,13,12,

            //top face
            //first triangle
            16,18,19,
            //second triangle
            19,17,16,

            //bottom face
            //first triangle
            20,22,23,
            //second triangle
            23,21,20
        };

        Vector2[] uvs = new Vector2[]
        {
            //front face 0,0 is bottom left, 1,1 is top right
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)
        };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
