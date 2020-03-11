using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();
        //ставим в клетку х или 0, в зависимости от СторонаИгрока
        button.interactable = false;//отключаем возможность юзать эту кнопку
        gameController.EndTurn();//завершаем ход
    }

    public void SetGameControllerReference( GameController controller)
    {
        gameController = controller;
    }
}
