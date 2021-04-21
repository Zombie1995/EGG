using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class v_up : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject container;
    public GameObject piston;
    bool down = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        down = false;
    }

    void FixedUpdate()
    {
        if (down)
        {
            container.GetComponent<Parametres>().Volume += 0.066f * Time.deltaTime;

            piston.transform.Translate(0, 0, 0.15962242f * Time.deltaTime);

            if (container.GetComponent<Parametres>().Volume > 1)
            {
                container.GetComponent<Parametres>().Volume = 1;
            }

            if (piston.transform.position.y > container.transform.position.y - 0.6437758f)
            {
                piston.transform.position = new Vector3(container.transform.position.x - 5.019381f, container.transform.position.y - 0.6437758f, container.transform.position.z - 3.04f);
            }
        }
    }

}