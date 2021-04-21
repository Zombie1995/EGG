using UnityEngine;

public class Catchy : MonoBehaviour
{
    //Just for rigidbodies
    Vector3 velocity;
    void Update()
    {
        if (GetComponent<Rigidbody>().useGravity)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > velocity.magnitude)
            {
                velocity = GetComponent<Rigidbody>().velocity;
            }
        }
    }

    Collision field;
    public void Free(float delay = 0f)
    {
        if (field != null)
        {
            for (int i = 0; i < field.gameObject.GetComponent<Influence>().objs.Length; i++)
            {
                if (field.gameObject.GetComponent<Influence>().objs[i] == gameObject)
                {
                    GameObject[] prObjs = field.gameObject.GetComponent<Influence>().objs;
                    field.gameObject.GetComponent<Influence>().objs = new GameObject[prObjs.Length - 1];
                    int count = 0;
                    for (int i1 = 0; i1 < prObjs.Length; i1++)
                    {
                        if (i1 == i)
                        {
                            continue;
                        }

                        field.gameObject.GetComponent<Influence>().objs[count] = prObjs[i1];
                        count++;
                    }

                    int[] prVerts = field.gameObject.GetComponent<Influence>().verts;
                    field.gameObject.GetComponent<Influence>().verts = new int[prVerts.Length - 1];
                    count = 0;
                    for (int i1 = 0; i1 < prVerts.Length; i1++)
                    {
                        if (i1 == i)
                        {
                            continue;
                        }

                        field.gameObject.GetComponent<Influence>().verts[count] = prVerts[i1];
                        count++;
                    }

                    break;
                }
            }

            StartCoroutine(EnableCollision(delay));
        }

        //Just for rigidbodies
        velocity = new Vector3(0, 0, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            field = collision;

            Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), field.gameObject.GetComponent<MeshCollider>(), true);

            float minDist = Vector3.Distance(field.contacts[0].point, transform.position);

            int contNum = 0;
            for (int i = 0; i < field.contacts.Length; i++)
            {
                if (Vector3.Distance(field.contacts[i].point, transform.position) < minDist)
                {
                    minDist = Vector3.Distance(field.contacts[i].point, transform.position);
                    contNum = i;
                }
            }

            Vector3[] vertices = field.gameObject.GetComponent<MeshFilter>().mesh.vertices;
            
            minDist = Vector3.Distance(vertices[0], field.gameObject.transform.InverseTransformPoint(field.contacts[contNum].point));

            int vertNum = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i], field.gameObject.transform.InverseTransformPoint(field.contacts[contNum].point)) < minDist)
                {
                    minDist = Vector3.Distance(vertices[i], field.gameObject.transform.InverseTransformPoint(field.contacts[contNum].point));
                    vertNum = i;
                }
            }

            float height = (Vector3.Angle(velocity, field.transform.up) > 90) ? ((transform.position - field.transform.position) - vertices[vertNum] * field.transform.localScale.x).magnitude : -((transform.position - field.transform.position) - vertices[vertNum] * field.transform.localScale.x).magnitude;

            field.gameObject.GetComponent<MeshFilter>().mesh.vertices[vertNum] = new Vector3(vertices[vertNum].x, height, vertices[vertNum].z);

            field.gameObject.GetComponent<Waves>().VelocityField[vertNum] += height > 0 ? -velocity.magnitude : velocity.magnitude;

            GameObject[] prObjs = field.gameObject.GetComponent<Influence>().objs;
            field.gameObject.GetComponent<Influence>().objs = new GameObject[prObjs.Length + 1];
            for (int i = 0; i < prObjs.Length; i++)
            {
                field.gameObject.GetComponent<Influence>().objs[i] = prObjs[i];
            }
            field.gameObject.GetComponent<Influence>().objs[prObjs.Length] = gameObject;

            int[] prVerts = field.gameObject.GetComponent<Influence>().verts;
            field.gameObject.GetComponent<Influence>().verts = new int[prVerts.Length + 1];
            for (int i = 0; i < prVerts.Length; i++)
            {
                field.gameObject.GetComponent<Influence>().verts[i] = prVerts[i];
            }
            field.gameObject.GetComponent<Influence>().verts[prObjs.Length] = vertNum;

            velocity = new Vector3(0, 0, 0);

            //Just for rigidbodies
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    System.Collections.IEnumerator EnableCollision(float delay) 
    {
        yield return new WaitForSeconds(delay);

        Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), field.gameObject.GetComponent<MeshCollider>(), false);

        field = null;
    }
}

/*GetComponent<MeshCollider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;

                */
/*
GameObject field;
    bool timerOn = false;
    float timer = 0;
    float timerMax = 1f;

        if (timerOn)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), field.GetComponent<MeshCollider>(), false);
                field = null; ;

                timer = 0;

                timerOn = false;
            }
        }*/
/*
            Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), collision.gameObject.GetComponent<MeshCollider>(), true);
        */
/*if (Vector3.Angle(velocity, field.transform.up) > 90)
            {
                float minDist = Vector3.Distance(field.contacts[0].point, transform.position);

                int contNum = 0;
                for (int i = 0; i < field.contacts.Length; i++)
                {
                    if (Vector3.Distance(field.contacts[i].point, transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(field.contacts[i].point, transform.position);
                        contNum = i;
                    }
                }

                Vector3[] vertices = field.gameObject.GetComponent<MeshFilter>().mesh.vertices;

                minDist = Vector3.Distance(vertices[0] * field.transform.localScale.x, field.contacts[contNum].point - field.transform.position);

                int vertNum = 0;
                for (int i = 0; i < vertices.Length; i++)
                {
                    if (Vector3.Distance(vertices[i] * field.transform.localScale.x, field.contacts[contNum].point - field.transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(vertices[i] * field.transform.localScale.x, field.contacts[contNum].point - field.transform.position);
                        vertNum = i;
                    }
                }

                float height = ((transform.position - field.transform.position) - vertices[vertNum] * field.transform.localScale.x).magnitude;

                field.gameObject.GetComponent<MeshFilter>().mesh.vertices[vertNum] = new Vector3(vertices[vertNum].x, height, vertices[vertNum].z);

                field.gameObject.GetComponent<Waves>().VelocityField[vertNum] -= velocity.magnitude;

                GameObject[] prObjs = field.gameObject.GetComponent<Influence>().objs;
                field.gameObject.GetComponent<Influence>().objs = new GameObject[prObjs.Length + 1];
                for (int i = 0; i < prObjs.Length; i++)
                {
                    field.gameObject.GetComponent<Influence>().objs[i] = prObjs[i];
                }
                field.gameObject.GetComponent<Influence>().objs[prObjs.Length] = gameObject;

                int[] prVerts = field.gameObject.GetComponent<Influence>().verts;
                field.gameObject.GetComponent<Influence>().verts = new int[prVerts.Length + 1];
                for (int i = 0; i < prVerts.Length; i++)
                {
                    field.gameObject.GetComponent<Influence>().verts[i] = prVerts[i];
                }
                field.gameObject.GetComponent<Influence>().verts[prObjs.Length] = vertNum;
            }
            else
            {
                StartCoroutine(EnableCollision(2));
            }*/
/*field.contacts[contNum].point - field.transform.position*/