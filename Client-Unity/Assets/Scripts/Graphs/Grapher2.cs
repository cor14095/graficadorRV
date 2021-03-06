﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;
using System;
using System.IO;
using System.Text;
using System.Net.Sockets;

public class Grapher2 : MonoBehaviour {

    //at least 0, high res = bad performance.
    public int resolution = 10;
    public float limInf = -10;
    public float limSup = 10;
    public float particleSize = 0.4f;
    public float z_space = 0.1f;
    public float limInfZ = -10;
    public float limSupZ = 10;
    public GameObject player;
    public GameObject canvas;
    public GameObject mainCamara;
    public Text loadBar;
    private bool playerActive = false;
    private int currentResolution;
    private int msgLost;
    private float currentlimInf;
    private float currentlimSup;
    private float currentlimInfZ;
    private float currentlimSupZ;
    private float currentz_space;
    private float currentParticleSize;
    private List<ParticleSystem.Particle> points;
    private ParticleSystem.Particle[] pointsArray;
    private ParticleSystem.Particle point;
    private Vector3[] cubeArray;
    private TcpClient tcpclnt;

    // Use this for initialization

    private void CreatePoints()
    {
        // Calculate values.
        var increment = 1f / (resolution);
        float x = limInf;
        float y = 0;
        float z = limInfZ;
        int space = 2;

        // Initialize stuff.
        currentResolution = resolution;
        points = new List<ParticleSystem.Particle>();
        point = new ParticleSystem.Particle();
        currentResolution = resolution;
        currentlimInf = limInf;
        currentlimSup = limSup;
        currentlimInfZ = limInfZ;
        currentlimSupZ = limSupZ;
        currentParticleSize = particleSize;
        currentz_space = z_space;

        //Debug.Log("Create points started.");
        do {
            do {
                //do {
                    var p = new Vector3(x * increment, 0.0f, z * z_space);
                    point.position = p;
                    point.startColor = new Color(255f, 255f, 255f);
                    point.startSize = particleSize;
                    points.Add(point);
                //    y += 1;
                //} while (y < 1);
                z += space;
            } while (z < limSupZ);
            z = limInfZ;
            x += space;
        } while (x < limSup);
        //Debug.Log("Create points ended.");
        doGraph();
    }

    public IEnumerator TCPRequest()
    {
        var initTime = Time.realtimeSinceStartup;
        Debug.Log("Calculate graph points started.");
        msgLost = 0;
        for (var i = 0; i < pointsArray.Length; i++)
        {
            var p = points[i].position;
            //f(x) = x

            // Method 2 evaluation
            //Send the code 777 + p.x , p.z , p.y 
            //Wait for return on code 515

            string strString = "777$" + p.x + "@" + p.z + "@" + p.y + '~';
            char[] str = strString.ToCharArray();
            Stream stm = tcpclnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[20];
            int k = stm.Read(bb, 0, 20);
            // Debug.Log(k);
            string response = "";

            for (int ii = 0; ii < k; ii++)
            {

                if (!char.IsWhiteSpace(Convert.ToChar(bb[ii])))
                {
                    response += Convert.ToChar(bb[ii]);
                }
            }

            response = response.Trim('\0');
            response = response.Trim();

            if (response.Length >= 1)
            {
                //Debug.Log(response);
                try
                {
                    p.y = (float.Parse(response));
                }
                catch (FormatException e)
                {
                    //Debug.Log(response);
                    msgLost++;
                }
            }

            //p.y = BitConverter.ToSingle(BitConverter.IsLittleEndian? Array.Reverse(bb) : bb, 0);

            // p.y = (float)method.Invoke(null, new object[] { p.x, p.z, p.y });

            // Debug.Log(p.y);
            if (float.IsInfinity(p.y) || float.IsNaN(p.y))
            {
                p.y = 0.0f;
                p.x = 0.0f;
                p.z = 0.0f;
            }

            if (p.y > 50) { p.y = 50.0f; }
            if (p.y < -50) { p.y = -50.0f; }

            pointsArray[i].position = p;
            loadBar.text = ((i / (pointsArray.Length * 1.0f)) * 100f).ToString("F2") + "%";
            GetComponent<ParticleSystem>().SetParticles(pointsArray, pointsArray.Length);

            // Give's a frame for the player.
            yield return null;
        }

        string str2 = "quit";
        Stream stm2 = tcpclnt.GetStream();

        ASCIIEncoding asen2 = new ASCIIEncoding();
        byte[] ba2 = asen2.GetBytes(str2);

        stm2.Write(ba2, 0, ba2.Length);
        tcpclnt.Close();

        var endTime = Time.realtimeSinceStartup;
        Debug.Log("Calculate graph points ended. took: " + (endTime - initTime) + " seconds for: " + pointsArray.Length + "points.");
        Debug.Log("Messages lost: " + msgLost.ToString() + " of " + pointsArray.Length.ToString() + " - (" + (msgLost * 100 / (double)pointsArray.Length).ToString() + "%)");
    }

    private void doGraph()
    {
        var s = GameObject.Find("Canvas").GetComponent<TreeCreator>().GetText();
        var PS = GetComponent<ParticleSystem>();
        //call visitor here
        s = TreeCreator.Creator(s);
        //Debug.Log(s);
        // Method 2 start
        string source = @"
            using System;
            using System.Collections;
            using System.Collections.Generic;

            namespace test
            {
                public class MyType
                {
                    public static double Evaluate(float x, float y, float z)
                    {
                        return (double) <!expression!>;
                    }
                }
            }
            ";
        // Initialice parameters.
        string expression = s;
        // Replace parameters in source.
        string finalSource = source.Replace("<!expression!>", expression);
        //Debug.Log(finalSource);
        tcpclnt = new TcpClient();
        try {
            // Deploy IP - c5.xlarge
            //var ip = "34.205.15.170";
            // Development IP - t2.micro
            //var ip = "18.208.138.62";
            // Localhost testings
            var ip = "localhost";

            var port = 8081;
            tcpclnt.Connect(ip, port);

            string str = "212 $ " + finalSource;
            Stream stm = tcpclnt.GetStream();
                        
            ASCIIEncoding asen= new ASCIIEncoding();
            byte[] ba=asen.GetBytes(str);

            stm.Write(ba,0,ba.Length);
            
            byte[] bb=new byte[1024];
            int k=stm.Read(bb,0,1024);
            string response = "";

            for (int i = 0; i < k; i++) {
                response += Convert.ToChar(bb[i]);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error " + e);
        }
        // Make points into an Array.
        pointsArray = points.ToArray();

        // Do Work...
        StartCoroutine(TCPRequest());

    }

    public void Graph() {
        CreatePoints();
    }

    private void Start() {
        currentResolution = resolution;
        points = new List<ParticleSystem.Particle>();
        point = new ParticleSystem.Particle();
        currentResolution = resolution;
        currentlimInf = limInf;
        currentlimSup = limSup;
        currentlimInfZ = limInfZ;
        currentlimSupZ = limSupZ;
        currentParticleSize = particleSize;
        currentz_space = z_space;
        playerActive = false;
        player.SetActive(playerActive);
        //canvas.SetActive(!playerActive);
        mainCamara.SetActive(!playerActive);
    }

    // Update is called once per frame
    private void Update () {
        //the points == null lets us remove start method
        if (currentResolution != resolution || currentlimInf != limInf || currentlimSup != limSup || currentParticleSize != particleSize ||
            currentlimInfZ != limInfZ || currentlimSupZ != limSupZ || currentz_space != z_space || points == null)
        {
            Graph();
        }
    }

   
}
