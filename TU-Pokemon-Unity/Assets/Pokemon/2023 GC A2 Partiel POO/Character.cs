﻿using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    /// 
    public enum TYPE
    {
        NORMAL,
    }

    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        //enemy
        TYPE _enemyType;
        int _enemyAttack;
        StatusPotential _enemyState;


        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            CurrentHealth = baseHealth;
        }

        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }

        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth 
        { 
        get => _baseHealth;
        //set => _baseHealth = value;
        }

        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack { get => _baseAttack; }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense 
        { 
           get => _baseDefense;

           //set => _baseDefense += value;
        
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed { get => _baseSpeed; }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }

        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive { get; set; } = true;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            //enemy
            _enemyType = s.Type;
            _enemyAttack = s.Power;
            _enemyState = s.Status;

            CurrentHealth = CurrentHealth - (_enemyAttack - _baseDefense);

            if(CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            if (CurrentHealth <= 0)
            {
                IsAlive = false;
            }
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException("Erreur la valeur ne peux être null");
            }

            CurrentEquipment = newEquipment;
            _baseHealth += CurrentEquipment.BonusHealth;
            _baseAttack += CurrentEquipment.BonusAttack;
            _baseDefense += CurrentEquipment.BonusDefense;
            _baseSpeed += CurrentEquipment.BonusSpeed;

        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            _baseHealth -= CurrentEquipment.BonusHealth;
            _baseAttack -= CurrentEquipment.BonusAttack;
            _baseDefense -= CurrentEquipment.BonusDefense;
            _baseSpeed -= CurrentEquipment.BonusSpeed;
            CurrentEquipment = null;
        }

        internal void ReceiveAttack(Character character2)
        {
            throw new NotImplementedException();
        }
    }
}
