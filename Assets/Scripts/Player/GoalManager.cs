using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private int charactersInGoal = 0;
    private PlayerController playerController;
    public GameObject congratulationsMessage;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        congratulationsMessage.SetActive(false);

        // Subscribe to goal events
        PlayableCharacter.OnGoalEnter += OnCharacterReachedGoal;
        PlayableCharacter.OnGoalExit += OnCharacterLeftGoal;
    }

    void OnCharacterReachedGoal (PlayableCharacter character) {
        charactersInGoal++;

        // If everyone is already in goal, deactivate all characters and show congrats message
        if (charactersInGoal == playerController.GetPlayableCharactersCount()) {
            Debug.Log("Everyone is in goal, so game will end now...");
            playerController.SwitchActiveCharacter(-1);
            congratulationsMessage.SetActive(true);
        }
    }

    void OnCharacterLeftGoal (PlayableCharacter character) {
        charactersInGoal--;
    }
}
