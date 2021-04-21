using UnityEngine;

public class CheckOverlay : MonoBehaviour
{
    public Material Red;
    public Material White;

    public bool overlayed = false;
    GameObject[] ChosenDots;

    void Update() 
    {
        ChooseDots();
        FindOverlay();
        if (overlayed)
        {
            GetComponent<ObjectPlacing>().Color = Red;
        }
        else 
        {
            GetComponent<ObjectPlacing>().Color = White;
        }
    }

    void ChooseDots() 
    {
        ChosenDots = new GameObject[0];
        int count = 0;

        //Count the number of common territory
        for (int i1 = 0; i1 < GetComponent<ObjectPlacing>().dots.Length; i1++) 
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().occupiedDots.Length; i2++)
            {
                if (GetComponent<ObjectPlacing>().dots[i1] == GetComponent<OccupiedDots>().occupiedDots[i2].transform.position)
                {
                    count++;
                }
            }
        }

        ChosenDots = new GameObject[count];
        count = 0;

        //Find them again and add
        for (int i1 = 0; i1 < GetComponent<ObjectPlacing>().dots.Length; i1++)
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().occupiedDots.Length; i2++)
            {
                if (GetComponent<ObjectPlacing>().dots[i1] == GetComponent<OccupiedDots>().occupiedDots[i2].transform.position)
                {
                    ChosenDots[count] = GetComponent<OccupiedDots>().occupiedDots[i2];
                    count++;
                }
            }
        }
    }

    void FindOverlay() 
    {
        overlayed = false;

        if (GetComponent<ObjectPlacing>().Selected.tag == "Wire")
        {
            for (int i1 = 0; i1 < GetComponent<ObjectPlacing>().objects.Length; i1++)
            {
                for (int i2 = 0; i2 < ChosenDots.Length; i2++)
                {
                    for (int i3 = 0; i3 < ChosenDots[i2].GetComponent<DotObjects>().Objects.Length; i3++)
                    {
                        if (ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].tag == "Wire")
                        {
                            if (GetComponent<ObjectPlacing>().objects[i1].transform.position == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.position)
                            {
                                if (GetComponent<ObjectPlacing>().objects[i1].transform.up == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.up || GetComponent<ObjectPlacing>().objects[i1].transform.up == -ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.up)
                                {
                                    overlayed = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (GetComponent<ObjectPlacing>().objects[i1].transform.position == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.position)
                            {
                                if (GetComponent<ObjectPlacing>().objects[i1].transform.up == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.forward || GetComponent<ObjectPlacing>().objects[i1].transform.up == -ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.forward)
                                {
                                    overlayed = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (overlayed)
                    {
                        break;
                    }
                }
                if (overlayed)
                {
                    break;
                }
            }
        }
        else 
        {
            for (int i2 = 0; i2 < ChosenDots.Length; i2++)
            {
                for (int i3 = 0; i3 < ChosenDots[i2].GetComponent<DotObjects>().Objects.Length; i3++)
                {
                    if (ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].tag == "Wire")
                    {
                        if (GetComponent<ObjectPlacing>().objects[0].transform.position == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.position)
                        {
                            if (GetComponent<ObjectPlacing>().objects[0].transform.forward == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.up || GetComponent<ObjectPlacing>().objects[0].transform.forward == -ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.up)
                            {
                                overlayed = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (GetComponent<ObjectPlacing>().objects[0].transform.position == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.position)
                        {
                            if (GetComponent<ObjectPlacing>().objects[0].transform.forward == ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.forward || GetComponent<ObjectPlacing>().objects[0].transform.forward == -ChosenDots[i2].GetComponent<DotObjects>().Objects[i3].transform.forward)
                            {
                                overlayed = true;
                                break;
                            }
                        }
                    }
                }
                if (overlayed)
                {
                    break;
                }
            }
        }
    }
}
