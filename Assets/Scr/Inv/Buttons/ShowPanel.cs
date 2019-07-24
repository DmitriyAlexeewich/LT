using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowPanel : MonoBehaviour {

	public GameObject Panel;
	public Inv InvScr;
	public void OnClick(){
		if (Panel.activeSelf) {
			Panel.SetActive (false);
		} else {
			Panel.SetActive (true);
		}
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
	}
}
