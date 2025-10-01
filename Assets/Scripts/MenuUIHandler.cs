using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI BestScore;
    public TMP_InputField PlayerName;
    public Button startButton;


    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(PlayerDataManager.Instance.BestPlayerName))
        {
            BestScore.text = "Best Score : "
            + PlayerDataManager.Instance.BestPlayerName + " : "
            + PlayerDataManager.Instance.BestScore;

            PlayerName.text = PlayerDataManager.Instance.BestPlayerName;
        }
    }

    public void StartNew()
    {
        // Player's name cannot be empty
        // Note: Zero-width space present in TMP_TexT
        if (string.IsNullOrWhiteSpace(PlayerName.text/*.Replace("\u200B", "")*/))
        {
            Debug.Log("Player's name cannot be empty.");
            PlayerName.text = "";
            startButton.interactable = false;
            return;
        }
        else
        {
            PlayerDataManager.Instance.PlayerName = PlayerName.text;
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
        PlayerDataManager.Instance.SavePlayerData();
    }
}
