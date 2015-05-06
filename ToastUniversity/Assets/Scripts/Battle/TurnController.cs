using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TurnController : MonoBehaviour {

    public static bool playerTurn = true;
    float timer;

    //LOLOLOL TOO LAZY TO MAKE AN ENEMY SCRIPT THAT DOES NOTHING
	public static int maxHP = 20;
    public static int enemyHealth = 20;
    int attackTarget = 0;
    bool attacking = false;
	public int enemyDamage = 3;
    public Transform powEffect;
	List<int> deadChars = new List<int>();
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
            if (timer <= 0 && !attacking) {
                float rng = Random.value;
				attackTarget = (int)(rng*characters.Length);
               /* if (rng < 0.33f) {
                    attackTarget = 0;
                } else if (rng < 0.66f) {
                    attackTarget = 1;
                } else {
                    attackTarget = 2;
                }*/
                attacking = true;
                characters[attackTarget].GetComponent<BattleBehavior>().BlockSequence();
            }
        }

        if (enemyHealth <= 0) {
			Application.LoadLevel("DaytimeMainMenu");

			GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null) {
                Destroy(enemy);
            }
        }
	}

    public void NextPlayerTurn () {
        playerTurn = true;
        attacking = false;
        foreach (GameObject character in characters) {
            //Instantiate(powEffect, character.transform.position, Quaternion.identity);
            //character.GetComponent<BattleBehavior>().TakeDamage(2);
            character.GetComponent<BattleBehavior>().NewTurn();
        }
    }
}
