using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // Attach the Button component to this variable in the Inspector
    public Button restartButton;

    private void Start()
    {
        // Add a click listener to the button
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
