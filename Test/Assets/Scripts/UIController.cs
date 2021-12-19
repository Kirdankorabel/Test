using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    public Text result;

    private void Start()
    {
        _startButton.onClick.AddListener(Play);
        _exitButton.onClick.AddListener(Exit);
        Target.victory += new System.Action(Win);
        Player.lose += new System.Action(Lose);
        GameController.Singletone.plyer.enabled = false;
    }

    private void Play()
    {
        GameController.Singletone.plyer.enabled = true;
        this.gameObject.SetActive(false);
        GameController.Singletone.gameObject.SetActive(true);
    }

    private void Exit()
        => Application.Quit();

    private void Win()
    {
        GameController.Singletone.plyer.enabled = false;
        this.gameObject.SetActive(true);
        result.text = "You win";
        Time.timeScale = 0;
    }

    private void Lose()
    {
        GameController.Singletone.plyer.enabled = false;
        this.gameObject.SetActive(true);        
        result.text = "You lose";
        Time.timeScale = 0;
    }
}
