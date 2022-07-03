
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LevelTimeAndHealthTracker : MonoBehaviour
{
    public LevelsData levelsData;
   
    public TextMeshProUGUI timer;
    public TextMeshProUGUI goldTime;
    public TextMeshProUGUI silverTime;

    public TextMeshProUGUI playerHealth;
    [System.NonSerialized]
    private float min = 0;
    [SerializeField]
    private int sec = 0;
    private int rewardsec = 0;
    private float TimeValue=0;
    //public float GoldMinutes;
    [SerializeField]
    private int GoldSeconds;
    //public float SilverMinutes;
    [SerializeField]
    private int SilverSeconds;
    //public float BronzeMinutes;
    private OnObsitcleColusions obsitcleColusions;
    [SerializeField]
    float timersecs = 0.0f;
    //public float BronzeSeconds;
    // Start is called before the first frame update

    int reward;   // 0= gold , 1= silver , 2=bronze
    private void OnEnable()
    {
         
        var sceneName = SceneManager.GetActiveScene().name;
        int sceneIndex;
        int.TryParse(sceneName.Split(' ')[1], out sceneIndex);

        GoldSeconds = levelsData.GetGoldSeconds(sceneIndex-1);
        SilverSeconds = levelsData.GetSilverSeconds(sceneIndex-1);
        TimeSpan t = TimeSpan.FromSeconds(GoldSeconds);

        
        goldTime.text = string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);

        t = TimeSpan.FromSeconds(SilverSeconds);
        silverTime.text = string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
    }
    

    void Start()
    {


        reward = 0;
        TimeValue = 0;
        min = 0;
        sec = 0;
        timer.text = "" + min + " : " + sec;



        
    }

    // Update is called once per frame
    void Update()
    {
        timersecs += Time.deltaTime;
        rewardsec= (int)(timersecs % 60);
        sec = (int)(timersecs);
        /*                                                     //Enable after adding health
        if(!obsitcleColusions)
            obsitcleColusions = FindObjectOfType<OnObsitcleColusions>();
        if (obsitcleColusions)
        {
            playerHealth.text = string.Format("{0}/100", obsitcleColusions.GetFakeHealth().ToString(), obsitcleColusions.GetPlayerMaxHealth());
        }
        */
        if (TimeValue <= 60)
        {
            TimeValue += Time.deltaTime;
        }
        else
        {
            TimeValue = 0;
            min++;
        }
        displayTime(TimeValue);


    }

    void displayTime(float timeValue)
    {
        if(timeValue < 0)
        {
            timeValue = 0;
        }
        float minutes = Mathf.FloorToInt(min);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        /*
        
            if (minutes < GoldMinutes)
            {
                timer.color = new Color(255, 199, 0, 255);         //logic For Gold

            }
            else if(minutes < SilverMinutes)
            {
            
            timer.color = new Color(208, 221, 255, 255);        //logic For Silver
            }
            else
            {

                timer.color = new Color(218, 0, 0, 255);            // Logic For Bronze
            }
        */
        if(sec<=GoldSeconds)
        {
            //timer.color = new Color(255, 199, 0, 255);         //logic For Gold
            reward = 0;
        }
        else if (sec < SilverSeconds)
        {
            reward = 1;
            // timer.color = new Color(208, 221, 255, 255);        //logic For Silver
        }
        else
        {
            reward = 2;
            // timer.color = new Color(218, 0, 0, 255);            // Logic For Bronze
        }
    }
        
    public int getReward()
    {
        return reward;
    }

}
