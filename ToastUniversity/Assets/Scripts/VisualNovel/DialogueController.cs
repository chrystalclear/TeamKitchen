using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DialogueController : MonoBehaviour {

    public Texture dialogueBox;
    public GUIStyle style;
    public GUIStyle labelStyle;
	public GUIStyle buttonStyle;
	/*public struct namedTexture{
		public string name;
		public Texture texture;
	}*/
	public Dictionary<string, Texture> imageDict = new Dictionary<string, Texture> ();
	public Texture[] arr;

    private bool boxVisible = false;
    private bool launchNextStep = false;
    private bool showingChoice = false;
    private string choice1 = "";
    private string choice2 = "";
    private string label = "";
    private string message = "";

    // Character images during dialogue.
    private string[] images;

    private Color darkened = new Color(.5f, .5f, .5f);
    private Color normal = new Color(1, 1, 1);

    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
		//Camera.main.aspect = 640f / 480f;
		foreach(Texture tex in arr){
			imageDict.Add (tex.name.ToLower(), tex);
		}
	}
	
	// Update is called once per frame
	void Update () {
		  if ((Input.GetButtonUp("Fire1") || (Input.touchCount>0&&Input.GetTouch (0).phase==TouchPhase.Ended)) && !showingChoice) {
		//if((Input.touchCount>0&&Input.GetTouch (0).phase == TouchPhase.Ended) && !showingChoice){
            if (!launchNextStep) {
                launchNextStep = true;
            }
        }
    }

    void OnGUI () {
		Color black = new Color (0, 0, 0);
        if (boxVisible) {
            // Left edge of the camera
            float leftEdgeX = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)).x;
            int cameraWidth = Camera.main.pixelWidth;
			int cameraHeight = Camera.main.pixelHeight;
			GUI.backgroundColor = Color.black;
			
		//	GUI.Button(Rect(0,0,cameraWidth,cameraHeight), "A button");
			int height = Camera.main.pixelHeight;
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
                                                    height),
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
                                                    height),
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
                                                        height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            } else {
                                GUI.DrawTexture(new Rect(leftEdgeX + cameraWidth / 4 + (i - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        height),
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
                                                        height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            } else {
                                GUI.DrawTexture(new Rect(leftEdgeX + cameraWidth / 4 + (i - middleL) * cameraWidth / 2,
                                                        0,
                                                        cameraWidth,
                                                        height),
                                    GetTextureFromString(images[i]),
                                    ScaleMode.ScaleToFit);
                            }
                        }
                    }
                }
            }

            // Draw box and text.
            GUI.color = normal;
            if (dialogueBox != null) {
                GUI.DrawTexture(new Rect(leftEdgeX,
                                        Screen.height * 12 / 20,
                                        cameraWidth,
                                        height * 2 / 5),
                                dialogueBox,
                                ScaleMode.StretchToFill);
            }
            if (label != null) {
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12 + cameraWidth * 2 / 32,
                                  height * 3 / 5 + height / 32,
                                  cameraWidth,
                                  height / 3),
                                  label,
                                  labelStyle);
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12,
				                   height * 3 / 5 + height * 3 / 32,
				                   cameraWidth * 7 / 8,
                                  height / 3),
                                  message,
                                  style);
            } else {
                GUI.Label(new Rect(leftEdgeX + cameraWidth / 12,
                                  height * 3 / 5 + height * 3 / 32,
                                  cameraWidth * 7 / 8,
                                  height / 3),
                                  message,
                                  style);
            }

            //Draw choice buttons if at a choice
            if (showingChoice) {
                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 10/100,
                                    height * 10/100,
                                    cameraWidth * 80/100,
				                        height * 15/100), choice1)) {
					showingChoice = false;
                    GetComponent<TestDialogue>().chooseFirstChoice();
                }
                if (GUI.Button(new Rect(leftEdgeX + cameraWidth * 10/100,
                                    height * 40/100,
                                    cameraWidth * 80/100,
				                        height * 15/100), choice2)) {
					showingChoice = false;
                    GetComponent<TestDialogue>().chooseSecondChoice();
                }
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

    public void ShowChoice (string c1, string c2) {
        showingChoice = true;
        choice1 = c1;
        choice2 = c2;
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
    }

    public bool ReadyForNextStep () {
        return launchNextStep;
    }
    public void ResetLaunchNextStep () {
        launchNextStep = false;
    }

    public bool GetBoxVisible () {
        return boxVisible;
    }
    public void SetBoxVisible (bool display) {
        boxVisible = display;
    }

}
