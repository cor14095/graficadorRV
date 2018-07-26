using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControll : MonoBehaviour {

    // Grid GameObject variables
    public GameObject xGrid;
    public GameObject yGrid;
    public GameObject zGrid;
    // Panel GameObject variable
    public GameObject Panel;
    // Panel Hide/Display button text
    public Text ButtonText;
    // Limits for graph.
    public InputField xMinRange;
    public InputField xMaxRange;
    public InputField yMinRange;
    public InputField yMaxRange;
    public InputField zMinRange;
    public InputField zMaxRange;
    // Grapher GameObject
    public GameObject Grapher;

    // Functions to enable/disable gridlines.
    public void IOxGrid()
    {
        if (xGrid.active)
        {
            xGrid.SetActive(false);
        } else
        {
            xGrid.SetActive(true);
        }
    }
    public void IOyGrid()
    {
        if (yGrid.active)
        {
            yGrid.SetActive(false);
        }
        else
        {
            yGrid.SetActive(true);
        }
    }
    public void IOzGrid()
    {
        if (zGrid.active)
        {
            zGrid.SetActive(false);
        }
        else
        {
            zGrid.SetActive(true);
        }
    }

    // Function to hide/display Controls.
    public void SHControlls()
    {
        RectTransform rt = Panel.GetComponent<RectTransform>();
        float baseY = rt.localPosition.y;
        // Debug.Log(rt.localPosition);
        if (rt.localPosition.x == 170)
        {
            // Then it's been displayed.
            rt.localPosition = new Vector3(469, baseY, 0);
            ButtonText.text = "M\no\ns\nt\nr\na\nr";
        } else
        {
            rt.localPosition = new Vector3(170, baseY, 0);
            ButtonText.text = "E\ns\nc\no\nn\nd\ne\nr";
        }
    }

    // On enable we must set the values of the limits to the required ones.
    private void OnEnable()
    {
        xMinRange.text = Grapher.GetComponent<Grapher2>().limInf.ToString();
        xMaxRange.text = Grapher.GetComponent<Grapher2>().limSup.ToString();
        yMinRange.text = "0";
        yMaxRange.text = "0";
        zMinRange.text = Grapher.GetComponent<Grapher2>().limInfZ.ToString();
        zMaxRange.text = Grapher.GetComponent<Grapher2>().limSupZ.ToString();
    }

}
