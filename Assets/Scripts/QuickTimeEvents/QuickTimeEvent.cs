using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{

    public int Player;

    public GameObject ButtonX;
    public GameObject ButtonY;
    public GameObject ButtonB;

    private Transform surroundingImage;
    private Camera camera;
    private float startTime;
    private float endTime;
    private float timeToInput = 1.5f;
    private float maxSurroundingScale = 0.4f;
    private float failSurroundingScale = 0.15f;

    private GameObject currentButton;

    private Action onSuccess;
    private Action onFail;

    private InputManager.Buttons selectedButton;

    void Start()
    {
        InitDependencies();
        gameObject.SetActive(false);
    }

    private void InitDependencies()
    {
        surroundingImage = transform.Find("SurroundingImage");
        camera = Camera.main;
    }

    public void StartQuickTimeEvent(Vector3 position, Action successAction, Action failAction)
    {
        startedEmissionRise = false;
        onSuccess = successAction;
        onFail = failAction;
        gameObject.SetActive(true);
        transform.position = position + 2 * Vector3.up;
        transform.forward = camera.transform.forward;
        NextRandomButton();
    }

    GameObject GetButtonFromEnum(InputManager.Buttons button)
    {
        switch (button)
        {
            case InputManager.Buttons.X:
                return ButtonX;
            case InputManager.Buttons.Y:
                return ButtonY;
            case InputManager.Buttons.B:
                return ButtonB;
        }
        return null;
    }

    void NextRandomButton()
    {
        SetEmissionForGem(currentButton,0f);
        surroundingImage.transform.localScale = Vector3.one * (failSurroundingScale + maxSurroundingScale);
        startTime = Time.time;
        endTime = startTime + timeToInput;
        selectedButton = (InputManager.Buttons)UnityEngine.Random.Range(1,3);
        ButtonX.SetActive(selectedButton == InputManager.Buttons.X);
        ButtonY.SetActive(selectedButton == InputManager.Buttons.Y);
        ButtonB.SetActive(selectedButton == InputManager.Buttons.B);

        currentButton = GetButtonFromEnum(selectedButton);
        SetEmissionForGem(currentButton, 0f);
    }

    private bool startedEmissionRise;
    void Update()
    {
        var surroundingTransformLocalScale = surroundingImage.transform.localScale.x;
        bool inputSuccess = InputManager.GetPlayerButtonDown((InputManager.Player) Player, selectedButton);

        if (surroundingTransformLocalScale < 0.3f)
        {
            SetEmissionForGem(currentButton,2 * (0.3f - surroundingTransformLocalScale)/(0.3f - failSurroundingScale));
            if (inputSuccess)
            {
                SetEmissionForGem(currentButton, 1f);
                onSuccess();
                NextRandomButton();
            }
        }

        if (surroundingTransformLocalScale > failSurroundingScale)
        {
            surroundingImage.transform.localScale =
                Vector3.one * (failSurroundingScale + maxSurroundingScale * (endTime - Time.time) / timeToInput);

        }
        else
        {
            onFail();
            NextRandomButton();
        }
    }

    void SetEmissionForGem(GameObject obj, float emission, IEnumerable<Material> materials = null)
    {
        if(!obj)
            return;

        if (materials == null)
            materials = obj.GetComponentsInChildren<MeshRenderer>().Select(mesh => mesh.material);

        foreach (var material in materials)
        {
            material.SetFloat("_Emission", Mathf.Min(1f, emission));
        }
    }

}
