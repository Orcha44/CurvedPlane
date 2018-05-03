using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class CurveTest : MonoBehaviour
{
    private class MeshData
    {
        public Vector3[] Vertices { get; set; }
        public int[] Triangles { get; set; }
    }

    [SerializeField] private float height = 4f;
    [SerializeField] private float radius = 6f;

    [SerializeField] [Range(1, 1024)] private int numSegments = 200;

    [SerializeField] [Range(0f, 360f)] private float curvatureDegrees = 60f;

    private MeshData plane;

    void Update()
    {
        Generate();
    }

    private void Generate()
    {
        GenerateScreen();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        var filter = GetComponent<MeshFilter>();

        var mesh = new Mesh();
        mesh.vertices = plane.Vertices;
        mesh.triangles = plane.Triangles;

        Vector3[] normales = new Vector3[mesh.vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = Vector3.back;

        mesh.normals = normales;

        Vector3[] vertices = mesh.vertices;

        Vector2[] uvs = new Vector2[mesh.vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        mesh.uv = uvs;
        filter.sharedMesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(-0.2f, 0.2f);
    }

    private void GenerateScreen()
    {
        plane = new MeshData
        {
            Vertices = new Vector3[(numSegments + 2) * 2],
            Triangles = new int[numSegments * 6]
        };

        int i, j;
        for (i = j = 0; i < numSegments + 1; i++)
        {
            GenerateVertexPair(ref i, ref j);

            if (i < numSegments)
            {
                GenerateLeftTriangle(ref i, ref j);
                GenerateRightTriangle(ref i, ref j);
            }
        }
    }

    private void GenerateVertexPair(ref int i, ref int j)
    {
        float amt = ((float)i) / numSegments;
        float arcDegrees = curvatureDegrees * Mathf.Deg2Rad;
        float theta = -0.5f + amt;

        var x = Mathf.Sin(theta * arcDegrees) * radius;
        var z = Mathf.Cos(theta * arcDegrees) * radius;

        plane.Vertices[i] = new Vector3(x, height / 2f, z);
        plane.Vertices[i + numSegments + 1] = new Vector3(x, -height / 2f, z);
    }

    private void GenerateLeftTriangle(ref int i, ref int j)
    {
        plane.Triangles[j++] = i;
        plane.Triangles[j++] = i + 1;
        plane.Triangles[j++] = i + numSegments + 1;
    }

    private void GenerateRightTriangle(ref int i, ref int j)
    {
        plane.Triangles[j++] = i + 1;
        plane.Triangles[j++] = i + numSegments + 2;
        plane.Triangles[j++] = i + numSegments + 1;
    }


}