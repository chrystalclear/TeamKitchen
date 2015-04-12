using UnityEngine;
using System.Collections;

public class TestDialogue : MonoBehaviour {

    public GameObject dialogueHandler;

    // The current step of dialogue. One array of messages.
    private int step = 0;
    // The current step in the array of messages.
    private int arrayStep = 0;
    // Finished with the current step. Stop sending messages until the next step.
    private bool finished = false;

    private string[] labels;
    private string[] messages;
    private string[][] images;

    // Use this for initialization
    void Start () {
        LoadMessages(step);
    }

    // Update is called once per frame
    void Update () {
        if (messages != null && arrayStep < messages.Length) {
            if (!dialogueHandler.GetComponent<DialogueController>().GetBoxVisible()) {
                dialogueHandler.GetComponent<DialogueController>().ShowText(labels[arrayStep], messages[arrayStep], images[arrayStep]);
                arrayStep++;
            }
        } else if (arrayStep == messages.Length && !dialogueHandler.GetComponent<DialogueController>().GetBoxVisible()) {
            // Load next level
            // Application.LoadLevel("OpeningFight");
        }
    }

    public void NextStep (int s = 10) {
        step += s;
        finished = false;
        LoadMessages(step);
    }

    private void LoadMessages (int s) {
        switch (s) {
            case 0:
                // Set this to the number of messages.
                int num_messages = 11;
                labels = new string[num_messages];
                messages = new string[num_messages];
                images = new string[num_messages][];
                int i = 0;

                labels[i] = null;
                messages[i] = "*VRRRRRRMMMMMM-CRASH!*";

                i = 1;
                labels[i] = "Students";
                messages[i] = "Hahahahaha!";

                i = 2;
                labels[i] = "Professor";
                messages[i] = "Sigh... You know you have to put the lid on the blender first, right?";
                images[i] = new string[1];
                images[i][0] = "Shaker_R_M";

                i = 3;
                labels[i] = "You";
                messages[i] = "...Oops...I...thought I did...";
                images[i] = new string[1];
                images[i][0] = "Shaker_R";

                i = 4;
                labels[i] = "You";
                messages[i] = "Sorry... It won't happen again.";
                images[i] = new string[1];
                images[i][0] = "Shaker_R";

                i = 5;
                labels[i] = "Professor";
                messages[i] = "Uh huh. Sounds just like what you said about the grill.";
                images[i] = new string[1];
                images[i][0] = "Shaker_R_M";

                i = 6;
                labels[i] = "Students";
                messages[i] = "Man, what a waste of our tuition dollars.";
                images[i] = new string[1];
                images[i][0] = "Shaker_R";

                i = 7;
                labels[i] = "Students";
                messages[i] = "I know. Our school starts a scholarship program and the first person they bring over is this klutz?";
                images[i] = new string[1];
                images[i][0] = "Shaker_R";

                i = 8;
                labels[i] = "Professor";
                messages[i] = "Well, looks like it's 2 o'clock. You all can go. Don't forget about the quiz tomorrow!";
                images[i] = new string[1];
                images[i][0] = "Shaker_R_M";

                i = 9;
                labels[i] = "Professor";
                messages[i] = "And YOU need to stay here and clean up this mess you've made.";
                images[i] = new string[1];
                images[i][0] = "Shaker_R_M";

                i = 10;
                labels[i] = "You";
                messages[i] = "Shoot, I won't make it to my next class in time. I'm going to fall so behind...";

                break;
        }
    }

    public bool IsFinished () {
        return finished;
    }
}
