using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConroller : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private Camera _camera;

    public bool _isOpenSettings = false;

    private int _score;
    private void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopUp.Close();
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")){
            if (_isOpenSettings)
            {
                settingsPopUp.Close();
            }
            else
            {
                OnOpenSettings();
                _isOpenSettings = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    public void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        if (_isOpenSettings)
        {
            GUI.Label(new Rect(posX, posY, size, size), " ");
        }
        else
        {
            GUI.Label(new Rect(posX, posY, size, size), "*");
        }
       
    }
    public void OnOpenSettings()
    {
        settingsPopUp.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer down");
    }

}
