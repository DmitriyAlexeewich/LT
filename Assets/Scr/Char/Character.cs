using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
public class Character : MonoBehaviour {

	public bool LockCursor;
	public bool LockPos;
	public float MoveSpeed;
	public float SprinFactor;
	public float SenseX;
	public float SenseY;
	public float ScopeSenseX;
	public float ScopeSenseY;
	public float MaxAxesY=60;
	public float MinAxesY = -60;
	public Camera Cam;
	public Animator FPSActorControlle;
	public float X = 0;
	public float Y = 0;
	public bool Seat;
	public bool Lie;
	public List<float> Stats;
	public GameObject StatsPanel;
	public Text ThirstText;
	public Text HungerText;
	public Text StaminaText;
	public Text BloodText;
	public KeyCode ForwardKey;
	public KeyCode BackwardKey;
	public KeyCode LeftKey;
	public KeyCode RightKey;
	public KeyCode SeatKey;
	public KeyCode SprintLieKey;
	public KeyCode ShowStats;
	public NoiseAndGrain Noise;
	Quaternion originalX;
	Quaternion originalY;
	Vector3 OriginalCamPosition;
	float StatsTimer;
	void Start(){
		if (this.gameObject.GetComponent<Rigidbody>()) {
			this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
		}
		originalX = this.gameObject.transform.localRotation;
		originalY = Cam.gameObject.transform.localRotation;
		OriginalCamPosition = Cam.gameObject.transform.localPosition;
	}
	void Update () {
		if(Input.GetKeyDown(ShowStats)){
			if (StatsPanel.activeSelf)
				StatsPanel.SetActive (false);
			else
				StatsPanel.SetActive (true);
		}
		ThirstText.text = (100 - (int)Stats[0]).ToString()+"%";
		HungerText.text = (100 - (int)Stats[1]).ToString()+"%";
		StaminaText.text = (100 - (int)Stats[8]).ToString()+"%";
		BloodText.text = (100 - (int)Stats[9]).ToString()+"%";
		if (StatsPanel.activeSelf)
			StatsTimer += Time.deltaTime;
		else
			StatsTimer = 0;
		if(StatsTimer > 3f)
			StatsPanel.SetActive (false);
		if (LockCursor == true) {
			Cursor.lockState = CursorLockMode.Locked;
			if (Input.GetMouseButton (1) && !Input.GetKey (SprintLieKey)) {
				X += Input.GetAxis ("Mouse X") * ScopeSenseX;
				Y += Input.GetAxis ("Mouse Y") * ScopeSenseY;
			} else {
				X += Input.GetAxis ("Mouse X") * SenseX;
				Y += Input.GetAxis ("Mouse Y") * SenseY;
			}
			Y = ClampAngle (Y, MinAxesY, MaxAxesY);
			this.gameObject.transform.localRotation = originalX * Quaternion.AngleAxis (X, Vector3.up);
			Cam.gameObject.transform.localRotation = originalY * Quaternion.AngleAxis (-Y, Vector3.right);
		} else {
			Cursor.lockState = CursorLockMode.None;
		}
		if (LockPos == true) {
			if (Input.GetAxis ("Tilt") != 0) {
				//Cam.gameObject.transform.localPosition = new Vector3 (1f * Input.GetAxis ("Tilt"), Cam.gameObject.transform.localPosition.y, Cam.gameObject.transform.localPosition.z);
				Cam.gameObject.transform.localRotation = originalY * Quaternion.AngleAxis (-20 * Input.GetAxis ("Tilt"), Vector3.forward) * Quaternion.AngleAxis (-Y, Vector3.right);
			} else {
				Cam.gameObject.transform.localPosition = OriginalCamPosition;
			}
			float Speed = 1f;
			if (Input.GetKey (SprintLieKey) && !Input.GetKey (SeatKey))
				Speed = SprinFactor;
			if (Input.GetMouseButton (1) && !Input.GetKey (SprintLieKey))
				Speed = 0.5f;
			if ((!Input.GetKey (ForwardKey)) || (!Input.GetKey (BackwardKey)) || (!Input.GetKey (RightKey)) || (!Input.GetKey (LeftKey))) {
				FPSActorControlle.SetFloat ("Horizontal", 0f);
				FPSActorControlle.SetFloat ("Vertical", 0f);
			}
			if (Input.GetKey (ForwardKey) && (Stats [8] < 100)) {
				this.transform.position += (this.transform.forward * MoveSpeed * Speed * Time.deltaTime);
				FPSActorControlle.SetFloat ("Vertical", 1f);
			}
			if (Input.GetKey (BackwardKey) && (Stats [8] < 100)) {
				this.transform.position -= (this.transform.forward * MoveSpeed * Speed * Time.deltaTime);
				FPSActorControlle.SetFloat ("Vertical", -1f);
			}
			if (Input.GetKey (RightKey) && (Stats [8] < 100)) {
				this.transform.position += (this.transform.right * MoveSpeed * Speed * Time.deltaTime);
				FPSActorControlle.SetFloat ("Horizontal", 1f);
			}
			if (Input.GetKey (LeftKey) && (Stats [8] < 100)) {
				this.transform.position -= (this.transform.right * MoveSpeed * Speed * Time.deltaTime);
				FPSActorControlle.SetFloat ("Horizontal", -1f);
			}
		} else {
			Cam.gameObject.transform.localRotation = originalY * Quaternion.AngleAxis (0, Vector3.forward) * Quaternion.AngleAxis (-Y, Vector3.right);
		}
		if ((Input.GetKey (ForwardKey) || Input.GetKey (BackwardKey) || Input.GetKey (RightKey) || Input.GetKey (LeftKey)) && (Stats [8] < 100)) {
			if(Input.GetKey (SprintLieKey))
				Stats[8] += 0.8f * Time.deltaTime;
			else
				Stats[8] += 0.4f * Time.deltaTime;
		}
		float n = new float ();
		if (Stats [0] < 100)
			Stats [0] += 0.01f * Time.deltaTime;
		else
			n += 0.25f;
		if (Stats [1] < 100)
			Stats [1] += 0.007f * Time.deltaTime;
		else
			n += 0.25f;
		if (Stats [2] < 100) {
			if (Stats [2] > 0)
				Stats [2] += 0.01f * Time.deltaTime;
		}else
			n += 0.25f;
		if (Stats [3] < 100) {
			if (Stats [3] > 0)
				Stats [3] += 0.01f * Time.deltaTime;
		}else
			n += 0.25f;
		if (Stats [4] == 1)
			Stats [10] = 1;
		if (Stats [5] > 100)
			Stats [10] = 1;
		if (Stats [6] < 100) {
			if(Stats [6] > 0)
				Stats [6] += 0.03f * Time.deltaTime;
		}
		if (Stats [7] < 100) {
			if (Noise.intensityMultiplier < Stats [7] / 20)
				Noise.intensityMultiplier += (Stats [7] / 20) * Time.deltaTime;
		} else {
			Stats [10] = 1;
		}
		if(Stats[8] > 0)
			Stats[8] -= (0.4f-(0.02f*(Stats[0]+Stats[1]))) * Time.deltaTime;
		if ((Stats [9] < 100) && (n > 0))
			Stats [9] += (Stats [9] / 100) * n * Time.deltaTime;
	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
