using UnityEngine;

public class Influence : MonoBehaviour
{
    public GameObject[] objs;
    public int[] verts;

    int size;

    void Start()
    {
        size = (int)Mathf.Sqrt(GetComponent<MeshFilter>().mesh.vertices.Length);
    }

    void Update()
    {
        if (objs.Length > 0) 
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].transform.position = (transform.up * GetComponent<MeshFilter>().mesh.vertices[verts[i]].y + transform.forward * GetComponent<MeshFilter>().mesh.vertices[verts[i]].z + transform.right * GetComponent<MeshFilter>().mesh.vertices[verts[i]].x) * transform.localScale.x + transform.position;

                Vector3 a = GetComponent<MeshFilter>().mesh.vertices[verts[i] + size] - GetComponent<MeshFilter>().mesh.vertices[verts[i]];
                Vector3 b = GetComponent<MeshFilter>().mesh.vertices[verts[i] + 1] - GetComponent<MeshFilter>().mesh.vertices[verts[i]];

                objs[i].transform.up = Vector3.Cross(a, b);

                objs[i].transform.eulerAngles = new Vector3(objs[i].transform.eulerAngles.x + transform.eulerAngles.x, objs[i].transform.eulerAngles.y + transform.eulerAngles.y, objs[i].transform.eulerAngles.z + transform.eulerAngles.z);
            }
        }
    }
}
