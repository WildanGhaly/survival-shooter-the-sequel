using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nightmare
{
    /// <summary>
    /// Class for debugging (cheats).
    /// To add more cheats:
    /// <list type="number">
    /// <item>declare new attribute DebugCommand</item>
    /// <item>define cheat in Awake()</item>
    /// <item>put cheat in CommandList definition, also in Awake()</item>
    /// </list>
    /// </summary>
    public class Debugger : MonoBehaviour
    {
        bool showConsole = false;
        bool showHelp = false;

        string input;

        public static DebugCommand HELP;
        public static DebugCommand INVULNERABLE;
        public static DebugCommand<float> HEAL;


        public List<object> CommandList;

        public void OnReturn()
        {
            if (showConsole)
            {
                HandleInput();
                input = "";
            }
        }
        [ContextMenu("ToggleDebug")]
        public void OnToggleDebugCheat()
        {
            showConsole = !showConsole;
        }

        private void Awake()
        {
            // Definition of cheats
            HELP = new DebugCommand("HELP", "Show lists of commands", "HELP", () =>
            {
                showHelp = !showHelp;
                Debug.Log("Showing debug HELP");
            });
            INVULNERABLE = new DebugCommand("INVULNERABLE", "Immune to any damage", "INVULNERABLE", () =>
            {
                // godMode in playerHealth turns on
                // godMode =! godMode
                // script is attached to playerWithUI prefab,
                // so we find by child object's component
                PlayerHealth health = GetComponentInChildren<PlayerHealth>();
                health.godMode = !health.godMode;
                Debug.Log($"INVULNERABILITY {health.godMode}");
            });
            HEAL = new DebugCommand<float>("HEAL", "Heal X amount of health", "HEAL <Amount>", (Amount) =>
            {
                // Calling HealthSystem.instance.healDamage(Amount)
                HealthSystem.Instance.HealDamage(Amount);
                Debug.Log($"HEAL {Amount}");
            });


            // Definition of cheat list
            CommandList = new List<object>
            {
                INVULNERABLE,
                HEAL,
                HELP,
            };
        }

        Vector2 scroll;

        private void OnGUI()
        {
            if (!showConsole) { return; }
            float y = 0;
            if (showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                
                Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * CommandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

                for (int i=0; i<CommandList.Count; i++)
                {
                    DebugCommandBase commandBase = CommandList[i] as DebugCommandBase;
                    string label = $"{commandBase.commandFormat} - {commandBase.commandDescription}";
                    Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();
                y += 100;
            }
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);

            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }

        private void HandleInput()
        {
            string[] properties = input.Split(' ');
            foreach (DebugCommandBase command in CommandList)
            {
                if (input.Contains(command.commandID))
                {
                    //(command as DebugCommand)?.Invoke();
                    if ((command as DebugCommand) != null)
                    {
                        (command as DebugCommand)?.Invoke();
                    } else if ((command as DebugCommand<float>) != null)
                    {
                        (command as DebugCommand<float>)?.Invoke(float.Parse(properties[1]));
                    }
                }
            }
        }
    }
}

public class DebugCommandBase
{
    private string _commandID;
    private string _commandDescription;
    private string _commandFormat;

    public string commandID { get { return _commandID; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }

    public DebugCommandBase(string id, string desc, string format)
    {
        _commandID = id;
        _commandDescription = desc;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;
    public DebugCommand (string id, string desc, string format, Action command) : base(id, desc, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}
public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command;
    public DebugCommand(string id, string desc, string format, Action<T1> command) : base(id, desc, format)
    {
        this.command = command;
    }
    public void Invoke(T1 value)
    {
        command.Invoke(value);
    }
}