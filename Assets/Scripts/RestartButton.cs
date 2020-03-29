using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button restartButton;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        gameManager.RestartGame();
    }
}
