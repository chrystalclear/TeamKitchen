using UnityEngine;
using System.Collections;

public class AttackEffectBehavior : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
        timer = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0) {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
	}
}
