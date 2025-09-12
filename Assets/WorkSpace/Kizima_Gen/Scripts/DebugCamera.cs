using UnityEngine;

[ExecuteAlways]
public class DebugCamera : MonoBehaviour {
    [Header("Target Camera (if null uses Camera.main)")]
    public Camera targetCamera;

    [Header("Circle settings")]
    //‚±‚±˜M‚ê‚Î‚æ‚³‚°
    public float radius = 50f;
    [Tooltip("Number of segments used to draw the circle. Higher = smoother.")]
    [Range(8, 512)] public int segments = 64;
    public Color circleColor = Color.cyan;
    public float lineWidth = 0.1f;

    [Header("Runtime (LineRenderer)")]
    [Tooltip("If true, a LineRenderer is created/used so the circle is visible in Game view.")]
    public bool showInGameView = true;

    // internal
    private LineRenderer lr;
    private GameObject lrGO;

    void Start () {
        if (targetCamera == null) targetCamera = Camera.main;
        SetupLineRenderer();
        UpdateLineRenderer();
    }


    void OnDisable() {
        // In editor, keep the LineRenderer object? Destroy it to avoid clutter.
        if (Application.isPlaying) {
            if (lrGO != null) Destroy(lrGO);
        }
        else {
            if (lrGO != null) DestroyImmediate(lrGO);
        }
    }

    void Update() {
        // keep camera reference if scene changes
        if (targetCamera == null) targetCamera = Camera.main;
        UpdateLineRenderer(); // safe cheap update per-frame
    }

    private void SetupLineRenderer() {
        if (!showInGameView) return;

        if (lrGO == null) {
            // create an owned child to hold the LineRenderer so it doesn't interfere with other objects
            lrGO = new GameObject("CameraCircle_LineRenderer");
            lrGO.hideFlags = HideFlags.DontSave; // don't persist in builds via editor; still destroyed on play stop
            lrGO.transform.SetParent(transform, false);
        }

        lr = lrGO.GetComponent<LineRenderer>();
        if (lr == null) lr = lrGO.AddComponent<LineRenderer>();

        lr.useWorldSpace = true;
        lr.loop = true;
        lr.positionCount = Mathf.Max(3, segments);
        lr.startWidth = lr.endWidth = lineWidth;

        // simple material: use built-in sprite default so color works without extra assets
        if (lr.material == null) {
            var mat = new Material(Shader.Find("Sprites/Default"));
            mat.hideFlags = HideFlags.HideAndDontSave;
            lr.material = mat;
        }
        lr.startColor = lr.endColor = circleColor;
    }

    private void UpdateLineRenderer() {
        // update line renderer properties in case changed in inspector
        if (showInGameView && lr != null) {
            lr.positionCount = segments;
            lr.startWidth = lr.endWidth = lineWidth;
            lr.startColor = lr.endColor = circleColor;
            if (lr.material != null) lr.material.color = circleColor;

            // compute circle positions around camera's position on XZ plane centered at camera
            Vector3 center = GetCameraPositionFlat();
            for (int i = 0; i < segments; i++) {
                float t = (float) i / segments;
                float ang = t * Mathf.PI * 2f;
                Vector3 pos = center + new Vector3(Mathf.Cos(ang), 0f, Mathf.Sin(ang)) * radius;
                lr.SetPosition(i, pos);
            }
        }
    }

    // helper: camera position but use camera's Y as center (so circle at camera height)
    private Vector3 GetCameraPositionFlat() {
        if (targetCamera != null) return targetCamera.transform.position;
        return Vector3.zero;
    }

    // Draw gizmos for scene view (and Game view gizmos if enabled)
    void OnDrawGizmos() {
        if (targetCamera == null) targetCamera = Camera.main;
        Vector3 center = GetCameraPositionFlat();
        Gizmos.color = circleColor;

        int gizSegments = Mathf.Clamp(segments, 8, 512);
        Vector3 prev = center + new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radius;
        for (int i = 1; i <= gizSegments; i++) {
            float ang = (float) i / gizSegments * Mathf.PI * 2f;
            Vector3 next = center + new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang)) * radius;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }

    // expose a public API to refresh immediately
    public void RefreshNow() {
        UpdateLineRenderer();
    }
}
