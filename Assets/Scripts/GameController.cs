using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public Text gameOverText;

    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject StartInfo;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    
    private int moveCount;//число ходов общее
    private int computerChoice;
    private string playerSide;
    private string computerSide;

    public bool playerMove;
    public float delay;//задержка перед ходом компа

    private void Awake()//загрузка экземпляра сценария
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);//отключим меню в начале игры
        moveCount = 0;// сброс счетчика ходов
        playerMove = true;//первый ход за человеком
    }

    private void Update()
    {
        if (playerMove == false)
        {
            delay +=delay * Time.deltaTime;
            if (delay >= 100)
            {
                computerChoice = Random.Range(0, buttonList.Length);
                if(buttonList[computerChoice].GetComponentInParent<Button>().interactable==true)//если поле свободно
                {
                    buttonList[computerChoice].text = GetComputerSide();//комп ставит свою Х или О
                    buttonList[computerChoice].GetComponentInParent<Button>().interactable = false;//отметить поле занятым
                    EndTurn();
                }
            }
        }
    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)//перебираем все кнопки
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);//читаем их значение
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);//подсветим Х
            computerSide = "O";
        }
        else
        {
            SetPlayerColors(playerO, playerX);//подсветим O
            computerSide = "X";
        }

        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);//включить кнопки полей
        SetPlayerButtons(false);//отключить кнопки выбора стороны
        StartInfo.SetActive(false);//отключили подсказку
        
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public string GetComputerSide()
    {
        return computerSide;
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
            GameOver(playerSide);
        }

        if ( 
            (buttonList[0].text == computerSide && buttonList[1].text == computerSide && buttonList[2].text == computerSide)
         || (buttonList[3].text == computerSide && buttonList[4].text == computerSide && buttonList[5].text == computerSide)
         || (buttonList[6].text == computerSide && buttonList[7].text == computerSide && buttonList[8].text == computerSide)
         || (buttonList[0].text == computerSide && buttonList[3].text == computerSide && buttonList[6].text == computerSide)
         || (buttonList[1].text == computerSide && buttonList[4].text == computerSide && buttonList[7].text == computerSide)
         || (buttonList[2].text == computerSide && buttonList[5].text == computerSide && buttonList[8].text == computerSide)
         || (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide)
         || (buttonList[2].text == computerSide && buttonList[4].text == computerSide && buttonList[6].text == computerSide)
         )
            {
            GameOver(computerSide);
            }





        if (moveCount >= 9)
        {
            GameOver("no one");
        }
        ChangeSides();
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;//подсветим текущего игорька
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;//затемним старого игорька
        oldPlayer.text.color = inactivePlayerColor.textColor;

    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);//тушим всю доску
        SetGameOverText(winningPlayer + " wins");
        SetPlayerColorsInactive();
    }

    void ChangeSides()
    {
        //playerSide = (playerSide == "X") ? "O" : "X";
        playerMove = (playerMove == true) ? false : true;//чередуем ход 
       // if (playerSide=="X")
        if (playerMove ==true)
        {
            SetPlayerColors(playerX, playerO);//подсветим Х
        }
        else
        {
            SetPlayerColors(playerO, playerX);// подсветим второго игрока, О
        }
    }

    void SetGameOverText(string value)//какой текст передадим, тот и напишет в меню
    {
        gameOverPanel.SetActive(true);//вызываем меню игры
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        SetPlayerButtons(true);//подсветить кнопки выбора стороны
        gameOverPanel.SetActive(false);//отключим меню в начале игры
        SetPlayerColorsInactive();
        StartInfo.SetActive(true);//включили подсказку
        moveCount = 0;
        playerMove = true;
        delay = 10;
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

    void SetPlayerButtons(bool toggle)//выбираем сторону с помощью кнопки
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;//подсветим текущего игорька
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;//затемним старого игорька
        playerO.text.color = inactivePlayerColor.textColor;

    }

}
