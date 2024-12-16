using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreInterface
{
    void ResetScores();                     //  Reset the scoreboard for the next game

    void AddLaunches(int launchIncrement);  //  Number of objects launched

    void UpdateScore(SNamesAndScores nameAndScore); //  Define name and score points for catches

    void DisplayMessage(string message);    //  Debug support
}
