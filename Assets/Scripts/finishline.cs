using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class finishline : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public LevelsData levelsData;

    [SerializeField]
    int reward;
    public GameObject rccCanvas;
    public GameObject oldCanvas;

    public GameObject finishMenu;
    public RCC_SceneManager rCC_SceneManager;
    public RCC_Camera rccCam;

    public LevelTimeAndHealthTracker levelTime;
    private SAudioManager audioManager;
    // Start is called before the first frame update

    private void Start()
    {
        audioManager = FindObjectOfType<SAudioManager>();
    }

    public int garReward()
    {
        return reward;
    }

    private void OnTriggerEnter(Collider other)
    {





        if (other.gameObject.tag == "CarTrigger")
        {
            var sceneName = SceneManager.GetActiveScene().name;
            int sceneIndex;
            int.TryParse(sceneName.Split(' ')[1], out sceneIndex);
            if(sceneIndex<levelsData.careerMode.Count)
             levelsData.careerMode[sceneIndex].isLocked = false;
            levelsData.careerMode[sceneIndex-1].isCompleted = true;
            Rigidbody rb = rCC_SceneManager.activePlayerVehicle.gameObject.GetComponent<Rigidbody>();

            

           
            rCC_SceneManager.activePlayerVehicle.KillEngine();
            
           
            rccCam.changeCameraToTop();
            rCC_SceneManager.activePlayerVehicle.SetCanControl(false);
            rccCanvas.SetActive(false);
            oldCanvas.SetActive(false);
            reward=levelTime.getReward();

            audioManager.Play("Winning");

            StartCoroutine(ShowFinishMenu());
            
        }
    }

    IEnumerator ShowFinishMenu()
    {
        yield return new WaitForSeconds(3f);
        
        finishMenu.SetActive(true);
        if (AdsManager.Instance)
        {
            AdsManager.Instance.showUnityAdmobInter();
            AdsManager.Instance.ShowRectangularBanner();
        }
    }
}
