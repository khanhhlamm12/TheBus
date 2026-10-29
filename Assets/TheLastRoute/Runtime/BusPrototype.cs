using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheLastRoute
{
    // First playable foundation. Temporary geometry is deliberately separate from final art.
    public sealed class BusPrototype : MonoBehaviour
    {
        public PrototypeSettings settings;
        public TMPro.TMP_FontAsset font;
        public PassengerRecord[] Passengers { get; private set; }
        public int Selected { get; private set; } = -1;
        public float Elapsed { get; private set; }
        public float Power { get; private set; }
        public float Battery { get; private set; }
        public bool Paused { get; private set; }
        public bool Notebook { get; private set; }
        public string Message { get; private set; } = "Đi dọc lối đi. Nhấn E cạnh hành khách để kiểm tra.";
        public string ActiveTool { get; private set; }
        public float ToolProgress => toolDuration > 0 ? toolElapsed / toolDuration : 0;
        public Transform Player => player;
        public bool FlashlightOn { get; private set; }

        readonly List<Rect> obstacles = new List<Rect>();
        readonly Vector2[] passengerPositions = { new Vector2(-1.35f, 3f), new Vector2(1.35f, 1f), new Vector2(-1.35f, -1f), new Vector2(1.35f, -3f) };
        Transform player;
        SpriteRenderer flashlight;
        Sprite sprite;
        BusPrototypeUI ui;
        float toolElapsed;
        float toolDuration;
        const float Radius = .23f;

        void Start()
        {
            if (!settings) settings = ScriptableObject.CreateInstance<PrototypeSettings>();
            Power = settings.maxPower;
            Battery = settings.maxBattery;
            Passengers = new[] {
                new PassengerRecord { id = "P01", displayName = "Lan", destination = "Bệnh viện", anomaly = AnomalyKind.None },
                new PassengerRecord { id = "P02", displayName = "Minh", destination = "Trạm rừng", anomaly = AnomalyKind.Reflection },
                new PassengerRecord { id = "P03", displayName = "Hạnh", destination = "Khu công nghiệp", anomaly = AnomalyKind.None },
                new PassengerRecord { id = "P04", displayName = "Bảo", destination = "Trạm cuối", anomaly = AnomalyKind.None }
            };
            BuildWorld();
            ui = gameObject.AddComponent<BusPrototypeUI>();
            ui.Initialize(this, font);
        }

        void BuildWorld()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(.5f, .5f), 1);
            var cameraObject = new GameObject("Prototype Camera", typeof(Camera), typeof(AudioListener));
            cameraObject.transform.SetParent(transform);
            var cam = cameraObject.GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 7.8f;
            cam.backgroundColor = new Color(.025f, .04f, .05f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            cameraObject.transform.position = new Vector3(0, 0, -10);
            Box("Bus floor", Vector2.zero, new Vector2(5.2f, 12.6f), new Color(.11f, .16f, .17f), 0);
            Box("Aisle", Vector2.zero, new Vector2(1.55f, 10.8f), new Color(.18f, .23f, .23f), 1);
            Box("Cabin", new Vector2(0, 5.8f), new Vector2(4.8f, .7f), new Color(.32f, .35f, .28f), 2);
            for (int row = 0; row < 6; row++)
            {
                float y = 4.5f - row * 1.8f;
                foreach (int side in new[] { -1, 1 })
                {
                    Vector2 p = new Vector2(side * 1.7f, y);
                    Box("Seat", p, new Vector2(1.35f, 1.1f), new Color(.22f, .34f, .32f), 2);
                    obstacles.Add(new Rect(p - new Vector2(.675f, .55f), new Vector2(1.35f, 1.1f)));
                    Box("Window", new Vector2(side * 2.48f, y), new Vector2(.1f, .9f), new Color(.27f, .44f, .5f), 2);
                }
            }
            // Passengers and seats share row centres; only the aisle is traversable.
            for (int i = 0; i < Passengers.Length; i++)
            {
                passengerPositions[i] = new Vector2(i % 2 == 0 ? -1.7f : 1.7f, 4.5f - i * 1.8f);
                Box(Passengers[i].id, passengerPositions[i], new Vector2(.52f, .65f), new Color(.67f - i * .06f, .55f, .4f + i * .06f), 4);
            }
            player = Box("Inspector", new Vector2(0, -5.4f), new Vector2(.43f, .55f), new Color(.87f, .72f, .39f), 5).transform;
            flashlight = Box("Flashlight indicator", Vector2.zero, new Vector2(.65f, .65f), new Color(1, .85f, .4f, .2f), 3);
            flashlight.enabled = false;
        }

        SpriteRenderer Box(string label, Vector2 position, Vector2 size, Color color, int order)
        {
            var go = new GameObject(label);
            go.transform.SetParent(transform);
            go.transform.position = position;
            go.transform.localScale = size;
            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.color = color;
            renderer.sortingOrder = order;
            return renderer;
        }

        void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame) TogglePause();
            if (Paused) { ui.Refresh(); return; }
            Elapsed += Time.deltaTime;
            if (keyboard != null)
            {
                if (keyboard.tabKey.wasPressedThisFrame) Notebook = !Notebook;
                if (keyboard.fKey.wasPressedThisFrame && Battery > 0) FlashlightOn = !FlashlightOn;
                if (Selected < 0 && !Notebook)
                {
                    Vector2 input = new Vector2((keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0),
                        (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0));
                    Move(input.normalized * settings.movementSpeed * Time.deltaTime);
                    if (keyboard.eKey.wasPressedThisFrame) OpenInspection(NearestPassenger());
                }
            }
            if (FlashlightOn)
            {
                Battery = Mathf.Max(0, Battery - settings.flashlightDrainPerSecond * Time.deltaTime);
                if (Battery <= 0) FlashlightOn = false;
            }
            flashlight.enabled = FlashlightOn;
            flashlight.transform.position = player.position + Vector3.up * .5f;
            TickTool(Time.deltaTime);
            ui.Refresh();
        }

        public void Move(Vector2 delta)
        {
            if (Paused || Notebook || Selected >= 0) return;
            Vector2 p = player.position;
            var x = new Vector2(p.x + delta.x, p.y);
            if (CanStand(x)) p = x;
            var y = new Vector2(p.x, p.y + delta.y);
            if (CanStand(y)) p = y;
            player.position = p;
        }

        bool CanStand(Vector2 p)
        {
            if (Mathf.Abs(p.x) > 2.35f - Radius || p.y < -5.9f || p.y > 5.15f) return false;
            foreach (var obstacle in obstacles)
            {
                var expanded = Rect.MinMaxRect(obstacle.xMin - Radius, obstacle.yMin - Radius, obstacle.xMax + Radius, obstacle.yMax + Radius);
                if (expanded.Contains(p)) return false;
            }
            return true;
        }

        public int NearestPassenger()
        {
            int result = -1;
            float nearest = settings.interactionRange;
            for (int i = 0; i < passengerPositions.Length; i++)
            {
                float d = Vector2.Distance(player.position, passengerPositions[i]);
                if (d < nearest) { result = i; nearest = d; }
            }
            return result;
        }

        public void OpenInspection(int index)
        {
            if (Paused || Notebook || index < 0 || index >= Passengers.Length || index != NearestPassenger()) return;
            Selected = index;
            Message = "Đọc sổ tay trước khi kết luận. Thiết bị không dừng đồng hồ.";
        }

        public void CloseInspection() { if (Paused) return; Selected = -1; CancelTool(); }
        public void ToggleNotebook() { if (!Paused) Notebook = !Notebook; }
        public void TogglePause() { Paused = !Paused; }
        public void CancelTool() { ActiveTool = null; toolElapsed = 0; toolDuration = 0; }

        public void StartTool(string tool)
        {
            if (Paused || Notebook || Selected < 0 || ActiveTool != null || (tool != "UV" && tool != "Gương")) return;
            if (tool == "UV" && Power <= 0) { Message = "Hết điện: UV không hoạt động. Chưa có kết quả."; return; }
            ActiveTool = tool;
            toolElapsed = 0;
            toolDuration = tool == "UV" ? settings.uvDuration : settings.mirrorDuration;
        }

        public void TickTool(float delta)
        {
            if (Paused || ActiveTool == null || Selected < 0) return;
            float step = Mathf.Min(delta, toolDuration - toolElapsed);
            if (ActiveTool == "UV")
            {
                float cost = step * settings.uvDrainPerSecond;
                if (Power + .0001f < cost) { Power = 0; CancelTool(); Message = "Phép đo bị ngắt vì hết điện; không ghi bằng chứng."; return; }
                Power = Mathf.Max(0, Power - cost);
            }
            toolElapsed += step;
            if (toolElapsed + .0001f < toolDuration) return;
            if (ActiveTool == "UV") Passengers[Selected].uvChecked = true;
            else Passengers[Selected].mirrorChecked = true;
            Message = "Đã ghi kết quả vào hồ sơ hành khách.";
            CancelTool();
        }

        public void Decide(PassengerDecision decision)
        {
            if (Paused || Notebook || Selected < 0 || ActiveTool != null) return;
            if (decision == PassengerDecision.Isolated)
                foreach (var passenger in Passengers)
                    if (passenger != Passengers[Selected] && passenger.decision == PassengerDecision.Isolated)
                    { Message = "Đã có một hành khách được đánh dấu cách ly. Hãy hủy đánh dấu trước."; return; }
            Passengers[Selected].decision = decision;
            Message = "Đã lưu quyết định. Prototype hiện chỉ đánh dấu; bàn giao và hậu quả sẽ được bổ sung.";
        }

        void OnDestroy()
        {
            if (sprite) { Destroy(sprite.texture); Destroy(sprite); }
        }
    }
}
