using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int activeCharIndex = 0;
    private int charactersInGoal = 0;
    private List<PlayableCharacter> playableCharacters;

    // Enables the camera at the specified position and disable the rest
    void SwitchActiveCharacter (int index) {
        if (index < 0 || index >= playableCharacters.Count) {
            Debug.LogWarning("Specified index does not belong to a character, so everyone will be deactivated");
        } else {
            Debug.Log("Switch to character " + playableCharacters[index].gameObject.name);
        }

        for (int i = 0; i < playableCharacters.Count; i++) {
            bool shouldBeActive = i == index;
            playableCharacters[i].isActiveCharacter = shouldBeActive;
            playableCharacters[i].assignedCamera.enabled = shouldBeActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Store list of characters found with tag "Player"
        playableCharacters = new List<PlayableCharacter>();
        foreach (GameObject playerObj in playerObjects) {
            PlayableCharacter character = playerObj.GetComponent<PlayableCharacter>();

            if (character) {
                playableCharacters.Add(character);
            } else {
                Debug.LogWarning("Object " + character.name + " is not a valid playable character!");
            }
        }

        // Enable first character by default
        SwitchActiveCharacter(activeCharIndex);

        // Subscribe to goal events
        PlayableCharacter.OnGoalEnter += OnCharacterReachedGoal;
        PlayableCharacter.OnGoalExit += OnCharacterLeftGoal;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch currently active character when the user presses tab key
        if (Input.GetKeyDown(KeyCode.Tab)) {
            activeCharIndex += 1;
            if (activeCharIndex == playableCharacters.Count) {
                activeCharIndex = 0;
            }

            SwitchActiveCharacter(activeCharIndex);
        }
    }

    void OnCharacterReachedGoal (PlayableCharacter character) {
        charactersInGoal++;

        // If all characters are already in goal, activate camera pointing to all and end game
        if (charactersInGoal == playableCharacters.Count) {
            Debug.Log("Everyone is in goal, so game will end now...");
            SwitchActiveCharacter(-1);
        }
    }

    void OnCharacterLeftGoal (PlayableCharacter character) {
        charactersInGoal--;
    }
}
