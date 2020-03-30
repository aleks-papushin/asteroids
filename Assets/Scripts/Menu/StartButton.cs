using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button startButton;

    void Start()
    {        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        gameManager.RestartGame();
    }
}
