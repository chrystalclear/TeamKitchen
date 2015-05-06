using UnityEngine;
using System.Collections;

public class BattleBehavior : MonoBehaviour {

    public GameObject turnController;

    public int associatedCharacter;
    public Transform attackIcon;
    public Transform waitIcon;
    public Transform blockIcon;
    public Transform powEffect;
	public float maxHP = 10;
    public float hp = 10;


	public Texture2D bgImage; 
	public Texture2D fgImage; 
	
	public float healthBarLength;

    int attack = 2;
    bool turnDone = false;

    Transform defend = null;
    public bool blockSuccess = false;
    float blockTimer = 0;
	private GUIStyle currentStyle = null;
	// Use this for initialization
	void Start () {
		healthBarLength = 128;    
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurrentHealth(0);
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
        blockTimer = 0.6f;
    }

	void OnGUI () {
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		Camera camera = Camera.main;
		InitStyles ();
		float x = camera.WorldToScreenPoint(transform.position).x-45;
		float y = camera.WorldToScreenPoint(-transform.position).y+560;
		// Draw the background image
		GUI.Box (new Rect (x,y, healthBarLength*2,16), "hello world", currentStyle);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		//GUI.color = Color.black;
		// Draw the foreground image
		GUI.Box (new Rect (x,y,healthBarLength*2,16), "hello world", currentStyle);

	}
	private void InitStyles()
	{
		if (currentStyle == null) {
				currentStyle = new GUIStyle (GUI.skin.box);
		}
		if(hp<maxHP*0.25){
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 0.5f ) );
		}
		else if(hp<maxHP*0.5){
			print("HI");
			currentStyle.normal.background=MakeTex(2,2,new Color(1f,1f,0f, 0.5f));
		}
		else{
			currentStyle.normal.background=MakeTex(2,2,new Color(0f,1f,0f,0.5f));
		}

	}
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
	public void AddjustCurrentHealth(int adj){
		
		hp += adj;
		
		if(hp <0)
			hp = 0;
		
		if(hp > maxHP)
			hp = maxHP;
		
		if(maxHP <1)
			maxHP = 1;
		
		healthBarLength =(64) * (hp / (float)maxHP);
	}
}
