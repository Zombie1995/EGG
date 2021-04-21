using UnityEngine;

public class Move : MonoBehaviour
{
    Camera cam;

    int prMousePositionX = 0;
    int prMousePositionY = 0;

    bool firstTouch = true;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                firstTouch = true;
                return;
            }
            Fluid fluid = hit.collider.gameObject.GetComponent<Fluid>();
            if (!fluid)
            {
                firstTouch = true;
                return;
            }

            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= fluid.w;
            pixelUV.y *= fluid.h;

            if (!firstTouch)
            {
                fluid.AddDensity(prMousePositionX, prMousePositionY, Random.Range(1, 50), Random.Range(1, 50), Random.Range(1, 50));
                fluid.AddVelocity(prMousePositionX, prMousePositionY, 1.5f * ((int)pixelUV.x - prMousePositionX), 1.5f * ((int)pixelUV.y - prMousePositionY));
            }
            
            prMousePositionX = (int)pixelUV.x;
            prMousePositionY = (int)pixelUV.y;

            firstTouch = false;
        }
        else 
        {
            firstTouch = true;
        }
    }
}
