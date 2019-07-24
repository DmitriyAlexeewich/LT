using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class Eyes : MonoBehaviour {

	public ClothesController Controller;
	public NoiseAndGrain NoiseEffect;
	public ColorCorrectionCurves ColorEffect;
	public Light LightEffect;
	public GameObject ScopeImg;
	void Update () {
		if (Input.GetKeyDown (Controller.EyesKey)) {
			if (ScopeImg.activeSelf) {
				ScopeImg.SetActive (false);
				NoiseEffect.enabled = false;
				ColorEffect.enabled = false;
				LightEffect.enabled = false;
			} else {
				if (Controller.InvScr.HotList [1].Engaged == true) {
					ScopeImg.SetActive (true);
					NoiseEffect.enabled = true;;
					ColorEffect.enabled = true;
					LightEffect.enabled = true;
				}
			}
		}
		if (Input.GetKeyDown (Controller.InvScr.InvOnOffKey)) {
			if(!Controller.InvScr.MainInv.activeSelf){
				if (ScopeImg.activeSelf) {
					ScopeImg.SetActive (false);
					NoiseEffect.enabled = false;
					ColorEffect.enabled = false;
					LightEffect.enabled = false;
				}
			}
		}
	}
}
