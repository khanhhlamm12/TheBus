# TODO — THE LAST ROUTE / Chuyến Xe Cuối

**Ngày tạo:** 29/10/2026  
**Tài liệu thiết kế:** [GDD.md](GDD.md)  
**Mục tiêu đầu tiên:** Prototype chơi được từ đầu đến cuối trong 10–15 phút.

> Tiến độ 29/10/2026: đã triển khai nền tảng top-down và UI kiểm tra; chưa hoàn thành M0–M2. Xem [hướng dẫn prototype](Documentation/PROTOTYPE.md) và [kết quả kiểm tra logic](Documentation/PrototypeSmokeChecks.txt). Các mục chưa được kiểm chứng đủ vẫn để trống.

## Cách sử dụng

**Asset v02:** Đã thêm 6 bảng concept mới tại [Art/Expansion-v02/README.md](Art/Expansion-v02/README.md). Chưa đánh dấu hoàn tất các mục sản xuất asset hoặc tích hợp gameplay tương ứng.

**Ưu tiên hiện tại:** Người dùng chuyển sang thiết kế asset trước và đã duyệt phong cách pixel art tối tăm. Tạm dừng mở rộng gameplay; theo dõi bộ thiết kế và backlog asset tại [Art/Expansion-v01/README.md](Art/Expansion-v01/README.md).

- **P0:** Bắt buộc cho prototype và để kiểm chứng vòng lặp cốt lõi.
- **P1:** Bắt buộc cho một ca hoàn chỉnh và bản phát hành theo GDD.
- **P2:** Hoàn thiện trải nghiệm; thực hiện sau khi vòng lặp đã ổn định.
- Thứ tự đề xuất: **M0 → M1 → M2 → M3 → M4 → M5 → M6**.
- Mỗi tính năng hoàn thành cần chạy được trong bản build, không chỉ trong Editor.
- Lưu các giá trị cân bằng dưới dạng cấu hình có thể sửa, tránh rải số cố định trong mã.

## M0 — Chốt thiết kế và nền tảng [P0]

### Phạm vi và quyết định còn thiếu

- [ ] Xác nhận tên làm việc, đối tượng người chơi và cấu hình PC mục tiêu.
- [x] Khảo sát phiên bản Unity, render pipeline, input và cấu trúc dự án hiện có trước khi chọn cách triển khai.
- [x] Chốt framework UI sau khi đánh giá nhu cầu chân dung nhiều lớp, kéo thả và tương tác công cụ.
- [ ] Chốt giới hạn prototype: một chặng mở rộng, bốn hành khách, UV, gương, một sự cố điện và một quy trình khẩn cấp.
- [ ] Xác định hai cấu hình thử riêng: chặng không có thực thể và chặng có đúng một thực thể.
- [ ] Chốt quy tắc khi ghế cách ly đã có người mà một thực thể khác tấn công; phải có phương án ứng phó không gây kẹt tiến trình.
- [ ] Chốt thời điểm thực thể còn ẩn mình chấp nhận lệnh cách ly và khi nào phải dùng đèn trấn áp.
- [ ] Chốt xử lý hành khách chưa kiểm tra đến điểm xuống giữa ca: cảnh báo, cơ hội xử lý và hậu quả.
- [x] Chốt việc ngắt công cụ giữa chừng: chi phí đã tiêu, tiến độ đo và điều kiện phải đo lại.
- [ ] Chốt cách tính thưởng một lần để tái kiểm tra hoặc đổi quyết định không tạo tiền vô hạn.
- [ ] Chốt cách hồi tài nguyên, thời gian sửa nguồn và khả năng sửa khi hết vật tư.

### Nền tảng kỹ thuật

- [ ] Tạo scene thử nghiệm riêng và cấu trúc thư mục cho mã, dữ liệu, prefab, UI, art, audio.
- [ ] Tạo dữ liệu cấu hình hành khách, công cụ, quy tắc, chặng và sự kiện.
- [ ] Tách trạng thái điều khiển: di chuyển, kiểm tra, menu tạm dừng và kết chặng. (Đã có ba trạng thái đầu; chưa có kết chặng.)
- [ ] Tạo đồng hồ mô phỏng chung; kiểm tra và sổ tay không dừng thời gian, menu tạm dừng có dừng.
- [ ] Thêm chế độ debug để chọn cấu hình khách, ép sự kiện và xem tài nguyên; không xuất hiện trong bản chơi thông thường.

**Điều kiện hoàn thành M0:** Phạm vi prototype và các trường hợp có thể gây kẹt tiến trình được ghi rõ; scene thử nghiệm có thể chạy và build.

## M1 — Di chuyển trong xe [P0]

- [ ] Dựng bản đồ xe bằng hình khối tạm: cabin, lối đi, ghế, cửa, tủ điện, tủ vật tư, công tắc khẩn cấp.
- [ ] Đặt ID và điểm tương tác cho từng ghế; xác định ghế cách ly.
- [x] Làm điều khiển WASD và va chạm, tránh mắc kẹt giữa ghế.
- [x] Làm camera top-down và thứ tự hiển thị nhân vật so với ghế, tường.
- [x] Làm tương tác E theo khoảng cách và đối tượng đang được chọn.
- [ ] Hiển thị nhắc tương tác phù hợp với trạng thái đối tượng.
- [x] Tạo bốn hành khách tạm; gắn cùng ID với dữ liệu kiểm tra.
- [ ] Làm di chuyển hành khách theo các điểm định sẵn để vào ghế và tới ghế cách ly. (Hiện hành khách đứng yên.)
- [ ] Thêm đèn pin, bật/tắt và chỉ báo pin.

**Điều kiện hoàn thành M1:** Người chơi đi được tới mọi điểm cần thiết, chọn đúng hành khách và không thể đi xuyên nội thất.

## M2 — Kiểm tra và bằng chứng [P0]

### Giao diện kiểm tra

- [x] Chuyển từ tương tác ghế sang màn hình cận cảnh đúng hành khách.
- [x] Khóa di chuyển nhân vật trong khi kiểm tra nhưng tiếp tục mô phỏng xe.
- [x] Làm nút thoát kiểm tra với thời gian phản hồi không quá 0,5 giây.
- [x] Dựng bố cục chân dung, giấy tờ, công cụ, nhật ký và quyết định.
- [ ] Tạo các lớp chân dung tạm: cơ thể, mắt, tay và phản chiếu.
- [ ] Hiển thị vé và dữ liệu trạm để đọc thủ công trong prototype.
- [x] Tạo sổ tay có quy tắc UV và gương; cung cấp trước tình huống cần sử dụng.

### Công cụ và lưu bằng chứng

- [ ] Làm vùng chiếu UV theo chuột trên tay và vé.
- [ ] Chỉ hiện dấu UV khi vùng chiếu đi qua khu vực hợp lệ.
- [ ] Trừ điện UV theo thời gian sử dụng; dừng khi hết điện hoặc đóng công cụ.
- [ ] Làm thao tác yêu cầu khách quay đầu và so sánh phản chiếu bằng gương.
- [x] Lưu bằng chứng theo hành khách và phép kiểm tra đã hoàn thành.
- [x] Khôi phục đúng bằng chứng khi thoát và mở lại kiểm tra.
- [ ] Phân biệt dấu hiệu gợi ý, mâu thuẫn và bằng chứng xác nhận trong dữ liệu.
- [ ] Thêm trạng thái công cụ không hoạt động/đo không hợp lệ; không trả kết quả bình thường khi lỗi.
- [x] Cố định danh tính thật của khách từ lúc sinh, không đổi khi mở UI hoặc đo lại.

**Điều kiện hoàn thành M2:** Có thể phân biệt một người thật đáng ngờ với một thực thể bằng quy tắc rõ ràng; mở lại UI không làm mất hoặc thay đổi bằng chứng.

## M3 — Quyết định và áp lực sinh tồn [P0]

### Phân loại hành khách

- [ ] Làm ba quyết định: cho tiếp tục, tái kiểm tra, cách ly.
- [ ] Hiển thị trạng thái trên danh sách hành khách và tại ghế.
- [ ] Cho phép tái kiểm tra hành khách đã được thông qua.
- [ ] Giới hạn một vị trí cách ly; báo rõ lý do khi không thể cách ly thêm.
- [ ] Cho phép hủy cách ly trước khi bàn giao theo quy tắc đã chốt ở M0.
- [ ] Bàn giao người bị cách ly tại trạm và giải phóng ghế.
- [ ] Ghi kết quả đúng/sai để tính báo cáo, tránh trả thưởng nhiều lần.

### Sinh tồn và sự cố

- [ ] Làm tài nguyên điện dự phòng và sức khỏe cho prototype.
- [ ] Làm bộ chuyển đổi nạp điện theo cấu hình.
- [ ] Thêm một sự cố mất điện có tín hiệu báo trước và điểm sửa rõ ràng.
- [ ] Cho phép sửa nguồn thủ công khi điện bằng 0.
- [ ] Làm bình điện khẩn cấp dùng một lần.
- [ ] Làm tiến trình thực thể: ẩn mình → gây nhiễu → đe dọa → tấn công.
- [ ] Hiển thị ít nhất hai tín hiệu trước tấn công, gồm cả chỉ báo không phụ thuộc âm thanh.
- [ ] Thử cửa sổ phản ứng 8–12 giây và kiểm tra quãng đường tới công tắc.
- [ ] Làm đèn trấn áp, mức tiêu điện, thời gian hồi và trạng thái bất động.
- [ ] Làm thao tác đưa thực thể đã bị trấn áp vào ghế cách ly.
- [ ] Gây sát thương khi xử lý trễ; thêm miễn sát thương ngắn và thời gian hồi sau đợt tấn công.
- [ ] Thực hiện phương án đã chốt cho trường hợp ghế cách ly đang đầy.

**Điều kiện hoàn thành M3:** Bỏ lọt thực thể gây hậu quả nhưng vẫn có cơ hội cứu vãn; hết điện và ghế cách ly đầy không tạo trạng thái bất khả thi ngoài các điều kiện thua đã định nghĩa.

## M4 — Prototype hoàn chỉnh [P0]

### Luồng chơi

- [ ] Làm màn nhận ca và hướng dẫn ngắn về hai công cụ.
- [ ] Làm một chặng thử nghiệm 10–15 phút với mở đầu, áp lực giữa chặng và trạm kết thúc.
- [ ] Làm điều kiện hoàn thành và màn thất bại.
- [ ] Làm nút chơi lại với cùng cấu hình hành khách và sự kiện ban đầu.
- [ ] Làm báo cáo cuối chặng: quyết định, sự thật và bằng chứng bị bỏ sót.
- [ ] Thêm tiếng động cơ, mưa, công cụ và cảnh báo bằng tài nguyên tạm có quyền sử dụng.
- [ ] Giữ âm thanh khoang xe trong màn hình kiểm tra.
- [ ] Xuất bản build PC để chơi thử ngoài Editor.

### Kiểm chứng vòng lặp

- [ ] Chơi hết chặng không có thực thể mà không buộc phải cách ly ai.
- [ ] Chơi hết chặng có thực thể bằng đường xác minh hợp lệ.
- [ ] Thử thông qua nhầm thực thể và phục hồi tình huống bằng ứng phó khẩn cấp.
- [ ] Thử cách ly nhầm người thật, sau đó hủy hoặc bàn giao và kiểm tra hậu quả.
- [ ] Kiểm tra đóng/mở UI nhanh, ngắt công cụ, tạm dừng và chuyển trạm trong lúc kiểm tra.
- [ ] Kiểm tra đồng hồ và tiêu hao tài nguyên ở nhiều tốc độ khung hình.
- [ ] Mời 3–5 người chưa biết đáp án chơi thử; ghi thời gian kiểm tra, quyết định và nguyên nhân họ đưa ra.
- [ ] Ghi số lần chuyển góc nhìn có mục đích, số cảnh báo bị bỏ qua và nguyên nhân thất bại.
- [ ] Đánh giá liệu người chơi hiểu bằng chứng sau báo cáo, có sợ khi chưa có jumpscare và có cơ hội sửa sai.
- [ ] Điều chỉnh chi phí quét, nhịp sự cố và mức rõ ràng của bằng chứng theo kết quả thử.

**Cổng quyết định M4:** Chỉ mở rộng nội dung khi người chơi hiểu cách suy luận và việc chuyển giữa tuần tra/kiểm tra tạo quyết định có ý nghĩa. Nếu chưa đạt, quay lại M2–M3.

## M5 — Một ca hoàn chỉnh và hệ thống bản đầy đủ [P1]

### Công cụ và hành khách

- [ ] Bổ sung đầu đọc vé với thời gian đọc và chi phí điện.
- [ ] Bổ sung máy đo nhịp với các trạng thái chờ, đang đo, hợp lệ và không hợp lệ.
- [ ] Thực hiện điều kiện xác nhận Kẻ Rỗng: hai phép đo hợp lệ kết hợp bằng chứng mắt.
- [ ] Hoàn thiện ba nhóm thực thể với hành vi phá hoại riêng.
- [ ] Thêm đối thoại trạm lên, điểm đến và người đi cùng.
- [ ] Thêm người thật có vé sai hoặc lời khai mâu thuẫn nhưng không có bằng chứng siêu nhiên.
- [ ] Thêm hành khách đổi ghế; cập nhật vị trí nhưng giữ nguyên danh tính và bằng chứng.
- [ ] Kiểm tra trình sinh khách chỉ chọn thực thể có đường xác minh bằng bộ công cụ hiện có.

### Vận hành và tiến trình

- [ ] Mở rộng thành bốn chặng mỗi ca, mỗi chặng mục tiêu 4–6 phút.
- [ ] Làm lịch lên/xuống của khách theo từng trạm, tối đa tám khách trên xe.
- [ ] Làm độ toàn vẹn xe, lỗi cửa và sửa chữa có chi phí.
- [ ] Làm tỉnh táo và hồi phục giới hạn cạnh cabin.
- [ ] Làm ảo giác không sửa dữ liệu bằng chứng hoặc kết quả công cụ hợp lệ.
- [ ] Làm sơ cứu, pin dự phòng và vật tư sửa chữa.
- [ ] Làm điểm nghiệp vụ, doanh thu, tiền phạt và thưởng kết ca.
- [ ] Làm mua vật tư giữa các ca và cấp phát tối thiểu để tránh vòng thua do nghèo.
- [ ] Làm lưu/đọc checkpoint đầu chặng, gồm seed, khách, tài nguyên, sự kiện và tiến trình chiến dịch.
- [ ] Kiểm tra kết quả trước/sau tải lại khớp nhau và không trả thưởng trùng.
- [ ] Thêm nhịp nghỉ sau sự kiện lớn; hạn chế sự kiện chồng lấn chưa có cách ứng phó.

**Điều kiện hoàn thành M5:** Một ca 20–30 phút chạy trọn vẹn với đủ công cụ, tài nguyên, báo cáo và checkpoint; mọi trường hợp được sinh đều có thể xác minh.

## M6 — Nội dung, hình ảnh, âm thanh và phát hành

### Nội dung năm ca [P1]

- [ ] Viết bản tin quy tắc, đối thoại, ghi chú và sự kiện cho từng ca.
- [ ] Lập bảng mở khóa công cụ/quy tắc; không sinh thử thách trước khi hướng dẫn.
- [ ] Xây dựng ca 1: học việc và dấu hiệu dễ xác minh.
- [ ] Xây dựng ca 2: UV, cách ly và thông tin về người tiền nhiệm.
- [ ] Xây dựng ca 3: đo nhịp, mất điện và hướng dẫn bộ đàm mâu thuẫn.
- [ ] Xây dựng ca 4: phối hợp công cụ và hành khách quay lại.
- [ ] Xây dựng ca 5: tổng hợp cơ chế và lựa chọn trạm cuối.
- [ ] Định nghĩa điều kiện kết thúc bằng bảng rõ ràng, tránh điều kiện ẩn không thể suy luận.
- [ ] Viết ít nhất một đường hoàn thành hợp lệ dù người chơi đã mắc sai sót.

### Hình ảnh và âm thanh [P1]

- [ ] Chốt kích thước sprite, độ phân giải nội bộ và quy tắc phối hợp pixel art với chân dung 2D.
- [ ] Hoàn thiện nội thất xe, ánh sáng và các trạng thái thiết bị hỏng.
- [ ] Hoàn thiện nhân viên soát vé và hoạt ảnh di chuyển/tương tác.
- [ ] Tạo khoảng 12 mẫu ngoại hình có thể kết hợp lớp; tránh ngoại hình làm lộ đáp án cố định.
- [ ] Hoàn thiện lớp dị thường mắt, tay, miệng và phản chiếu.
- [ ] Hoàn thiện UI thiết bị, giấy tờ, sổ tay và báo cáo, hỗ trợ đầy đủ dấu tiếng Việt.
- [ ] Tạo cảnh ngoài xe bằng lớp cuộn và biến thể trạm dừng.
- [ ] Hoàn thiện âm thanh động cơ, mưa, thân xe, cửa, công cụ, bộ đàm và thực thể.
- [ ] Cân bằng âm lượng để cảnh báo nghe được mà không vô tình tiết lộ danh tính thực thể.
- [ ] Kiểm tra giấy phép và ghi công tài nguyên bên ngoài.

### Trải nghiệm và khả năng tiếp cận [P1/P2]

- [ ] [P1] Phụ đề hội thoại/bộ đàm và chỉ báo trực quan cho cảnh báo quan trọng.
- [ ] [P1] Dấu hiệu có hình dạng hoặc chữ, không chỉ khác màu.
- [ ] [P1] Tùy chọn giảm nhấp nháy, rung màn hình, nhiễu và âm thanh đột ngột.
- [ ] [P1] Menu âm lượng, hiển thị, tạm dừng và tiếp tục.
- [ ] [P2] Chế độ hỗ trợ kéo dài thời gian phản ứng, giữ nguyên logic suy luận.
- [ ] [P2] Điều chỉnh cỡ chữ và kiểm tra giao diện ở các tỷ lệ màn hình mục tiêu.
- [ ] [P2] Tinh chỉnh chuyển cảnh và hiệu ứng analog sau khi thông tin kiểm tra đã dễ đọc.

### Kiểm thử và bản phát hành [P1]

- [ ] Kiểm tra toàn bộ bảng quy tắc bằng dữ liệu: mỗi thực thể có bằng chứng đủ và mỗi phép đo lỗi được nhận diện.
- [ ] Kiểm tra đồng bộ hành khách giữa sprite, ghế, UI, cách ly và checkpoint.
- [ ] Kiểm tra nhiều thực thể, ghế cách ly đầy, nguồn cạn và chuyển trạm cùng lúc.
- [ ] Kiểm tra không thể nhận thưởng nhiều lần bằng đổi quyết định hoặc tải lại.
- [ ] Kiểm tra không có dấu hiệu vô tình tiết lộ đáp án qua tên file, UI debug hoặc âm thanh riêng.
- [ ] Chơi trọn năm ca theo các hướng quyết định và điều kiện kết thúc khác nhau.
- [ ] Đo hiệu năng trên cấu hình PC mục tiêu, ưu tiên lúc UI kiểm tra và sự kiện cùng hoạt động.
- [ ] Kiểm tra lưu game hỏng/thiếu, thoát giữa chặng và tiếp tục chiến dịch.
- [ ] Xuất release candidate, tắt debug và kiểm tra trên máy ngoài môi trường phát triển.
- [ ] Hoàn thiện hướng dẫn điều khiển, credit, số phiên bản và danh sách lỗi đã biết.

**Điều kiện hoàn thành M6:** Bản build đạt phạm vi GDD, có thể hoàn thành chiến dịch, lưu/đọc ổn định và không còn lỗi chặn tiến trình đã biết.

## Ngoài phạm vi hiện tại

Không đưa các mục dưới đây vào lịch thực hiện trừ khi có quyết định thay đổi phạm vi:

- Lái xe bus thủ công hoặc mô phỏng giao thông.
- Khám phá thế giới ngoài xe.
- Chiến đấu bằng vũ khí.
- Chơi mạng hoặc co-op.
- Bản đồ sinh ngẫu nhiên quy mô lớn.
- Hệ thống hội thoại AI hoặc sinh nội dung trực tiếp trong runtime.

## Mẫu ghi nhận sau mỗi lần chơi thử

| Trường | Nội dung cần ghi |
|---|---|
| Phiên bản / ngày | Build và thời điểm thử |
| Cấu hình | Ca, chặng, seed, bộ công cụ |
| Kết quả | Hoàn thành/thất bại, thời gian |
| Quyết định sai | Hành khách, bằng chứng đã có, lý do người chơi |
| Điểm gây khó hiểu | Quy tắc, UI, âm thanh hoặc thao tác |
| Tài nguyên | Lượng còn lại, thời điểm cạn, khả năng hồi phục |
| Lỗi | Bước tái hiện, kết quả thực tế, kết quả mong đợi |
| Hành động tiếp theo | Một thay đổi cụ thể và cách kiểm chứng |
