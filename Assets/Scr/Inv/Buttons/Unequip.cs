using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unequip : MonoBehaviour {

	public HotContainer HotContainerScr;
	public Inv InvScr;
	Decode Decoder = new Decode();
	public void OnClick(){
		if(HotContainerScr.Engaged == true){
			InvScr.AudioComponent.PlayOneShot (InvScr.UnequipSound);
			InvScr.ItemList.Add (HotContainerScr.ItemScr);
			InvScr.Create (InvScr.ItemList.Count - 1);
			int TID = Decoder.Code (HotContainerScr.ItemScr.ID, 1) * 10;
			TID += Decoder.Code (HotContainerScr.ItemScr.ID, 2);
			if ((TID / 10) == 3) {
				if ((TID % 10) == 1) {
					for (int j = 6; j < 10; j++) {
						if (InvScr.HotList [j].Engaged == true) {
							InvScr.ItemList.Add (InvScr.HotList [j].ItemScr);
							InvScr.Create (InvScr.ItemList.Count - 1);
							InvScr.HotList [j].Engaged = false;
							InvScr.HotList [j].Icon.sprite = InvScr.HotList [j].DefaultIcon;
							InvScr.HotList [j].ItemScr = null;
							InvScr.HotList [j].Health.text = null;
						}
					}
				} else if ((TID % 10) == 2){
					for (int j = 10; j < 14; j++) {
						if (InvScr.HotList [j].Engaged == true) {
							InvScr.ItemList.Add (InvScr.HotList [j].ItemScr);
							InvScr.Create (InvScr.ItemList.Count - 1);
							InvScr.HotList [j].Engaged = false;
							InvScr.HotList [j].Icon.sprite = InvScr.HotList [j].DefaultIcon;
							InvScr.HotList [j].ItemScr = null;
							InvScr.HotList [j].Health.text = null;

						}
					}
				}
			}
			HotContainerScr.ItemScr = null;
			HotContainerScr.Engaged = false;
			HotContainerScr.Icon.sprite = HotContainerScr.DefaultIcon;
			HotContainerScr.Health.text = null;
			HotContainerScr.Icon.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f, HotContainerScr.Icon.gameObject.GetComponent<RectTransform> ().anchoredPosition.y);
			HotContainerScr.ActionPanel.SetActive (false);
		}
	}
	void OnDisable(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
	}
}
