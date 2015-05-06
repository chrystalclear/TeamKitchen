using UnityEngine;
using System.Collections;

public class TestDialogue : MonoBehaviour {

    public GameObject dialogueHandler;

    // The current step of dialogue. One array of messages.
    private int step = 0;
    // The current step in the array of messages.
    private int arrayStep = 0;
    // The current step in making choices.
    private int choiceStep = 0;
    // Finished with the current step. Stop sending messages until the next step.
    private bool finished = false;
    // True iff we're currently in dialogue from making the first choice.
    private bool firstChoice = false;

    public int num_messages = 13;
    private string[] labels;
    private string[] messages;
    private string[][] images;
    /* Example of what the choice indices are:
     * 
     * "Hey how are you?"               (message 1)
     * CHOICE                           (choiceIndices[0] = 1)
     * I'm fine!                        (choiceMessages[0] = "I'm fine!")
     * I'm feeling terrible!            (choiceMessages[1] = "I'm feeling terrible!")
     * 
     * "I'm fine!"                      (message 2)
     * ENDCHOICE                        (choiceSecond[0] = 3)
     * 
     * "I'm feeling terrible!"          (message 3)
     * ENDCHOICE                        (choiceEnd[0] = 4)
     * 
     * "Oh okay. I didn't care anyway.  (message 4)
     * */
    private int[] choiceIndices; // The indices of the messages that show a choice after them.
    private int[] choiceSecond; // The indices of the messages that the choice will jump to if the 2nd choice is made.
    private int[] choiceEnd; // The indices of the messages that the end of the first choice will jump to after it's done.
    private string[] choiceMessages; // The options presented at each choice. choiceMessages[2*i] and [2*i + 1] represent the options for choiceIndices[i]

	public string dialogFileName;
    // Use this for initialization
    void Start () {
        LoadMessages(step);
    }

    // Update is called once per frame
    void Update () {
        if (dialogueHandler == null) {
            dialogueHandler = GameObject.FindGameObjectWithTag("DialogueController");
        }
        if (messages != null && arrayStep < messages.Length) {
            if (dialogueHandler.GetComponent<DialogueController>().ReadyForNextStep()) {
                if (choiceIndices[choiceStep] != 0 && choiceIndices[choiceStep] == arrayStep - 1) {
                    dialogueHandler.GetComponent<DialogueController>().ShowChoice(choiceMessages[choiceStep * 2], choiceMessages[choiceStep * 2 + 1]);
                    choiceStep++;
                    dialogueHandler.GetComponent<DialogueController>().ResetLaunchNextStep();
                } else {
                    if (firstChoice && arrayStep == choiceSecond[choiceStep - 1]) {
                        arrayStep = choiceEnd[choiceStep - 1];
                        firstChoice = false;
                    }
                    dialogueHandler.GetComponent<DialogueController>().ShowText(labels[arrayStep], messages[arrayStep], images[arrayStep]);
                    arrayStep++;
                    dialogueHandler.GetComponent<DialogueController>().ResetLaunchNextStep();
                }
            }
        } else if (arrayStep == messages.Length && dialogueHandler.GetComponent<DialogueController>().ReadyForNextStep()) {
            dialogueHandler.GetComponent<DialogueController>().SetBoxVisible(false);
            ProgressController.NextStage();
        }
    }

    /* DED METHOD
    public void NextStep (int s = 10) {
        step += s;
        finished = false;
        //LoadMessages(step);
    }
     * */

    public void LoadMessages (int s) {
		string[] lines = System.IO.File.ReadAllLines(dialogFileName);
        //string url = "http://www.calvinkirbikaka.com/games/ToastUniversity/testScript.txt";
        //WWW www = new WWW(url);
        //string[] lines = www.text.Split('\n');
        // Reset everything
        finished = false;
        step = 0;
        arrayStep = 0;
        choiceStep = 0;
        labels = new string[num_messages];
        messages = new string[num_messages];
        images = new string[num_messages][];
        choiceIndices = new int[num_messages / 2];
        choiceSecond = new int[num_messages / 2];
        choiceEnd = new int[num_messages / 2];
        choiceMessages = new string[num_messages];
		int i = 0;
		int j = 0;
		int k;
        int choice_count = 0;
        bool first_choice = false;
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
            if (i == lines.Length) {
                Debug.Log("num messages: " + j);
                return;
            }

            // Look for choices
            if (lines[i].Equals("CHOICE")) {
                choiceIndices[choice_count] = j;
                i++;
                choiceMessages[choice_count * 2] = lines[i];
                i++;
                choiceMessages[choice_count * 2 + 1] = lines[i];
                i++;
                choice_count++;
                first_choice = true;
            } else if (lines[i].Equals("ENDCHOICE")) {
                if (first_choice) {
                    choiceSecond[choice_count - 1] = j + 1;
                    first_choice = false;
                } else {
                    if (i == lines.Length) {
                        choiceEnd[choice_count - 1] = -1;
                    } else {
                        choiceEnd[choice_count - 1] = j + 1;
                    }
                }
                i++;
            }
            i++;
			j++;

		}
        Debug.Log("num messages: " + j);

    }

    public bool IsFinished () {
        return finished;
    }

    public void chooseSecondChoice () {
        arrayStep = choiceSecond[choiceStep - 1];
    }

    public void chooseFirstChoice () {
        firstChoice = true;
    }
}
