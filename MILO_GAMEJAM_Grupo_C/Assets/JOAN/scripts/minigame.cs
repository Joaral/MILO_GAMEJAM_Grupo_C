using UnityEngine;
using UnityEngine.InputSystem;

public class minigame : MonoBehaviour
{
    [Header("Minigame Settings")]
    public float chargeSpeed = 0.6f;
    public float chargeValue = 0f;

    public float maxScale = 3f;

    public bool isPlaying = false;
    public bool isCharging = false;

    [Header("Other Settings")]
    public InputSystem_Actions inputActions;
    public minigameUI ui;

    [Header("Time and Score Manager")]
    public TimeAndScoreManager scoreManager;

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        StartMinigame();
        ui.isFirst = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!isPlaying) return;

        HandleInput();
    }

    void StartMinigame()
    {
        isPlaying = true;
        isCharging = false;
        chargeValue = 0f;

        Debug.Log("minigame started");
    }

    void HandleInput()
    {
        var interact = inputActions.Player.Attack;

       if (interact.WasPressedThisFrame())
        {
            if (ui.isFirst)
            {
                ui.isFirst = false;
            }
            chargeValue = 0f;
            isCharging = true;
        }

        if (interact.IsPressed() && chargeValue < maxScale)
        {
            isCharging = true;
            ChargeFlower();
        }
        if (interact.WasReleasedThisFrame())
        {
            StopCharging();
            ui.AnimateToLast();
            if(chargeValue >= 2f)
            {
                Debug.Log("¡Ha crecido demasiado!");

                scoreManager.AddScore(-10);
            }
            else if (chargeValue <= 1.7f)
            {
                Debug.Log("¡A penas ha crecido!");

                scoreManager.AddScore(-10);
            }
            else
            {
                Debug.Log("¡La planta ha crecido saludable!");
                scoreManager.AddScore(100);
            }
        }
    }

    void ChargeFlower()
    {
        chargeValue += Time.deltaTime * chargeSpeed;
        chargeValue = Mathf.Clamp(chargeValue, 0, maxScale);
    }

    void StopCharging()
    {
        isCharging = false;
        Debug.Log("Carga detenida en: " + chargeValue);
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public float GetChargeValue()
    {
        return chargeValue;
    }
}
