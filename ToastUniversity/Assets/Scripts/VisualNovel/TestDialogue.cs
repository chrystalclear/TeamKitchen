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

	public string dialogFileName;
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
		string[] lines = System.IO.File.ReadAllLines(dialogFileName);
        // Set this to the number of messages.
        int num_messages = 11;
        labels = new string[num_messages];
        messages = new string[num_messages];
        images = new string[num_messages][];
		int i = 0;
		int j = 0;
		int k;
		while (i<lines.Length) {
			labels [j] = lines[i++];
			messages [j] = lines[i++];
			int length;
			int.TryParse (lines[i++], out length);
			k=0;
			if(length!=0){
				images[j] = new string[length];
				while(k<length){
					images[j][k++]=lines[i++];
				}
			}
			i++;
			j++;

		}

    }

    public bool IsFinished () {
        return finished;
    }
}
