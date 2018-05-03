using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class CurvedPlane : MonoBehaviour
{

    [Tooltip("Count of triangles in the plane")]
    [SerializeField]
    [Range(2, 100)]
    public int quality = 40;


    [SerializeField]
    public float m_CurveCoeficientX = 0;



    [SerializeField]
    [Tooltip("Scaling textures scale to change planes scale")]
    public float Scale = 1;

    [SerializeField]
    [Tooltip("width of a plane, if SizeByImageInMaterial is OFF")]
    [Range(1.0f, 10.0f)]
    public float width = 6f;

    [SerializeField]
    [Tooltip("height of a plane, if SizeByImageInMaterial is OFF")]
    [Range(1.0f, 10.0f)]
    public float height = 4f;


    #region Private values
    Vector3[] vertices;
    Vector3[] normals;
    float m_Radius;
    Material _material;
    GameObject _target;
    #endregion


    void Update()
    {
        UpdatePlane();
    }

    void OnEnable()
    {
        CreatePlane();
    }

    public void UpdatePlane()
    {
        CreatePlane();
    }

    private Vector3 Center
    {
        get
        {
            return Vector3.zero; ;
        }
    }

    public bool GetCurving()
    {
        return true;
    }

    public Material material
    {
        get
        {
            _material = GetComponent<MeshRenderer>().sharedMaterial;
            return _material;
        }
        set
        {
            _material = value;
        }
    }


    public GameObject Target
    {
        get
        {
            _target = Camera.main.gameObject;
            return _target;
        }
        set
        {
            _target = value;
        }
    }

    private void CreatePlane()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }
        renderer.material = material;
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        if (filter == null)
        {
            filter = gameObject.AddComponent<MeshFilter>();
        }
        Mesh mesh = filter.sharedMesh;
        mesh.Clear();

        int resX = quality;
        int resY = quality;

        m_Radius = GetRadius(Center, Target.transform.position);

        #region Vertices		
        float _height = height;
        float _width = width;

        vertices = new Vector3[resX * resY];
        for (int y = 0; y < resY; y++)
        {
            float yPos = ((float)y / (resY - 1) - .5f) * _height;
            for (int x = 0; x < resX; x++)
            {
                float xPos = ((float)x / (resX - 1) - .5f) * _width;

                vertices[x + y * resX] = CalculateZposition(new Vector3(xPos, yPos, 0));
            }
        }

        #endregion

        #region Normales
        Vector3[] normales = new Vector3[vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = Vector3.back;
        #endregion

        #region UVs		
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int v = 0; v < resY; v++)
        {
            for (int u = 0; u < resX; u++)
            {
                uvs[u + v * resX] = new Vector2((float)u / (resX - 1), (float)v / (resY - 1));
            }
        }
        #endregion

        #region Triangles
        int nbFaces = (resX - 1) * (resY - 1);
        int[] triangles = new int[nbFaces * 6];
        int t = 0;
        for (int face = 0; face < nbFaces; face++)
        {
            int i = face % (resX - 1) + (face / (resY - 1) * resX);

            triangles[t++] = i + resX;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + resX;
            triangles[t++] = i + resX + 1;
            triangles[t++] = i + 1;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        ;
    }

    private Vector3 CalculateZposition(Vector3 PointPosition)
    {
        Vector3 newPos = PointPosition;
        if (m_CurveCoeficientX == 0 && m_CurveCoeficientX == 1f)
        {
            newPos.z = GetZdistance(GetDistFromCenter(newPos));
        }
        else
        {
            float x = GetXdistFromCenter(PointPosition) * m_CurveCoeficientX * transform.localScale.x;
            float y = GetYdistFromCenter(PointPosition) * 0 * transform.localScale.y;
            float h = Mathf.Sqrt(x * x + y * y);
            newPos.z = GetZdistance(h);
        }
        return newPos;
    }

    private float GetZdistance(float distFromCenter)
    {
        return -(m_Radius - Mathf.Sqrt(m_Radius * m_Radius - distFromCenter * distFromCenter));
    }


    private float GetXdistFromCenter(Vector3 pos)
    {
        Vector3 center = Center;
        center.z = 0;
        center.y = 0;
        pos.y = 0;
        pos.z = 0;
        return Vector3.Distance(center, pos);
    }

    private float GetYdistFromCenter(Vector3 pos)
    {
        Vector3 center = Center;
        center.z = 0;
        center.x = 0;
        pos.x = 0;
        pos.z = 0;
        return Vector3.Distance(center, pos);
    }

    private float GetDistFromCenter(Vector3 pos)
    {
        pos.z = 0;
        Vector3 center = Center;
        center.z = 0;
        return Vector3.Distance(center, pos);
    }

    private float GetRadius(Vector3 center, Vector3 camera)
    {
        return Vector3.Distance(center, camera);
    }

}
