using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParamChanger : MonoBehaviour
{
    public GameObject Panel;
    int panelWidth = 100;
    int panelHeight = 70;
    public InputField Value;

    public GameObject Selected;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CheckUI())
            {
                ray = new Ray(transform.position, GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f)) - transform.position);
                Physics.Raycast(ray, out hit);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Accumulator" || hit.collider.gameObject.tag == "Resistor")
                    {
                        Selected = hit.collider.gameObject;

                        FillPanel();
                    }
                    else
                    {
                        Panel.SetActive(false);
                        Selected = null;
                    }
                }
                else
                {
                    Panel.SetActive(false);
                    Selected = null;
                }
            }
        }

        if (Selected) 
        {
            ValueAssign();
        }
    }

    void FillPanel() 
    {
        Panel.SetActive(true);

        switch (Selected.tag) 
        {
            case "Accumulator":
                Panel.transform.Find("Name").GetComponent<Text>().text = "Accumulator";
                Panel.transform.Find("Text").GetComponent<Text>().text = "Voltage:";
                Value.text = Selected.GetComponent<ObjParametres>().Voltage.ToString();
                break;
            case "Resistor":
                Panel.transform.Find("Name").GetComponent<Text>().text = "Resistor";
                Panel.transform.Find("Text").GetComponent<Text>().text = "Resistance:";
                Value.text = Selected.GetComponent<ObjParametres>().Resistance.ToString();
                break;
        }
    }

    void ValueAssign() 
    {
        switch (Selected.tag)
        {
            case "Accumulator":
                float.TryParse(Value.text, out Selected.GetComponent<ObjParametres>().Voltage);
                //Selected.GetComponent<ObjParametres>().Voltage = float.Parse(Value.text);
                break;
            case "Resistor":
                float.TryParse(Value.text, out Selected.GetComponent<ObjParametres>().Resistance);
                //Selected.GetComponent<ObjParametres>().Resistance = float.Parse(Value.text);
                break;
        }
    }

    bool CheckUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> resultData = new List<RaycastResult>();
        pointerData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerData, resultData);

        if (resultData.Count > 0)
        {
            return false;
        }

        return true;
    }
}
