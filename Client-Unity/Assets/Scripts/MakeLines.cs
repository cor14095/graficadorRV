using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeLines : MonoBehaviour {

    public int xScale = 5;
    public int yScale = 5;
    public int zScale = 5;

    // Main holders
    public GameObject lineHolderY;
    public GameObject lineHolderX;
    public GameObject lineHolderZ;

    private int maxRange = 100;

    // Use this for initialization
    void Start () {
        // Make X Lines
        GameObject xLine = GameObject.Find("lineX");
		for (var x = (-1 * maxRange / xScale); x <= (maxRange / xScale); x++) {
            var tempLine = Instantiate(xLine, new Vector3(1.0f * x * yScale / 2.0f, 0.0f, 0.0f), Quaternion.identity, lineHolderX.transform);
            var tempLine2 = Instantiate(xLine, new Vector3(0.0f, 1.0f * x * yScale / 2.0f, 0.0f), Quaternion.identity, lineHolderX.transform);
            tempLine2.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
        // Make Y lines
        GameObject yLine = GameObject.Find("lineY");
        for (var y = (-1 * maxRange / yScale); y <= (maxRange / yScale); y++) {
            var tempLine = Instantiate(yLine, new Vector3(0.0f, 0.0f, 1.0f * y * yScale / 2.0f), Quaternion.identity, lineHolderY.transform);
            var tempLine2 = Instantiate(yLine, new Vector3(0.0f, 1.0f * y * yScale / 2.0f, 0.0f), Quaternion.identity, lineHolderY.transform);
            tempLine2.transform.Rotate(90.0f, 0.0f, 0.0f);
        }
        // Make Z lines
        GameObject zLine = GameObject.Find("lineZ");
        for (var z = (-1 * maxRange / zScale); z <= (maxRange / zScale); z++) {
            var tempLine = Instantiate(zLine, new Vector3(1.0f * z * yScale / 2.0f, 0.0f, 0.0f), Quaternion.identity, lineHolderZ.transform);
            tempLine.transform.Rotate(90.0f, 0.0f, 0.0f);
            var tempLine2 = Instantiate(zLine, new Vector3(0.0f, 0.0f, 1.0f * z * yScale / 2.0f), Quaternion.identity, lineHolderZ.transform);
            tempLine2.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
