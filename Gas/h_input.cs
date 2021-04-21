using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class h_input : MonoBehaviour, IPointerClickHandler
{
    public GameObject container;
    public Transform switcher;

    Vector3 left;
    Vector3 right;

    public void OnPointerClick(PointerEventData eventData)
    {
        container.GetComponent<Parametres>().heat_input = !(container.GetComponent<Parametres>().heat_input);

        left = new Vector3(container.transform.position.x - 3.751116f, container.transform.position.y + 0.2659972f, container.transform.position.z - 5.327703f);
        right = new Vector3(container.transform.position.x - 3.699929f, container.transform.position.y + 0.2678772f, container.transform.position.z - 5.382488f);
    
    }

    void Start() 
    {
        left = new Vector3(container.transform.position.x - 3.751116f, container.transform.position.y + 0.2659972f, container.transform.position.z - 5.327703f);
        right = new Vector3(container.transform.position.x - 3.699929f, container.transform.position.y + 0.2678772f, container.transform.position.z - 5.382488f);
    }

    void FixedUpdate() 
    {
        if (container.GetComponent<Parametres>().heat_input)
        {
            switcher.transform.position = Vector3.Lerp(switcher.transform.position, left, 5 * Time.deltaTime);
        }
        else
        {
            switcher.transform.position = Vector3.Lerp(switcher.transform.position, right, 5 * Time.deltaTime);
        }
    }
}
