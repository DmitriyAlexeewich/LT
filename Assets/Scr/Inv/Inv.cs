using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class Inv : MonoBehaviour {

	public List<Item> ItemList;
	public HotContainer[] HotList = new HotContainer[18];
	public MainSort MainSortScr;
	public AudioClip[] UseSound = new AudioClip[7];
	public AudioClip EquipSound;
	public AudioClip UnequipSound;
	public AudioSource AudioComponent;
	public Color MouseEnterColor;
	public Color MouseExitColor;
	public Color CoincidenceColor;
	public Color UncoincidenceColor;
	public Character CharacterScr;
	public Camera Cam;
	public Container Cont;
	public Text WeightText;
	public KeyCode PickUpKey;
	public KeyCode InvOnOffKey;
	public Canvas canvas;
	public GameObject DropPoint;
	public GameObject MainInv;
	public GameObject ItemPanel;
	public GameObject Equip;
	public GameObject Unequip;
	public GameObject Drop;
	public GameObject DropAll;
	public GameObject Use;
	public GameObject Unload;
	public GameObject InfoPanel;
	public int MaxWeight;
	public int MaxDistance;
	[HideInInspector]
	public float Weight;
	Decode Decoder = new Decode();
	void Update () {
		if ((Input.GetKeyDown (PickUpKey))&&(!MainInv.activeSelf)) {
			RaycastHit hit;
			Ray ray = new Ray ();
			ray = Cam.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.GetComponent<Item> ()) {
					Item TItem = hit.collider.gameObject.GetComponent<Item> ();
					if ((Vector3.Distance (hit.point, this.transform.position) <= MaxDistance) && (Weight + TItem.Array [1] <= MaxWeight)) {
						List<int> TID = new List<int> ();
						for (int i = 0; i < 2; i++)
							TID.Add (Decoder.Code ((int)TItem.ID, i));
						if (((TID [0] == 2) || (TID [0] == 3))&& (TID [1] == 1) && (ItemList.Count > 0)) {
							for (int i = 0; i < ItemList.Count; i++) {
								if (ItemList [i].ID == TItem.ID) {
									ItemList [i].Array [0] += TItem.Array [0];
									Weight += TItem.Array [1];
									Destroy (TItem.gameObject);
									break;
								}
							}
						}else if (TItem != null) {
							ItemList.Add (hit.collider.gameObject.GetComponent<Item> ());
							Destroy (hit.collider.gameObject.GetComponent<Item> ().gameObject);
						}
					}
				}
			}
		}
		if (Input.GetKeyDown (InvOnOffKey)) {
			if (MainInv.activeSelf) {
				for(int i=0; i<ItemPanel.transform.childCount; i++){
					Destroy (ItemPanel.transform.GetChild (i).gameObject);
				}
				AudioComponent.PlayOneShot (UseSound [0]);
				CharacterScr.LockCursor = true;
				MainInv.SetActive (false);
			} else {
				if(MainSortScr.IDList.Count > 0)
					MainSortScr.IDList.RemoveRange (0, MainSortScr.IDList.Count);
				MainSortScr.TypeSort = 0;
				MainSortScr.Order = false;
				AudioComponent.PlayOneShot (UseSound [1]);
				for (int i = 0; i < ItemList.Count; i++) {
					Create (i);
					MainSortScr.IDList.Add (ItemList [i]);
					Weight += ItemList [i].Array [1];
				}
				WeightText.text = Weight.ToString() + "kg";
				CharacterScr.LockCursor = false;
				MainInv.SetActive (true);
			}
		}
	}
	public void Create(int i){
		GameObject TCont = Instantiate (Cont.gameObject, ItemPanel.transform);
		TCont.GetComponent<Image> ().color = MouseExitColor;
		TCont.GetComponent<Container> ().InvScr = this;
		TCont.GetComponent<Container> ().ItemScr = ItemList[i];
		TCont.GetComponent<Container> ().Icon.sprite = Resources.Load<Sprite>("Icons/"+ItemList[i].ID.ToString());
		TCont.GetComponent<Container> ().HealthCount.text = ((int)ItemList [i].Array [0]).ToString();
		if(ItemList [i].Array [1]>1)
			TCont.GetComponent<Container> ().Weight.text = ((int)ItemList [i].Array [1]).ToString()+"kg";
		else
			TCont.GetComponent<Container> ().Weight.text = ((int)(ItemList [i].Array [1]*1000)).ToString()+"g";
		TCont.GetComponent<Container> ().Cost.text = ((int)ItemList [i].Array[2]).ToString()+"Rub";
		List<int> TID = new List<int>();
		for (int j = 0; j < 2; j++)
			TID.Add (Decoder.Code ((int)ItemList[i].ID, j));
		if ((TID [0] == 2 || (TID [0] == 3) && (TID [1] == 1))) {
			TCont.GetComponent<Container> ().HealthCount.text += "X";
		} else {
			TCont.GetComponent<Container> ().HealthCount.text += "%";
		}
		TextAsset XMLFile = Resources.Load<TextAsset> ("Info/" + ItemList [i].ID.ToString ());
		string XMLText = XMLFile.text;
		TCont.GetComponent<Container> ().Name.text = Decoder.ParseXml (XMLText,0);
		switch (TID [0]) {
		case 1:{
				GameObject TEquip = Instantiate (Equip, TCont.GetComponent<Container> ().ActionPanel.transform);
				TEquip.GetComponent<Equip> ().ContainerScr = TCont.GetComponent<Container> ();
				break;
			}
		case 2:{
				GameObject TUse = Instantiate (Use, TCont.GetComponent<Container> ().ActionPanel.transform);
				break;
			}
		default:{
				break;
			}

		}
		GameObject TDropAll = Instantiate (DropAll, TCont.GetComponent<Container> ().ActionPanel.transform);
		TDropAll.GetComponent<DropAll>().ContainerScr = TCont.GetComponent<Container> ();/*
		if (((TID [0] == 2) || (TID [0] == 3)) && (TID [1] == 1) && (ItemList [i].Array [0] > 1)) {
			GameObject TDrop = Instantiate (Drop, TCont.GetComponent<Container> ().ActionPanel.transform);
			TDrop.GetComponent<Drop>().ContainerScr = TCont.GetComponent<Container> ();
		}*/
		if((TID [0] == 1) && (TID [1] == 3)&&(ItemList[i].Array[5] > 0)){
			GameObject TUnload = Instantiate (Unload, TCont.GetComponent<Container> ().ActionPanel.transform);
			TUnload.GetComponent<Unload>().ContainerScr = TCont.GetComponent<Container> ();
		}
		TCont.GetComponent<Container> ().InfoPanel.InvScr = this;
		TCont.GetComponent<Container> ().InfoPanel.canvas = canvas;
		TCont.GetComponent<Container> ().InfoPanel.ContainerScr = TCont.GetComponent<Container> ();
	}
}
