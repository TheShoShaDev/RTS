using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Entity))]
public class Health : MonoBehaviour
{
	[SerializeField] private Entity _entity;
	[SerializeField] private float _health;

	public float CurrentHealth { get; protected set; }
	public float MaxHealth => _health;

	public event Action<float, float> HealthChanged;

	public bool IsDead => CurrentHealth <= 0;

	protected virtual void Start()
	{
		CurrentHealth = MaxHealth;
	}

	protected virtual void OnDecreseHealth(float value)
	{
		if (value < 0)
		{
			throw new Exception("Taken damage cannot be less than 0");
		}

		CurrentHealth -= value;
		HealthChanged?.Invoke(CurrentHealth, MaxHealth);
	}

	protected virtual void OnIncreseHealth(float value)
	{
		if (value < 0)
		{
			throw new Exception("Heal cannot be less than 0");
		}

		CurrentHealth += value;
		HealthChanged?.Invoke(CurrentHealth, MaxHealth);
	}

	protected void HealthChangedInvoke()
	{
		HealthChanged?.Invoke(CurrentHealth, MaxHealth);
	}

	private void OnEnable()
	{
		_entity.GotDamage += OnDecreseHealth;
	}

	private void OnDisable()
	{
		_entity.GotDamage -= OnDecreseHealth;
	}

	private void OnValidate()
	{
		_entity ??= GetComponent<Entity>();
	}
}
