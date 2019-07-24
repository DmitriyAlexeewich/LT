using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class DialogContainer : MonoBehaviour {

	public Text DialText;
	public TextAsset Dial;
	public int ID;
	Decode Decoder = new Decode();
	/*void OnEnable(){
		string XMLText = Dial.text;
		string TText += Decoder.ParseDialXml (XMLText,UnityEngine.Random.Range(0,3));
		DialText.text = TText;
	}
	string Search(string SText){
		string t = null;
		for (int i = 0; i < SText.Length; i++) {
			if (SText [i] == '_') {
				int x = (int)Char.GetNumericValue (SText [i + 1]);
				t+=Decoder.ParseDialXml (Resources.Load<TextAsset> ("Dialogs/NPC/"+x.ToString()).text,UnityEngine.Random.Range(0,3));
				i += 2;
			} else {
				t += SText [i];
			}
		}
		return t;
	}*/
}
