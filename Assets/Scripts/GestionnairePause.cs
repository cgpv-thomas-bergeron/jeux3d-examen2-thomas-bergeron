using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionnairePause : MonoBehaviour
{
    public static GestionnairePause Instance { get; private set; }

    [SerializeField]
    private float tempPause = 5;

    [SerializeField]
    private int prix = 5;

    public event Action<float> pauseAnimation;

    [SerializeField]
    private Boule boule;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerInput controles = ControleurJeu.Instance.Controles;

        controles.actions.FindAction("AcheterPause").started += Pause;
    }

    private void Pause(InputAction.CallbackContext contexte)
    {
        if (boule.getNombrePieces() >= prix)
        {
            pauseAnimation.Invoke(tempPause);
        }
    }
}
