using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        public static DebugCommand KILL_ALL_ENEMY;
        public static DebugCommand MOTHERLODE;
        public static DebugCommand SKIP;
        public static DebugCommand<int> COIN;
        public static DebugCommand<float> HEAL;
        public static DebugCommand<int> LOAD_LEVEL;
        public static DebugCommand<int> LOAD_QUEST;

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
            KILL_ALL_ENEMY = new DebugCommand("KILL_ALL_ENEMY", "Kill all enemy in the level", "KILL_ALL_ENEMY", () =>
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] bosses = GameObject.FindGameObjectsWithTag("FinalBoss");
                GameObject[] targets = new GameObject[enemies.Length + bosses.Length];

                if (enemies.Length == 0 || bosses.Length == 0)
                {
                    Debug.Log("There is no enemy found");
                    return;
                }

                enemies.CopyTo(targets, 0);
                bosses.CopyTo(enemies, enemies.Length);

                foreach (var target in targets)
                {
                    if (target.GetComponent<EnemyHealth>() == null)
                    {
                        Debug.Log($"Enemy {target.name} does not have a health, destroying immideately");
                        Destroy(target);
                    } else
                    {
                        target.GetComponent<EnemyHealth>().TakeDamage(target.GetComponent<EnemyHealth>().currentHealth, target.transform.position);
                    }
                }
                Debug.Log($"Killed {targets.Length}");
            });
            MOTHERLODE = new DebugCommand("MOTHERLODE", "Gain 99999 Coins", "MOTHERLODE", () =>
            {
                GameObject gameManager = GameObject.FindGameObjectWithTag("SceneManager");
                if (gameManager == null)
                {
                    Debug.Log("SceneManager is not found");
                    return;
                }

                gameManager.GetComponentInChildren<GameManager>().addCoin(99999);
                Debug.Log("MOTHERLODE");
            });
            COIN = new DebugCommand<int>("COIN", "Gain X number of Coins", "COIN <Amount>", (Amount) =>
            {
                GameObject gameManager = GameObject.FindGameObjectWithTag("SceneManager");
                if (gameManager == null)
                {
                    Debug.Log("SceneManager is not found");
                    return;
                }

                gameManager.GetComponentInChildren<GameManager>().addCoin(Amount);
                Debug.Log($"COIN {Amount}");
            });
            LOAD_LEVEL = new DebugCommand<int>("LOAD_LEVEL", "Directly load level with id X", "LOAD_LEVEL <Index>", (Index) =>
            {
                SceneManager.LoadScene(Math.Clamp(Index, 1, 8));
            });
            LOAD_QUEST = new DebugCommand<int>("LOAD_QUEST", "Directly load quest with id X", "LOAD_QUEST <Index>", (Index) =>
            {
                GameObject gameManager = GameObject.FindGameObjectWithTag("SceneManager");
                if (gameManager == null)
                {
                    Debug.Log("SceneManager is not found");
                    return;
                }

                GameManager.INSTANCE.currentQuestID = Index;
                GameManager.INSTANCE.LoadQuestScene();
            });
            SKIP = new DebugCommand("SKIP", "Skip current quest and load lobby scene", "SKIP", () =>
            {
                GameObject gameManager = GameObject.FindGameObjectWithTag("SceneManager");
                if (gameManager == null)
                {
                    Debug.Log("SceneManager is not found");
                    return;
                }

                GameManager.INSTANCE.currentQuestID++;
                SceneManager.LoadScene(4);
            });


            // Definition of cheat list
            CommandList = new List<object>
            {
                HELP,
                INVULNERABLE,
                HEAL,
                KILL_ALL_ENEMY,
                MOTHERLODE,
                SKIP,
                COIN,
                LOAD_LEVEL,
                LOAD_QUEST,
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
                    } else if ((command as DebugCommand<int>) != null)
                    {
                        (command as DebugCommand<int>)?.Invoke(int.Parse(properties[1]));
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