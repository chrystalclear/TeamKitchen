using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressController : MonoBehaviour {

    /* Game Stage key:
     * 0 = Day 0. Start of game, first conversation.
     * 1 = Day 0. After school, talk with Janitor.
     * 2 = Night 0. First night dialogue.
     * 3 = Night 0. First night combat / tutorial.
     * 4 = Night 0. After combat dialogue.
     * 5 = Day 1.
     * */
    public static int gameStage;
    public static GameObject dialogueController;

    public static GameObject background;
    public GameObject backgroundGrabber;
    public static Dictionary<string, Sprite> backgroundSprites = new Dictionary<string, Sprite>();
    public Sprite[] bgsprites;

	// Use this for initialization
    void Start () {
        gameStage = 0;
        background = backgroundGrabber;
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController");
        foreach (Sprite spr in bgsprites) {
            backgroundSprites.Add(spr.name, spr);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void NextStage () {
        switch (gameStage) {
            case 0:
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites["ClassroomAft(WIP)"];
                dialogueController.GetComponent<TestDialogue>().dialogFileName = "Assets/Scripts/VisualNovel/Day0AfterSchool.txt";
				dialogueController.GetComponent<TestDialogue>().textAsset=Resources.Load ("Day0AfterSchool") as TextAsset;
				print (dialogueController.GetComponent<TestDialogue>().textAsset.ToString ());
                dialogueController.GetComponent<TestDialogue>().num_messages = 29;
                dialogueController.GetComponent<TestDialogue>().LoadMessages(0);
                gameStage++;
                break;
            case 1:
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites["ClassroomNight(WIP)"];
                dialogueController.GetComponent<TestDialogue>().dialogFileName = "Assets/Scripts/VisualNovel/Day0Night.txt";
				dialogueController.GetComponent<TestDialogue>().textAsset=Resources.Load ("Day0Night") as TextAsset;
				dialogueController.GetComponent<TestDialogue>().num_messages = 53;
                dialogueController.GetComponent<TestDialogue>().LoadMessages(0);
                gameStage++;
                break;
            case 2:
                // Load next level
                Application.LoadLevel("Battle");
                break;
        }
    }

}
