using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        GetComponent<Creator>().enabled = true;
        GetComponent<Creator>().panel.SetActive(false);
        GetComponent<Creator>().Selected = null;
        GetComponent<Creator>().pos_tr_vel_fls = true;
        GetComponent<Creator>().TrajActive(false);

        GetComponent<ObserverGravity>().tail_enable(false);
        GetComponent<ObserverGravity>().enabled = false;
        GetComponent<RunGravity>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") || Input.GetMouseButtonDown(1))
        {
            Cursor.visible = !Cursor.visible;
   
            GetComponent<Creator>().enabled = !GetComponent<Creator>().enabled;
            GetComponent<Creator>().panel.SetActive(false);
            GetComponent<Creator>().Selected = null;
            GetComponent<Creator>().pos_tr_vel_fls = true;
            GetComponent<Creator>().TrajActive(false);

            GetComponent<ObserverGravity>().tail_enable(false);
            GetComponent<ObserverGravity>().enabled = !GetComponent<ObserverGravity>().enabled;
            GetComponent<RunGravity>().enabled = false;
            GetComponent<RunGravity>().objs = FindObjectsOfType<Gravity>();
        }
    }
}
