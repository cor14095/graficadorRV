using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGraph : MonoBehaviour {

    public Transform cubePrefab;
	[Range(10, 100)]
	public int resolution = 10;

	private Transform[] points;

	void Awake()
	{
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		Vector3 position;
		position.y = 0f;
		position.z = 0f;
		points = new Transform[resolution * resolution];

		for (int i = 0, z = 0; z < resolution; z++)
		{
			position.z = (z + 0.5f) * step - 1f;
			for (int x = 0; x < resolution; x++, i++)
			{
				if (x == resolution)
				{
					x = 0;
					z += 1;
				}

				Transform point = Instantiate(cubePrefab);
				position.x = (x + 0.5f) * step - 1f;
				
				point.localPosition = position;
				point.localScale = scale;
				point.SetParent(transform, false);
				points[i] = point;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < points.Length; i++)
		{
			Transform point = points[i];
			Vector3 position = point.localPosition;
			position.y = Mathf.Sin(Mathf.PI * (position.x + position.z + Time.time));
			point.localPosition = position;
		}
	}
}
