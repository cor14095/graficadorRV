using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher1 : MonoBehaviour {

	//at least 0, high res = bad performance.
	[Range(1,1000)]
	public int resolution = 10;
    [Range(-1000, 0)]
    public int limInf = 0;
    [Range(1, 1000)]
    public int limSup = 10;
	private int currentResolution;
    private int currentlimInf;
    private int currentlimSup;
    private ParticleSystem.Particle[] points;

    public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine
	}

	public FunctionOption function;

	private delegate float FunctionDelegate (float x);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine
	};

	// Use this for initialization

	void CreatePoints(){
        points = new ParticleSystem.Particle[Mathf.Abs(limInf) + limSup + 1];
        float increment = 1f / (resolution - 1);

        currentResolution = resolution;
        currentlimInf = limInf;
        currentlimSup = limSup;

        for (int i = limInf; i < limSup; i++){
			float x = i * increment;
			points[Mathf.Abs(limInf) + i].position = new Vector3(x, 0f, 0f);
			points[Mathf.Abs(limInf) + i].startColor = new Color(x, 0f, 0f);
			points[Mathf.Abs(limInf) + i].startSize = 0.1f;
		}

	}


	
	// Update is called once per frame
	void Update () {
		//the points == null lets us remove start method
		if (currentResolution != resolution || currentlimInf != limInf || currentlimSup != limSup ||  points == null) {
			CreatePoints ();
		}
		FunctionDelegate f = functionDelegates[(int)function];
        for (int i = limInf; i < limSup; i++) {
			Vector3 p = points [Mathf.Abs(limInf) + i].position;
			//f(x) = x
			p.y = f(p.x);
			points [Mathf.Abs(limInf) + i].position = p;
			//color change
			Color c = points[Mathf.Abs(limInf) + i].startColor;
			c.g = p.y;
			points [Mathf.Abs(limInf) + i].startColor = c;
		}
		this.GetComponent<ParticleSystem> ().SetParticles (points, points.Length);

	}

	static float Linear (float x){
		return x;
	}

	private static float Exponential (float x) {
        return x * x;
	}

	private static float Parabola (float x){
		x = 2f * x - 1f;
		return x * x;
	}

	private static float Sine (float x){
		return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x);
	}
}
