using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Camera Cam;//Камера
	public Character CharacterScr;//Скрипт персонажа
	public WeaponCode WeaponCodeScr;//Скрипт игрового объекта оружия
	public SilencerBarrel SilencerBarrelScr;//Скрипт игрового объекта Глушителя / Длинного ствола
	public FlashlightLaser FlashlightLaserScr;//Скрипт игрового объекта Фонарика / Лазера
	public BipodsHandel BipodsHandelScr;//Скрипт игрового объекта Сошек / Рукоятки
	public Scope ScopeScr;//Скрипт игрового объекта Прицела
	public bool TypeOfShooting;//Тип стрельбы (дробь / патрон)
	public int ModsOfShooting;//Количество режимов стрельбы
	public int CollarSize;//Размер магазина
	public int Collar;//Патронов в магазине
	public int AmmoSize;//Количество патронов
	public int AmmoID;//Код патронов
	public float ShootTime;//Время одного выстрела
	public float Health;//Состояние оружия
	public float Volume;//Громкость
	public float Damage;//Урон
	public float RangeY;//Отдача
	public float RangeX;//разброс
	public float Distance;//Дистанция
	public float Brightness;//Яркость
	public float GunMoveY;//Кинетическая энергия по оси Y
	public float GunMoveX;//Кинетическая энергия по оси X
	public KeyCode ReloadKey;//Кнопка перезарядки
	public KeyCode SwitchKey;//Кнопка смена режима стрельбы
	public KeyCode OnBelt;//Кнопка на ремень
	public AudioSource AudioComponent;//Компонент проигрывающий звуки
	public AudioClip SwitchSound;//Звук смена режима стрельбы
	public AudioClip BeltOnSound;//Звук (вкл,выкл,"На ремень")
	public AudioClip BeltOffSound;//Звук (вкл,выкл,"На ремень")
	public AudioClip WeaponDrawSound;//Звук (вкл,выкл,"На ремень")
	public AudioClip WeaponHolstedSound;//Звук (вкл,выкл,"На ремень")
	public AudioClip ReloadSound;//Звук перезарядки
	public AudioClip ShootAudio;//Звук выстрела
	public AudioClip SilenceShootAudio;//Звук выстрела с глушителем
	public GameObject MainBone;//Основная кость оружия
	public GameObject ShootParticle;//Частицы выстрела
	public GameObject SmokeParticle;//Частицы Дыма
	public List<GameObject> BulletHoles;//Массив дыр от пуль(0-Дерево, 1-Бетон, 2-Металл, 3-Земля, 4-Вода, 5-Кровь, 6-Дефолтные дыры)
	public Light LaserPoint;//Точка лазера
	public int UsingMod;//Тип стрельбы который используют именно сейчас
	float Timer;//Таймер выстрела
	float MouseTimer;//Таймер удержания кнопки
	float TimeDelay;//Отложенное время
	float SmokeFactor;//Коефициент дыма 
	float OriginSpeed;//Стандартная скорость
	bool ShootBool;//Закончен выстрел или нет
	bool FirstShoot;//Первый выстрел или нет
	bool Safty;//Безопасный режим или нет
	int i;//Счётчик выстрелов
	Vector3 StartPosition;//Стартовая позиция
	Vector3 StartRotation;//Стартовое вращение
	void OnEnable(){
		SmokeFactor = 0.25f / (ShootTime * CollarSize);
	}
	void Start(){
		StartRotation = MainBone.transform.localRotation.eulerAngles;
		StartPosition = MainBone.transform.localPosition;
		OriginSpeed = WeaponCodeScr.AnimComp ["Shoot"].speed;
		if(UsingMod == 1)
			WeaponCodeScr.AnimComp["Shoot"].speed = OriginSpeed;
		else
			WeaponCodeScr.AnimComp["Shoot"].speed = WeaponCodeScr.AnimComp["Shoot"].length/(ShootTime/1.5f);
		//WeaponCodeScr.AnimComp["Shoot"].speed = WeaponCodeScr.AnimComp["Shoot"].length/(ShootTime/1.5f);
	}
	void Update(){
		if (Input.GetKeyDown (ReloadKey)) {
			WeaponCodeScr.AnimComp.CrossFade ("Reload");
			if(!AudioComponent.isPlaying)
				AudioComponent.PlayOneShot (ReloadSound);
			AmmoSize -= (CollarSize - Collar);
			Collar = CollarSize;
			if(FlashlightLaserScr != null)
				FlashlightLaserScr.LaserMove = true;
		}
		if ((Input.GetKeyDown (OnBelt))&&(!Input.GetMouseButton (1))) {
			if (Safty == true) {
				Safty = false;
				AudioComponent.PlayOneShot (BeltOffSound);
			} else {
				Safty = true;
				AudioComponent.PlayOneShot (BeltOnSound);
			}
		}
		Belt();
		if (((Input.GetKey(CharacterScr.ForwardKey))||(Input.GetKey (CharacterScr.BackwardKey)) || (Input.GetKey (CharacterScr.RightKey)) || (Input.GetKey (CharacterScr.LeftKey))) && (!WeaponCodeScr.AnimComp.IsPlaying ("Reload"))&&(!Input.GetMouseButton (0))) {
			WeaponCodeScr.AnimComp.CrossFade ("Move");
			if(FlashlightLaserScr != null)
				FlashlightLaserScr.LaserMove = true;
		} else if((!WeaponCodeScr.AnimComp.IsPlaying ("Reload"))&&(!WeaponCodeScr.AnimComp.IsPlaying ("Sprint"))){
			WeaponCodeScr.AnimComp.CrossFade ("Idle");
			if(Safty == false)
			if(FlashlightLaserScr != null)
				FlashlightLaserScr.LaserMove = false;
		}
		if ((Input.GetKey(CharacterScr.ForwardKey))&&(Input.GetKey (CharacterScr.SprintLieKey)) && (!Input.GetKey (CharacterScr.SeatKey)) && (CharacterScr.Seat == false) && (!WeaponCodeScr.AnimComp.IsPlaying ("Reload"))&&(!Input.GetMouseButton (0))) {
			WeaponCodeScr.AnimComp.CrossFade ("Sprint");
			if(FlashlightLaserScr != null)
				FlashlightLaserScr.LaserMove = true;
		} else if((!WeaponCodeScr.AnimComp.IsPlaying ("Reload"))&&(!WeaponCodeScr.AnimComp.IsPlaying ("Move"))){
			WeaponCodeScr.AnimComp.CrossFade ("Idle");
			if(Safty == false)
				if(FlashlightLaserScr != null)
					FlashlightLaserScr.LaserMove = false;
		}
		if (Input.GetMouseButtonDown (1))
			WeaponCodeScr.AnimComp ["Idle"].speed = WeaponCodeScr.AnimComp ["Idle"].speed / 5;
		if (Input.GetMouseButtonUp (1))
			WeaponCodeScr.AnimComp ["Idle"].speed = WeaponCodeScr.AnimComp ["Idle"].speed * 5;
		if (!Input.GetMouseButton (1)) {
			if (Safty == false)
				GunMove ();
			if (MainBone.transform.localPosition == StartPosition)
				MainBone.transform.localPosition = StartPosition;
		}else if((!WeaponCodeScr.AnimComp.IsPlaying ("Reload"))&&(!Input.GetKeyDown (ReloadKey))&&(!Input.GetKey (CharacterScr.SprintLieKey))&&(!WeaponCodeScr.AnimComp.IsPlaying ("Sprint"))){
			WeaponCodeScr.AnimComp.Play ("Idle",PlayMode.StopAll);
			WeaponCodeScr.AnimComp.Stop ("Shoot");
			WeaponCodeScr.AnimComp.Stop ("Move");
			Safty = false;
			if (ScopeScr != null) {
				Vector3 NewPosition  = new Vector3 (WeaponCodeScr.AimNoScope.x, -Vector3.Distance(MainBone.transform.position,ScopeScr.AimBone.transform.position), WeaponCodeScr.AimNoScope.z);
				if (MainBone.transform.localPosition != NewPosition)
					MainBone.transform.localPosition = Vector3.Lerp (MainBone.transform.localPosition, NewPosition, Time.deltaTime * GunMoveX);
				else
					MainBone.transform.localPosition = new Vector3 (WeaponCodeScr.AimNoScope.x, -Vector3.Distance(MainBone.transform.position,ScopeScr.AimBone.transform.position), WeaponCodeScr.AimNoScope.z);
			} else {
				if (MainBone.transform.localPosition != WeaponCodeScr.AimNoScope) 
					MainBone.transform.localPosition = Vector3.Lerp (MainBone.transform.localPosition, WeaponCodeScr.AimNoScope, Time.deltaTime * GunMoveX);
				else
					MainBone.transform.localPosition = WeaponCodeScr.AimNoScope;
			}
			if (MainBone.transform.localRotation != Quaternion.Euler (StartRotation))
				MainBone.transform.localRotation = Quaternion.Lerp (MainBone.transform.localRotation, Quaternion.Euler (StartRotation), Time.deltaTime * GunMoveX);
			else
				MainBone.transform.localRotation = Quaternion.Euler (StartRotation);
		}
		if ((Input.GetKeyDown (SwitchKey))&&(!Input.GetMouseButton (0))) {
			AudioComponent.PlayOneShot (SwitchSound);
			if (UsingMod < (ModsOfShooting)) {
				UsingMod++;
				WeaponCodeScr.AnimComp["Shoot"].speed = WeaponCodeScr.AnimComp["Shoot"].length/(ShootTime/1.5f);
			}else {
				UsingMod = 1;
				WeaponCodeScr.AnimComp["Shoot"].speed = OriginSpeed;
			}
		}
		ParticleSystem.MainModule TParticleMain = SmokeParticle.GetComponent<ParticleSystem> ().main;
		if (Input.GetMouseButton (0)){
			Safty = false;
			switch (UsingMod) {
				case 2:{
					MouseTimer += Time.deltaTime;
					TParticleMain.startColor = new Color(30f, 30f, 30f, SmokeFactor*MouseTimer);
					TimeDelay = ShootTime;
					if (FirstShoot == false) {
						Timer = TimeDelay;
						FirstShoot = true;
					}
					Timer += Time.deltaTime;
					if (Timer >= TimeDelay) {
						Shoot ();
						Timer = 0;
					} else {
						ShootParticle.GetComponent<MuzzleFlash> ().MuzzleFlashParticle.Stop ();
						ShootParticle.SetActive (false);
					}
					break;
				}
				case 1:{
					TParticleMain.startColor = new Color(30f, 30f, 30f, SmokeFactor);
					if (ShootBool == false) {
						Shoot ();
						if (Collar > 0) {
							SmokeParticle.transform.position = ShootParticle.transform.position;
							SmokeParticle.SetActive (true);
							SmokeParticle.GetComponent<ParticleSystem> ().Play();
						}
						ShootBool = true;
					} else {
						ShootParticle.GetComponent<MuzzleFlash> ().MuzzleFlashParticle.Stop ();
						ShootParticle.SetActive (false);
					}
					break;
				}
				case 3:{
					TParticleMain.startColor = new Color(30f, 30f, 30f, SmokeFactor*ShootTime*3);
					if(i<3){
						TimeDelay = ShootTime;
						if (i < 1)
							Timer = TimeDelay;
						else
							Timer += Time.deltaTime;
						if(Timer>=TimeDelay){
							Shoot ();
							Timer = 0;
							i++;
						}
						if((i==2)&&(Collar>0)){
							SmokeParticle.transform.position = ShootParticle.transform.position;
							SmokeParticle.SetActive (true);
							SmokeParticle.GetComponent<ParticleSystem> ().Play();
						}
					} else {
						ShootParticle.GetComponent<MuzzleFlash> ().MuzzleFlashParticle.Stop ();
						ShootParticle.SetActive (false);
					}
					break;
				}
			}
			if (Collar == 0) {
				ShootBool = false;
				ShootParticle.SetActive (false);
				if (UsingMod == 1) {
					FirstShoot = false;
				}
			}				
		} else {
			i = 0;
			Timer = 0;
			TimeDelay = 0;
			ShootBool = false;
			ShootParticle.SetActive (false);
			if ((Input.GetMouseButtonUp(0))&&(Collar > 0)&&(UsingMod==1)) {
				FirstShoot = false;
				SmokeParticle.transform.position = ShootParticle.transform.position;
				SmokeParticle.SetActive (true);
				SmokeParticle.GetComponent<ParticleSystem> ().Play();
			}
			if(SmokeParticle.GetComponent<ParticleSystem>().isStopped){
				SmokeParticle.SetActive (false);
			}
		}
	}
	int n = 0;
	void Shoot(){
		float TDistance = new float();
		float TRangeX = new float();
		float TRangeY = new float();
		float TDamage = new float();
		if (SilencerBarrelScr != null) {
			TDistance += SilencerBarrelScr.Distance;
			TRangeX += SilencerBarrelScr.RangeX;
			TRangeY += SilencerBarrelScr.RangeY;
			TDamage += SilencerBarrelScr.Damage;
		}
		if (BipodsHandelScr != null) {
			TRangeY += BipodsHandelScr.RangeY;
			if(BipodsHandelScr.OnOff == true)
				TRangeX += BipodsHandelScr.RangeX;
			else
				TRangeY += BipodsHandelScr.RangeY;
		}
		if (Input.GetAxis ("Fire2") == 1) {
			if (ScopeScr != null) {
				TRangeX = ScopeScr.RangeX;
				TRangeY = ScopeScr.RangeY;
			}
		}
		if(Collar>0){
			Health -= 0.1f;
			if (TypeOfShooting == false)
				n = 1;
			else
				n = 5;
			for (int k = 0; k < n; k++) {
				RaycastHit hit;
				Ray ray = new Ray ();
				if (n == 1)
					ray = Cam.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
				else {					
					ray = Cam.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
					ray.direction = Quaternion.Euler (Random.Range (5f, -5f), Random.Range (5f, -5f), 0) * Cam.transform.forward;

				}
				if (Physics.Raycast (ray, out hit)) {
					if (Vector3.Distance (hit.point, this.transform.position) <= (Distance + TDistance)) {
						switch (hit.collider.gameObject.name [0]) {
							case 'N':
								{
									SpawnHoles (0, hit);
									break;
								}
							case 'C':
								{
									SpawnHoles (1, hit);
									break;
								}
							case 'M':
								{
									SpawnHoles (2, hit);
									break;
								}
							case 'E':
								{
									SpawnHoles (3, hit);
									break;
								}
							case 'W':
								{
									SpawnHoles (4, hit);
									break;
								}
							case 'B':
								{
									SpawnHoles (5, hit);
									break;
								}
							default:
							{
								SpawnHoles (6, hit);
								break;
							}

						}
					}
				}
			}
			CharacterScr.X += Random.Range (-(RangeX + TRangeX), (RangeX + TRangeX));
			CharacterScr.Y += Random.Range (0f, (RangeY + TRangeY));
			Collar--;
			if ((SilencerBarrelScr != null) && (SilencerBarrelScr.Volume > 0f)) {
				AudioComponent.PlayOneShot (SilenceShootAudio);	
			} else {
				AudioComponent.PlayOneShot (ShootAudio);
			}
			WeaponCodeScr.AnimComp.Play ("Shoot", PlayMode.StopAll);
			if (WeaponCodeScr.AnimComp.IsPlaying ("Shoot")) {
				ShootParticle.transform.position = WeaponCodeScr.SilencerBarrelBone.transform.position;
				ShootParticle.transform.Rotate (Vector3.up, Random.Range (0f, 360f));
				ShootParticle.SetActive (true);
				ShootParticle.GetComponent<MuzzleFlash> ().MuzzleFlashParticle.Play ();
			}
			if (Collar == 0) {
				ShootBool = false;
				ShootParticle.SetActive (false);
				FirstShoot = false;
				SmokeParticle.transform.position = ShootParticle.transform.position;
				SmokeParticle.SetActive (true);
				SmokeParticle.GetComponent<ParticleSystem> ().Play ();
			}
			//Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);
		}
	}
	void SpawnHoles(int i, RaycastHit hit){
		GameObject THole = Instantiate (BulletHoles [i]);
		THole.transform.parent = hit.transform;
		THole.transform.position = hit.point + hit.normal * 0.0001f;
		if(i<6)
			THole.transform.rotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);
		else
			THole.transform.rotation = Quaternion.FromToRotation (Vector3.up, hit.normal);
  	}
	void GunMove(){
		float MoveX = (Input.GetAxis ("Mouse X")+Input.GetAxis ("Horizontal")) * Time.deltaTime * GunMoveX;
		float MoveY = Input.GetAxis ("Mouse Y") * Time.deltaTime * GunMoveY;
		float MoveZ = -Input.GetAxis ("Vertical") * Time.deltaTime * GunMoveX/2;
		Vector3 NewPosition = new Vector3 (StartPosition.x + MoveX, StartPosition.y + MoveY, StartPosition.z + MoveZ);
		MainBone.transform.localPosition = Vector3.Lerp (MainBone.transform.localPosition, NewPosition, Time.deltaTime/2);

	}
	void Belt(){
		if (Safty == true) {
			if(FlashlightLaserScr != null)
				FlashlightLaserScr.LaserMove = true;
			MainBone.transform.localPosition = Vector3.Lerp (MainBone.transform.localPosition, new Vector3 (0.8f, -0.44f, 0.18f), Time.deltaTime * 5);
			MainBone.transform.localRotation = Quaternion.Lerp (MainBone.transform.localRotation, Quaternion.Euler (17.3f, -47.36f, 0f), Time.deltaTime * 5);
			if (MainBone.transform.localRotation.x > 17.2f) {
				MainBone.transform.localRotation = Quaternion.Euler (17.3f, -47.36f, 0f);
			}
			if (MainBone.transform.localPosition.x > 0.79f) {
				MainBone.transform.localPosition = new Vector3 (0.8f, -0.44f, 0.18f);
			}
		} else {
			if ((!Input.GetMouseButton (1))||(WeaponCodeScr.AnimComp.IsPlaying ("Reload"))||(Input.GetKeyDown (ReloadKey))) {
				MainBone.transform.localPosition = Vector3.Lerp (MainBone.transform.localPosition, StartPosition, Time.deltaTime * 5);
				MainBone.transform.localRotation = Quaternion.Lerp (MainBone.transform.localRotation, Quaternion.Euler (StartRotation), Time.deltaTime * 5);
				if (MainBone.transform.localRotation.x == 0.5f) {
					MainBone.transform.localRotation = Quaternion.Euler (StartRotation);
				}
			}
		}
	}
}
