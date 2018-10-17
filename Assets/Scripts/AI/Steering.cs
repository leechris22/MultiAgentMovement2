using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the linear and angular acceleration
[System.Serializable]
public struct Steering {
    public Vector2 linear;
    public float angular;

    // Constructor
    public Steering(Vector2 lin, float ang) {
        linear = lin;
        angular = ang;
    }

    // Operator overloads
    public static Steering operator+(Steering a, Steering b) {
        return new Steering(a.linear + b.linear, a.angular + b.angular);
    }

    public static Steering operator-(Steering a, Steering b) {
        return new Steering(a.linear - b.linear, a.angular - b.angular);
    }

    public static Steering operator/(Steering a, float b) {
        return new Steering(a.linear / b, a.angular / b);
    }

    public static Steering operator*(Steering a, float b) {
        return new Steering(a.linear * b, a.angular * b);
    }

}
