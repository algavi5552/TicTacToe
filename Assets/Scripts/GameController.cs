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

    private void Awake()//загрузка экземпляра сценария
    {
        gameOverPanel.SetActive(false);//отключим меню в начале игры
        SetGameControllerReferenceOnButtons();
        playerSide = "X"; //первым ходит Х
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
         ChangeSides();

    }

    void GameOver()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;//отключаем возможность юзать все кнопки
            gameOverPanel.SetActive(true);//вызываем меню игры
            gameOverText.text = playerSide + " wins";
        }
        
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
