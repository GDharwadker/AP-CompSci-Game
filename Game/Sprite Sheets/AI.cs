using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	double i = 0;
	float j = 0;
	double randomNum = 0;
	bool isCollision = false;
	bool goAttack = false;
	bool detect; 
	float charX;
	float charY;
	float enemyX;
	float enemyY;
	float Xdiff;
	float Ydiff;
	public float smallY;
	public float smallX;
	float temp;
	// Use this for initialization
	void Start () {
		randomNum = Mathf.Ceil (Random.value * 4);
	}
	
	// Update is called once per frame
	void Update () {
		detect = GameObject.Find("Trigger_Circle").GetComponent<Detect>().Spotting();
		//Debug.Log (detect);
		if (!detect) {
			if (i == 120) {
				randomNum = Mathf.Ceil (Random.value * 4);
				i = 0;
			}
			//Debug.Log (randomNum);
			if (randomNum == 1) {
				transform.Translate (0.02f, 0, 0);
				//Debug.Log ("Moved left");
				if (isCollision) {
					randomNum = 3;
					isCollision = false;
					//Debug.Log ("Avoided and turned right");
				}
			}
			if (randomNum == 2) {
				transform.Translate (0, 0.02f, 0);
				//Debug.Log ("Moved up");
				if (isCollision) {
					randomNum = 4;
					isCollision = false;
					//Debug.Log ("Avoided and turned down");
				}
			}
			if (randomNum == 3) {
				transform.Translate (-0.02f, 0, 0);
				//Debug.Log ("Moved right");
				if (isCollision) {
					randomNum = 1;
					isCollision = false;
					//Debug.Log ("Avoided and turned left");
				}
			}
			if (randomNum == 4) {
				transform.Translate (0, -0.02f, 0);
				//Debug.Log ("Moved down");
				if (isCollision) {
					randomNum = 2;	
					isCollision = false;
					//Debug.Log ("Avoided and turned up");
				}
			}
			i++;
			//Debug.Log (i);
			transform.rotation = Quaternion.identity;
		} else {
			Follow ();
			//isCollision = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "PathDecals" || col.gameObject.name == "TreeStumps" || col.gameObject.name == "Grass") {
			isCollision = true;
			//Debug.Log ("Collided");
		}
		if (col.gameObject.name == "Char") {
			//Debug.Log ("Attack");
			goAttack = true;
		}
		detect = false;
	}
	void Follow() {
		if (detect) {
			charX = GameObject.Find ("Char").GetComponent<Transform> ().position.x;
			charY = GameObject.Find ("Char").GetComponent<Transform> ().position.y;
			enemyX = GameObject.Find ("Enemy").GetComponent<Transform> ().position.x;
			enemyY = GameObject.Find ("Enemy").GetComponent<Transform> ().position.y;
			Xdiff = charX - enemyX;
			Ydiff = charY - enemyY;
			Debug.Log (Xdiff + "," + Ydiff);
			if (Ydiff > 0) {
				transform.Translate (0, smallY,0);
			}
			if (Ydiff < 0) {
				transform.Translate (0, -smallY,0);
			}
			if (Xdiff > 0) {
				transform.Translate (smallX, 0,0);
			}
			if (Xdiff < 0) {
				transform.Translate (-smallX, 0,0);
			}
		}
	}
}
