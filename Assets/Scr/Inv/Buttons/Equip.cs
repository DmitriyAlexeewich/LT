using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Equip : MonoBehaviour {

	public Container ContainerScr;
	Decode Decoder = new Decode();
	bool Clickable = false;
	public void OnClick(){
		if (Clickable == true) {
			ContainerScr.InvScr.AudioComponent.PlayOneShot (ContainerScr.InvScr.EquipSound);
			int TID = Decoder.Code (ContainerScr.ItemScr.ID, 1) * 10;
			TID += Decoder.Code (ContainerScr.ItemScr.ID, 2);
			for (int i = 0; i < 19; i++) {
				if (ContainerScr.InvScr.HotList [i].ID == TID) {
					if (ContainerScr.InvScr.HotList [i].Engaged == true) {
						ContainerScr.InvScr.ItemList.Add (ContainerScr.InvScr.HotList [i].ItemScr);
						ContainerScr.InvScr.Create (ContainerScr.InvScr.ItemList.Count - 1);
					}
					if ((TID / 10) == 3) {
						if ((TID % 10) == 1) {
							for (int j = 6; j < 10; j++) {
								if (ContainerScr.InvScr.HotList [j].Engaged == true) {
									if ((int)ContainerScr.ItemScr.Array [j+11] != Decoder.Code (ContainerScr.InvScr.HotList [j].ItemScr.ID, 3)) {
										ContainerScr.InvScr.ItemList.Add (ContainerScr.InvScr.HotList [j].ItemScr);
										ContainerScr.InvScr.Create (ContainerScr.InvScr.ItemList.Count - 1);
										ContainerScr.InvScr.HotList [j].Engaged = false;
										ContainerScr.InvScr.HotList [j].Icon.sprite = ContainerScr.InvScr.HotList [j].DefaultIcon;
										ContainerScr.InvScr.HotList [j].ItemScr = null;
										ContainerScr.InvScr.HotList [j].Health.text = null;
									}
								}
							}
						} else {
							for (int j = 10; j < 14; j++) {
								if (ContainerScr.InvScr.HotList [j].Engaged == true) {
									if ((int)ContainerScr.ItemScr.Array [j+7] != Decoder.Code (ContainerScr.InvScr.HotList [j].ItemScr.ID, 3)) {
										ContainerScr.InvScr.ItemList.Add (ContainerScr.InvScr.HotList [j].ItemScr);
										ContainerScr.InvScr.Create (ContainerScr.InvScr.ItemList.Count - 1);
										ContainerScr.InvScr.HotList [j].Engaged = false;
										ContainerScr.InvScr.HotList [j].Icon.sprite = ContainerScr.InvScr.HotList [j].DefaultIcon;
										ContainerScr.InvScr.HotList [j].ItemScr = null;
										ContainerScr.InvScr.HotList [j].Health.text = null;
									}
								}
							}
						}
					}
					ContainerScr.InvScr.HotList [i].ItemScr = ContainerScr.ItemScr;
					ContainerScr.InvScr.HotList [i].Engaged = true;
					ContainerScr.InvScr.HotList [i].Icon.sprite = Resources.Load<Sprite>("Icons/"+ContainerScr.ItemScr.ID.ToString());
					ContainerScr.InvScr.HotList [i].Icon.color = ContainerScr.InvScr.MouseExitColor;
					ContainerScr.InvScr.HotList [i].gameObject.GetComponent<Image>().color = ContainerScr.InvScr.MouseExitColor;
					ContainerScr.InvScr.HotList [i].Health.text = ((int)ContainerScr.ItemScr.Array [0]).ToString()+"%";
					ContainerScr.InvScr.ItemList.Remove (ContainerScr.ItemScr);
					Destroy (ContainerScr.gameObject);
				}
			}
		}
	}
	public void  MouseEnter(){
		if (Decoder.Code (ContainerScr.ItemScr.ID, 1) == 2) {
			if (Decoder.Code (ContainerScr.ItemScr.ID, 2) < 5) {
				if ((ContainerScr.InvScr.HotList [14].Engaged == true)&&(Decoder.Code (ContainerScr.ItemScr.ID, 3) == ((int)ContainerScr.InvScr.HotList [14].ItemScr.Array [Decoder.Code (ContainerScr.ItemScr.ID, 2) + 16])))
					Clickable = true;
				else
					Clickable = false;
			} else {
				if ((ContainerScr.InvScr.HotList [15].Engaged == true)&&(Decoder.Code (ContainerScr.ItemScr.ID, 3) == ((int)ContainerScr.InvScr.HotList [15].ItemScr.Array [Decoder.Code (ContainerScr.ItemScr.ID, 2) + 12])))
					Clickable = true;
				else
					Clickable = false;
			}
		} else
			Clickable = true;
		if(Clickable == true)
			this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseEnterColor;
		else
			this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.UncoincidenceColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseExitColor;
	}
}
