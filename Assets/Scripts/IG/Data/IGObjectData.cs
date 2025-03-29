using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGObjectData
{
    private int _number;

    private string _operator;

    public bool IsOperator { get { return (_operator == null) || ( _operator == ""); }}

    public bool IsNumber { get { return (0 < _number) && (_number < 10);  }}

    public int Number { get { return _number; } set { _number = value; } }

    public string Operator { get { return _operator; } set { _operator = value; } }
}
