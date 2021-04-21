using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parametres : MonoBehaviour
{
    public GameObject volume;
    public GameObject moles;
    public GameObject temperature;
    public GameObject pressure;

    public bool heat_input = true;
    public float MolarMass = 0.004f;
    public float tempInput = 273;
    public float C = 5200.0f;

    float R = 8.31f;
    public float Volume = 1.0f;
    public float Moles = 0.0f;
    public float Temperature = 0.0f;
    float Pressure = 0.0f;
    
    void FixedUpdate()
    {
        volume.GetComponent<Text>().text = Volume.ToString();
        moles.GetComponent<Text>().text = ((int)Moles).ToString();
        temperature.GetComponent<Text>().text = ((int)Temperature).ToString();

        Pressure = (R * Moles * Temperature) / Volume;
        pressure.GetComponent<Text>().text = ((int)Pressure).ToString();
    }
};
