using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropAll : MonoBehaviour {

	public Container ContainerScr;
	Decode Decoder = new Decode();
	public void OnClick(){
		List<int> TID = new List<int>();
		for (int j = 0; j < 2; j++)
			TID.Add (Decoder.Code ((int)ContainerScr.ItemScr.ID, j));
		if ((TID [0] == 2 || (TID [0] == 3)) && (TID [1] == 1)&&(ContainerScr.ItemScr.Array [0] > ContainerScr.ItemScr.Array [3])) {
			int n = (int)(ContainerScr.ItemScr.Array [0] / ContainerScr.ItemScr.Array [3]);
			GameObject Pref = Resources.Load<GameObject> ("Prefabs/" + ContainerScr.ItemScr.ID.ToString ());
			if ((ContainerScr.ItemScr.Array [0] - n * ContainerScr.ItemScr.Array [3]) > 0)
				n++;
			for (int i = 0; i < n; i++) {
				GameObject TObject = Instantiate (Pref);
				TObject.transform.position = ContainerScr.InvScr.DropPoint.transform.position;
				if( i+1 == n)
					TObject.GetComponent<Item> ().Array [0] = ContainerScr.ItemScr.Array [0] - (n-1) * ContainerScr.ItemScr.Array [3];
				else
					TObject.GetComponent<Item> ().Array [0] = ContainerScr.ItemScr.Array[3];
				for (int j = 1; j < ContainerScr.ItemScr.Array.Count; j++) {
					TObject.GetComponent<Item> ().Array[j] = ContainerScr.ItemScr.Array[j];
				}
			}
		} else {
			GameObject TObject = Instantiate (Resources.Load<GameObject>("Prefabs/"+ContainerScr.ItemScr.ID.ToString()));
			TObject.transform.position = ContainerScr.InvScr.DropPoint.transform.position;
			TObject.GetComponent<Item> ().Array = ContainerScr.ItemScr.Array;
		}
		ContainerScr.InvScr.Weight -= ContainerScr.ItemScr.Array [1];
		ContainerScr.InvScr.WeightText.text = ContainerScr.InvScr.Weight.ToString() + "kg";
		int tID = Decoder.Code (ContainerScr.ItemScr.ID, 1) * 10;
		tID += Decoder.Code (ContainerScr.ItemScr.ID, 2);
		for (int i = 0; i < 19; i++) {
			if (ContainerScr.InvScr.HotList [i].ID == tID) {
				ContainerScr.InvScr.HotList [i].Icon.color = ContainerScr.InvScr.MouseExitColor;
			}
		}
		ContainerScr.InvScr.ItemList.Remove (ContainerScr.ItemScr);
		Destroy (ContainerScr.gameObject);
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseExitColor;
	}
}
