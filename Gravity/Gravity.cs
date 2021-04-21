using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  
    public float mass;
    public Vector3 curVel;

    public void Influence(Gravity[] Objs, float timeStep, float gravitConst)
    {
        foreach (Gravity otherObj in Objs){
            if (otherObj != this){
                float dist_sqrd = (transform.position - otherObj.transform.position).sqrMagnitude;
                Vector3 dir_of_acc = (transform.position - otherObj.transform.position).normalized;
                Vector3 acceleration_to_obj = dir_of_acc * (gravitConst * mass / dist_sqrd);
                otherObj.curVel += acceleration_to_obj * timeStep;
            }
        }
    }

    public void Move(float timeStep) 
    {
        transform.position += (curVel / 2) * timeStep;
    }
}
