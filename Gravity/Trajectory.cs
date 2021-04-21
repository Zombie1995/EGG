using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public float pass_time = 1.0f;

    LineRenderer[] lines;
    Vector3[,] points;
    int pnum = 1000;
    pseudo_Gravity[] objs;

    void FixedUpdate()
    {
        GetObjs();

        for (int i = 0; i < pnum; i++)
        {
            CalcPoint(i, 100, GetComponent<RunGravity>().grav_const, GetComponent<RunGravity>().t_s);
        }

        for (int i = 0; i < objs.Length; i++)
        {
            GetArr(i);
            lines[i].positionCount = pnum;
            lines[i].SetPositions(array);
        }
    }

    void GetObjs()
    {
        Gravity[] origins = FindObjectsOfType<Gravity>();
        objs = new pseudo_Gravity[origins.Length];
        for (int i = 0; i < origins.Length; i++)
        {
            objs[i] = new pseudo_Gravity();
            objs[i].mass = origins[i].mass;
            objs[i].curVel = origins[i].curVel;
            objs[i].position = origins[i].transform.position;
        }

        points = new Vector3[origins.Length, pnum];
        lines = FindObjectsOfType<LineRenderer>();
    }
        
    Vector3[] array;
    void GetArr(int obj_num) 
    {
        array = new Vector3[pnum];
        for (int i = 0; i < pnum; i++)
        {
            array[i] = points[obj_num, i];
        }
    }

    void CalcPoint(int point_num, int num_passes, float grav_const, float t_s) 
    {
        for (int i = 0; i < num_passes; i++)
        {
            for (int i2 = 0; i2 < objs.Length; i2++)
            {
                objs[i2].Influence(objs, t_s, grav_const);
            }
            for (int i2 = 0; i2 < objs.Length; i2++)
            {
                objs[i2].Move(t_s);
            }
        }
        for (int i = 0; i < objs.Length; i++)
        {
            points[i, point_num] = objs[i].position;
        }
    }
}

class pseudo_Gravity
{
    public float mass;
    public Vector3 curVel;
    public Vector3 position;

    public void Influence(pseudo_Gravity[] Objs, float timeStep, float gravitConst)
    {
        foreach (pseudo_Gravity otherObj in Objs)
        {
            if (otherObj != this)
            {
                float dist_sqrd = (position - otherObj.position).sqrMagnitude;
                Vector3 dir_of_acc = (position - otherObj.position).normalized;
                Vector3 acceleration_to_obj = dir_of_acc * (gravitConst * mass / dist_sqrd);
                otherObj.curVel += acceleration_to_obj * timeStep;
            }
        }
    }

    public void Move(float timeStep)
    {
        position += (curVel / 2) * timeStep;
    }
}