using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;



/// <summary>
/// snake3D.cs
/// 16/6/2017
/// naveensachdeva90@gmail.com
/// <para>
/// Movement of snakes using up/down/left/right arrow keys and swipe operations
/// Spawning of fruits objects
/// </para>
/// </summary>


public class snake3D : MonoBehaviour {
	
	public GameObject tailPrefab;
	public GameObject[] food;
	public Transform rBorder;
	public Transform lBorder;
	public Transform tBorder;
	public Transform bBorder;


	List<Transform> tail = new List<Transform>();

	private float speed = 0.1f;

	Vector3 vector = Vector3.forward; 
	Vector3 moveVector;

	bool eat = false;
	bool vertical = false;
	bool horizontal = true;


	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	//Vector3 lastPos;
	Quaternion lastPos;

	/// <summary>
	/// Start snake movements and fruit objects
	/// </summary>
	void Start () {
		SpawnFruit ();
		InvokeRepeating("Movement", 1.0f, speed);
	}

	/// <summary>
	/// Spawning fruit 
	/// </summary>
	public void SpawnFruit() {
		int x = (int)Random.Range (lBorder.position.x, rBorder.position.x);
		int z = (int)Random.Range (bBorder.position.z, tBorder.position.z);

		Instantiate (food[Random.Range(0,3)], new Vector3 (x, 48f,z), Quaternion.identity);
	}



	void Update () {

		#if UNITY_EDITOR || UNITY_STANDALONE //Check if we are running either in the Unity editor or in a standalone build.

		if (Input.GetKey (KeyCode.RightArrow) && horizontal) {
			horizontal = false;
			vertical = true;
			vector = Vector3.right;

			transform.localRotation = Quaternion.Euler (0, 90, 0);

			lastPos = Quaternion.Euler (0, 90, 0);
		} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
			
			horizontal = true;
			vertical = false;
			vector = new Vector3 (0, 0, 1);

			transform.localRotation = Quaternion.Euler (0, 0, 0);

			lastPos = Quaternion.Euler (0, 0, 0);

		
		} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
			
			horizontal = true;
			vertical = false;
			vector = new Vector3 (0, 0, -1);

		
			transform.localRotation = Quaternion.Euler(0, -180, 0);

			lastPos = Quaternion.Euler (0, -180, 0);
		} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
			
			horizontal = false;
			vertical = true;
			vector = -Vector3.right;

			transform.localRotation = Quaternion.Euler (0, -90, 0);
			lastPos = Quaternion.Euler (0,-90, 0);
		}


		Swipe_MouseClick();


		#else                                //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		Swipe();
		#endif

		moveVector = vector / 3f;

	}

	/// <summary>
	/// mouse swipe operations
	/// </summary>
	void Swipe_MouseClick(){

		if(Input.GetMouseButtonDown(0))     {
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);     
		}    
		if(Input.GetMouseButtonUp(0))     {
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y); 

			//normalize the 2d vector
			currentSwipe.Normalize();

			//swipe upwards
			if(currentSwipe.y > 0 &&  currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f && vertical)  {
				Debug.Log("up swipe");       

				horizontal = true;
				vertical = false;
				vector = new Vector3 (0, 0, 1);
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				lastPos = Quaternion.Euler (0, 0, 0);
			}
			//swipe down
			if(currentSwipe.y < 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && vertical)    {       
				Debug.Log("down swipe");   

				horizontal = true;
				vertical = false;
				vector = new Vector3 (0, 0, -1);
				transform.localRotation = Quaternion.Euler(0, -180, 0);
				lastPos = Quaternion.Euler (0, -180, 0);
			}
			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f &&  currentSwipe.y < 0.5f && horizontal)   {        
				Debug.Log("left swipe");       

				horizontal = false;
				vertical = true;
				vector = -Vector3.right;
				transform.localRotation = Quaternion.Euler (0, -90, 0);
				lastPos = Quaternion.Euler (0,-90, 0);
			}
			//swipe right
			if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && horizontal)     {        
				Debug.Log("right swipe");    

				horizontal = false;
				vertical = true;
				vector = Vector3.right;
				transform.localRotation = Quaternion.Euler (0, 90, 0);
				lastPos = Quaternion.Euler (0, 90, 0);
			}   
		}

	}


	/// <summary>
	/// touch swipe operation on smartphones
	/// </summary>
	public void Swipe()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);

				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe upwards
				if(currentSwipe.y > 0 &&  currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f && vertical)
				{
					Debug.Log("up swipe");

					horizontal = true;
					vertical = false;
					vector = new Vector3 (0, 0, 1);

					transform.localRotation = Quaternion.Euler (0, 0, 0);

					lastPos = Quaternion.Euler (0, 0, 0);
				}
				//swipe down
				if(currentSwipe.y < 0 &&  currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f && vertical)
				{
					Debug.Log("down swipe");
					horizontal = true;
					vertical = false;
					vector = new Vector3 (0, 0, -1);


					transform.localRotation = Quaternion.Euler(0, -180, 0);

					lastPos = Quaternion.Euler (0, -180, 0);
				}
				//swipe left 
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f &&  currentSwipe.y < 0.5f && horizontal)
				{
					Debug.Log("left swipe");

					horizontal = false;
					vertical = true;
					vector = -Vector3.right;

					transform.localRotation = Quaternion.Euler (0, -90, 0);
					lastPos = Quaternion.Euler (0,-90, 0);
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f &&  currentSwipe.y < 0.5f && horizontal)
				{
					horizontal = false;
					vertical = true;
					vector = Vector3.right;

					transform.localRotation = Quaternion.Euler (0, 90, 0);

					lastPos = Quaternion.Euler (0, 90, 0);

					Debug.Log("right swipe");
				}
			}
		}
	}
	/// <summary>
	/// Snake movements handling
	/// </summary>
	int count = 0;

	public static bool gameOverBool;
	void Movement() {

		if (count < 3) {
			count++;
			eat = true;
		}
		if (!gameOverBool) {
			Vector3 ta = transform.position;
			if (eat) {
			
				if (speed > 0.002) {
					speed = speed - 0.002f;
				}
				GameObject g = (GameObject)Instantiate (tailPrefab, ta, Quaternion.identity);

				tail.Insert (0, g.transform);
//			Debug.Log(speed);
				eat = false;
			} else if (tail.Count > 0) {
				tail.Last ().position = ta;
				tail.Last ().rotation = lastPos;
				tail.Insert (0, tail.Last ());
				tail.RemoveAt (tail.Count - 1);
			}
			transform.parent.Translate (moveVector);
		}
	}


	/// <summary>
	/// collision trigger with fruit objects,Obstacle and boundaries 
	/// </summary>
	void OnTriggerEnter(Collider c) {

		if (c.name.StartsWith("food")) {
			eat = true;
			Destroy(c.gameObject);
			SpawnFruit();
			UIController.Total_Score += 10;

			GameObject.Find ("UIController").SendMessage ("UpdateScore");
			GameObject.Find ("fruitEatSfx").GetComponent<AudioSource> ().Play ();
		}
		else {
			gameOverBool = true;
			GameObject.Find ("bgMusic").GetComponent<AudioSource> ().Stop ();
			GameObject.Find ("damageSfx").GetComponent<AudioSource> ().Play ();
			GameObject.Find ("UIController").SendMessage ("GameOver");
		}
	}


}
