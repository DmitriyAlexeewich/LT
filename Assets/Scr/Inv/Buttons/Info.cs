using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class Info : MonoBehaviour {

	public Container ContainerScr;
	public Inv InvScr;
	public Canvas canvas;
	Decode Decoder = new Decode();
	public void MouseEnter(){
		InvScr.InfoPanel.SetActive (true);
		TextAsset XMLFile = Resources.Load<TextAsset> ("Info/" + ContainerScr.ItemScr.ID.ToString ());
		string XMLText = XMLFile.text;
		InvScr.InfoPanel.GetComponentInChildren<Text>().text = Decoder.ParseXml (XMLText,1);
		Vector2 Size = InvScr.InfoPanel.transform.GetChild(0).GetComponent<RectTransform> ().sizeDelta;
		InvScr.InfoPanel.transform.GetChild (0).GetComponent<RectTransform> ().anchoredPosition = new Vector2(0, 0);
		Vector3 NewPosition = new Vector3 ();
		if (Input.mousePosition.y + InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.y > Screen.height) {
			NewPosition.y = Input.mousePosition.y - ((InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.y)*canvas.scaleFactor)  / 2;
		} else {
			NewPosition.y = Input.mousePosition.y + ((InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.y)* canvas.scaleFactor) / 2;
		}
		if (Input.mousePosition.x + InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.x > Screen.width) {
			NewPosition.x = Input.mousePosition.x - ((InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.x)*canvas.scaleFactor) / 2;
		} else {
			NewPosition.x = Input.mousePosition.x + ((InvScr.InfoPanel.GetComponent<RectTransform> ().sizeDelta.x)*canvas.scaleFactor) / 2;
		}
		InvScr.InfoPanel.transform.position = NewPosition;
	}
	public void MouseExit(){
		InvScr.InfoPanel.SetActive (false);
	}
}
