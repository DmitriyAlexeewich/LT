using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HotContainer : MonoBehaviour {

	public int ID;
	public Inv InvScr;
	public Item ItemScr;
	public Image Icon;
	public Text Health;
	public Sprite DefaultIcon;
	public GameObject ActionPanel;
	//[HideInInspector]
	public bool Engaged;
	void OnEnable(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
		if (Engaged == false) {
			Icon.sprite = DefaultIcon;
			Health.text = null;
			ActionPanel.SetActive (false);
		} else {
			Icon.sprite = Resources.Load<Sprite>("Icons/"+ItemScr.ID.ToString());
			Health.text = ((int)ItemScr.Array[0]).ToString()+"%";
		}
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseEnterColor;
		if (Engaged == false)
			Icon.color = InvScr.MouseEnterColor;
		else {
			ActionPanel.SetActive (true);
			Icon.gameObject.GetComponent<RectTransform> ().anchoredPosition -= new Vector2 (11.757f, 0f);
		}
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = InvScr.MouseExitColor;
		Icon.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f, Icon.gameObject.GetComponent<RectTransform> ().anchoredPosition.y);
		if(Engaged == false)
			Icon.color = InvScr.MouseExitColor;
		ActionPanel.SetActive (false);
	}
}
