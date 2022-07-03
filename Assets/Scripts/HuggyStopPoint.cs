using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HuggyStopPoint : MonoBehaviour
{
    public GameObject PlayerAndHuggyPoints;
    public LevelsData levelsData;
    public TextMeshProUGUI huggyPointsTMP;
    public TextMeshProUGUI playerPointsTMP;
    int huggyPoints;
    int playerpoints;
    bool playerWin = false;
    private void Start()
    {
        huggyPoints = levelsData.GetHuggyWinPoints();
        playerpoints = levelsData.GetPlayerWinPoints();
        huggyPointsTMP.text = huggyPoints.ToString();
        playerPointsTMP.text = playerpoints.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CarTrigger"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            levelsData.PlayerWon();
            huggyPoints = levelsData.GetHuggyWinPoints();
            playerpoints = levelsData.GetPlayerWinPoints();
            huggyPointsTMP.text = huggyPoints.ToString();
            playerPointsTMP.text = playerpoints.ToString();
            PlayerAndHuggyPoints.SetActive(true);
            StartCoroutine(HidePoints());

        }
        if(other.CompareTag("Huggy"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            levelsData.HuggyWon();
            huggyPoints = levelsData.GetHuggyWinPoints();
            playerpoints = levelsData.GetPlayerWinPoints();
            huggyPointsTMP.text = huggyPoints.ToString();
            playerPointsTMP.text = playerpoints.ToString();
            PlayerAndHuggyPoints.SetActive(true);
            StartCoroutine(HidePoints());
        }
    }
    IEnumerator HidePoints()
    {
        yield return new WaitForSeconds(3f);
        PlayerAndHuggyPoints.SetActive(false);
    }
}
