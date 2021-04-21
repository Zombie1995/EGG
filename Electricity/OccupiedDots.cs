using UnityEngine;

public class OccupiedDots : MonoBehaviour
{
    //Are necessery for optimization(to check dots occupied when creating lines)
    public GameObject[] occupiedDots;

    //Dots with crossings
    public GameObject[] crossingDots;
}
