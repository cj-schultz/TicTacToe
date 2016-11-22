using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public enum State { Player, Enemy, Empty };
    public State owner;

    public enum Type { Corner, Edge, center };
    public Type type;

    private Image image;

    private GameManager gameManager;

    void Start()
    {
        owner = State.Empty;       
        gameManager = GameManager.instance;
        image = GetComponent<Image>();
    }

    public void PlayerSelect()
    {
        if (gameManager.IsPlayersTurn && owner == State.Empty)
        {
            image.color = Color.green;
            owner = State.Player;
            gameManager.TurnTaken(this);
        }        
    }

    public void EnemySelect()
    {
        if (!gameManager.IsPlayersTurn && owner == State.Empty)
        {
            image.color = Color.red;
            owner = State.Enemy;
            gameManager.TurnTaken(this);
        }
    }
}
