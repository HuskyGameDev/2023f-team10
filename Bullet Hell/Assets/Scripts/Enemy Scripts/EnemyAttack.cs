using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Presets;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Shooter shooterScript = null;

    private enum attackPatterns { VERTICAL, HORIZONTAL, RADIAL};

    [SerializeField] string currentAttackPattern = "default";

    //wait at least this long before doing an attack
    [SerializeField] float waitToAttack = 0;

    //used in other scripts to time attacks
    public bool canAttack = true;

    //treat this like a set of kvp's where the key is the string representing the name of a preset for the shooter script (a bit complicated because unity doesn't display dictionaries in the editor)
    private Dictionary<string, Preset> presetsDict = new Dictionary<string, Preset>();

    [System.Serializable]
    public class presetStorage
    {
        public string presetName;
        public Preset presetObject;
    }

    [SerializeField] presetStorage[] presetOptions;

    //enemy does an attack every amount of seconds (I think) set here
    [SerializeField] float attackSpeed = 0;
    private float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        shooterScript = GetComponent<Shooter>();

        attackTimer = waitToAttack;

        //compile into a dictionary for convinience so we can call presets by name and don't have to remember index
        for(int i = 0; i < presetOptions.Length; i++) 
        {
            presetsDict.Add(presetOptions[i].presetName, presetOptions[i].presetObject);
        }
    }

    private void FixedUpdate()
    {
        if (attackTimer <= 0)
        {
            doAttack();
            attackTimer = attackSpeed;
        }
        else
        {
            shooterScript.Shoot(0);
            attackTimer -= Time.fixedDeltaTime;
        }
    }

    //use this to change the attack pattern during runtime
    public void doAttack()
    {
        if (canAttack)
        {
            if (currentAttackPattern.Equals("manual"))
            {
                //do nothing, this lets the shooting method be set manually in the editor
            } 
            else if (currentAttackPattern.Equals("default"))
            {
                presetsDict["default"].ApplyTo(shooterScript);
            }
            else if(currentAttackPattern.Equals("Flare Burst"))
            {
                presetsDict["Flare Burst"].ApplyTo(shooterScript);
            }
            else if(currentAttackPattern.Equals("Wave"))
            {
                presetsDict["Wave"].ApplyTo(shooterScript);
            }

            shooterScript.Shoot(1);
        }
    }
}
