using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BipodsHandel : MonoBehaviour {

	public int ID;//Код сошек / рукоятки
	public float RangeY;//Отдача рукоятки
	public float ActiveRangeY;//Отдача сошек
	public float RangeX;//Разброс сошек / рукоятки
	public float Distance;//Дистанция сошек /рукоятки
	public bool OnOff;//Стоят ли сошки или нет
	public Vector3 OriginalRotation;//оргинальный поворот

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Static"))
			OnOff = true;
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Static"))
			OnOff = false;
	}
}
