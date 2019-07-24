using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SortByType : MonoBehaviour {

	public MainSort MainSortScr;
	public int ID;
	Decode Decoder = new Decode();
	public void OnClick(){
		int Length = new int ();
		if ((ID / 100) > 0)
			Length = 3;
		else
			Length = 2;
		for (int i = 0; i < MainSortScr.InvScr.ItemPanel.transform.childCount; i++) {
			Destroy (MainSortScr.InvScr.ItemPanel.transform.GetChild (i).gameObject);
		}
		if (MainSortScr.Order == false) {
			for (int i = 0; i < MainSortScr.IDList.Count; i++) {
				if (Decoder.IntLength (MainSortScr.IDList [i].ID, Length) == ID) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			}
		} else {
			for (int i = MainSortScr.IDList.Count-1; i > -1; i--) {
				if (Decoder.IntLength (MainSortScr.IDList [i].ID, Length) == ID) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			}
		}
		MainSortScr.TypeSort = ID;
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseExitColor;
	}
}
