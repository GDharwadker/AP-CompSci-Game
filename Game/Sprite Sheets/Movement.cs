using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed = 10f;
	private bool up, left, right, down;
	private int delay = 0;
	public Texture[] textures;
	public int currentTexture = 0;
	private bool isAttacked = false;
	private int health = 0;
	public GUIStyle st = new GUIStyle();
	private bool collided = false;
	private Rigidbody2D rBody;
	public Vector2 y = new Vector2(0,-1);
	public Vector2 x = new Vector2(-1,0);
	public int dragNum = 100;
	public double seconds = 0.1f;
	public int debuff = 25;


	void Start() {
		health = 100;
		rBody= GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		float movementx = speed;
		float movementy = speed; 
		//transform.rotation = Quaternion.identity;

		CheckUpdate ();
		if (Input.GetKey (KeyCode.W) && up) {
			transform.Translate (0, movementy, 0);
			/*currentTexture %= textures.Length;
			GetComponent<Renderer> ().material.mainTexture = textures [currentTexture];
			if (delay > 3) {
				currentTexture++;
				delay = 0;
			}
			delay++;
		} else {
			GetComponent<Renderer> ().material.mainTexture = textures [0];*/
		}
		if (Input.GetKey(KeyCode.S) && down) {
			transform.Translate (0,-movementy,0);
		}
		if (Input.GetKey(KeyCode.D) && right) {
			transform.Translate (-movementx,0,0);
		}
		if (Input.GetKey (KeyCode.A) && left) {
			transform.Translate (movementx, 0, 0);
		} 
	}

	void CheckUpdate() {

		if(Input.GetKey(KeyCode.W)){
			up = true;
			left = right = down = false;
		}
		if(Input.GetKey(KeyCode.A)){
			left = true;
			up = right = down = false;
		}
		if(Input.GetKey(KeyCode.D)){
			right = true;
			up = left = down = false;
		}
		if(Input.GetKey(KeyCode.S)){
			down = true;
			up = left = right = false;
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Enemy") {
			//Debug.Log("Attacked: " + health);
			StartCoroutine(ColorChange());
			health -= debuff;
				if (up) {
					rBody.AddForce(y, ForceMode2D.Impulse);
					rBody.drag = dragNum;
				}
				if (down) {
					rBody.AddForce(-y, ForceMode2D.Impulse);
					rBody.drag = dragNum;
				}
				if (left) {
					rBody.AddForce(x, ForceMode2D.Impulse);
					rBody.drag = dragNum;
				}
				if (right) {
					rBody.AddForce(-x, ForceMode2D.Impulse);
					rBody.drag = dragNum;
				}


		}
	}

	IEnumerator ColorChange() {
		GetComponent<SpriteRenderer>().color = Color.red;
		//Debug.Log ("Waiting");
		yield return new WaitForSeconds((float)seconds);
		//Debug.Log ("Finished");
		GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds((float)seconds);
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds((float)seconds);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	void OnGUI() {
		st.fontSize = 50;
		st.normal.textColor = Color.white;
		GUI.Label(new Rect(0,0,100,100), "Life: " + health.ToString(), st);

		if (health == 0) {
			st.fontSize = 250;
			GUI.Label(new Rect(0,200,100,100), "GAME OVER", st);
		}
	}


}
