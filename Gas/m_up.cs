using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class m_up : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
            container.GetComponent<Parametres>().Moles += 2 * Time.deltaTime;

            if (!container.GetComponent<Parametres>().heat_input)
            {
                if (container.GetComponent<Parametres>().Moles < 999)
                {
                    //T = (m*T + m2(T2 - T))/m
                    container.GetComponent<Parametres>().Temperature =
                        ((container.GetComponent<Parametres>().Moles * container.GetComponent<Parametres>().MolarMass) * container.GetComponent<Parametres>().Temperature + (2 * Time.deltaTime * container.GetComponent<Parametres>().MolarMass) * (container.GetComponent<Parametres>().tempInput - container.GetComponent<Parametres>().Temperature)) / (container.GetComponent<Parametres>().Moles * container.GetComponent<Parametres>().MolarMass);
                }
            }

            if (container.GetComponent<Parametres>().Moles > 999)
            {
                container.GetComponent<Parametres>().Moles = 999;
            }
        }
    }

}