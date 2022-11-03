using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]//Для проверки присоедены ли все необходимые сценарию компоненты
[AddComponentMenu("Control Script/FPS Input")]//Добавление сценария в меню компонентов в редакторе Unity

public class FPSInput : MonoBehaviour
{
    private CharacterController _charController;//Переменная для ссылки на компонент CharacterController

    public float speed = 6.0f;
    public float gravity = -9.8f;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();// Доступ к другим компонентам, присоедененным к этому же объекту
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;//Считывание с клавиатуры(a/d)
        float deltaZ = Input.GetAxis("Vertical") * speed;//Считывание с клавиатуры(w/s)
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);//Ограничение движения по диагонали
        movement.y = gravity;

        movement *= Time.deltaTime;//Привязка движения ко времени, а не к кадрам
        movement = transform.TransformDirection(movement);//Преобразования вектора движения от локальных к глобальным координатам
        _charController.Move(movement);//Вызов метода Move из CharacterController
    }
}
