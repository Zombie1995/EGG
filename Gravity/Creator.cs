using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    public InputField Mass;

    public GameObject planet;
    public GameObject star;
    GameObject Object;

    public GameObject panel;

    public LineRenderer[] lines;

    public bool pos_tr_vel_fls = true;
    float delta_move = 1.0f;
    float delta_vel = 1.0f;

    public GameObject Selected;
    Vector3 cur_pos;
    int obj_num;
    
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        Object = star;
        obj_num = 0;
        pos_tr_vel_fls = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Create();
        }

        if (Input.GetKeyDown("q"))
        {
            Delete();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (CheckUI())
            {
                Chooser();
            }
        }

        if (Input.GetKeyDown("x")) 
        {
            pos_tr_vel_fls = !pos_tr_vel_fls;
        }

        if (Input.GetKeyDown("z"))
        {
            TrajActive(!en_or_dis);
        }

        ObjChanger();

        if (Selected)
        {
            ValueAssign();
        }

        Move_or_SetVel();

        Scaler();
    }

    void ObjChanger()
    {
        if (Input.GetKeyDown("1"))
        {
            Object = planet;
        }
        if (Input.GetKeyDown("2"))
        {
            Object = star;
        }
    }

    void FillPanel()
    {
        panel.transform.Find("Name").GetComponent<Text>().text = Selected.name;
        panel.transform.Find("X").GetComponent<Text>().text = Selected.transform.position.x.ToString();
        panel.transform.Find("Y").GetComponent<Text>().text = Selected.transform.position.y.ToString();
        panel.transform.Find("Z").GetComponent<Text>().text = Selected.transform.position.z.ToString();
        panel.transform.Find("VX").GetComponent<Text>().text = Selected.GetComponent<Gravity>().curVel.x.ToString();
        panel.transform.Find("VY").GetComponent<Text>().text = Selected.GetComponent<Gravity>().curVel.y.ToString();
        panel.transform.Find("VZ").GetComponent<Text>().text = Selected.GetComponent<Gravity>().curVel.z.ToString();
        Mass.text = Selected.GetComponent<Gravity>().mass.ToString();
    }

    void ValueAssign()
    {
        float.TryParse(Mass.text, out Selected.GetComponent<Gravity>().mass);
    }

    void Chooser()
    {
        ray = new Ray(transform.position, GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10000f)) - transform.position);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            Selected = hit.collider.gameObject;

            panel.SetActive(true);
            FillPanel();
        }
        else
        {
            Selected = null;
            panel.SetActive(false);
            pos_tr_vel_fls = true;
        }
    }

    void Create() 
    {
        cur_pos = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth / 2, GetComponent<Camera>().pixelHeight / 2, 10f));
        obj_num++;

        Instantiate(Object, cur_pos, Quaternion.identity).name = "Object " + obj_num;
        GetComponent<RunGravity>().objs = FindObjectsOfType<Gravity>();
        TrajActive(en_or_dis);
    }

    void Delete() 
    {
        if (Selected)
        {
            Destroy(Selected);
            Selected = null;
            panel.SetActive(false);
            pos_tr_vel_fls = true;
            GetComponent<RunGravity>().objs = FindObjectsOfType<Gravity>();
            TrajActive(en_or_dis);
        }
    }

    void Move_or_SetVel() 
    {
        if (Selected)
        {
            if (Input.GetKey("w"))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(0, 0, delta_move);
                    FillPanel();
                }
                else 
                {
                    Selected.GetComponent<Gravity>().curVel.z += delta_vel;
                    FillPanel();
                }
            }
            if (Input.GetKey("a"))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(-delta_move, 0, 0);
                    FillPanel();
                }
                else
                {
                    Selected.GetComponent<Gravity>().curVel.x -= delta_vel;
                    FillPanel();
                }
            }
            if (Input.GetKey("s"))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(0, 0, -delta_move);
                    FillPanel();
                }
                else
                {
                    Selected.GetComponent<Gravity>().curVel.z -= delta_vel;
                    FillPanel();
                }
            }
            if (Input.GetKey("d"))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(delta_move, 0, 0);
                    FillPanel();
                }
                else
                {
                    Selected.GetComponent<Gravity>().curVel.x += delta_vel;
                    FillPanel();
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(0, delta_move, 0);
                    FillPanel();
                }
                else
                {
                    Selected.GetComponent<Gravity>().curVel.y += delta_vel;
                    FillPanel();
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (pos_tr_vel_fls)
                {
                    Selected.transform.Translate(0, -delta_move, 0);
                    FillPanel();
                }
                else
                {
                    Selected.GetComponent<Gravity>().curVel.y -= delta_vel;
                    FillPanel();
                }
            }
        }
    }

    void Scaler() 
    {
        if (Input.GetKey("f")) 
        {
            if (pos_tr_vel_fls)
            {
                delta_move *= 0.9f;
            }
            else
            {
                delta_vel *= 0.9f;
            }
        }
        if (Input.GetKey("g"))
        {
            if (pos_tr_vel_fls)
            {
                delta_move /= 0.9f;
            }
            else
            {
                delta_vel /= 0.9f;
            }
        }
    }

    bool en_or_dis = false;
    public void TrajActive(bool active) 
    {
        en_or_dis = active;

        lines = FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer line in lines) 
        {
            line.enabled = en_or_dis;
        }

        if (en_or_dis)
        {
            Time.fixedDeltaTime = GetComponent<Trajectory>().pass_time;
        }
        else 
        {
            Time.fixedDeltaTime = GetComponent<RunGravity>().t_s;
        }

        GetComponent<Trajectory>().enabled = en_or_dis;
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
