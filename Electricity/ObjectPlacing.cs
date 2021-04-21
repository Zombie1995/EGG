using System;
using UnityEngine;

public class ObjectPlacing : MonoBehaviour
{
    public Material Color;

    public GameObject Accumulator;
    public GameObject Wire;
    public GameObject Capacitor;
    public GameObject Inductor;
    public GameObject Resistor;
    public GameObject Ammeter;
    public GameObject Voltmeter;
    public GameObject Sphere;

    public Vector3 basement;
    public GameObject[] objects;
    public Vector3[] dots;

    public GameObject Selected;

    void Start()
    {
        Selected = Wire;
    }

    void Update()
    {
        //Delete previous
        DeletePrev();
        //Dot and distance
        GetDot();
        dist = dot - basement;
        //Calculate and create new line or object
        if (Selected != Wire)
        {
            CalcDirection();
            CreateObject();
        }
        else 
        {
            CalcLine();
            CreateLine();
        }
    }

    void DeletePrev()
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
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

    Vector3 Direction;
    void CalcDirection()
    {
        if (Math.Abs(dist.z) > 2 * Math.Abs(dist.x))
        {
            Direction = new Vector3(0, 0, ((dist.z > 0.0f) ? 1 : -1));
            objects = new GameObject[1];
            dots = new Vector3[2];
        }
        else if (Math.Abs(dist.x) > 2 * Math.Abs(dist.z))
        {
            Direction = new Vector3(((dist.x > 0.0f) ? 1 : -1), 0, 0);
            objects = new GameObject[1];
            dots = new Vector3[2];
        }
        else
        {
            Direction = new Vector3(((dist.x > 0.0f) ? 0.5f : -0.5f), 0, ((dist.z > 0.0f) ? 0.5f : -0.5f));
            objects = new GameObject[2];
            dots = new Vector3[3];
        }
    }

    void CreateObject()
    {
        dots[0] = basement;
        GameObject obj;

        if (dist != new Vector3(0, 0, 0))
        {
            //Creating object
            obj = Instantiate(Selected, dots[0], Quaternion.identity);
            //Rotate
            obj.transform.forward = Direction;
            //Move
            obj.transform.position = Direction / 2 + dots[0];
            //Scale objWire
            obj.transform.Find("objWire").localScale = new Vector3(obj.transform.Find("objWire").localScale.x, Direction.magnitude / 2 / obj.transform.localScale.y, obj.transform.Find("objWire").localScale.z);
            obj.transform.Find("objWire").transform.Find("End1").localScale = new Vector3(obj.transform.Find("objWire").transform.Find("End1").localScale.x, Direction.magnitude / 4, obj.transform.Find("objWire").transform.Find("End1").localScale.z);
            obj.transform.Find("objWire").transform.Find("End2").localScale = new Vector3(obj.transform.Find("objWire").transform.Find("End2").localScale.x, Direction.magnitude / 4, obj.transform.Find("objWire").transform.Find("End2").localScale.z);
            //Rotate objWire
            obj.transform.Find("objWire").transform.up = Direction;
            //Color
            obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Color;
            //In the list
            objects[0] = obj;
            dots[1] = dots[0] + Direction;

            if (objects.Length > 1) 
            {
                //Creating wire
                obj = Instantiate(Wire, dots[1], Quaternion.identity);
                //Rotate
                obj.transform.up = Direction;
                //Move
                obj.transform.position = Direction / 2 + dots[1];
                //Scale
                obj.transform.localScale = new Vector3(Wire.transform.localScale.x, Direction.magnitude / 2, Wire.transform.localScale.z);
                obj.transform.Find("End1").localScale = new Vector3(Wire.transform.Find("End1").localScale.x, Direction.magnitude / 2, Wire.transform.Find("End1").localScale.z);
                obj.transform.Find("End2").localScale = new Vector3(Wire.transform.Find("End2").localScale.x, Direction.magnitude / 2, Wire.transform.Find("End2").localScale.z);
                //Color
                obj.GetComponent<MeshRenderer>().material = Color;
                //In the list
                objects[1] = obj;
                dots[2] = dots[1] + Direction;
            }
        }
        else 
        {
            obj = Instantiate(Sphere, dots[0], Quaternion.identity);
            objects[0] = obj;
        }
    }

    int Diags_num = 0;
    Vector3 Diag;
    int Straights_num = 0;
    Vector3 Straight;
    void CalcLine()
    {
        Diags_num = 0;
        Straights_num = 0;

        if (Math.Abs(dist.x) < Math.Abs(dist.z))
        {
            Diags_num = (int)Math.Abs(dist.x);
            Straights_num = (int)Math.Abs(dist.z) - Diags_num;
            Straight = new Vector3(0, 0, ((dist.z > 0.0f) ? 1 : -1));
        }
        else
        {
            Diags_num = (int)Math.Abs(dist.z);
            Straights_num = (int)Math.Abs(dist.x) - Diags_num;
            Straight = new Vector3(((dist.x > 0.0f) ? 1 : -1), 0, 0);
        }

        Diags_num *= 2;
        Diag = new Vector3(((dist.x > 0.0f) ? 0.5f : -0.5f), 0, ((dist.z > 0.0f) ? 0.5f : -0.5f));

        if (Diags_num + Straights_num != 0)
        {
            objects = new GameObject[Diags_num + Straights_num];
            dots = new Vector3[Diags_num + Straights_num + 1];
        }
        else
        {
            objects = new GameObject[1];
            dots = new Vector3[1];
        }
    }

    void CreateLine()
    {
        dots[0] = basement;
        GameObject segment;

        int count = 0;

        if ((Straights_num + Diags_num) == 0)
        {
            segment = Instantiate(Sphere, dots[count], Quaternion.identity);
            objects[count] = segment;
        }
        else
        {
            //Diagonal lines
            //Scale
            Wire.transform.localScale = new Vector3(Wire.transform.localScale.x, Diag.magnitude / 2, Wire.transform.localScale.z);
            Wire.transform.Find("End1").localScale = new Vector3(Wire.transform.Find("End1").localScale.x, Diag.magnitude / 2, Wire.transform.Find("End1").localScale.z);
            Wire.transform.Find("End2").localScale = new Vector3(Wire.transform.Find("End2").localScale.x, Diag.magnitude / 2, Wire.transform.Find("End2").localScale.z);
            
            for (count = count; count < Diags_num; count++)
            {
                segment = Instantiate(Wire, dots[count], Quaternion.identity);
                //Move
                segment.transform.position = Diag / 2 + dots[count];
                //Rotate
                segment.transform.up = Diag;
                //Color
                segment.GetComponent<MeshRenderer>().material = Color;
                //In the list
                objects[count] = segment;
                dots[count + 1] = dots[count] + Diag;
            }

            //Staright lines
            //Scale
            Wire.transform.localScale = new Vector3(Wire.transform.localScale.x, Straight.magnitude / 2, Wire.transform.localScale.z);
            Wire.transform.Find("End1").localScale = new Vector3(Wire.transform.Find("End1").localScale.x, Straight.magnitude / 4, Wire.transform.Find("End1").localScale.z);
            Wire.transform.Find("End2").localScale = new Vector3(Wire.transform.Find("End2").localScale.x, Straight.magnitude / 4, Wire.transform.Find("End2").localScale.z);

            for (count = count; count < (Straights_num + Diags_num); count++)
            {
                segment = Instantiate(Wire, dots[count], Quaternion.identity);
                //Move
                segment.transform.position = Straight / 2 + dots[count];
                //Rotate
                segment.transform.up = Straight;
                //Color
                segment.GetComponent<MeshRenderer>().material = Color;
                //In the list
                objects[count] = segment;
                dots[count + 1] = dots[count] + Straight;
            }
        }
    }
}

/*
    int Diags_num = 0;
    Vector3 Diag;
    int Straights_num = 0;
    Vector3 Straight;
    void CalcLine()
    {
        Diags_num = 0;
        Straights_num = 0;

        if (Math.Abs(dist.x) < Math.Abs(dist.z))
        {
            Diags_num = (int)Math.Abs(dist.x);
            Straights_num = (int)Math.Abs(dist.z) - Diags_num;
            Straight = new Vector3(0, 0, ((dist.z > 0.0f) ? 1 : -1));
        }
        else
        {
            Diags_num = (int)Math.Abs(dist.z);
            Straights_num = (int)Math.Abs(dist.x) - Diags_num;
            Straight = new Vector3(((dist.x > 0.0f) ? 1 : -1), 0, 0);
        }

        Diags_num *= 2;
        Diag = new Vector3(((dist.x > 0.0f) ? 0.5f : -0.5f), 0, ((dist.z > 0.0f) ? 0.5f : -0.5f));

        if (Diags_num + Straights_num != 0)
        {
            line = new GameObject[Diags_num + Straights_num];
            dots = new Vector3[Diags_num + Straights_num + 1];
        }
        else
        {
            line = new GameObject[1];
            dots = new Vector3[1];
        }
    }

    void CreateLine()
    {
        dots[0] = basement;
        GameObject segment;

        int count = 0;

        if ((Straights_num + Diags_num) == 0)
        {
            Segment.transform.Find("Wire").localScale = new Vector3(Segment.transform.Find("Wire").localScale.x, 0, Segment.transform.Find("Wire").localScale.z);
            Segment.transform.Find("Wire").transform.Find("End").localScale = new Vector3(Segment.transform.Find("Wire").transform.Find("End").localScale.x, 0, Segment.transform.Find("Wire").transform.Find("End").localScale.z);
            segment = Instantiate(Segment, dots[count], Quaternion.identity);
            line[count] = segment;
        }
        else
        {
            //Diagonal lines
            //Scale
            Segment.transform.Find("Wire").localScale = new Vector3(Segment.transform.Find("Wire").localScale.x, Diag.magnitude, Segment.transform.Find("Wire").localScale.z);
            Segment.transform.Find("Wire").transform.Find("End").localScale = new Vector3(Segment.transform.Find("Wire").transform.Find("End").localScale.x, Diag.magnitude, Segment.transform.Find("Wire").transform.Find("End").localScale.z);
            //Rotate
            Segment.transform.Find("Wire").up = Diag;
            //Color
            Segment.transform.Find("Wire").GetComponent<MeshRenderer>().material = Color;

            for (count = count; count < Diags_num; count++)
            {
                segment = Instantiate(Segment, dots[count], Quaternion.identity);
                //Move
                segment.transform.Find("Wire").position = Diag / 2 + dots[count];
                //In the list
                line[count] = segment;
                dots[count + 1] = dots[count] + Diag;
            }

            //Staright lines
            //Scale
            Segment.transform.Find("Wire").localScale = new Vector3(Segment.transform.Find("Wire").localScale.x, Straight.magnitude, Segment.transform.Find("Wire").localScale.z);
            Segment.transform.Find("Wire").transform.Find("End").localScale = new Vector3(Segment.transform.Find("Wire").transform.Find("End").localScale.x, Straight.magnitude / 2, Segment.transform.Find("Wire").transform.Find("End").localScale.z);
            //Rotate
            Segment.transform.Find("Wire").up = Straight;
            //Color
            Segment.transform.Find("Wire").GetComponent<MeshRenderer>().material = Color;

            for (count = count; count < (Straights_num + Diags_num); count++)
            {
                segment = Instantiate(Segment, dots[count], Quaternion.identity);
                //Move
                segment.transform.Find("Wire").position = Straight / 2 + dots[count];
                //In the list
                line[count] = segment;
                dots[count + 1] = dots[count] + Straight;
            }
        }
    }*/
