using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : Entity
{
    public static Dictionary<int, List<Unit>> UNITS_BY_OWNER;

    protected string _uid;
    protected Transform _transform;
    protected int _currentHealth;
    protected int _level;
    protected bool _levelMaxedOut;
    protected float _fieldOfView;
    protected int _owner;
    protected int _attackDamage;
    protected float _attackRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
