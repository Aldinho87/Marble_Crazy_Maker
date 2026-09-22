using UnityEngine;
using UnityEngine.UI;

public class YLevelSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    [Header("Buttons")]
    [SerializeField] private Button levelMinus2Button;
    [SerializeField] private Button levelMinus1Button;
    [SerializeField] private Button level0Button;
    [SerializeField] private Button levelPlus1Button;
    [SerializeField] private Button levelPlus2Button;

    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindAnyObjectByType<GridManager>();
        }

        ConfigureButtons();
        RefreshVisualState();
    }

    private void ConfigureButtons()
    {
        if (levelMinus2Button != null)
            levelMinus2Button.onClick.AddListener(() => SelectLevel(-2));

        if (levelMinus1Button != null)
            levelMinus1Button.onClick.AddListener(() => SelectLevel(-1));

        if (level0Button != null)
            level0Button.onClick.AddListener(() => SelectLevel(0));

        if (levelPlus1Button != null)
            levelPlus1Button.onClick.AddListener(() => SelectLevel(1));

        if (levelPlus2Button != null)
            levelPlus2Button.onClick.AddListener(() => SelectLevel(2));
    }

    private void SelectLevel(int level)
    {
        if (gridManager == null)
            return;

        gridManager.SetActiveYLevel(level);

        RefreshVisualState();
    }

    private void RefreshVisualState()
    {
        if (gridManager == null)
            return;

        int activeLevel =
            gridManager.ActiveYLevel;

        UpdateButtonState(
            levelMinus2Button,
            activeLevel == -2
        );

        UpdateButtonState(
            levelMinus1Button,
            activeLevel == -1
        );

        UpdateButtonState(
            level0Button,
            activeLevel == 0
        );

        UpdateButtonState(
            levelPlus1Button,
            activeLevel == 1
        );

        UpdateButtonState(
            levelPlus2Button,
            activeLevel == 2
        );
    }

    private void UpdateButtonState(
        Button button,
        bool active)
    {
        if (button == null)
            return;

        ColorBlock colors =
            button.colors;

        colors.normalColor =
            active
                ? new Color(0.1f, 0.8f, 1f, 1f)
                : new Color(0.25f, 0.25f, 0.25f, 1f);

        colors.highlightedColor =
            new Color(0.4f, 0.9f, 1f, 1f);

        colors.pressedColor =
            new Color(0.05f, 0.6f, 0.8f, 1f);

        button.colors = colors;
    }
}
