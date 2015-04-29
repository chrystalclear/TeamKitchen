using UnityEngine;
using System.Collections;

public class BattleBehavior : MonoBehaviour {

    public int associatedCharacter;
    public Transform attackIcon;
    public Transform waitIcon;
    public Transform blockIcon;
    public Transform powEffect;

    int hp = 10;
    int attack = 2;
    bool turnDone = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turnDone) {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        } else {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
	}

    void OnMouseUp () {
        if (!turnDone) {
            Transform icon = Instantiate(attackIcon, new Vector3(transform.position.x, transform.position.y + 1.5f, 0), Quaternion.identity) as Transform;
            icon.GetComponent<BattleIconsBehavior>().associatedCharacter = associatedCharacter;
            icon = Instantiate(waitIcon, new Vector3(transform.position.x, transform.position.y - 1.5f, 0), Quaternion.identity) as Transform;
            icon.GetComponent<BattleIconsBehavior>().associatedCharacter = associatedCharacter;
        }
    }

    public void AttackCommand () {
        TurnController.enemyHealth -= attack;
        Instantiate(powEffect, new Vector3(1.69f + Random.Range(-0.5f, 0.5f), 3.02f + Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
        turnDone = true;
    }

    public void WaitCommand () {
        turnDone = true;
    }

    public bool TurnIsDone () {
        return turnDone;
    }

    public void NewTurn () {
        turnDone = false;
    }

    public void TakeDamage (int dmg) {
        hp -= dmg;
    }
}
