using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightLaser : MonoBehaviour {

	public int ID;//Код Фонаря / Лазера
	public float Power;//Максимальный заряд
	public float NowPower;//заряд
	public float LightLaserDistance;//Максимальная дальность 
	public Color FlashlightLaserColor;//Цвет 
	public Light FlashLightLight;//Свет фонарика
	public List<Sprite> FlashlightSprites;//Спрайты повреждения фонаря
	public Camera LaserCamera;//Камера для лазера
	public Camera MainCamera;//Основная камера
	public float LaserMinDistance;//Минимальная дальность лазера
	public bool LaserMove;//Нужно ли двигать лазер
	GameObject LaserPoint;//Точка света лазера
	bool OnOff;//Включение выключение фонарика/лазера
	float Timer;//Таймер вычета заряда
	float DLight;//Коефициент света фонаря
	float DLaser;//Коефициент света лазера
	public Vector3 OriginalRotation;//оргинальный поворот
	void OnEnable(){
		LaserPoint = MainCamera.gameObject.GetComponent<Weapon> ().LaserPoint.gameObject;
		DLight = 2f / Power;
		DLaser = 5f / Power;
	}
	void Update(){
		if (Input.GetMouseButtonDown (2)) {
			if (OnOff == true)
				OnOff = false;
			else
				OnOff = true;
		}
		if (OnOff == true) {
				if (NowPower > 0) {
					Timer += Time.deltaTime;
					if (Timer > 72f) {
						NowPower--;
						FlashLightLight.intensity -= DLight;
						LaserPoint.GetComponent<Light> ().intensity -= DLaser;
						Timer = 0f;
					}
				}
				if (LaserCamera != null) {
					RaycastHit hit;
					Ray ray = MainCamera.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
					if (Physics.Raycast (ray, out hit)) {
						if ((Vector3.Distance (MainCamera.transform.position, hit.point) > LaserMinDistance) && (LaserMove == false)) {
							LaserCamera.transform.LookAt (hit.point);
						}
					}
					RaycastHit Laserhit;
					Ray Laserray = LaserCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
					if (Physics.Raycast (Laserray, out Laserhit)) {
						LaserPoint.SetActive (true);
						LaserPoint.transform.position = Laserhit.point + hit.normal * 0.01f;	
					} else {
						LaserPoint.SetActive (false);
					}
				} 
				if (FlashLightLight != null) {
					FlashLightLight.enabled = true;
				}
		} else {
			if (LaserCamera != null)
				LaserPoint.SetActive (false);
			if (FlashLightLight != null) 
				FlashLightLight.enabled = false;
		}
	}
}
