﻿using UnityEngine;
using System.Collections;

public class DaytimeMenuController : MonoBehaviour {

    /* Menu States:
     * 0 = Main Menu (should I talk, check info, or go to options?)
     * 1 = Chat Select (who should I talk to?)
     * 2 = Char Info
     * 3 = Options
     * */
    private int menuState;
    public GUIStyle style;

	// Use this for initialization
	void Start () {
        menuState = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI () {
        float leftEdgeX = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)).x;
        int cameraWidth = Camera.main.pixelWidth;
        int cameraHeight = Camera.main.pixelHeight;

        switch (menuState) {
            /* MAIN MENU */
            case 0:
                GUI.Label(new Rect(leftEdgeX + cameraWidth * 5 / 100,
                                   cameraHeight * 5 / 100,
                                   cameraWidth * 20 / 100,
                                   cameraHeight * 15 / 100), "Day 2", style);

                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 20 / 100,
                                        cameraHeight * 20 / 100,
                                        cameraWidth * 60 / 100,
                                        cameraHeight * 15 / 100), "Chat")) {
                    menuState = 1;
                }
                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 20 / 100,
                                        cameraHeight * 42 / 100,
                                        cameraWidth * 60 / 100,
                                        cameraHeight * 15 / 100), "Appliance Info")) {
                }
                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 20 / 100,
                                        cameraHeight * 64 / 100,
                                        cameraWidth * 60 / 100,
                                        cameraHeight * 15 / 100), "Options")) {
                }
                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 65 / 100,
                                        cameraHeight * 85 / 100,
                                        cameraWidth * 30 / 100,
                                        cameraHeight * 10 / 100), "End Class")) {
                }
                break;
            /* CHAT SELECT */
            case 1:
                break;
        }
        
    }
}
