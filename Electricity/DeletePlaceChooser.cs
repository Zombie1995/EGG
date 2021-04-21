using UnityEngine;

public class DeletePlaceChooser : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GetComponent<DeletePlacing>().enabled)
            {
                ray = new Ray(transform.position, GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f)) - transform.position);
                Physics.Raycast(ray, out hit);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Dot")
                    {
                        GetComponent<DeletePlacing>().enabled = true;
                        GetComponent<DeletePlacing>().basement = hit.collider.gameObject.transform.position;
                    }
                }
            }
            else 
            {
                foreach (GameObject segment in GetComponent<DeletePlacing>().line)
                {
                    Destroy(segment);
                }
                if (GetComponent<DeletePlacing>().line.Length != 0) 
                {
                    Deleting();
                }
                GetComponent<DeletePlacing>().enabled = false;
            }
        }
    }

    void Deleting() 
    {
        DotObjects[] AllDots = FindObjectsOfType<DotObjects>();
        DotObjects[] ChosenDots = new DotObjects[GetComponent<DeletePlacing>().dots.Length];

        //Choose necessery dots
        for (int i1 = 0; i1 < ChosenDots.Length; i1++)
        {
            for (int i2 = 0; i2 < AllDots.Length; i2++)
            {
                if (AllDots[i2].transform.position == GetComponent<DeletePlacing>().dots[i1])
                {
                    ChosenDots[i1] = AllDots[i2];
                    break;
                }
            }
        }

        //Searching one object in two areas and deleting it
        for (int i1 = 0; i1 < (ChosenDots.Length - 1); i1++)
        {
            for (int i2 = 0; i2 < ChosenDots[i1].Objects.Length; i2++)
            {
                for (int i3 = 0; i3 < ChosenDots[i1 + 1].Objects.Length; i3++)
                {
                    if (ChosenDots[i1].Objects[i2] == ChosenDots[i1 + 1].Objects[i3])
                    {
                        Destroy(ChosenDots[i1].Objects[i2]);

                        UpdateList(ref ChosenDots[i1].Objects, i2);
                        UpdateList(ref ChosenDots[i1 + 1].Objects, i3);

                        //Check if dot is empty and deleting it from occupied dots
                        if (ChosenDots[i1].Objects.Length == 0)
                        {
                            for (int i4 = 0; i4 < GetComponent<OccupiedDots>().occupiedDots.Length; i4++)
                            {
                                if (GetComponent<OccupiedDots>().occupiedDots[i4] == ChosenDots[i1].gameObject)
                                {
                                    UpdateList(ref GetComponent<OccupiedDots>().occupiedDots, i4);
                                    break;
                                }
                            }
                        }
                        if (ChosenDots[i1 + 1].Objects.Length == 0)
                        {
                            for (int i4 = 0; i4 < GetComponent<OccupiedDots>().occupiedDots.Length; i4++)
                            {
                                if (GetComponent<OccupiedDots>().occupiedDots[i4] == ChosenDots[i1 + 1].gameObject)
                                {
                                    UpdateList(ref GetComponent<OccupiedDots>().occupiedDots, i4);
                                    break;
                                }
                            }
                        }

                        i2 = ChosenDots[i1].Objects.Length + 2;
                        i3 = ChosenDots[i1 + 1].Objects.Length + 2;
                    }
                }
            }
        }

        //Deleting unnecessery crossing objects
        DeleteCrossings(ChosenDots);

        AllDots = new DotObjects[0];
        ChosenDots = new DotObjects[0];
    }

    void UpdateList(ref GameObject[] List, int numObjDeleted)
    {
        GameObject[] Objects = new GameObject[List.Length - 1];

        int count = 0;
        for (int i1 = 0; i1 < List.Length; i1++)
        {
            if (i1 != numObjDeleted)
            {
                Objects[count] = List[i1];
                count++;
            }
        }
        List = Objects;

        Objects = new GameObject[0];
    }

    void DeleteCrossings(DotObjects[] chosen) 
    {
        for (int i1 = 0; i1 < chosen.Length; i1++) 
        {
            if (chosen[i1].Objects.Length <= 3) 
            {
                for (int i2 = 0; i2 < chosen[i1].Objects.Length; i2++)
                {
                    if (chosen[i1].Objects[i2].tag == "Crossing")
                    {
                        //Deleting crossingDot from the list if there is no crossing
                        for (int i3 = 0; i3 < GetComponent<OccupiedDots>().crossingDots.Length; i3++)
                        {
                            if (GetComponent<OccupiedDots>().crossingDots[i3] == chosen[i1].gameObject)
                            {
                                UpdateList(ref GetComponent<OccupiedDots>().crossingDots, i3);
                                break;
                            }
                        }

                        Destroy(chosen[i1].Objects[i2]);
                        UpdateList(ref chosen[i1].Objects, i2);

                        break;
                    }
                }
            }
        }
    }
}
