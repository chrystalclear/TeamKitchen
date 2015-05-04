using UnityEngine;
using System.Collections;

public class BattleIconsBehavior : MonoBehaviour {

    public int associatedCharacter;
    /* Icon types:
     * 0 = Attack Icon
     * 1 = Wait Icon
     * 2 = Defend Icon
     * */
    public int iconType;
    GameObject character;
    bool mouseOver = false;

	// Use this for initialization
	void Start () {
	    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Character")) {
            if (obj.GetComponent<BattleBehavior>().associatedCharacter == associatedCharacter) {
                character = obj;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1")) {
            if (mouseOver) {
                if (iconType == 0) {
                    character.GetComponent<BattleBehavior>().AttackCommand();
                } else if (iconType == 1) {
                    character.GetComponent<BattleBehavior>().WaitCommand();
                } else if (iconType == 2) {
                    character.GetComponent<BattleBehavior>().blockSuccess = true;
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    return;
                }
                Destroy(gameObject);
            } else {
                if (iconType == 2) {
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    return;
                }
                Destroy(gameObject);
            }
        }
	}

    void OnMouseEnter () {
        mouseOver = true;
    }

    void OnMouseExit () {
        mouseOver = false;
    }
}
