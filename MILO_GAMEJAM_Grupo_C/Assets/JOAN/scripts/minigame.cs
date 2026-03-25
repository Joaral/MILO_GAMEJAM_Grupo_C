using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("UI Sprite Randomizer (Canvas)")]
    public Image targetPlantaImage;
    public Image targetSiluetaImage;
    public List<Sprite> spritesPlantas = new();
    public List<Sprite> spritesSiluetas = new();
    public bool setNativeSize = false;

    public float perfectScale;

    private int _currentIndex = -1;

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        if (targetPlantaImage == null)
            targetPlantaImage = GetComponent<Image>();

        PickRandomPlantAndShowSilueta(forceDifferentThanCurrent: false);

        StartMinigame();
        ui.isFirst = true;
        Time.timeScale = 1;

        perfectScale = maxScale * 0.675f;
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
            PickRandomPlantAndShowSilueta(forceDifferentThanCurrent: true);

            if (ui.isFirst)
                ui.isFirst = false;

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

            if (chargeValue >= maxScale * 0.85)
            {
                Debug.Log("¡Ha crecido demasiado!");
                scoreManager.AddScore(-10);
            }
            else if (chargeValue <= maxScale * 0.50)
            {
                Debug.Log("¡Apenas ha crecido!");
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

    public bool IsPlaying() => isPlaying;
    public float GetChargeValue() => chargeValue;

    private void PickRandomPlantAndShowSilueta(bool forceDifferentThanCurrent)
    {
        if (spritesPlantas == null || spritesPlantas.Count == 0) return;
        if (spritesSiluetas == null || spritesSiluetas.Count == 0) return;

        int maxIndex = Mathf.Min(spritesPlantas.Count, spritesSiluetas.Count) - 1;
        if (maxIndex < 0) return;

        int newIndex = _currentIndex;

        if (maxIndex == 0)
        {
            newIndex = 0;
        }
        else if (forceDifferentThanCurrent)
        {
            int safety = 0;
            while (newIndex == _currentIndex && safety < 50)
            {
                newIndex = Random.Range(0, maxIndex + 1);
                safety++;
            }
        }
        else
        {
            newIndex = Random.Range(0, maxIndex + 1);
        }

        _currentIndex = newIndex;

        ApplyToImage(targetSiluetaImage, spritesSiluetas[_currentIndex]);

        ApplyToImage(targetPlantaImage, spritesPlantas[_currentIndex]);

        //Escalar la silueta al punto medio del rango bueno
        if (ui != null && targetSiluetaImage != null)
        {
            Vector3 baseScale = ui.GetInitialScale();

            float perfectScale = maxScale * 0.675f; // punto medio del rango bueno

            targetSiluetaImage.transform.localScale = baseScale * perfectScale;

            //opcional: hacerla transparente
            Color c = targetSiluetaImage.color;
            c.a = 0.4f;
            targetSiluetaImage.color = c;
        }
    }

    private void ApplyToImage(Image img, Sprite sprite)
    {
        if (img == null) return;

        img.sprite = sprite;
        img.preserveAspect = true;

        if (setNativeSize)
            img.SetNativeSize();
    }
}