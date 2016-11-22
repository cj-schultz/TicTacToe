using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Setup
    public static GameManager instance;

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

    public bool IsPlayersTurn { get { return playersTurn; } }
    public int turnsTaken = 0;

    private bool playersTurn = true;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TurnTaken(Square square)
    {
        Board.instance.SquareSelected(square);

        turnsTaken++;

        // Draw
        if(turnsTaken >= 9)
        {
            Invoke("Restart", 1.5f);
        }

        // The player has won
        if(Board.instance.CheckPlayerWin())
        {

        }

        playersTurn = !playersTurn;

        if (!playersTurn)
        {
            enemy.TakeTurn();
        }
    }

    public void EnemyWin()
    {
        Debug.Log("ENEMY HAS WON");
        SceneManager.LoadScene(0);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
