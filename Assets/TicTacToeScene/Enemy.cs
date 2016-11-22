using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;

    private Board board;

    void Start()
    {
        gameManager = GameManager.instance;
        board = Board.instance;
    }

    public void TakeTurn()
    {
        if(gameManager.turnsTaken == 0)
        {
            ExecuteOpeningMove();
        }
        else if(gameManager.turnsTaken == 1)
        {
            // TODO : Optimize the move
            PickRandomSquare();
        }
        else
        {
            // Try to win
            Square squareToPick = CheckForCondition(Square.State.Enemy);
            if(squareToPick != null)
            {             
                squareToPick.EnemySelect();
                gameManager.EnemyWin();
                return;
            }

            // Try to block
            squareToPick = CheckForCondition(Square.State.Player);
            if (squareToPick != null)
            {              
                squareToPick.EnemySelect(); 
                return;
            }

            PickRandomSquare();
        }
    }

    private Square CheckForCondition(Square.State condition)
    {
        Square squareToPick = null;

        Square[,] gameBoard = board.GameBoard;

        // Check top row
        squareToPick = CheckRow(0, gameBoard, condition);
        if(squareToPick != null)
        {
            return squareToPick;
        }

        // Check middle row
        squareToPick = CheckRow(1, gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check bottom row
        squareToPick = CheckRow(2, gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check left column
        squareToPick = CheckColumn(0, gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check middle column
        squareToPick = CheckColumn(1, gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check right column
        squareToPick = CheckColumn(2, gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check left to right diag column
        squareToPick = LeftToRightDiag(gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        // Check right to left diag column
        squareToPick = RightToLeftDiag(gameBoard, condition);
        if (squareToPick != null)
        {
            return squareToPick;
        }

        return squareToPick;
    }

    // TODO
    private void ExecuteOpeningMove()
    {        
    }

    private void PickRandomSquare()
    {
        Random r = new Random();
        Square square = Board.instance.squaresLeft[Random.Range(0, Board.instance.squaresLeft.Count - 1)];
        square.GetComponent<Square>().EnemySelect();
    }

    #region Checking rows, columns and diags for a blocking square
    private Square CheckRow(int row, Square[,] gameBoard, Square.State condition)
    {
        Square[] squares = new Square[3];
        Square.State[] states = new Square.State[3];

        squares[0] = gameBoard[row, 0];        
        squares[1] = gameBoard[row, 1];
        squares[2] = gameBoard[row, 2];

        states[0] = squares[0].owner;
        states[1] = squares[1].owner;
        states[2] = squares[2].owner;

        // x x _
        if (states[0] == condition && states[1] == condition && states[2] == Square.State.Empty)
        {
            return squares[2];
        }

        // x _ x
        if (states[0] == condition && states[1] == Square.State.Empty && states[2] == condition)
        {
            return squares[1];
        }

        // _ x x
        if (states[0] == Square.State.Empty && states[1] == condition && states[2] == condition)
        {
            return squares[0];
        }

        return null;
    }

    private Square CheckColumn(int column, Square[,] gameBoard, Square.State condition)
    {
        Square[] squares = new Square[3];
        Square.State[] states = new Square.State[3];

        squares[0] = gameBoard[0, column];
        squares[1] = gameBoard[1, column];
        squares[2] = gameBoard[2, column];

        states[0] = squares[0].owner;
        states[1] = squares[1].owner;
        states[2] = squares[2].owner;

        // x
        // x
        // _
        if (states[0] == condition && states[1] == condition && states[2] == Square.State.Empty)
        {
            return squares[2];
        }

        // x 
        // _ 
        // x
        if (states[0] == condition && states[1] == Square.State.Empty && states[2] == condition)
        {
            return squares[1];
        }

        // _ 
        // x 
        // x
        if (states[0] == Square.State.Empty && states[1] == condition && states[2] == condition)
        {
            return squares[0];
        }

        return null;
    }

    private Square LeftToRightDiag(Square[,] gameBoard, Square.State condition)
    {
        Square[] squares = new Square[3];
        Square.State[] states = new Square.State[3];

        squares[0] = gameBoard[0, 0];
        squares[1] = gameBoard[1, 1];
        squares[2] = gameBoard[2, 2];

        states[0] = squares[0].owner;
        states[1] = squares[1].owner;
        states[2] = squares[2].owner;

        // x
        //   x
        //     _
        if (states[0] == condition && states[1] == condition && states[2] == Square.State.Empty)
        {
            return squares[2];
        }

        // x 
        //   _ 
        //     x
        if (states[0] == condition && states[1] == Square.State.Empty && states[2] == condition)
        {
            return squares[1];
        }

        // _ 
        //   x 
        //     x
        if (states[0] == Square.State.Empty && states[1] == condition && states[2] == condition)
        {
            return squares[0];
        }

        return null;
    }

    private Square RightToLeftDiag(Square[,] gameBoard, Square.State condition)
    {
        Square[] squares = new Square[3];
        Square.State[] states = new Square.State[3];

        squares[0] = gameBoard[0, 2];
        squares[1] = gameBoard[1, 1];
        squares[2] = gameBoard[2, 0];

        states[0] = squares[0].owner;
        states[1] = squares[1].owner;
        states[2] = squares[2].owner;

        //     x
        //   x
        // _
        if (states[0] == condition && states[1] == condition && states[2] == Square.State.Empty)
        {
            return squares[2];
        }

        //     x 
        //   _ 
        // x
        if (states[0] == condition && states[1] == Square.State.Empty && states[2] == condition)
        {
            return squares[1];
        }

        //     _ 
        //   x 
        // x
        if (states[0] == Square.State.Empty && states[1] == condition && states[2] == condition)
        {
            return squares[0];
        }

        return null;
    }
    #endregion
}
