using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCode : MonoBehaviour {

	public int ID;//Код оружия
	public Vector3 AimNoScope;//Позиция для прицеливания (мушка)
	public Vector3 HandelHandPos;//Положение рук при активной рукоятки
	public Vector3 HandelHandRot;//Поворот руки при активной рукоятке
	public Vector3 StartHandelHandPos;//Стартовое положение рук при активной рукоятки
	public Vector3 StartHandelHandRot;//Стартовый поворот руки при активной рукоятке
	public GameObject SilencerBarrelBone;//Кость для глушителя / длинного ствола
	public GameObject BipodsHandelBone;//Кость для сошек/рукоятки
	public GameObject FlashlightLaseBone;//Кость для  фонарика/лазера
	public GameObject ScopeBone;//Кость для прицела
	public GameObject CaseDropBone;//Кость для выброса гильз
	public GameObject HandBone;//Кость руки
	public GameObject BipodsBone;//Кость сошек
	public Animation AnimComp;//Компонет для воспроизведения анимации анимации
	public bool Holsted;
	void Update(){
		if (AnimComp.IsPlaying ("-Draw"))
			Holsted = true;
		if ((Holsted == true) && (!AnimComp.IsPlaying ("-Draw")))
			this.gameObject.SetActive (false);
	}
}
