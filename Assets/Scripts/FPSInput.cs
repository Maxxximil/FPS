using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]//��� �������� ���������� �� ��� ����������� �������� ����������
[AddComponentMenu("Control Script/FPS Input")]//���������� �������� � ���� ����������� � ��������� Unity

public class FPSInput : MonoBehaviour
{
    private CharacterController _charController;//���������� ��� ������ �� ��������� CharacterController

    public float speed = 6.0f;
    public float gravity = -9.8f;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();// ������ � ������ �����������, �������������� � ����� �� �������
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;//���������� � ����������(a/d)
        float deltaZ = Input.GetAxis("Vertical") * speed;//���������� � ����������(w/s)
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);//����������� �������� �� ���������
        movement.y = gravity;

        movement *= Time.deltaTime;//�������� �������� �� �������, � �� � ������
        movement = transform.TransformDirection(movement);//�������������� ������� �������� �� ��������� � ���������� �����������
        _charController.Move(movement);//����� ������ Move �� CharacterController
    }
}
