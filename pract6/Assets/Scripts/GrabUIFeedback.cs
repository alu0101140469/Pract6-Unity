using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Actualiza un texto TextMeshPro cuando se agarra o se suelta el objeto.
/// Añadir al mismo GameObject que contiene XR Grab Interactable.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
public class GrabUIFeedback : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private string objectLabel = "Objeto de la Asset Store";

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
            return;

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        if (grabInteractable == null)
            return;

        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void Start()
    {
        SetStatus("Listo: acércate al objeto y selecciónalo.");
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        SetStatus($"{objectLabel}: AGARRADO");
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        SetStatus($"{objectLabel}: SOLTADO");
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
        else
            Debug.LogWarning("GrabUIFeedback: asigna el TextMeshPro en el campo Status Text.", this);
    }
}
