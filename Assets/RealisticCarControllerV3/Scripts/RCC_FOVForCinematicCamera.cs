//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Animation attached to "Animation Pivot" of the Cinematic Camera is feeding FOV float value.
/// </summary>
public class RCC_FOVForCinematicCamera : MonoBehaviour {




	private RCC_CinematicCamera cinematicCamera;
	public float FOV = 30f;

	public GameObject oneSprite,twoSprite,threeSprite,goSprite;

	int play1,play2,play3,playend = 0;
	

	//********* Custom CODE  *******

	Camera cam;
	Animator m_Animator;
	[SerializeField]
	RCC_SceneManager rccsceneManager;
	GameObject rccPivotCamera;
	Camera rccCam;
	SAudioManager sAudioManager;

	//********* Custom CODE  *******


	void Awake () {
		
		cinematicCamera = GetComponentInParent<RCC_CinematicCamera> ();

		rccPivotCamera= GameObject.FindGameObjectWithTag("RccPivotCamera");

		//********* Custom CODE  *******

		oneSprite = GameObject.FindGameObjectWithTag("3");
		twoSprite = GameObject.FindGameObjectWithTag("2");
		threeSprite = GameObject.FindGameObjectWithTag("1");
		goSprite= GameObject.FindGameObjectWithTag("Go");

		m_Animator = GetComponent<Animator>();
		cam = GetComponent<Camera>();
		
		cam.enabled = false;
		if(oneSprite)
		oneSprite.SetActive(true);
		if(twoSprite)
		twoSprite.SetActive(false);
		if(threeSprite)
		threeSprite.SetActive(false);
		if(goSprite)
		goSprite.SetActive(false);
		//********* Custom CODE  *******


	}

    private void Start()
    {
		rccCam = rccPivotCamera.GetComponent<Camera>();
		rccCam.enabled = false;
		cam.enabled = true;
		rccsceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
		if (!rccsceneManager.activePlayerVehicle)
			rccsceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
		rccsceneManager.activePlayerVehicle.KillEngine();

		sAudioManager = FindObjectOfType<SAudioManager>();
	}

	void Update () {

		cinematicCamera.targetFOV = FOV;

		
		if(!sAudioManager)
			sAudioManager = FindObjectOfType<SAudioManager>();

		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("StopCamera"))
		{
			if(threeSprite)
			threeSprite.SetActive(false);
			if(goSprite)
			goSprite.SetActive(true);
			rccCam.enabled = true;
			cam.enabled=false;
			
			
			if (sAudioManager)
            {
				if (playend == 0)
				{
					sAudioManager.Play("CountDownEnd");
					
					playend = 1;
					
				}
				


			}
			else
            {
			//Debug.Log("Warning!! Audio Manager Not Found in RCC_FovCinematicCamera");
            }

			StartCoroutine(DisableGo());
		}
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("New Animation"))
		{
			if(oneSprite)
			oneSprite.SetActive(true);
			if(rccsceneManager.activePlayerVehicle)
			rccsceneManager.activePlayerVehicle.KillEngine();
			if (sAudioManager)
			{
				if (play1 == 0)
				{
					
					sAudioManager.Play("CountDown");
					play1 = 1;
				}

			}
			else
			{
				

			}
		}
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("New Animation2"))
		{
			if(oneSprite)
			oneSprite.SetActive(false);
			if(twoSprite)
			twoSprite.SetActive(true);
			rccsceneManager.activePlayerVehicle.KillEngine();
			if (sAudioManager)
			{
				if (play2 == 0)
				{

					sAudioManager.Play("CountDown");
					play2 = 1;
				}
			}
			else
			{
				
			}
		}
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("New Animation3"))
		{
			if(twoSprite)
			twoSprite.SetActive(false);
			if(threeSprite)
			threeSprite.SetActive(true);
			
			if (sAudioManager)
			{
				
				if (play3 == 0)
				{
					sAudioManager.Play("CountDown");
					play3 = 1;
				}
				StartCoroutine(waitfor1sec());
			}
			else
			{
				Debug.Log("Warning!! Audio Manager Not Found in RCC_FovCinematicCamera");
				StartCoroutine(waitfor1sec());
			}
		}
	}
	IEnumerator waitfor1sec()
    {
		yield return new WaitForSeconds(0.5f);
		rccsceneManager.activePlayerVehicle.StartEngine();
		if(sAudioManager)
			sAudioManager.Play("EngineStart");
	}
	IEnumerator DisableGo()
	{
		yield return new WaitForSeconds(2f);
		if(goSprite)
		goSprite.SetActive(false);
		this.gameObject.SetActive(false);
	}
}
