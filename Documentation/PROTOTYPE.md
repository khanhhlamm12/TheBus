# Prototype nền tảng — The Last Route

Ngày: 29/10/2026. Đây là bước khởi đầu của M0–M2, chưa phải ca chơi hoàn chỉnh M4.

## Chạy trong Unity

1. Mở `Assets/TheLastRoute/Scenes/BusPrototype.unity` riêng trong Unity 6000.6.7f1.
2. Nhấn Play và bấm vào Game View.
3. Đi dọc lối đi bằng WASD, đứng ngang ghế có người và nhấn E.

Menu `The Last Route/Create or Open Prototype` mở lại scene. Các hình khối và UI được dựng khi Play; Edit Mode chỉ có đối tượng điều phối và dữ liệu cấu hình.

## Chạy bản Windows

Mở `Builds/Prototype/TheLastRoute.exe`. Giữ nguyên các thư mục và DLL đi kèm. Bản Development Windows đã build thành công; đã chạy thử khởi động nền không đồ họa, chưa kiểm chứng toàn bộ thao tác trực tiếp trong cửa sổ standalone. Giao diện đã được xem bằng Game View của Editor.

Để build lại, chạy phương thức Editor `TheLastRoute.Editor.PrototypeSetup.BuildWindows()`. Phương thức chỉ build scene prototype và không thay danh sách scene của dự án.

## Điều khiển

| Phím / thao tác | Chức năng |
|---|---|
| WASD | Đi lại; không đi xuyên ghế |
| E | Kiểm tra người gần nhất trong phạm vi |
| Chuột | Chọn công cụ và quyết định trong UI |
| F | Bật/tắt chỉ báo đèn pin và tiêu hao pin |
| Tab | Mở/đóng sổ tay; thời gian vẫn chạy |
| Esc | Tạm dừng/tiếp tục đồng hồ, di chuyển và phép kiểm tra |
| Trở lại | Thoát kiểm tra; giữ bằng chứng đã hoàn thành |

## Đã triển khai

- Bản đồ hình khối tạm, 12 ghế, bốn hành khách cố định.
- Di chuyển top-down, va chạm lối đi và chọn hành khách theo khoảng cách.
- UI tiếng Việt, hồ sơ, sổ tay, đồng hồ, điện thiết bị và pin.
- UV và gương dưới dạng phép kiểm tra đếm thời gian; kết quả được lưu theo ID.
- Hủy phép đo không ghi bằng chứng; điện đã dùng không hoàn lại.
- Quyết định thông qua, tái kiểm tra, đánh dấu cách ly và hủy quyết định.
- Giới hạn một hồ sơ được đánh dấu cách ly.
- Cấu hình thời gian/chi phí/tốc độ trong `Assets/TheLastRoute/Data/PrototypeSettings.asset`.

## Giới hạn hiện tại

- Không có điều kiện thắng/thua, tấn công, sửa điện, hồi vật tư, checkpoint, âm thanh hay kết ca.
- Cách ly chỉ là trạng thái hồ sơ; hành khách chưa di chuyển hoặc được bàn giao.
- UV chưa có vùng rê chuột; gương chưa có hoạt ảnh phản chiếu. Kết quả hiện bằng chữ.
- Đèn pin mới có chỉ báo hình khối, chưa phải ánh sáng/độ tối hoàn chỉnh.
- Dữ liệu khách cố định và được tạo lại khi bắt đầu Play; chưa có sinh ngẫu nhiên hoặc lưu đĩa.
- Toàn bộ art là placeholder phục vụ kiểm chứng kỹ thuật.

## Quyết định nền tảng đã chốt

- Dùng Unity 6.6, URP 2D, Input System và uGUI/TMP từ các package có sẵn.
- Mở UI kiểm tra khóa di chuyển nhưng không dừng đồng hồ.
- Mở sổ tay vẫn cho phép phép đo đang chạy tiếp tục; Esc dừng cả hai.
- Thoát kiểm tra hủy phép đo đang dở, giữ bằng chứng cũ và chi phí đã tiêu.
- Hồ sơ hành khách được dùng chung ở top-down và UI; không tạo lại đáp án khi kiểm tra.

## Kiểm chứng

11 smoke check logic đã chạy trực tiếp trong Unity Play Mode, kết quả ở `PrototypeSmokeChecks.txt`. Bao gồm di chuyển, va chạm, tương tác gần nhất, khóa di chuyển, lưu bằng chứng, hủy phép đo, tạm dừng, UV và giới hạn cách ly. Đây là kiểm chứng logic qua API runtime, chưa thay thế buổi chơi thử thủ công toàn bộ bằng bàn phím/chuột.

Ảnh Game View nằm trong `Assets/Screenshots/`. Font tạm là Roboto-Regular từ package Unity Searcher; giữ thông tin nguồn và giấy phép trong `Assets/TheLastRoute/UI/`.

## Bước tiếp theo

1. Làm vùng tương tác UV và hoạt ảnh gương để người chơi tự quan sát bằng chứng.
2. Tạo dữ liệu cấu hình chặng/hành khách thay cho bộ cố định.
3. Thêm sự cố điện có sửa thủ công và quy trình cách ly thực tế.
4. Kết nối đe dọa, ứng phó khẩn cấp, kết chặng và báo cáo để đạt M4.
