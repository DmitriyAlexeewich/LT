using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
public class Decode : MonoBehaviour {

	public int Code(int ID, int pos){
		int Power = new int();
		int TID = ID;
		while (TID > 9) {
			Power++;
			TID /= 10;
		}
		TID = (int) (ID / Mathf.Pow (10, Power - pos));
		for (int i = pos; i > 0; i--)
			TID %= (int)Mathf.Pow (10, i);
		return TID;
	}
	public int IntLength(int ID, int Length){
		int TID = new int ();
		for (int i = Length; i > 0; i--) {
			TID += Code (ID, i - 1) * (int)Mathf.Pow(10, Length - i);
		}
		return TID;
	}
	public string ParseXml(string XMLText, int i){
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load ( new StringReader(XMLText));
		string xmlPathPattern = "//InfoStat/";
		switch (i){
			case 0:{
				xmlPathPattern += "Name";
				break;
			}
			case 1:{
				xmlPathPattern += "Info";
				break;
			}
		}
		XmlNodeList myNodeList = xmlDoc.SelectNodes (xmlPathPattern);
		xmlPathPattern = myNodeList [0].InnerXml;
		return (xmlPathPattern);
	}
	public string ParseDialXml(string XMLText, int i){
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load ( new StringReader(XMLText));
		string xmlPathPattern = "//Dial/";
		switch (i){
		case 0:{
				xmlPathPattern += "h1";
				break;
			}
		case 1:{
				xmlPathPattern += "h2";
				break;
			}
		case 2:{
				xmlPathPattern += "h3";
				break;
			}
		case 3:{
				xmlPathPattern += "h4";
				break;
			}
		}
		XmlNodeList myNodeList = xmlDoc.SelectNodes (xmlPathPattern);
		xmlPathPattern = myNodeList [0].InnerXml;
		return (xmlPathPattern);
	}
}
