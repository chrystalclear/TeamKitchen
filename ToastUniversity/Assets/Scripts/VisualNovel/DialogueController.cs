using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DialogueController : MonoBehaviour {

    public Texture dialogueBox;
    public GUIStyle style;
    public GUIStyle labelStyle;
	/*public struct namedTexture{
		public string name;
		public Texture texture;
	}*/
	public  Dictionary<string, Texture> imageDict = new Dictionary<string, Texture> ();
	public Texture[] arr;

    private bool boxVisible = false;
    private string label = "";
    private string message = "";

    // Character images during dialogue.
    private string[] images;

    private Color darkened = new Color(.5f, .5f, .5f);
    private Color normal = new Color(1, 1, 1);

    // Use this for initialization
    void Start () {
		foreach(Texture tex in arr){
			imageDict.Add (tex.name.ToLower(), tex);
		}
		print(imageDict);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonUp("Fire1")) {
            if (boxVisible) {
                SetBoxVisible(false);
            }
        }
    }

    void OnGUI () {
        if (boxVisible) {
            // Left edge of the camera
            float leftEdgeX = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)).x;
            int cameraWidth = Camera.main.pixelWidth;

            // Draw character images. Main talkers are always drawn last.
            if (images != null) {
                // If odd number of images, draw one in the center, and the rest off center
                if (images.Length % 2 == 1) {
                    int middle = images.Length / 2;
                    for (int i = images.Length - 1; i >= 0; i--) {
                        GUI.color = darkened;
                        if (!IsMainTalker(images[i])) {
                            GUI.DrawTexture(new Rect(leftEdgeX + (i - middle) * cameraWidth / 5,
                                                    0,
                                                    cameraWidth,
                                                    Screen.height),
                                            GetTextureFromString(images[i]),
                                            ScaleMode.ScaleToFit);
                        }
                    }
                    for (int i = images.Length - 1; i >= 0; i--) {
                        GUI.color = normal;
                        if (IsMainTalker(images[i])) {
                            GUI.DrawTexture(new Rect(leftEdgeX + (i - middle) * cameraWidth / 5,
                                                    0,
                                                    cameraWidth,
                                                    Screen.height),
                                            GetTextureFromString(images[i]),
                                            ScaleMode.ScaleToFit);
                        }
                    }
                } else { // Else, draw them off center
                    int middleL = images.Length / 2; // The middle image, rounded up. (e.g. indices 0, 1, 2, 3; and 4 / 2 = 2)
                    for (int i = images.Length - 1; i >= 0; i--) {
                        GUI.color = darkened;
                        if (!IsMainTalker(images[i])) {
                            if (i < middleL) {
                                GUI.DrawTexture(new Rect(leftEdgeX - cameraWidth / 4 + ((i + 1) - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        Screen.height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            } else {
                                GUI.DrawTexture(new Rect(leftEdgeX + cameraWidth / 4 + (i - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        Screen.height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            }
                        }
                    }
                    for (int i = images.Length - 1; i >= 0; i--) {
                        GUI.color = normal;
                        if (IsMainTalker(images[i])) {
                            if (i < middleL) {
                                GUI.DrawTexture(new Rect(leftEdgeX - cameraWidth / 4 + ((i + 1) - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        Screen.height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            } else {
                                GUI.DrawTexture(new Rect(leftEdgeX + cameraWidth / 4 + (i - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        Screen.height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            }
                        }
                    }
                }
            }

            // Draw box and text.
            GUI.color = normal;
            GUI.DrawTexture(new Rect(leftEdgeX,
                                    Screen.height * 3 / 5,
                                    cameraWidth,
                                    Screen.height * 2 / 5),
                            dialogueBox,
                            ScaleMode.StretchToFill);
            if (label != null) {
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12 + cameraWidth / 32,
                                  Screen.height * 3 / 5 + Screen.height / 32,
                                  cameraWidth,
                                  Screen.height / 3),
                                  label,
                                  labelStyle);
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12,
                                  Screen.height * 3 / 5 + Screen.height * 2 / 32,
                                  cameraWidth * 6 / 8,
                                  Screen.height / 3),
                                  message,
                                  style);
            } else {
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12,
                                  Screen.height * 3 / 5 + Screen.height * 2 / 32,
                                  cameraWidth * 6 / 8,
                                  Screen.height / 3),
                                  message,
                                  style);
            }
        }
    }

    void OnMouseClick () {
    }

    public void ShowText (string l, string m, string[] left = null) {
        SetBoxVisible(true);
        label = l;
        message = m;
        images = left;
    }

    public void ShowText (string m, string[] left = null) {
        SetBoxVisible(true);
        label = null;
        message = m;
        images = left;
    }

    private bool IsMainTalker (string s) {
        return s.Substring(s.Length - 2).Equals("_M");
    }

    private Texture GetTextureFromString (string s) {
        if (s.Substring(s.Length - 2).Equals("_M")) {
            s = s.Substring(0, s.Length - 2);
        }
	//	if (!imageDict.ContainsKey (s)) {
	//		return null;
	//	}
		return imageDict [s.ToLower ()];
        /*switch (s) {
            case "Shaker_R":
                return shaker_R;
            case "Freezer":
                return freezer;
            case "Microwave":
                return microwave;
        }
        return null;*/
    }

    public bool GetBoxVisible () {
        return boxVisible;
    }
    public void SetBoxVisible (bool display) {
        boxVisible = display;
    }

}
