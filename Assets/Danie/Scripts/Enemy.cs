using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 startPos = new Vector3(-10, 0f, 0f);
    [SerializeField] private Vector3 endPos = new Vector3(10f, 0f, 0f);
    [SerializeField] private float moveSpeed = 2f;
    
    protected abstract void OnStart();
    protected abstract void act();
    protected int SCORE_BONUS;

    private Rigidbody2D rb;

    public Vector3 start
    {
        get => startPos;
        set => startPos = value;
    }

    public Vector3 end
    {
        get => endPos;
        set => endPos = value;
    }

    public float speed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public void SnapToPath(float time)
    {
        transform.localPosition = Vector3.Lerp(startPos, endPos, (Mathf.Sin(time * speed) + 1) * .5f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        SnapToPath(Time.time);
        act();
    }

    void OnTriggerEnter2D(Collider2D data)
    {
        if (data.gameObject.tag == "Player")
        {
            Player player = data.gameObject.GetComponent<Player>();
            if (player.isBoosted()) {
                player.incScore(SCORE_BONUS);
                Destroy(this.gameObject);
            } else {
                player.die();
            }
        }
    }
}

// The second argument in the EditorToolAttribute flags this as a Component tool. That means that it will be instantiated
// and destroyed along with the selection. EditorTool.targets will contain the selected objects matching the type.
[EditorTool("Enemy Tool", typeof(Enemy))]
class EnemyTool : EditorTool, IDrawSelectedHandles
{
    // Enable or disable preview animation
    bool m_AnimateEnemies;

    // Global tools (tools that do not specify a target type in the attribute) are lazy initialized and persisted by
    // a ToolManager. Component tools (like this example) are instantiated and destroyed with the current selection.
    void OnEnable()
    {
        // Allocate unmanaged resources or perform one-time set up functions here
    }

    void OnDisable()
    {
        // Free unmanaged resources, state teardown.
    }

    // The second "context" argument accepts an EditorWindow type.
    [Shortcut("Activate Enemy Tool", typeof(SceneView), KeyCode.P)]
    static void EnemyToolShortcut()
    {
        if (Selection.GetFiltered<Enemy>(SelectionMode.TopLevel).Length > 0)
            ToolManager.SetActiveTool<EnemyTool>();
        else
            Debug.Log("No Enemies selected!");
    }

    // Called when the active tool is set to this tool instance. Global tools are persisted by the ToolManager,
    // so usually you would use OnEnable and OnDisable to manage native resources, and OnActivated/OnWillBeDeactivated
    // to set up state. See also `EditorTools.{ activeToolChanged, activeToolChanged }` events.
    public override void OnActivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Entering Enemy Tool"), .1f);
    }

    // Called before the active tool is changed, or destroyed. The exception to this rule is if you have manually
    // destroyed this tool (ex, calling `Destroy(this)` will skip the OnWillBeDeactivated invocation).
    public override void OnWillBeDeactivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Exiting Enemy Tool"), .1f);
    }

    // Equivalent to Editor.OnSceneGUI.
    public override void OnToolGUI(EditorWindow window)
    {
        if (!(window is SceneView sceneView))
            return;

        Handles.BeginGUI();
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                m_AnimateEnemies = EditorGUILayout.Toggle("Animate Enemies", m_AnimateEnemies);
                // To animate Enemies we need the Scene View to repaint at fixed intervals, so enable `alwaysRefresh`
                // and scene FX (need both for this to work). In older versions of Unity this is called `materialUpdateEnabled`
                sceneView.sceneViewState.alwaysRefresh = m_AnimateEnemies;
                if (m_AnimateEnemies && !sceneView.sceneViewState.fxEnabled)
                    sceneView.sceneViewState.fxEnabled = true;

                if (GUILayout.Button("Snap to Path"))
                    foreach (var obj in targets)
                        if (obj is Enemy enemy)
                            enemy.SnapToPath((float)EditorApplication.timeSinceStartup);
            }

            GUILayout.FlexibleSpace();
        }
        Handles.EndGUI();

        foreach (var obj in targets)
        {
            if (!(obj is Enemy enemy))
                continue;

            if (m_AnimateEnemies && Event.current.type == EventType.Repaint)
                enemy.SnapToPath((float)EditorApplication.timeSinceStartup);

            EditorGUI.BeginChangeCheck();
            var start = Handles.PositionHandle(enemy.start, Quaternion.identity);
            var end = Handles.PositionHandle(enemy.end, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(enemy, "Set Enemy Destinations");
                enemy.start = start;
                enemy.end = end;
            }
        }
    }

    // IDrawSelectedHandles interface allows tools to draw gizmos when the target objects are selected, but the tool
    // has not yet been activated. This allows you to keep MonoBehaviour free of debug and gizmo code.
    public void OnDrawHandles()
    {
        foreach (var obj in targets)
        {
            if (obj is Enemy enemy)
                Handles.DrawLine(enemy.start, enemy.end, 6f);
        }
    }
}