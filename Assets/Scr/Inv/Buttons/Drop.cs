using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Drop : MonoBehaviour {

	public InputField CountText;
	public Container ContainerScr;
	public void OnClick(){
		if (Check (CountText.text) == true) {
			int Count = Int32.Parse (CountText.text);
			if ((Count > 0) && (Count <= ContainerScr.ItemScr.Array [0])) {
				int n = (int)(Count / ContainerScr.ItemScr.Array [3]);
				GameObject Pref = Resources.Load<GameObject> ("Prefabs/" + ContainerScr.ItemScr.ID.ToString ());
				if ((Count - n * ContainerScr.ItemScr.Array [3]) > 0)
					n++;
				for (int i = 0; i < n; i++) {
					GameObject TObject = Instantiate (Pref);
					TObject.transform.position = ContainerScr.InvScr.DropPoint.transform.position;
					if (i + 1 == n)
						TObject.GetComponent<Item> ().Array [0] = Count - (n - 1) * ContainerScr.ItemScr.Array [3];
					else
						TObject.GetComponent<Item> ().Array [0] = ContainerScr.ItemScr.Array [3];
					for (int j = 1; j < ContainerScr.ItemScr.Array.Count; j++) {
						TObject.GetComponent<Item> ().Array [j] = ContainerScr.ItemScr.Array [j];
					}
				}
				if (Count < ContainerScr.ItemScr.Array [0]) {
					ContainerScr.ItemScr.Array [0] -= Count;
					ContainerScr.HealthCount.text = ContainerScr.ItemScr.Array [0].ToString ();
					ContainerScr.InvScr.Weight -= ContainerScr.ItemScr.Array [1];
					ContainerScr.InvScr.WeightText.text = ContainerScr.InvScr.Weight.ToString() + "kg";
				} else {
					ContainerScr.InvScr.Weight -= ContainerScr.ItemScr.Array [1];
					ContainerScr.InvScr.WeightText.text = ContainerScr.InvScr.Weight.ToString() + "kg";
					ContainerScr.InvScr.ItemList.Remove (ContainerScr.ItemScr);
					Destroy (ContainerScr.gameObject);
				}
			}		
		}
		CountText.text = null;
	}
	bool Check(string str){
		foreach (char c in str)
		{
			if (c < '0' || c > '9')
				return false;
		}
		return true;
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseExitColor;
	}
}
