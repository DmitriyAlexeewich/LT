using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SortOrder : MonoBehaviour {

	public MainSort MainSortScr;
	public void OnClick(){
		for (int i = 0; i < MainSortScr.InvScr.ItemPanel.transform.childCount; i++) {
			Destroy (MainSortScr.InvScr.ItemPanel.transform.GetChild (i).gameObject);
		}
		if (MainSortScr.Order == true) {
			if (MainSortScr.TypeSort == 0) {
				for (int i = 0; i < MainSortScr.IDList.Count; i++) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			} else {
				for (int i = 0; i < MainSortScr.TypeSortlist.Count; i++) {
					if (MainSortScr.TypeSortlist [i].ID == MainSortScr.TypeSort) {
						MainSortScr.TypeSortlist [i].OnClick ();
						break;
					}
				}
			}
			MainSortScr.Order = false;
		} else {
			if (MainSortScr.TypeSort == 0) {
				for (int i = MainSortScr.IDList.Count - 1; i > -1; i--) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			} else {
				for (int i = 0; i < MainSortScr.TypeSortlist.Count; i++) {
					if (MainSortScr.TypeSortlist [i].ID == MainSortScr.TypeSort) {
						MainSortScr.TypeSortlist [i].OnClick ();
						break;
					}
				}
			}
			MainSortScr.Order = true;
		}
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseExitColor;
	}
}
