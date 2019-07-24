using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SortCancel : MonoBehaviour {

	public MainSort MainSortScr;
	public void OnClick(){
		for (int i = 0; i < MainSortScr.InvScr.ItemPanel.transform.childCount; i++) {
			Destroy (MainSortScr.InvScr.ItemPanel.transform.GetChild (i).gameObject);
		}
		if(MainSortScr.IDList.Count > 0)
			MainSortScr.IDList.RemoveRange (0, MainSortScr.IDList.Count);
		for (int i = 0; i < MainSortScr.InvScr.ItemList.Count; i++) {
			MainSortScr.InvScr.Create (i);
			MainSortScr.IDList.Add (MainSortScr.InvScr.ItemList [i]);
		}
		MainSortScr.TypeSort = 0;
		MainSortScr.Order = false;
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = MainSortScr.InvScr.MouseExitColor;
	}
}
