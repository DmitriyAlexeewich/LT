using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

	public Inv InvScr;
	public MoralityStats MoralStatsScr;
	public Weapon WeaponScr;
	public Character CharScr;
	public Camera Cam;
	public GameObject DialogCanvas;
	public GameObject PlayerDialPanel;
	public GameObject AllDialPanel;
	public GameObject Cont;
	public float Distance;
	public KeyCode DialogKey;
	public DayTime DTScr;
	StaticNPC StaticNPCScr;
	Decode Decoder = new Decode();
	void Update () {
		if (Input.GetKeyDown (DialogKey)) {
			if (DialogCanvas.activeSelf) {
				DialogCanvas.SetActive (false);
				for(int i=0; i<PlayerDialPanel.transform.childCount; i++){
					Destroy (PlayerDialPanel.transform.GetChild (i).gameObject);
				}
				for(int i=0; i<AllDialPanel.transform.childCount; i++){
					Destroy (AllDialPanel.transform.GetChild (i).gameObject);
				}
				if(WeaponScr.WeaponCodeScr != null)
					WeaponScr.enabled = true;
				InvScr.enabled = true;
				CharScr.LockCursor = true;
				CharScr.LockPos = true;
			} else {
				RaycastHit hit;
				Ray ray = new Ray ();
				ray = Cam.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
				if (Physics.Raycast (ray, out hit)) {
					if ((Vector3.Distance (hit.point, this.transform.position) <= Distance)&&(hit.collider.gameObject.GetComponent<StaticNPC>())) {
						StaticNPCScr = hit.collider.gameObject.GetComponent<StaticNPC> ();
						GameObject TObject = Instantiate (Cont, AllDialPanel.transform);
						string TText = null;
						if (StaticNPCScr.Mood == 0) {
							if ((DTScr.DTime == 0)||(DTScr.DTime == 3))
								TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/2").text, 0);
							else
								TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/2").text, 3);
							TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/1").text, DTScr.DTime);
							TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/3").text, StaticNPCScr.Jargon);
						} else {
							TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/2").text, StaticNPCScr.Mood);
							TText += Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/Universal/3").text, StaticNPCScr.Jargon);
						}
						TObject.GetComponent<DialogContainer> ().DialText.text = TText;
						DialogCanvas.SetActive (true);
						WeaponScr.enabled = false;
						InvScr.enabled = false;
						CharScr.LockCursor = false;
						CharScr.LockPos = false;

					}
				}
			}
		}
	}
}
