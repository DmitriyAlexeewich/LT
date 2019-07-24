using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {

	public int ID;
	public float Health;
	public float PsyProtection;
	public float RadiationProtection;
	public float ToxicProtection;
	public Vector3 Angles;
	public GameObject GasMaskImg;
	public ClothesController Controller;
	public AudioSource AudioComponent;
	public AudioClip GasMaskBreath;
	void Update(){
		if (Input.GetKeyDown (Controller.MaskKey)) {
			if (AudioComponent.enabled == true) {
				AudioComponent.enabled = false;
				GasMaskImg.SetActive (false);
			} else {
				if (Controller.InvScr.HotList [2].Engaged == true) {
					AudioComponent.enabled = true;
					GasMaskImg.SetActive (true);
					AudioComponent.clip = GasMaskBreath;
					AudioComponent.Play ();
				}
			}
		}
		if (Input.GetKeyDown (Controller.InvScr.InvOnOffKey)) {
			if(!Controller.InvScr.MainInv.activeSelf){
				if (AudioComponent.enabled == true) {
					GasMaskImg.SetActive (false);
					AudioComponent.enabled = false;
				}
			}
		}
	}
}
