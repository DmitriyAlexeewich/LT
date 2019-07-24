using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SortByStat : MonoBehaviour {

	public MainSort MainSortScr;
	public int StatPos; // 0-Health/Count 1-Weaight 2-Cost
	public void OnClick(){
		for (int i = 0; i < MainSortScr.InvScr.ItemPanel.transform.childCount; i++) {
			Destroy (MainSortScr.InvScr.ItemPanel.transform.GetChild (i).gameObject);
		}
		for (int i = 1; i < MainSortScr.IDList.Count; i++) {
			Item cur = MainSortScr.IDList [i];
			int j = i;
			while (j > 0 && cur.Array [StatPos] < MainSortScr.IDList [j - 1].Array [StatPos]) {
				MainSortScr.IDList [j] = MainSortScr.IDList [j - 1];
				j--;
			}
			MainSortScr.IDList [j] = cur;
		}
		if (MainSortScr.TypeSort == 0) {
			if (MainSortScr.Order == false) {
				for (int i = 0; i < MainSortScr.IDList.Count; i++) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			} else {
				for (int i = MainSortScr.IDList.Count-1; i > -1; i--) {
					MainSortScr.InvScr.Create (MainSortScr.InvScr.ItemList.IndexOf (MainSortScr.IDList [i]));
				}
			}
		} else {
			for (int i = 0; i < MainSortScr.TypeSortlist.Count; i++) {
				if (MainSortScr.TypeSortlist [i].ID == MainSortScr.TypeSort) {
					MainSortScr.TypeSortlist [i].OnClick ();
					break;
				}
			}
		}
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseExitColor;
	}
}
