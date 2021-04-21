using System;
using UnityEngine;

public class DeletePlacing : MonoBehaviour
{
    public GameObject RedLine;
    public Vector3 basement;
    public GameObject[] line;
    public Vector3[] dots;

    void Start()
    {
    }

    void Update()
    {
        //Delete previous
        DeletePrev();
        //Dot and distance
        GetDot();
        dist = dot - basement;
        //Calculate diagonals, horizontal and vertical segments and create new line
        CalcLine();
        CreateLine();
    }

    void DeletePrev()
    {
        foreach (GameObject segment in line)
        {
            Destroy(segment);
        }
    }

    RaycastHit hit;
    Ray ray;
    Vector3 dot;
    Vector3 dist;
    void GetDot()
    {
        ray = new Ray(transform.position, GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f)) - transform.position);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Dot")
            {
                dot = hit.collider.gameObject.transform.position;
            }
            else
            {
                //dot = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            }
        }
        else
        {
            //dot = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        }
    }

    int Lines_num = 0;
    Vector3 Line;
    void CalcLine()
    {
        Lines_num = 0;

        if (Math.Abs(dist.z) > 2 * Math.Abs(dist.x))
        {
            Lines_num = (int)Math.Abs(dist.z);
            Line = new Vector3(0, 0, ((dist.z > 0.0f) ? 1 : -1));
        }
        else if (Math.Abs(dist.x) > 2 * Math.Abs(dist.z))
        {
            Lines_num = (int)Math.Abs(dist.x);
            Line = new Vector3(((dist.x > 0.0f) ? 1 : -1), 0, 0);
        }
        else
        {
            Lines_num = (((int)Math.Abs(dist.x) < (int)Math.Abs(dist.z)) ? (int)Math.Abs(dist.x) * 2 : (int)Math.Abs(dist.z) * 2);
            Line = new Vector3(((dist.x > 0.0f) ? 0.5f : -0.5f), 0, ((dist.z > 0.0f) ? 0.5f : -0.5f));
        }

        line = new GameObject[Lines_num];
        dots = new Vector3[Lines_num + 1];
        
    }

    void CreateLine()
    {
        dots[0] = basement;
        GameObject segment;

        for (int count = 0; count < Lines_num; count++)
        {
            segment = Instantiate(RedLine, dots[count], Quaternion.identity);
            //Scale
            segment.transform.localScale = new Vector3(segment.transform.localScale.x, Line.magnitude / 2, segment.transform.localScale.z);
            //Rotate
            segment.transform.up = Line;
            //Move
            segment.transform.position = Line / 2 + dots[count];
            //In the list
            line[count] = segment;
            dots[count + 1] = dots[count] + Line;
        }
    }
}