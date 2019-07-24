using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesController : MonoBehaviour {

	public Inv InvScr;
	public List<Head> HeadList;
	public List<Mask> MaskList;
	public List<Eyes> EyesList;
	public List<Body> BodyList;
	public List<Legs> LegsList;
	public List<Boots> BootsList;
	public GameObject DefBoots;
	public KeyCode MaskKey;
	public KeyCode HeadKey;
	public KeyCode EyesKey;
	bool OnOff;
	void Update(){
		if (!InvScr.MainInv.activeSelf) {
			if (OnOff == false) {
				if (InvScr.HotList [4].Engaged == true) {
					for (int i = 0; i < LegsList.Count; i++) {
						if (InvScr.HotList [4].ItemScr.ID == LegsList [i].ID) {
							LegsList [i].gameObject.SetActive (true);
							break;
						}
					}
				}
				if (InvScr.HotList [5].Engaged == true) {
					for (int i = 0; i < BootsList.Count; i++) {
						if (InvScr.HotList [5].ItemScr.ID == BootsList [i].ID) {
							BootsList [i].gameObject.SetActive (true);
							DefBoots.SetActive (false);
							break;
						}
					}
				} else {
					DefBoots.SetActive (true);
				}
				OnOff = true;
			}
		} else {
			for (int i = 0; i < LegsList.Count; i++) {
				LegsList [i].gameObject.SetActive (false);
			}
			for (int i = 0; i < BootsList.Count; i++) {
				BootsList [i].gameObject.SetActive (false);
				DefBoots.SetActive (true);
			}
			OnOff = false;
		}
	}
}
