using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Cambia el color del objeto cuando un Interactor lo selecciona
/// y recupera el color inicial cuando termina la selección.
/// Añadir al mismo GameObject que contiene XR Grab Interactable.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
public class ChangeColorOnSelect : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color selectedColor = Color.green;

    private XRGrabInteractable grabInteractable;
    private Color originalColor;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (targetRenderer != null)
            originalColor = targetRenderer.material.color;
        else
            Debug.LogWarning($"{name}: no se ha asignado un Renderer para cambiar el color.", this);
    }

    private void OnEnable()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
            return;

        grabInteractable.selectEntered.AddListener(OnSelected);
        grabInteractable.selectExited.AddListener(OnDeselected);
    }

    private void OnDisable()
    {
        if (grabInteractable == null)
            return;

        grabInteractable.selectEntered.RemoveListener(OnSelected);
        grabInteractable.selectExited.RemoveListener(OnDeselected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        if (targetRenderer != null)
            targetRenderer.material.color = selectedColor;
    }

    private void OnDeselected(SelectExitEventArgs args)
    {
        if (targetRenderer != null)
            targetRenderer.material.color = originalColor;
    }
}
