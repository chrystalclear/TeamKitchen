﻿using UnityEngine;
using System.Collections;

public class TitleScreenController : MonoBehaviour {


    public GUIStyle style;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI () {
        float leftEdgeX = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)).x;
        int cameraWidth = Camera.main.pixelWidth;

        GUI.Label(new Rect(leftEdgeX + cameraWidth * 30 / 100,
                            Screen.height * 20 / 100,
                            cameraWidth * 60 / 100,
                            Screen.height * 15 / 100), "Toast University", style);

        if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 20 / 100,
                                Screen.height * 70 / 100,
                                cameraWidth * 60 / 100,
                                Screen.height * 15 / 100), "Start Game")) {
            Application.LoadLevel("NameSelect");
        }
    }
}
