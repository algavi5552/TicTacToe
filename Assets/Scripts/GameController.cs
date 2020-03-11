using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;//число ходов общее

    public GameObject restartButton;
    private void Awake()//загрузка экземпляра сценария
    {
        gameOverPanel.SetActive(false);//отключим меню в начале игры
        SetGameControllerReferenceOnButtons();
        playerSide = "X"; //первым ходит Х
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)//перебираем все кнопки
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);//читаем их значение
        }
    }
    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()//конец хода, проверка на победу
        //простите за жуткий код, у автора видеоуроков он еще хуже, а времени осталось 1 сутки до сдачи=)
    {
        moveCount++;

        if (
            (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
         || (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
         || (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
         || (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
         || (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
         || (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
         || (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
         || (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
           )
        {
            GameOver();
        }

        if(moveCount >= 9)
        {
            SetGameOverText("no one wins");
        }

        ChangeSides();
      
    }

    void GameOver()
    {
        SetBoardInteractable(false);//тушим всю доску
        SetGameOverText(playerSide + " wins");
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void SetGameOverText(string value)//какой текст передадим, тот и напишет в меню
    {
        gameOverPanel.SetActive(true);//вызываем меню игры
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);//отключим меню в начале игры
        playerSide = "X"; //первым ходит Х
        moveCount = 0;
        SetBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";//стираем все Х и О
        }

    }
    void SetBoardInteractable(bool toggle)//тумблер, выставляет доску готовой к кликам
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;// возможность юзать все кнопки
            
        }
    }
}
