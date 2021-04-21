using UnityEngine;

public class InstrumentSwitcher : MonoBehaviour
{
    /*void Start()
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        
    }*/

    void Update()
    {

        if (Input.GetKeyDown("e") && !GetComponent<DeletePlacing>().enabled)
        {
            GetComponent<DeletePlaceChooser>().enabled = false;
            GetComponent<ObjectPlaceChooser>().enabled = true;
        }
        if (Input.GetKeyDown("q") && !GetComponent<ObjectPlacing>().enabled)
        {
            setDel();
        }
        if (Input.GetKeyDown("1") && !GetComponent<DeletePlacing>().enabled) 
        {
            setWire();
        }
        if (Input.GetKeyDown("2") && !GetComponent<DeletePlacing>().enabled)
        {
            setAcc();
        }
        if (Input.GetKeyDown("3") && !GetComponent<DeletePlacing>().enabled)
        {
            setRes();
        }
        if (Input.GetKeyDown("4") && !GetComponent<DeletePlacing>().enabled)
        {
            setAmm();
        }
        if (Input.GetKeyDown("5") && !GetComponent<DeletePlacing>().enabled)
        {
            setVolt();
        }
    }

    public void setWire()
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        GetComponent<ObjectPlacing>().Selected = GetComponent<ObjectPlacing>().Wire;
    }
    public void setAcc() 
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        GetComponent<ObjectPlacing>().Selected = GetComponent<ObjectPlacing>().Accumulator;
    }
    public void setRes()
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        GetComponent<ObjectPlacing>().Selected = GetComponent<ObjectPlacing>().Resistor;
    }
    public void setAmm()
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        GetComponent<ObjectPlacing>().Selected = GetComponent<ObjectPlacing>().Ammeter;
    }
    public void setVolt()
    {
        GetComponent<DeletePlaceChooser>().enabled = false;
        GetComponent<ObjectPlaceChooser>().enabled = true;
        GetComponent<ObjectPlacing>().Selected = GetComponent<ObjectPlacing>().Voltmeter;
    }
    public void setDel()
    {
        GetComponent<DeletePlaceChooser>().enabled = true;
        GetComponent<ObjectPlaceChooser>().enabled = false;
    }
}
