using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Use : MonoBehaviour {

	public Container ContainerScr;
	Decode Decoder = new Decode();
	public void OnClick(){
		int n = (int)ContainerScr.ItemScr.Array [4];
		for (int i = 0; i < n; i++) {
			ContainerScr.InvScr.CharacterScr.Stats [(int)ContainerScr.ItemScr.Array [5+i]] += ContainerScr.ItemScr.Array [6+i];
		}
		if (ContainerScr.ItemScr.Array [0] > 1) {
			ContainerScr.HealthCount.text = ((int)ContainerScr.ItemScr.Array [0]).ToString() + "X";
			ContainerScr.ItemScr.Array [0]--;
			ContainerScr.InvScr.Weight -= ContainerScr.ItemScr.Array [1];
			ContainerScr.InvScr.WeightText.text = ContainerScr.InvScr.Weight.ToString() + "kg";
		} else {
			ContainerScr.InvScr.Weight -= ContainerScr.ItemScr.Array [1];
			ContainerScr.InvScr.WeightText.text = ContainerScr.InvScr.Weight.ToString() + "kg";
			ContainerScr.InvScr.ItemList.Remove (ContainerScr.ItemScr);
			Destroy (ContainerScr.gameObject);
		}
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseExitColor;
	}
}
