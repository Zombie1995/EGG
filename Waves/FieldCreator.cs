using UnityEngine;

public class FieldCreator : MonoBehaviour
{
    public GameObject Field;
    public Material FieldMaterial;

    int field_width = 50;
    int field_height = 50;
    float scale = 1f;

    void Start()
    {
        CreateField();
    }
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            CreateField();
        }
    }*/

    void CreateField() 
    {
        GameObject obj = Instantiate(Field, transform.position + transform.forward * 10, transform.rotation);

        obj.transform.up = transform.forward;

        obj.transform.localScale = new Vector3(scale, scale, scale);

        MeshFilter filter = obj.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        filter.mesh = mesh;

        Vector3[] Vertices = new Vector3[field_width * field_height];
        for(int i1 = 0; i1< field_height; i1++) 
        {
            for (int i2 = 0; i2 < field_width; i2++)
            {
                Vertices[field_width * i1 + i2] = new Vector3(i2 - field_width / 2, 0, i1 - field_height / 2);
            }
        }

        int[] Triangles = new int[(field_width - 1) * (field_height - 1) * 6];
        for (int i1 = 0; i1 < field_height - 1; i1++)
        {
            for (int i2 = 0; i2 < field_width - 1; i2++)
            {
                Triangles[(field_width - 1) * 6 * i1 + i2 * 6] = field_width * i1 + i2;
                Triangles[(field_width - 1) * 6 * i1 + i2 * 6 + 1] = field_width * (i1 + 1) + i2;
                Triangles[(field_width - 1) * 6 * i1 + i2 * 6 + 2] = field_width * (i1 + 1) + i2 + 1;

                Triangles[(field_width - 1) * 6 * i1 + i2 * 6 + 3] = field_width * i1 + i2 + 1;
                Triangles[(field_width - 1) * 6 * i1 + i2 * 6 + 4] = field_width * i1 + i2;
                Triangles[(field_width - 1) * 6 * i1 + i2 * 6 + 5] = field_width * (i1 + 1) + i2 + 1;
            }
        }

        Vector2[] UV = new Vector2[field_width * field_height];
        for (int i1 = 0; i1 < field_height; i1++)
        {
            for (int i2 = 0; i2 < field_width; i2++)
            {
                UV[field_width * i1 + i2] = new Vector2(i2 - field_width / 2, i1 - field_height / 2);
            }
        }

        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.uv = UV;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        obj.GetComponent<MeshCollider>().sharedMesh = mesh;

        obj.GetComponent<MeshRenderer>().material = FieldMaterial;

        obj.GetComponent<Waves>().field_height = field_height;
        obj.GetComponent<Waves>().field_width = field_width;
    }
}

/*
Vector3[] Vertices = new Vector3[4]
        {
            new Vector3(-5, 0, -5),
            new Vector3(-5, 0, 5),
            new Vector3(5, 0, 5),
            new Vector3(5, 0, -5),
        };

int[] Triangles = new int[6] { 0, 1, 2, 0, 2, 3 }; 
 */
/*
                if (field_width * i1 + i2 == 210) 
                {
                    Vertices[field_width * i1 + i2] = new Vector3(i2 - field_width / 2, 20, i1 - field_height / 2);
                }
 */