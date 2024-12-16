using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour, IScoreInterface
{
    private int launches;
    private int catches;
    private int score;
    private string message;

    private TextMeshPro textDisplay;

    private SNamesAndScores[] aNamesAndScores = new SNamesAndScores[50];
    private int numOfUsedNames;

    // Start is called before the first frame update
    private void Start()
    {
        textDisplay = gameObject.AddComponent<TextMeshPro>();
        textDisplay.fontSize = 3;
        textDisplay.alignment = TextAlignmentOptions.Top;
        textDisplay.rectTransform.localScale = new Vector3(2, 1, 1);
        ResetScores();
    }


    // Update is called once per frame
    private void Update()
    {
        textDisplay.text = "\nCatches:  " + catches + " out of " + launches;
        textDisplay.text += "\nScore: " + score + "\n\n";

        for (int i = 0; i < numOfUsedNames; i++)
        {
            if (i > 0)
            {
                textDisplay.text += aNamesAndScores[i].score + " x ";
                textDisplay.text += aNamesAndScores[i].name + "\n";
            }
        }
        textDisplay.text += "\n" + message;
    }

    public void ResetScores()
    {
        launches = 0;
        score = 0;
        catches = 0;
        message = "Press Button to Start";

        for (int i = 0; i < aNamesAndScores.Length; i++) {
            aNamesAndScores[i].name = "";
            aNamesAndScores[i].score = 0;
        }
        numOfUsedNames = 1;
    }


    public void AddLaunches(int launchIncrement)  //  Number of objects launched
    {
        launches += launchIncrement;
        if (launches == 1)
        {
            message = "";       //  Reset start message
        }
    }


    public void UpdateScore(SNamesAndScores nameAndScore)   //  Add score points for catches
    {
        catches++;                      //  Update general statistics
        score += nameAndScore.score;    //  Update general statistics

        //  Update object specific statistics
        bool nameFound = false;
        for (int i = 0; i < numOfUsedNames; i++)
        {
            if (aNamesAndScores[i].name == nameAndScore.name)
            {
                aNamesAndScores[i].score++;
                nameFound = true;
            }
        }
        if (nameFound == false) {
            aNamesAndScores[numOfUsedNames].name = nameAndScore.name;
            aNamesAndScores[numOfUsedNames].score = 1;
            numOfUsedNames++;
        }
    }


    public void DisplayMessage(string messageUpdate)    //  Debug support
    {
        message = messageUpdate;
    }

}
