using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class WeaponController : MonoBehaviour {

	public Inv InvScr;
	public AudioListener ast;
	public Weapon WeaponScr;
	public KeyCode OnOffFirstWeapon;
	public KeyCode OnOffSecondtWeapon;
	public List<WeaponCode> FirstWeaponList;
	public List<WeaponCode> SecondWeaponList;
	public List<SilencerBarrel> SilencerBarrelList;
	public List<FlashlightLaser> FlashlightLaserList;
	public List<BipodsHandel> BipodsHandelList;
	public List<Scope> ScopeList;
	Decode Decoder = new Decode();
	public int UsingWeapon;
	int LastWeapon;
	void Update(){
		if (Input.GetKeyDown (OnOffFirstWeapon)) {
			if (UsingWeapon == 1) {
				UsingWeapon = 0;
			} else if(InvScr.HotList[14].Engaged == true){
				if ((InvScr.HotList [15].Engaged == true) && (UsingWeapon == 2)) {
					WeaponStatsSave ();
				}
				UsingWeapon = 1;
				WeaponStatsAdd (14);
			}
		}
		if (Input.GetKeyDown (OnOffSecondtWeapon)) {
			if (UsingWeapon == 2) {
				UsingWeapon = 0;
			} else if(InvScr.HotList[15].Engaged == true){
				if ((InvScr.HotList [14].Engaged == true) && (UsingWeapon == 1)) {
					WeaponStatsSave ();
				}
				UsingWeapon = 2;
				WeaponStatsAdd (15);
			}
		}
		if (Input.GetKeyDown (InvScr.InvOnOffKey)) {
			if (InvScr.MainInv.activeSelf) {
				LastWeapon = UsingWeapon;
				UsingWeapon = 0;
			} else {
				if ((LastWeapon == 1) && (InvScr.HotList [14].Engaged == true)) {
					UsingWeapon = 1;
					WeaponStatsAdd (14);
				} else if ((LastWeapon == 2) && (InvScr.HotList [15].Engaged == true)) {
					UsingWeapon = 2;
					WeaponStatsAdd (15);
				} else if (InvScr.HotList [14].Engaged == true) {
					UsingWeapon = 1;
					WeaponStatsAdd (14);
				} else if (InvScr.HotList [15].Engaged == true) {
					UsingWeapon = 2;
					WeaponStatsAdd (15);
				} else
					UsingWeapon = 0;
			}
		}
		if ((UsingWeapon == 0)&&(WeaponScr.enabled == true)&&(WeaponScr.WeaponCodeScr.gameObject.activeSelf)) {
			WeaponStatsSave ();
		}
		if ((WeaponScr.enabled == false)&&(UsingWeapon != 0)&&(!WeaponScr.WeaponCodeScr.AnimComp.IsPlaying ("Draw"))) {
			WeaponScr.enabled = true;
		}
		if ((WeaponScr.WeaponCodeScr != null)&&(UsingWeapon == 0)&&(!WeaponScr.WeaponCodeScr.AnimComp.IsPlaying ("-Draw"))) {
			WeaponScr.WeaponCodeScr.gameObject.SetActive (false);
			WeaponScr.WeaponCodeScr = null;
		}
	}
	void WeaponStatsSave(){
		int HID = new int ();
		if (Decoder.Code (WeaponScr.WeaponCodeScr.ID, 2) == 1)
			HID = 14;
		else
			HID = 15;
		InvScr.HotList [HID].ItemScr.Array [0] = WeaponScr.Health;
		InvScr.HotList [HID].ItemScr.Array [6] = WeaponScr.Collar;
		InvScr.HotList [HID].ItemScr.Array [10] = WeaponScr.Volume;
		InvScr.HotList [HID].ItemScr.Array [11] = WeaponScr.Damage;
		InvScr.HotList [HID].ItemScr.Array [12] = WeaponScr.RangeX;
		InvScr.HotList [HID].ItemScr.Array [13] = WeaponScr.Collar;
		InvScr.HotList [HID].ItemScr.Array [14] = WeaponScr.RangeY;
		InvScr.HotList [HID].ItemScr.Array [15] = WeaponScr.Brightness;
		InvScr.HotList [HID].ItemScr.Array [16] = WeaponScr.UsingMod;
		for (int i = 0; i < InvScr.ItemList.Count; i++) {
			if (InvScr.ItemList [i].ID == WeaponScr.AmmoID){
				if (WeaponScr.AmmoSize > 0) {
					InvScr.ItemList [i].Array [3] = (float)WeaponScr.AmmoSize;
				} else {
					InvScr.ItemList.Remove (InvScr.ItemList [i]);
				}
			}
		}
		for (int i = 0; i < SilencerBarrelList.Count; i++)
			SilencerBarrelList [i].gameObject.SetActive (false);
		for (int i = 0; i < FlashlightLaserList.Count; i++)
			FlashlightLaserList [i].gameObject.SetActive (false);
		for (int i = 0; i < BipodsHandelList.Count; i++)
			BipodsHandelList [i].gameObject.SetActive (false);
		for (int i = 0; i < ScopeList.Count; i++)
			ScopeList [i].gameObject.SetActive (false);
		WeaponScr.WeaponCodeScr.AnimComp.Play ("-Draw");
		WeaponScr.ShootParticle.SetActive (false);
		WeaponScr.SmokeParticle.SetActive (false);
		WeaponScr.enabled = false;
	}
	void WeaponStatsAdd(int HID){
		if (HID == 14) {
			for (int i = 0; i < FirstWeaponList.Count; i++)
				if (FirstWeaponList [i].ID == InvScr.HotList [HID].ItemScr.ID)
					WeaponScr.WeaponCodeScr = FirstWeaponList [i];
			AddSilencer (6);
			AddBipods (7);
			AddFlashlight (8);
			AddScope (9);
		}
		if (HID == 15) {
			for (int i = 0; i < SecondWeaponList.Count; i++)
				if (SecondWeaponList [i].ID == InvScr.HotList [HID].ItemScr.ID)
					WeaponScr.WeaponCodeScr = SecondWeaponList [i];
			AddSilencer (10);
			AddBipods (11);
			AddFlashlight (12);
			AddScope (13);
		}
		WeaponScr.Health = InvScr.HotList [HID].ItemScr.Array[0];
		if (InvScr.HotList [HID].ItemScr.Array [3] == 0)
			WeaponScr.TypeOfShooting = false;
		else
			WeaponScr.TypeOfShooting = true;
		WeaponScr.ModsOfShooting = (int)InvScr.HotList [HID].ItemScr.Array [4];
		WeaponScr.CollarSize = (int)InvScr.HotList [HID].ItemScr.Array [5];
		WeaponScr.Collar = (int)InvScr.HotList [HID].ItemScr.Array [6];
		WeaponScr.AmmoID = (int)InvScr.HotList [HID].ItemScr.Array [8];
		for (int i = 0; i < InvScr.ItemList.Count; i++) {
			if (InvScr.ItemList [i].ID == WeaponScr.AmmoID)
				WeaponScr.AmmoSize += (int)InvScr.ItemList [i].Array [3];
		}
		WeaponScr.ShootTime = InvScr.HotList [HID].ItemScr.Array [9];
		WeaponScr.Volume = InvScr.HotList [HID].ItemScr.Array [10];
		WeaponScr.Damage = InvScr.HotList [HID].ItemScr.Array [11];
		WeaponScr.RangeX = InvScr.HotList [HID].ItemScr.Array [12];
		WeaponScr.RangeY = InvScr.HotList [HID].ItemScr.Array [13];
		WeaponScr.Distance = InvScr.HotList [HID].ItemScr.Array [14];
		WeaponScr.Brightness = InvScr.HotList [HID].ItemScr.Array [15];
		WeaponScr.UsingMod = (int)InvScr.HotList [HID].ItemScr.Array [16];
		WeaponScr.ShootAudio = Resources.Load<AudioClip>("Sounds/"+InvScr.HotList [HID].ItemScr.ID.ToString()+"Shoot");
		WeaponScr.SilenceShootAudio = Resources.Load<AudioClip>("Sounds/"+(InvScr.HotList [HID].ItemScr.ID).ToString()+"SilenceShoot");
		WeaponScr.ReloadSound = Resources.Load<AudioClip>("Sounds/"+InvScr.HotList [HID].ItemScr.ID.ToString()+"Reload");
		WeaponScr.WeaponCodeScr.Holsted = false;
		WeaponScr.WeaponCodeScr.gameObject.SetActive (true);
		WeaponScr.WeaponCodeScr.AnimComp.Play ("Draw");
	}
	void AddSilencer(int j){
		if (InvScr.HotList [j].Engaged == true) {
			for (int i = 0; i < SilencerBarrelList.Count; i++) {
				if (SilencerBarrelList [i].ID == InvScr.HotList [j].ItemScr.ID) {
					SilencerBarrelList [i].gameObject.SetActive (true);
					WeaponScr.SilencerBarrelScr = SilencerBarrelList [i];
					SilencerBarrelList [i].gameObject.transform.parent = WeaponScr.WeaponCodeScr.SilencerBarrelBone.transform;
					SilencerBarrelList [i].gameObject.transform.localRotation = Quaternion.Euler (SilencerBarrelList [i].OriginalRotation);
					SilencerBarrelList [i].gameObject.transform.localPosition = new Vector3 (0f, 0f, 0f);
					break;
				}
			}
		}
	}
	void AddBipods(int j){
		if (InvScr.HotList [j].Engaged == true) {
			for (int i = 0; i < BipodsHandelList.Count; i++) {
				if (BipodsHandelList [i].ID == InvScr.HotList [j].ItemScr.ID) {
					BipodsHandelList [i].gameObject.SetActive (true);
					WeaponScr.BipodsHandelScr = BipodsHandelList [i];
					BipodsHandelList [i].gameObject.transform.parent = WeaponScr.WeaponCodeScr.BipodsHandelBone.transform;
					BipodsHandelList [i].gameObject.transform.localRotation = Quaternion.Euler (BipodsHandelList [i].OriginalRotation);
					BipodsHandelList [i].gameObject.transform.localPosition = new Vector3 (0f, 0f, 0f);
					break;
				}
			}
		}
	}
	void AddFlashlight(int j){
		if (InvScr.HotList [j].Engaged == true) {
			for (int i = 0; i < FlashlightLaserList.Count; i++) {
				if (FlashlightLaserList [i].ID == InvScr.HotList [j].ItemScr.ID) {
					FlashlightLaserList [i].gameObject.SetActive (true);
					WeaponScr.FlashlightLaserScr = FlashlightLaserList [i];
					FlashlightLaserList [i].gameObject.transform.parent = WeaponScr.WeaponCodeScr.FlashlightLaseBone.transform;
					FlashlightLaserList [i].gameObject.transform.localRotation = Quaternion.Euler (FlashlightLaserList [i].OriginalRotation);
					FlashlightLaserList [i].gameObject.transform.localPosition = new Vector3 (0f, 0f, 0f);
					break;
				}
			}
		}
	}
	void AddScope(int j){
		if (InvScr.HotList [j].Engaged == true) {
			for (int i = 0; i < ScopeList.Count; i++) {
				if (ScopeList [i].ID == InvScr.HotList [j].ItemScr.ID) {
					ScopeList [i].gameObject.SetActive (true);
					WeaponScr.ScopeScr = ScopeList [i];
					ScopeList [i].gameObject.transform.parent = WeaponScr.WeaponCodeScr.ScopeBone.transform;
					ScopeList [i].gameObject.transform.localRotation = Quaternion.Euler (ScopeList [i].OriginalRotation);
					ScopeList [i].gameObject.transform.localPosition = new Vector3 (0f, 0f, 0f);
					break;
				}
			}
		}
	}
}
