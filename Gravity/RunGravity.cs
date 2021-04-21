using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGravity : MonoBehaviour
{
    public Gravity[] objs;
    public float t_s = 0.001f;
    public float grav_const = 1.0f;

    void Start() 
    {
        Time.fixedDeltaTime = t_s;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].Influence(objs, t_s, grav_const);
        }

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].Move(t_s);
        }
    }
}
