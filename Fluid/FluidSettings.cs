using UnityEngine;
using UnityEngine.UI;

public class FluidSettings : MonoBehaviour
{
    public Fluid fluid;

    public GameObject setButton;
    public GameObject panel;
    InputField diffusion;
    InputField viscocity;
    InputField densityReduction;
    InputField accuracy;

    bool setOn = false;

    void Start()
    {
        diffusion = panel.transform.Find("Diffusion").GetComponent<InputField>();
        viscocity = panel.transform.Find("Viscocity").GetComponent<InputField>();
        densityReduction = panel.transform.Find("Density reduction").GetComponent<InputField>();
        accuracy = panel.transform.Find("Accuracy").GetComponent<InputField>();

        setOn = false;
    }

    
    void Update()
    {
        if (setOn) 
        {
            float.TryParse(diffusion.text, out fluid.diff);
            float.TryParse(viscocity.text, out fluid.visc);
            float.TryParse(densityReduction.text, out fluid.densityReduce);
            int.TryParse(accuracy.text, out fluid.linSolveIterNum);
        }
    }

    public void SettingOn() 
    {
        setButton.SetActive(false);
        panel.SetActive(true);

        diffusion.text = fluid.diff.ToString();
        viscocity.text = fluid.visc.ToString();
        densityReduction.text = fluid.densityReduce.ToString();
        accuracy.text = fluid.linSolveIterNum.ToString();

        setOn = true;
    }
    public void SettingOff()
    {
        setButton.SetActive(true);
        panel.SetActive(false);

        setOn = false;
    }
}
