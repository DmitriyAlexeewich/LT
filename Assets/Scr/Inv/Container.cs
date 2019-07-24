using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Container : MonoBehaviour {

	public Item ItemScr;
	public Inv InvScr;
	public Image Icon;
	public Text Name;
	public Text HealthCount;
	public Text Weight;
	public Text Cost;
	public Info InfoPanel;
	public GameObject ActionPanel;
	Decode Decoder = new Decode();
	public List<int> HotContIndex;
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
		if (Decoder.Code ((int)ItemScr.ID, 0) == 1) {
			int TID = Decoder.Code (ItemScr.ID, 1) * 10;
			TID += Decoder.Code (ItemScr.ID, 2);
			for (int i = 0; i < 19; i++) {
				if (InvScr.HotList [i].ID == TID) {
					HotContIndex.Add(i);
					switch (Decoder.Code ((int)ItemScr.ID, 1)) {
						case 2:{					
							if (((TID % 10) < 5) && (InvScr.HotList [14].Engaged == true)) {
								if (InvScr.HotList [14].ItemScr.Array [16 + (TID % 10)] == (Decoder.Code (ItemScr.ID, 3))) {
									InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.CoincidenceColor;
									InvScr.HotList [i].Icon.color = InvScr.CoincidenceColor;
								}else{
									InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.UncoincidenceColor;
									InvScr.HotList [i].Icon.color = InvScr.UncoincidenceColor;
								}
							} else if (((TID % 10) > 4) && (InvScr.HotList [15].Engaged == true)) {
								if (InvScr.HotList [15].ItemScr.Array [11 + (TID % 10)] == (Decoder.Code (ItemScr.ID, 3))) {
									InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.CoincidenceColor;
									InvScr.HotList [i].Icon.color = InvScr.CoincidenceColor;
								}else{
									InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.UncoincidenceColor;
									InvScr.HotList [i].Icon.color = InvScr.UncoincidenceColor;
								}
							} else {
								InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.UncoincidenceColor;
								InvScr.HotList [i].Icon.color = InvScr.UncoincidenceColor;
							}
							break;
						}
						case 3:{
							if ((TID % 10) == 1) {
								for (int j = 17; j < 21; j++) {
									HotContIndex.Add(j-11);
									if ((InvScr.HotList [j - 11].Engaged == true) && (Decoder.Code (InvScr.HotList [j - 11].ItemScr.ID, 3) == ((int)ItemScr.Array [j]))) {
										InvScr.HotList [j - 11].gameObject.GetComponent<Image> ().color = InvScr.CoincidenceColor;
										InvScr.HotList [j - 11].Icon.color = InvScr.CoincidenceColor;
									} else if(InvScr.HotList [j - 11].Engaged == true){
										InvScr.HotList [j - 11].gameObject.GetComponent<Image> ().color = InvScr.UncoincidenceColor;
										InvScr.HotList [j - 11].Icon.color = InvScr.UncoincidenceColor;
									}
								}
								InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
								InvScr.HotList [i].Icon.color = InvScr.MouseEnterColor;
							}
							break;
						}
						default:{
							InvScr.HotList [i].gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
							InvScr.HotList [i].Icon.color = InvScr.MouseEnterColor;
							break;
						}
					}
				}
			}
		}
	}
	public void  MouseExit(){
		for (int i = 0; i < HotContIndex.Count; i++){
			InvScr.HotList [HotContIndex[i]].gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
			InvScr.HotList [HotContIndex[i]].Icon.color = InvScr.MouseExitColor;
		}
		HotContIndex.RemoveRange (0, HotContIndex.Count);
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
	}
}
