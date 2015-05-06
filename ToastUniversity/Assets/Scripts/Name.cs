using UnityEngine;
using System.Collections;

public class Name : MonoBehaviour {

    public static string playerName = "Avery";
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
        int cameraHeight = Camera.main.pixelHeight;

        playerName = GUI.TextField(new Rect(leftEdgeX + cameraWidth * 45 / 100,
                                cameraHeight * 49 / 100,
                                cameraWidth * 50 / 100,
                                cameraHeight * 10 / 100), playerName, 14, style);

        if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 65 / 100,
                                cameraHeight * 85 / 100,
                                cameraWidth * 30 / 100,
                                cameraHeight * 10 / 100), "Confirm Name")) {
            Application.LoadLevel("VisualNovel");
        }
    }
}
