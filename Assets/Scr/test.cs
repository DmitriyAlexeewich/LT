using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	Decode Decoder = new Decode();
	void Start(){
		Debug.Log(Decoder.IntLength (12345, 3));
	}
}
