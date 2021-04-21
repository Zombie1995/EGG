using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSwitcher : MonoBehaviour
{
    bool instr_param = false;
    bool electricity = false;
    bool resetValues = false;

    public GameObject InstrPanel;

    public GameObject field;

    void Update()
    {
        if (!GetComponent<ObjectPlacing>().enabled && !GetComponent<DeletePlacing>().enabled) 
        {
            if (Input.GetKeyDown("x") && !GetComponent<ObserverElectricity>().enabled && !instr_param)
            {
                if (GetComponent<InstrumentSwitcher>().enabled)
                {
                    up_down = -1;
                }
                else
                {
                    up_down = 1;
                }

                instr_param = true;
            }
            if (Input.GetKeyDown("r") && !GetComponent<ParamChanger>().enabled && !GetComponent<ObserverElectricity>().enabled && !electricity)
            {
                if (GetComponent<OccupiedDots>().occupiedDots.Length != 0)
                {
                    GetComponent<ElectricityCalc>().CalcCircuit();

                    InstrPanel.SetActive(false);

                    up_down = -1;

                    electricity = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && GetComponent<ObserverElectricity>().enabled && !resetValues)
            {
                up_down = 1;

                resetValues = true;
            }
        }

        if (up_down != 0)
        {
            FieldMove();
        }
        else 
        {
            if (instr_param) 
            {
                GetComponent<InstrumentSwitcher>().enabled = !GetComponent<InstrumentSwitcher>().enabled;
                if (GetComponent<InstrumentSwitcher>().enabled)
                {
                    GetComponent<ObjectPlaceChooser>().enabled = true;
                    GetComponent<DeletePlaceChooser>().enabled = false;

                    InstrPanel.SetActive(true);
                }
                else
                {
                    GetComponent<ObjectPlaceChooser>().enabled = false;
                    GetComponent<DeletePlaceChooser>().enabled = false;

                    InstrPanel.SetActive(false);
                }

                GetComponent<ParamChanger>().enabled = !GetComponent<ParamChanger>().enabled;
                if (!GetComponent<ParamChanger>().enabled)
                {
                    GetComponent<ParamChanger>().Selected = null;
                    GetComponent<ParamChanger>().Panel.SetActive(false);
                }

                instr_param = false;
            }
            if (electricity) 
            {
                GetComponent<InstrumentSwitcher>().enabled = false;
                GetComponent<ObjectPlaceChooser>().enabled = false;
                GetComponent<DeletePlaceChooser>().enabled = false;

                GetComponent<ObserverElectricity>().enabled = true;
                Cursor.visible = false;

                electricity = false;
            }
            if (resetValues) 
            {
                GetComponent<InstrumentSwitcher>().enabled = true;
                GetComponent<ObjectPlaceChooser>().enabled = true;
                GetComponent<DeletePlaceChooser>().enabled = false;

                GetComponent<ObserverElectricity>().enabled = false;
                Cursor.visible = true;

                GetComponent<ElectricityCalc>().ResetVars();

                InstrPanel.SetActive(true);

                resetValues = false;
            }
        }
    }

    int up_down = 0;
    void FieldMove() 
    {
        Vector3 up = new Vector3(0, 0, 0);
        Vector3 down = new Vector3(0, -2, 0);

        if (up_down == 1)
        {
            if (field.transform.position.y > -0.1f)
            {
                field.transform.position = up;
                up_down = 0;
            }
            else
            {
                field.transform.position = Vector3.Lerp(field.transform.position, up, 5 * Time.deltaTime);
            }
        }
        if (up_down == -1)
        {
            if (field.transform.position.y < -1.9f)
            {
                field.transform.position = down;
                up_down = 0;
            }
            else
            {
                field.transform.position = Vector3.Lerp(field.transform.position, down, 5 * Time.deltaTime);
            }
        }
    }
}

/*StartCoroutine(GoToCreating());
StartCoroutine(GoToEectricity());*/
/*IEnumerator GoToEectricity() 
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneAt(1));
        SceneManager.MoveGameObjectToScene(field, SceneManager.GetSceneAt(1));
        yield return null;
        SceneManager.UnloadSceneAsync(0);
    }
    IEnumerator GoToCreating()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneAt(0));
        SceneManager.MoveGameObjectToScene(field, SceneManager.GetSceneAt(0));
        yield return null;
        SceneManager.UnloadSceneAsync(1);
    }*/
/*if (Input.GetKeyDown("x") && !GetComponent<ObjectPlacing>().enabled && !GetComponent<DeletePlacing>().enabled)
        {
            GetComponent<InstrumentSwitcher>().enabled = !GetComponent<InstrumentSwitcher>().enabled;
            if (GetComponent<InstrumentSwitcher>().enabled)
            {
                GetComponent<ObjectPlaceChooser>().enabled = true;
                GetComponent<DeletePlaceChooser>().enabled = false;
            }
            else 
            {
                GetComponent<ObjectPlaceChooser>().enabled = false;
                GetComponent<DeletePlaceChooser>().enabled = false;
            }
            
            GetComponent<ParamChanger>().enabled = !GetComponent<ParamChanger>().enabled;
            if (!GetComponent<ParamChanger>().enabled) 
            {
                GetComponent<ParamChanger>().Selected = null;
                GetComponent<ParamChanger>().Panel.SetActive(false);
            }

            moving = true;
        }
        if (Input.GetKeyDown("r") && !GetComponent<ObjectPlacing>().enabled && !GetComponent<DeletePlacing>().enabled && !GetComponent<ParamChanger>().enabled)
        {
            GetComponent<InstrumentSwitcher>().enabled = false;
            GetComponent<ObjectPlaceChooser>().enabled = false;
            GetComponent<DeletePlaceChooser>().enabled = false;

            GetComponent<Observer>().enabled = true;
            Cursor.visible = false;

            moving = true;

            GetComponent<ElectricityCalc>().CalcCircuit();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GetComponent<Observer>().enabled)
        {
            GetComponent<InstrumentSwitcher>().enabled = true;
            GetComponent<ObjectPlaceChooser>().enabled = true;
            GetComponent<DeletePlaceChooser>().enabled = false;

            GetComponent<Observer>().enabled = false;
            Cursor.visible = true;

            moving = true;

            resetValues = true;
        }

        if (moving)
        {
            FieldMove();

            if (!moving && resetValues)
            {
                GetComponent<ElectricityCalc>().ResetVars();

                resetValues = false;
            }
        }*/