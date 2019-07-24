using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNPC : MonoBehaviour {

	public int Mood;
	public int Jargon;
	public float MoodTimer;
	float Timer;
	void Update(){
		Timer += Time.deltaTime;
		if (Timer >= MoodTimer) {
			Mood = Random.Range (1, 3);
			Timer = 0;
		}
	}
}
