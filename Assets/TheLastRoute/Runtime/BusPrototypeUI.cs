using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace TheLastRoute
{
    public sealed class BusPrototypeUI : MonoBehaviour
    {
        BusPrototype game;
        TMP_FontAsset font;
        RectTransform canvas;
        GameObject inspection, notebook, pause;
        TMP_Text status, prompt, details, evidence, progress, log, roster;
        readonly Color ink = new Color(.88f, .9f, .83f);
        readonly Color panel = new Color(.055f, .09f, .1f, 1f);

        public void Initialize(BusPrototype owner, TMP_FontAsset typeface)
        {
            game = owner;
            font = typeface;
            var go = new GameObject("Prototype UI", typeof(Canvas), typeof(UnityEngine.UI.CanvasScaler), typeof(UnityEngine.UI.GraphicRaycaster));
            go.transform.SetParent(transform);
            go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = go.GetComponent<UnityEngine.UI.CanvasScaler>();
            scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1440, 900);
            scaler.matchWidthOrHeight = .5f;
            canvas = go.GetComponent<RectTransform>();
            if (!FindAnyObjectByType<EventSystem>())
            {
                var events = new GameObject("Prototype EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
                events.transform.SetParent(transform);
                events.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();
            }

            var left = Panel(canvas, "Shift panel", .02f, .18f, .29f, .95f);
            Text(left, "THE LAST ROUTE\nTUYẾN 09 • CA ĐÊM", 29, .07f, .81f, .94f, .97f);
            status = Text(left, "", 22, .07f, .62f, .94f, .8f);
            roster = Text(left, "", 19, .07f, .27f, .94f, .60f);
            Text(left, "WASD  Di chuyển\nE  Kiểm tra hành khách\nF  Đèn pin (chỉ báo tạm)\nTab  Sổ tay  •  Esc  Tạm dừng", 18, .07f, .06f, .94f, .25f);
            var right = Panel(canvas, "Dispatch", .72f, .37f, .98f, .95f);
            Text(right, "NHẬT KÝ CA", 25, .08f, .85f, .92f, .96f);
            log = Text(right, "", 21, .08f, .35f, .92f, .81f);
            Text(right, "BẢN DỰNG NỀN TẢNG\nHình khối và chân dung tạm.\nChưa có tấn công hoặc kết ca.", 18, .08f, .08f, .92f, .29f);
            var bottom = Panel(canvas, "Interaction prompt", .31f, .025f, .70f, .115f);
            prompt = Text(bottom, "", 21, .04f, .08f, .96f, .93f);

            var inspect = Panel(canvas, "Inspection", .06f, .13f, .94f, .93f);
            inspection = inspect.gameObject;
            Text(inspect, "BÀN KIỂM TRA / TUYẾN 09", 30, .03f, .88f, .79f, .98f);
            Button(inspect, "Trở lại", .82f, .9f, .97f, .97f, game.CloseInspection);
            details = Text(inspect, "", 23, .04f, .54f, .33f, .85f);
            evidence = Text(inspect, "", 21, .66f, .42f, .96f, .83f);
            var portrait = Panel(inspect, "Portrait placeholder", .36f, .29f, .62f, .84f);
            Text(portrait, "CHÂN DUNG TẠM", 17, .06f, .88f, .95f, .98f);
            var torso = Panel(portrait, "Torso", .15f, .02f, .85f, .48f);
            torso.GetComponent<UnityEngine.UI.Image>().color = new Color(.23f, .34f, .34f);
            var face = Panel(portrait, "Face", .28f, .45f, .72f, .82f);
            face.GetComponent<UnityEngine.UI.Image>().color = new Color(.58f, .52f, .42f);
            Text(face, "•   •\n\n—", 28, .15f, .05f, .9f, .88f);
            progress = Text(inspect, "", 19, .04f, .26f, .96f, .37f);
            Button(inspect, "Quét UV", .04f, .16f, .23f, .24f, () => game.StartTool("UV"));
            Button(inspect, "Thử gương", .25f, .16f, .44f, .24f, () => game.StartTool("Gương"));
            Button(inspect, "Sổ tay", .46f, .16f, .65f, .24f, game.ToggleNotebook);
            Button(inspect, "Thông qua", .04f, .04f, .25f, .12f, () => game.Decide(PassengerDecision.Cleared));
            Button(inspect, "Tái kiểm tra", .28f, .04f, .49f, .12f, () => game.Decide(PassengerDecision.Recheck));
            Button(inspect, "Đánh dấu cách ly", .52f, .04f, .76f, .12f, () => game.Decide(PassengerDecision.Isolated));
            Button(inspect, "Hủy quyết định", .79f, .04f, .97f, .12f, () => game.Decide(PassengerDecision.Unchecked));

            var book = Panel(canvas, "Rulebook", .17f, .2f, .83f, .86f);
            notebook = book.gameObject;
            Text(book, "SỔ TAY NHÂN VIÊN • BẢN TIN 01", 28, .05f, .82f, .95f, .97f);
            Text(book, "1. Gương: yêu cầu khách quay đầu. Phản chiếu không quay theo là bằng chứng thực thể.\n\n2. UV: chỉ dấu TAM GIÁC KÉP trùng mẫu cấm mới xác nhận thực thể.\n\n3. Vé sai, lo lắng hoặc ít nói không đủ để kết luận.\n\n4. Kết quả được giữ khi rời màn hình kiểm tra. Phép đo đang chạy bị hủy khi thoát; điện đã dùng không hoàn lại.\n\nĐồng hồ vẫn chạy khi đọc sổ tay. Esc để tạm dừng.", 23, .05f, .15f, .95f, .81f);
            Button(book, "Đóng sổ tay", .65f, .035f, .94f, .12f, game.ToggleNotebook);
            var paused = Panel(canvas, "Paused", .29f, .35f, .71f, .65f);
            pause = paused.gameObject;
            Text(paused, "ĐÃ TẠM DỪNG", 34, .1f, .51f, .9f, .9f);
            Button(paused, "Tiếp tục", .2f, .12f, .8f, .38f, game.TogglePause);
            Refresh();
        }

        public void Refresh()
        {
            inspection.SetActive(game.Selected >= 0);
            notebook.SetActive(game.Notebook);
            pause.SetActive(game.Paused);
            status.text = $"Thời gian  {Mathf.FloorToInt(game.Elapsed / 60):00}:{Mathf.FloorToInt(game.Elapsed % 60):00}\nĐiện thiết bị  {game.Power:0}\nPin đèn pin  {game.Battery:0}";
            roster.text = "HÀNH KHÁCH\n";
            foreach (var p in game.Passengers) roster.text += $"{p.id}  {p.displayName}\n{PassengerRecord.DecisionLabel(p.decision)}\n";
            log.text = game.Message;
            int near = game.NearestPassenger();
            prompt.text = near >= 0 ? "[E] Kiểm tra " + game.Passengers[near].displayName : "Tuần tra dọc lối đi";
            if (game.Selected < 0) return;
            var passenger = game.Passengers[game.Selected];
            details.text = $"{passenger.id} / {passenger.displayName}\n\nĐiểm đến\n{passenger.destination}\n\n{PassengerRecord.DecisionLabel(passenger.decision)}";
            evidence.text = "BẰNG CHỨNG\n\n" + passenger.Evidence;
            progress.text = game.ActiveTool == null ? "Chọn công cụ. Kết quả chỉ ghi khi phép kiểm tra hoàn tất." : $"{game.ActiveTool}: {game.ToolProgress:P0} — đang kiểm tra...";
        }

        RectTransform Rect(Transform parent, string name, float x0, float y0, float x1, float y1)
        {
            var go = new GameObject(name, typeof(RectTransform));
            var rect = go.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = new Vector2(x0, y0);
            rect.anchorMax = new Vector2(x1, y1);
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            return rect;
        }
        RectTransform Panel(Transform parent, string name, float x0, float y0, float x1, float y1)
        {
            var rect = Rect(parent, name, x0, y0, x1, y1);
            var image = rect.gameObject.AddComponent<UnityEngine.UI.Image>();
            image.color = panel;
            image.raycastTarget = true;
            return rect;
        }
        TMP_Text Text(Transform parent, string value, int size, float x0, float y0, float x1, float y1)
        {
            var rect = Rect(parent, "Label", x0, y0, x1, y1);
            var label = rect.gameObject.AddComponent<TextMeshProUGUI>();
            label.font = font;
            label.text = value;
            label.fontSize = size;
            label.color = ink;
            label.raycastTarget = false;
            label.textWrappingMode = TextWrappingModes.Normal;
            return label;
        }
        void Button(Transform parent, string caption, float x0, float y0, float x1, float y1, UnityEngine.Events.UnityAction action)
        {
            var rect = Panel(parent, caption, x0, y0, x1, y1);
            rect.GetComponent<UnityEngine.UI.Image>().color = new Color(.22f, .34f, .33f);
            var button = rect.gameObject.AddComponent<UnityEngine.UI.Button>();
            button.targetGraphic = rect.GetComponent<UnityEngine.UI.Image>();
            button.onClick.AddListener(action);
            Text(rect, caption, 19, .04f, .08f, .96f, .92f).alignment = TextAlignmentOptions.Center;
        }
    }
}
