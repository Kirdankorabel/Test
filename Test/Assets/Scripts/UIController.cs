using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private Text _result;

    private void Start()
    {
        _startPanel.SetActive(true);
        _resultPanel.SetActive(false);

        _startButton.onClick.AddListener(Play);
        _exitButton.onClick.AddListener(Exit);
        _acceptButton.onClick.AddListener(Accept);

        Target.victory += new System.Action(Win);
        Player.lose += new System.Action(Lose);
        GameController.Singletone.player.enabled = false;
    }

    private void Play()
    {
        GameController.Singletone.player.enabled = true;
        _startPanel.SetActive(false);
        GameController.Singletone.gameObject.SetActive(true);
    }

    private void Exit()
        => Application.Quit();

    private void Win()
    {
        _resultPanel.SetActive(true);
        _result.text = "You win";
        GameController.Singletone.player.enabled = false;
    }

    private void Lose()
    {
        _resultPanel.SetActive(true);
        _result.text = "You lose";
        GameController.Singletone.player.enabled = false;
    }

    private void Accept()
    {
        _startPanel.SetActive(true);
        _resultPanel.SetActive(false);
        GameController.Singletone.Restart();
    }
}
