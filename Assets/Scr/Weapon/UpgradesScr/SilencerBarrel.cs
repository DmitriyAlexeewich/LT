using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerBarrel : MonoBehaviour {

	public int ID;//Код глушителя / длинного ствола
	public float Volume;// Грокость глушителя
	public float Damage;// Урон  глушителя / длинного ствола
	public float Distance;//Дистанция глушителя / длинного ствола
	public float RangeY;//Отдача глушителя / длинного ствола
	public float RangeX;//Разброс глушителя / длинного ствола
	public Vector3 OriginalRotation;//оргинальный поворот
}
