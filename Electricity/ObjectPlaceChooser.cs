using UnityEngine;

public class ObjectPlaceChooser : MonoBehaviour
{
    public Material Dark;
    public Material Gray;
    public Material Yellow;

    public GameObject Crossing;

    RaycastHit hit;
    Ray ray;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GetComponent<ObjectPlacing>().enabled)
            {
                ray = new Ray(transform.position, GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f)) - transform.position);
                Physics.Raycast(ray, out hit);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Dot")
                    {
                        GetComponent<ObjectPlacing>().enabled = true;
                        GetComponent<CheckOverlay>().enabled = true;
                        GetComponent<ObjectPlacing>().basement = hit.collider.gameObject.transform.position;
                    }
                }
            }
            else
            {
                if (!GetComponent<CheckOverlay>().overlayed)
                {
                    if (GetComponent<ObjectPlacing>().objects[0].tag == "Sphere")
                    {
                        Destroy(GetComponent<ObjectPlacing>().objects[0]);
                    }
                    else
                    {
                        InputObjsInDots();
                    }
                    GetComponent<ObjectPlacing>().objects = new GameObject[0];
                    GetComponent<CheckOverlay>().enabled = false;
                    GetComponent<ObjectPlacing>().enabled = false;
                }
            }
        }
    }

    void InputObjsInDots()
    {
        GameObject[] prevObjects;
        DotObjects[] AllDots = FindObjectsOfType<DotObjects>();
        DotObjects[] ChosenDots = new DotObjects[GetComponent<ObjectPlacing>().dots.Length];

        //Choose necessery dots
        for (int i1 = 0; i1 < ChosenDots.Length; i1++)
        {
            for (int i2 = 0; i2 < AllDots.Length; i2++)
            {
                if (AllDots[i2].transform.position == GetComponent<ObjectPlacing>().dots[i1])
                {
                    ChosenDots[i1] = AllDots[i2];
                    break;
                }
            }
        }
        //Adding them to occupied dots or not
        for (int i1 = 0; i1 < ChosenDots.Length; i1++)
        {
            bool presence = false;
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().occupiedDots.Length; i2++)
            {
                if (ChosenDots[i1].gameObject == GetComponent<OccupiedDots>().occupiedDots[i2])
                {
                    presence = true;
                    break;
                }
            }
            if (!presence)
            {
                prevObjects = GetComponent<OccupiedDots>().occupiedDots;
                GetComponent<OccupiedDots>().occupiedDots = new GameObject[prevObjects.Length + 1];

                for (int i2 = 0; i2 < prevObjects.Length; i2++)
                {
                    GetComponent<OccupiedDots>().occupiedDots[i2] = prevObjects[i2];
                }
                GetComponent<OccupiedDots>().occupiedDots[prevObjects.Length] = ChosenDots[i1].gameObject;
            }
        }

        //Adding new created lines in dots
        ///
        prevObjects = ChosenDots[0].Objects;
        ChosenDots[0].Objects = new GameObject[prevObjects.Length + 1];
        for (int i2 = 0; i2 < prevObjects.Length; i2++)
        {
            ChosenDots[0].Objects[i2] = prevObjects[i2];
        }
        ChosenDots[0].Objects[prevObjects.Length] = GetComponent<ObjectPlacing>().objects[0];
        //Color
        Coloring(ChosenDots[0].Objects[prevObjects.Length]);
        ///
        for (int i1 = 1; i1 < (ChosenDots.Length - 1); i1++)
        {
            prevObjects = ChosenDots[i1].Objects;
            ChosenDots[i1].Objects = new GameObject[prevObjects.Length + 2];

            for (int i2 = 0; i2 < prevObjects.Length; i2++)
            {
                ChosenDots[i1].Objects[i2] = prevObjects[i2];
            }
            ChosenDots[i1].Objects[prevObjects.Length] = GetComponent<ObjectPlacing>().objects[i1 - 1];
            ChosenDots[i1].Objects[prevObjects.Length + 1] = GetComponent<ObjectPlacing>().objects[i1];
            //Color
            Coloring(ChosenDots[i1].Objects[prevObjects.Length]);
            Coloring(ChosenDots[i1].Objects[prevObjects.Length + 1]);
        }
        ///
        prevObjects = ChosenDots[(ChosenDots.Length - 1)].Objects;
        ChosenDots[(ChosenDots.Length - 1)].Objects = new GameObject[prevObjects.Length + 1];
        for (int i2 = 0; i2 < prevObjects.Length; i2++)
        {
            ChosenDots[(ChosenDots.Length - 1)].Objects[i2] = prevObjects[i2];
        }
        ChosenDots[(ChosenDots.Length - 1)].Objects[prevObjects.Length] = GetComponent<ObjectPlacing>().objects[(ChosenDots.Length - 1) - 1];
        //Color
        Coloring(ChosenDots[(ChosenDots.Length - 1)].Objects[prevObjects.Length]);
        ///

        //Creating crossing objects for signing crossings and later know how the electricity needs to spread 
        CreateCrossings(ChosenDots);

        prevObjects = new GameObject[0];
        AllDots = new DotObjects[0];
        ChosenDots = new DotObjects[0];
    }

    void CreateCrossings(DotObjects[] chosen)
    {
        for (int i1 = 0; i1 < chosen.Length; i1++)
        {
            if (chosen[i1].Objects.Length > 2)
            {
                bool presence = false;
                for (int i2 = 0; i2 < chosen[i1].Objects.Length; i2++)
                {
                    if (chosen[i1].Objects[i2].tag == "Crossing")
                    {
                        presence = true;
                        break;
                    }
                }
                if (!presence)
                {
                    GameObject[] prObjects = chosen[i1].Objects;
                    chosen[i1].Objects = new GameObject[prObjects.Length + 1];

                    for (int i2 = 0; i2 < prObjects.Length; i2++)
                    {
                        chosen[i1].Objects[i2] = prObjects[i2];
                    }
                    chosen[i1].Objects[prObjects.Length] = Instantiate(Crossing, chosen[i1].transform.position, Quaternion.identity);


                    //Adding crossingDot in the list
                    prObjects = GetComponent<OccupiedDots>().crossingDots;
                    GetComponent<OccupiedDots>().crossingDots = new GameObject[prObjects.Length + 1];
                    for (int i2 = 0; i2 < prObjects.Length; i2++)
                    {
                        GetComponent<OccupiedDots>().crossingDots[i2] = prObjects[i2];
                    }
                    GetComponent<OccupiedDots>().crossingDots[prObjects.Length] = chosen[i1].gameObject;

                }
            }
        }
    }

    void Coloring(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Accumulator":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Gray;
                break;
            case "Wire":
                obj.GetComponent<MeshRenderer>().material = Dark;
                break;
            case "Capacitor":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Gray;
                break;
            case "Inductor":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Dark;
                break;
            case "Resistor":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Yellow;
                break;
            case "Ammeter":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Gray;
                break;
            case "Voltmeter":
                obj.transform.Find("Color").GetComponent<MeshRenderer>().material = Gray;
                break;
        }
    }
}
