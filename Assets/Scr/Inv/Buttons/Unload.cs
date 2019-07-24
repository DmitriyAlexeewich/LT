using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class Unload : MonoBehaviour {

	public Container ContainerScr;
	Decode Decoder = new Decode();
	public void OnClick(){
		int AmmoID = (int)ContainerScr.ItemScr.Array [8];
		Inv TInv = ContainerScr.InvScr;
		bool Find = new bool ();
		for (int i = 0; i < TInv.ItemList.Count; i++) {
			if (TInv.ItemList [i].ID == AmmoID) {
				ContainerScr.InvScr.ItemList [i].Array [0] += ContainerScr.ItemScr.Array [6];
				ContainerScr.InvScr.ItemPanel.transform.GetChild (i).GetComponent<Container> ().HealthCount.text = ContainerScr.InvScr.ItemList [i].Array [0].ToString() + "X";
				Find = true;
			} else {
				Find = false;
			}
		}
		ContainerScr.InvScr.ItemList[ContainerScr.InvScr.ItemList.IndexOf(ContainerScr.ItemScr)].Array [6] = 0;
		if (Find == false) {
			GameObject ItemG = Resources.Load<GameObject> ("Prefabs/" + AmmoID.ToString ());
			ContainerScr.InvScr.ItemList.Add (ItemG.GetComponent<Item> ());
			GameObject TCont = Instantiate (ContainerScr.InvScr.Cont.gameObject, ContainerScr.InvScr.ItemPanel.transform);
			TCont.GetComponent<Container> ().InvScr = ContainerScr.InvScr;
			TCont.GetComponent<Container> ().ItemScr = ContainerScr.InvScr.ItemList[ContainerScr.InvScr.ItemList.Count-1];
			TCont.GetComponent<Container> ().Icon.sprite = Resources.Load<Sprite>("Icons/"+AmmoID.ToString());
			TCont.GetComponent<Container> ().HealthCount.text = ((int)ItemG.GetComponent<Item> ().Array [0]).ToString();
			TCont.GetComponent<Container> ().Weight.text = ((int)ItemG.GetComponent<Item> ().Array [1]).ToString()+"kg";
			TCont.GetComponent<Container> ().HealthCount.text += "X";
			TextAsset XMLFile = Resources.Load<TextAsset> ("Info/" + AmmoID.ToString ());
			string XMLText = XMLFile.text;
			TCont.GetComponent<Container> ().Name.text = Decoder.ParseXml (XMLText,0);
			GameObject TDropAll = Instantiate (ContainerScr.InvScr.DropAll, TCont.GetComponent<Container> ().ActionPanel.transform);
			TDropAll.GetComponent<DropAll>().ContainerScr = TCont.GetComponent<Container> ();
			GameObject TDrop = Instantiate (ContainerScr.InvScr.Drop, TCont.GetComponent<Container> ().ActionPanel.transform);
			TDrop.GetComponent<Drop>().ContainerScr = TCont.GetComponent<Container> ();
			TCont.GetComponent<Container> ().InfoPanel.InvScr = ContainerScr.InvScr;
			TCont.GetComponent<Container> ().InfoPanel.ContainerScr = TCont.GetComponent<Container> ();
		}
		Destroy (this.gameObject);
	}
	public void  MouseEnter(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseEnterColor;
	}
	public void  MouseExit(){
		this.gameObject.GetComponent<Image> ().color = ContainerScr.InvScr.MouseExitColor;
	}
}
