using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime : MonoBehaviour {

	public GameObject SunAdnMoon;
	public Light SunLight;
	public float TimeArg;
	public float SunArg;
	public int DTime;
	public bool day;
	void Update () {
		SunAdnMoon.transform.rotation *= Quaternion.AngleAxis (TimeArg*Time.deltaTime, Vector3.left);
		Debug.Log (Mathf.Abs(SunAdnMoon.transform.rotation.x));
		if (Mathf.Abs (SunAdnMoon.transform.rotation.x) < 0.7f) {
			if ((SunLight.intensity < 1) && (day == false)) {
				SunLight.intensity += Time.deltaTime / SunArg;
			}
		} else {
			SunLight.intensity -= Time.deltaTime / SunArg;
			day = true;
		}
		if ((Mathf.Abs (SunAdnMoon.transform.rotation.x) > 0f) && (Mathf.Abs (SunAdnMoon.transform.rotation.x) < 0.01f))
			day = false;
		if ((SunLight.intensity > 0) && (SunLight.intensity < 0.2f) && (day == false))
			DTime = 0;
		if (SunLight.intensity > 0.2f)
			DTime = 1;
		if ((SunLight.intensity > 0) && (SunLight.intensity < 0.2f) && (day == true))
			DTime = 2;
		if (SunLight.intensity == 0f)
			DTime = 3;
			
	}
}
