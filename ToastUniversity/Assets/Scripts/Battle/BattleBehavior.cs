using UnityEngine;
using System.Collections;

public class BattleBehavior : MonoBehaviour {

    public GameObject turnController;

    public int associatedCharacter;
    public Transform attackIcon;
    public Transform waitIcon;
    public Transform blockIcon;
    public Transform powEffect;

    int hp = 10;
    int attack = 2;
    bool turnDone = false;

    Transform defend = null;
    public bool blockSuccess = false;
    float blockTimer = 0;

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
        if (defend != null) {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0) {
                if (!blockSuccess) {
                    Instantiate(powEffect, transform.position, Quaternion.identity);
                    TakeDamage(2);
                }
                Destroy(defend.gameObject);
                defend = null;
                turnController.GetComponent<TurnController>().NextPlayerTurn();
            }
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

    public void BlockSequence () {
        defend = Instantiate(blockIcon, transform.position, Quaternion.identity) as Transform;
        defend.GetComponent<BattleIconsBehavior>().associatedCharacter = associatedCharacter;
        blockSuccess = false;
        blockTimer = 0.4f + Random.Range(0.1f, 0.35f);
    }
}
