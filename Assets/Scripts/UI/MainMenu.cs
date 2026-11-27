using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.interactable = SaveSystem.HasSave;
    }

    public void Continue()
    {
        SaveData data = SaveSystem.Load();
        GameProgress.Apply(data);

        int targetLevel = Mathf.Max(1, data.levelIndex);
        Flow.Go(targetLevel);
    }

    public void NewGame()
    {
        SaveSystem.Delete();
        GameProgress.Reset();
        Flow.Go(1);
    }

    public void Quit() => Flow.Quit();
}