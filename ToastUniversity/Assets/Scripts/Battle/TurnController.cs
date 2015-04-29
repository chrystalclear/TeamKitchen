using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {

    public static bool playerTurn = true;
    float timer;

    //LOLOLOL TOO LAZY TO MAKE AN ENEMY SCRIPT THAT DOES NOTHING
    public static int enemyHealth = 16;

    public Transform powEffect;

    GameObject[] characters;

	// Use this for initialization
	void Start () {
        timer = 0;
        characters = new GameObject[3];
        int i = 0;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Character")) {
            characters[i] = obj;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        int turn_count = 0;
        foreach (GameObject character in characters) {
            if (character.GetComponent<BattleBehavior>().TurnIsDone()) {
                turn_count++;
            }
        }
        if (playerTurn && turn_count == 3 && enemyHealth > 0) {
            playerTurn = false;
            timer = 1.5f;
        }
        if (!playerTurn) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                playerTurn = true;
                foreach (GameObject character in characters) {
                    Instantiate(powEffect, character.transform.position, Quaternion.identity);
                    character.GetComponent<BattleBehavior>().TakeDamage(2);
                    character.GetComponent<BattleBehavior>().NewTurn();
                }
            }
        }

        if (enemyHealth <= 0) {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null) {
                Destroy(enemy);
            }
        }
	}
}
