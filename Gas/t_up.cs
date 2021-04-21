using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class t_up : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject container;
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
            container.GetComponent<Parametres>().Temperature += 2 * Time.deltaTime;

            if (container.GetComponent<Parametres>().Temperature > 9999)
            {
                container.GetComponent<Parametres>().Temperature = 9999;
            }
        }
    }

}