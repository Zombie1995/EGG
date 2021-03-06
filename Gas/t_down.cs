using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class t_down : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
            container.GetComponent<Parametres>().Temperature -= 2 * Time.deltaTime;

            if (container.GetComponent<Parametres>().Temperature < 0) 
            {
                container.GetComponent<Parametres>().Temperature = 0;
            }
        }

        if (container.GetComponent<Parametres>().Moles == 0)
        {
            container.GetComponent<Parametres>().Temperature -= 8 * Time.deltaTime;

            if (container.GetComponent<Parametres>().Temperature < 0)
            {
                container.GetComponent<Parametres>().Temperature = 0;
            }
        }
    }

}
