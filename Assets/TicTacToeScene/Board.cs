using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    #region Singleton Setup
    public static Board instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There exists a game manager already");
            return;
        }

        instance = this;
    }
    #endregion

    [Header("From top left to bottom right")]
    [SerializeField]
    private List<Square> squares;

    public Square[,] GameBoard { get { return gameBoard; } }

    [HideInInspector]
    public List<Square> squaresLeft;   

    private Square[,] gameBoard;

    void Start()
    {
        FillBoard();

        squaresLeft = squares;
    }

    public void SquareSelected(Square square)
    {
        squaresLeft.Remove(square);
    }

    public bool CheckPlayerWin()
    {


        return false;
    }

    private void FillBoard()
    {
        gameBoard = new Square[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int squareNumber = 3 * i + j;
                gameBoard[i, j] = squares[squareNumber];

                AssignSquareType(squareNumber, gameBoard[i, j]);
            }
        }
    }

    private void AssignSquareType(int squareNumber, Square square)
    {
        switch (squareNumber)
        {
            case 0:
            case 2:
            case 6:
            case 8:
                square.type = Square.Type.Corner;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
                square.type = Square.Type.Edge;
                break;
            case 4:
                square.type = Square.Type.center;
                break;
        }
    }
}
