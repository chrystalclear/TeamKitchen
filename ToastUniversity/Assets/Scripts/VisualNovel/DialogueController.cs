using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {

    public Texture dialogueBox;
    public GUIStyle style;
    public GUIStyle labelStyle;

    private bool boxVisible = false;
    private string label = "";
    private string message = "";

    // Character images during dialogue.
    private string[] images;

    private Color darkened = new Color(.5f, .5f, .5f);
    private Color normal = new Color(1, 1, 1);

    // Use this for initialization
    void Start () {
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
                for (int i = images.Length - 1; i >= 0; i--) {
                    GUI.color = darkened;
                    if (!IsMainTalker(images[i])) {
                        GUI.DrawTexture(new Rect(leftEdgeX + (images.Length - (i + 1)) * cameraWidth / 11,
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
                        GUI.DrawTexture(new Rect(leftEdgeX + (images.Length - (i + 1)) * cameraWidth / 11,
                                                0,
                                                cameraWidth,
                                                Screen.height),
                            GetTextureFromString(images[i]),
                            ScaleMode.ScaleToFit);
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
        switch (s) {
            case "Shaker_R":
                return shaker_R;
        }
        return null;
    }

    public bool GetBoxVisible () {
        return boxVisible;
    }
    public void SetBoxVisible (bool display) {
        boxVisible = display;
    }

    public Texture shaker_R;
}
