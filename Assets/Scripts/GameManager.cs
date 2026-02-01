using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cardObject;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] TMP_Text ModeText;
    [SerializeField] TMP_Text lelvelText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Slider energySlider;
    private List<List<bool>> statusPreview = new List<List<bool>>();
    private List<List<bool>> blankStatus = new List<List<bool>>();

    // private List<List<bool>> Modifiers = new List<List<bool>>();
    private List<GameObject> hand = new List<GameObject>();
    private int selectedCardIndex = -1;
    private int handSize = 5;
    private int currentEnergy;
    private int maxEnergy = 100;
    public List<List<bool>> status = new List<List<bool>>();
    public string currentCardMode = "ADD";
    public int gameLevel;
    // public enum CardMode { ADD, SUBTRACT };
    // public CardMode cardMode;
    EnemyManager enemyManager;
    StatusManager statusManager;
    CellManager cellManager;
    Card card;

    void Awake()
    {
        cellManager = GetComponent<CellManager>();
        enemyManager = GetComponent<EnemyManager>();
        statusManager = GetComponent<StatusManager>();
        card = FindFirstObjectByType<Card>();

        gameLevel = 1;
        lelvelText.text = gameLevel.ToString();
        // cardMode = CardMode.ADD;
    }

    void Start()
    {
        for (int y = 0; y < 10; y++)
        {
            List<bool> row = new List<bool>();

            for (int x = 0; x < 10; x++)
            {
                row.Add(false);

            }
            status.Add(row);
            blankStatus.Add(row);
        }

        currentEnergy = maxEnergy;
        UpdateEnergyBar();
        spawnCard();
    }

    void onCardSelect(int cardIndex, List<List<bool>> cardModifier)
    {
        if (selectedCardIndex == cardIndex)
        {
            selectedCardIndex = -1;
            updatePreview(blankStatus);
        }
        else
        {
            selectedCardIndex = cardIndex;
            updatePreview(cardModifier);
        }

    }

    void updatePreview(List<List<bool>> cardModifier)
    {

        // Get mode
        var mode = "OR";
        if (currentCardMode != "ADD")
        {
            mode = "XOR";
        }

        // Apply modifier to preview
        statusPreview = statusManager.ApplyModifiers(status, cardModifier, mode);
        cellManager.UpdateCellColor(statusPreview);

        for (int i = 0; i < handSize; i++)
        {
            if (selectedCardIndex >= 0 && i == selectedCardIndex)
            {
                hand[i].GetComponent<Card>().cardUp();
            }
            else
            {
                hand[i].GetComponent<Card>().cardDown();
            }
        }
    }

    void spawnCard()
    {
        for (int i = 0; i < handSize; i++)
        {
            int offset = i * 5;

            GameObject newcard = Instantiate(cardObject, cardSpawnPoint.position + new Vector3(offset, 0, 0), Quaternion.identity);
            newcard.GetComponent<Card>().setNewCard();

            List<List<bool>> shapeMatrix = newcard.GetComponent<Card>().GetShapeMatrix();

            // Add clicker to Object
            var clicker = newcard.AddComponent<ClickCallback>();
            var cardIndex = i;
            clicker.OnClickAction = () => onCardSelect(cardIndex, shapeMatrix);

            // Add card to hand
            hand.Add(newcard);
        }
    }

    // public void GetCardShapeMask(List<List<bool>> shape)
    // {
    //     Modifiers = shape;
    //     Debug.Log(Modifiers.Capacity);
    // }

    // public void GetCardShapeUnMask(List<List<bool>> shape)
    // {
    //     Modifiers = shape;
    //     Debug.Log(Modifiers);
    // }

    public void UpdateGameStatus()
    {
        // List<List<bool>> newStatus = statusManager.ApplyModifiers(status, Modifiers, "OR");
        hand[selectedCardIndex].GetComponent<Card>().useCard();
        status = statusPreview;
        cellManager.UpdateCellColor(status);
    }

    public void ResetStatus()
    {
        for (int y = 0; y < status.Count; y++)
        {
            for (int x = 0; x < status[y].Count; x++)
            {
                status[y][x] = false;
            }
        }

        selectedCardIndex = -1;
        statusPreview = status;
        cellManager.UpdateCellColor(status);
    }

    public void toggleMode()
    {
        if (currentCardMode == "ADD")
        {
            currentCardMode = "SUB";
            var cardModifier = hand[selectedCardIndex].GetComponent<Card>().GetShapeMatrix();
            updatePreview(cardModifier);
        }
        else
        {
            currentCardMode = "ADD";
            var cardModifier = hand[selectedCardIndex].GetComponent<Card>().GetShapeMatrix();
            updatePreview(cardModifier);
        }

        ModeText.text = currentCardMode;
    }

    public void resetHand()
    {
        foreach (GameObject cardObj in hand)
        {
            if (cardObj != null)
            {
                Destroy(cardObj);
            }
        }

        hand.Clear();
        selectedCardIndex = -1;

        statusPreview = status;
        cellManager.UpdateCellColor(status);

        spawnCard();
    }

    public void commit()
    {
        UpdateGameStatus();
    }

    public void push()
    {
        int energyUsed = calculateEnergy();
        // Debug.Log(energyUsed);
        currentEnergy -= energyUsed;
        // Debug.Log(currentEnergy);
        UpdateEnergyBar();

        enemyManager.takeDamage();
        enemyManager.ememiesMove();
        resetHand();
        ResetStatus();
        lelvelText.text = gameLevel.ToString();
    }

    void UpdateEnergyBar()
    {
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
        if (currentEnergy <= 0)
        {
            GameOver();
        }
    }

    public void resetEnergy()
    {
        currentEnergy = 100;
        UpdateEnergyBar();
    }

    int calculateEnergy()
    {
        int energyAmount = 0;

        for (int y = 0; y < status.Count; y++)
        {
            for (int x = 0; x < status[y].Count; x++)
            {
                if (status[y][x] == true)
                {
                    energyAmount += 1;
                }
            }
        }

        return energyAmount;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}