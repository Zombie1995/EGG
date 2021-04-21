using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject Monkey;
    public GameObject Boat;
    public GameObject Duck;

    GameObject catchedObj;
    bool catched = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (!catched)
            {
                Take();
            }
            else 
            {
                Release();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (objsNum < maxObjs)
            {
                if (!catched)
                {
                    Create();

                    objsNum++;
                }
            }
        }

        if (catched) 
        {
            catchedObj.transform.position = Vector3.Lerp(catchedObj.transform.position, transform.forward * 10f + transform.position, 5f * Time.deltaTime);

            catchedObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Take() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (hit.collider != null) 
        {
            if (!hit.transform.CompareTag("Field")) 
            {
                catchedObj = hit.collider.gameObject;

                catchedObj.GetComponent<Rigidbody>().isKinematic = true;

                catchedObj.GetComponent<Catchy>().Free();

                catched = true;
            }
        } 
    }

    void Release() 
    {
        catchedObj.GetComponent<Rigidbody>().isKinematic = false;

        catchedObj = null;

        catched = false;
    }

    int objsNum = 0;
    int maxObjs = 10;
    void Create()
    {
        int objNum = Random.Range(1, 4);

        switch (objNum) 
        {
            case 1:
                catchedObj = Instantiate(Monkey, transform.position + transform.forward * 10, transform.rotation);
                break;
            case 2:
                catchedObj = Instantiate(Boat, transform.position + transform.forward * 10, transform.rotation);
                break;
            case 3:
                catchedObj = Instantiate(Duck, transform.position + transform.forward * 10, transform.rotation);
                break;
        }

        catchedObj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        catchedObj.GetComponent<Rigidbody>().isKinematic = true;

        catchedObj.GetComponent<Catchy>().Free();

        catched = true;
    }
}
