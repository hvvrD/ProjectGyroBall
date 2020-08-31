using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;
	[SerializeField] private Transform spawnPoint;

	private Rigidbody rb;
	private int count;

	//Tap variables
	private int TapCount;
	private float NewTime;
	private float MaxDubbleTapTime = 1f;

	//Debug
	public Vector3 accelerometer;

	private void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	private void Update()
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Ended)
			{
				TapCount += 1;
			}

			if (TapCount == 1)
			{

				NewTime = Time.time + MaxDubbleTapTime;
			}
			else if (TapCount == 2 && Time.time <= NewTime)
			{
				//Respawn
				this.transform.position = spawnPoint.position;

				TapCount = 0;
			}

		}
		if (Time.time > NewTime)
		{
			TapCount = 0;
		}
	}

	private void FixedUpdate ()
	{
		//TODO: if keyboard
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		float moveHorizontal = Input.acceleration.x;
		float moveVertical = 0f;// Input.acceleration.y;

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);

		//Debug
		accelerometer = Input.acceleration;
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	private void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();

		if (count >= 12) 
		{
			winText.text = "You Win!";
		}
	}
}