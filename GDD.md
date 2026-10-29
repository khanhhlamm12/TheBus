# GAME DESIGN DOCUMENT — THE LAST ROUTE

**Tên tạm:** Chuyến Xe Cuối  
**Phiên bản:** 0.1 — Thiết kế nền tảng cho prototype  
**Ngày cập nhật:** 29/10/2026  
**Thể loại:** Survival Horror / Simulation / Puzzle / Anomaly Detection  
**Chế độ:** Một người chơi  
**Nền tảng mục tiêu:** PC — chuột và bàn phím  
**Thời lượng dự kiến:** 5 ca đêm, mỗi ca 20–30 phút  
**Kế hoạch thực hiện:** [TODO.md](TODO.md)

> **Trụ cột thiết kế:** Quan sát có chủ đích — Ra quyết định dưới áp lực — Sống sót với hậu quả.
>
> Các thông số dưới đây là giá trị khởi điểm để thử nghiệm, chưa phải cân bằng cuối cùng.

---

## 1. Executive Summary — Tóm tắt dự án

### 1.1. Logline

Trên chuyến xe bus cuối cùng chạy qua vùng ngoại ô bị phong tỏa, một nhân viên soát vé phải kiểm tra danh tính hành khách, giữ các thiết bị hoạt động và xác định những thứ đang giả làm người trước khi xe đến trạm cuối.

### 1.2. Trải nghiệm chủ đạo

Người chơi liên tục đối mặt với ba câu hỏi:

- Hành khách này có thực sự là con người?
- Có đáng tiêu hao thêm thời gian và năng lượng để kiểm chứng?
- Trong lúc mình tập trung kiểm tra, điều gì đang xảy ra phía sau?

Nỗi sợ đến từ việc phải hoàn thành một công việc có quy trình rõ ràng trong môi trường ngày càng khó tin. Hành khách đáng ngờ có thể hoàn toàn vô hại; một người có giấy tờ hợp lệ vẫn có thể là thực thể.

### 1.3. Điểm nhấn bán hàng — USP

| USP | Giá trị trải nghiệm |
|---|---|
| Một xe bus, hai góc nhìn | Top-down giúp quản lý không gian; màn hình cận cảnh buộc người chơi tập trung vào hành khách và mất khả năng quan sát toàn xe. |
| Kẻ giả mạo tồn tại giữa các lần kiểm tra | Thực thể đã được thông qua có thể đổi chỗ, phá thiết bị hoặc đe dọa người khác. |
| Quy trình kiểm tra gắn với sinh tồn | Quét thêm giúp tăng độ chắc chắn nhưng làm hao điện và kéo dài thời gian bỏ trống lối đi. |
| Không gian quen thuộc thay đổi từng chút | Một ghế có thêm người, cửa sổ phản chiếu sai, chuông dừng vang lên ở đoạn đường không có trạm. |

### 1.4. Phạm vi khả thi cho đội indie

Bản phát hành đầu tiên sử dụng:

- Một bản đồ nội thất xe bus cố định: cabin, cửa trước, khoang ghế và cửa sau.
- 12 vị trí ghế; tối đa 8 hành khách cùng lúc.
- 5 ca đêm với mức độ phức tạp tăng dần.
- 3 nhóm thực thể, 4 công cụ kiểm tra và khoảng 12 mẫu ngoại hình có thể hoán đổi chi tiết.
- Không có chiến đấu trực tiếp, lái xe thủ công hoặc khám phá ngoài xe.
- Phong cảnh ngoài cửa sổ được thể hiện bằng các lớp hình chuyển động và sự kiện dựng sẵn.

**Mục tiêu prototype:** Chứng minh rằng việc rời màn hình kiểm tra để xử lý sự cố trên xe tạo ra quyết định thú vị, không chỉ tạo thêm thao tác.

---

## 2. Core Gameplay Loop — Vòng lặp cốt lõi

### 2.1. Vòng lặp trong một ca

**Nhận ca → Đón khách → Tuần tra → Kiểm tra → Phân loại → Xử lý hậu quả → Bảo dưỡng → Đến trạm tiếp theo.**

| Bước | Hành động của người chơi | Áp lực hoặc quyết định |
|---|---|---|
| 1. Nhận ca | Đọc bản tin tuyến, kiểm tra thiết bị, nhận vật tư. | Ghi nhớ quy tắc áp dụng cho ca hiện tại. |
| 2. Đón khách | Quan sát khách lên xe, số ghế và biểu hiện ban đầu. | Chọn người cần kiểm tra trước. |
| 3. Tuần tra | Đi dọc lối đi, nghe âm thanh, kiểm tra cửa và tủ điện. | Cân bằng nhiệm vụ vận hành với kiểm tra hành khách. |
| 4. Kiểm tra | Đối chiếu vé, quan sát, phỏng vấn và dùng công cụ. | Thu thập đủ bằng chứng với chi phí hợp lý. |
| 5. Phân loại | Cho tiếp tục, đánh dấu tái kiểm tra hoặc cách ly. | Chấp nhận mức độ rủi ro của quyết định. |
| 6. Xử lý | Bàn giao người bị cách ly tại trạm, sửa sự cố hoặc ứng phó thực thể. | Hậu quả có thể xuất hiện trễ. |
| 7. Kết ca | Nhận báo cáo, lương và vật tư cho ca sau. | Hiểu sai sót, điều chỉnh cách làm việc. |

### 2.2. Nhịp độ một ca

Mỗi ca có 4 chặng đường. Một chặng kéo dài khoảng 4–6 phút, gồm:

1. **Đầu chặng:** Khách mới lên, không khí tương đối ổn định.
2. **Giữa chặng:** Xuất hiện hành vi đáng ngờ hoặc sự cố thiết bị.
3. **Cuối chặng:** Người chơi phải hoàn tất các quyết định trước khi xe dừng.
4. **Tại trạm:** Khách xuống, bàn giao người bị cách ly và tiếp nhận khách mới.

Đồng hồ tuyến chạy trong cả hai góc nhìn. Mở menu tạm dừng sẽ dừng trò chơi; mở sổ tay trong lúc kiểm tra sẽ không dừng thời gian.

Không bắt buộc mọi chặng đều có thực thể. Các chặng hoàn toàn bình thường giúp người chơi tránh suy luận theo quy luật “luôn phải có một kẻ giả mạo”.

### 2.3. Điều kiện thắng và thua

**Hoàn thành ca:** Xe tới trạm cuối, người chơi còn sống và độ toàn vẹn xe lớn hơn 0.

**Thua ca khi:**

- Người chơi mất toàn bộ sức khỏe.
- Độ toàn vẹn xe giảm về 0.

Sai sót đơn lẻ thường tạo hậu quả có thể khắc phục. Tấn công nghiêm trọng phải có dấu hiệu báo trước và một khoảng thời gian phản ứng.

Game lưu tại đầu mỗi chặng. Khi thất bại, người chơi chơi lại chặng đó với cùng cấu hình hành khách để có thể học từ bằng chứng đã bỏ sót.

---

## 3. Detailed Mechanics — Cơ chế chi tiết

### 3.1. Pha di chuyển — Top-down

#### Không gian và điều khiển

- **WASD:** Di chuyển.
- **E:** Tương tác với hành khách hoặc thiết bị.
- **F:** Bật/tắt đèn pin.
- **Tab:** Mở sổ tay và danh sách hành khách.
- **Esc:** Tạm dừng.

Người chơi di chuyển trong một bản đồ nhỏ, có các vị trí tương tác cố định. Hành khách chủ yếu ngồi tại ghế; việc di chuyển của họ dùng các điểm đích định sẵn thay vì hệ thống tìm đường phức tạp.

Các hoạt động tuần tra gồm:

- Kiểm tra ghế có người chưa được xác minh.
- Phát hiện khách đổi chỗ hoặc xuất hiện sai vị trí.
- Khởi động lại đèn khoang và bộ nguồn.
- Kiểm tra cửa sau khi cảm biến báo mở.
- Đáp ứng yêu cầu của khách để thu thập thông tin hoặc giảm căng thẳng.

#### Tài nguyên sinh tồn

| Tài nguyên | Quy tắc đề xuất | Cách phục hồi |
|---|---|---|
| Sức khỏe: 3 mức | Mất một mức khi bị tấn công. Có khoảng miễn sát thương ngắn sau khi trúng đòn. | Một bộ sơ cứu mỗi ca. |
| Pin đèn pin: 100 | Hao khoảng 1 điểm/giây khi bật. Không ảnh hưởng nguồn điện thiết bị kiểm tra. | Thay pin dự phòng tại tủ vật tư. |
| Điện dự phòng: 100 | Cấp cho công cụ quét và đèn khẩn cấp. Đèn khoang thông thường dùng nguồn riêng. | Bộ chuyển đổi nạp chậm khi hoạt động; có thể bị phá. |
| Tỉnh táo: 100 | Giảm khi tiếp xúc lâu với thực thể hoặc chứng kiến sự kiện bất thường. | Nghỉ ngắn cạnh cabin, có giới hạn hồi mỗi chặng. |
| Toàn vẹn xe: 100 | Giảm do cửa lỗi, thiết bị quá tải hoặc thực thể phá hoại. | Sửa chữa phục hồi một phần, tiêu hao vật tư. |

Tỉnh táo thấp gây tiếng động giả và hình bóng thoáng qua. **Nó không thay đổi bằng chứng đã ghi hoặc kết quả của thiết bị đang hoạt động đúng.** Điều này giữ được yếu tố tâm lý mà không biến suy luận thành đoán mò.

Khi điện dự phòng cạn, người chơi vẫn có thể đọc vé, đối thoại và xem hồ sơ; các công cụ điện tạm ngừng. Sự cố nguồn phải luôn sửa được bằng thao tác thủ công, tránh tình trạng không còn cách tiếp tục.

### 3.2. Pha kiểm tra — Inspection

#### Chuyển góc nhìn

Khi tương tác với hành khách:

1. Người chơi đứng tại vị trí tương tác cạnh ghế.
2. Màn hình chuyển sang hình cận cảnh hành khách.
3. Bảng dụng cụ mở ở cạnh dưới.
4. Mô phỏng trên xe tiếp tục chạy.

Thoát kiểm tra mất không quá 0,5 giây. Bằng chứng và thao tác đã hoàn thành được lưu theo từng hành khách.

#### Bố cục giao diện

- **Giữa màn hình:** Khuôn mặt, thân trên, hai tay và một phần ghế.
- **Bên trái:** Vé xe, hồ sơ tuyến và câu trả lời của khách.
- **Bên phải:** Công cụ đang chọn và kết quả đọc.
- **Phía dưới:** Nhật ký bằng chứng, trạng thái thiết bị, các nút quyết định.

Ảnh cận cảnh được dựng từ các lớp 2D: cơ thể, mắt, miệng, tay, giấy tờ và lớp hiệu ứng. Cách này cho phép tạo nhiều biến thể mà không phải vẽ lại toàn bộ chân dung.

#### Công cụ kiểm tra

| Công cụ | Thao tác và kết quả | Chi phí đề xuất | Giới hạn |
|---|---|---|---|
| Đầu đọc vé | Đặt vé lên khay; đọc mã vé, trạm lên và điểm đến. | 3 giây, 1 điện | Vé sai có thể do khách trốn vé hoặc lỗi hành chính. |
| Đèn UV | Rê vùng chiếu lên vé và tay; tìm dấu xác thực hoặc ký hiệu dị thường. | 1 điện/giây | Một số dấu mờ cần đối chiếu mẫu trong sổ tay. |
| Máy đo nhịp tiếp xúc | Kẹp cảm biến vào ngón tay; đọc nhịp sau khi tín hiệu ổn định. | 6 giây, 5 điện | Báo “không hợp lệ” nếu khách cử động hoặc thiết bị lỗi. |
| Gương kiểm chứng | Chọn mắt, miệng hoặc bàn tay để quan sát chuyển động và phản chiếu đồng thời. | 5 giây, không dùng điện | Chỉ xác nhận một số nhóm thực thể; cần thực hiện đúng động tác yêu cầu. |

Đối thoại là một thao tác miễn phí về điện nhưng tiêu tốn thời gian. Các câu hỏi tập trung vào dữ kiện có thể kiểm chứng: trạm lên, điểm đến, vật dụng mang theo hoặc người đi cùng.

**Nguyên tắc:** Không công cụ nào phân loại được mọi thực thể. Mỗi nhóm có một dấu hiệu quyết định hoặc một tổ hợp bằng chứng rõ ràng.

### 3.3. Hệ thống phát hiện dị thường

#### Ba lớp thông tin

1. **Dấu hiệu gợi ý:** Giúp chọn đối tượng kiểm tra, chưa đủ để kết luận. Ví dụ: tránh giao tiếp, mặc áo ướt, ngồi ở cuối xe.
2. **Mâu thuẫn có thể giải thích:** Yêu cầu kiểm chứng bổ sung. Ví dụ: vé không khớp trạm lên, nhớ sai điểm đến.
3. **Bằng chứng xác nhận:** Vi phạm quy tắc vật lý hoặc quy tắc thực thể đã được hướng dẫn. Ví dụ: hình phản chiếu không làm theo động tác dù gương đã được kiểm tra.

Ngoại hình, khuyết tật, bệnh lý hoặc cách nói chuyện khác thường không được dùng làm bằng chứng xác nhận thực thể.

#### Nhóm thực thể ban đầu

| Nhóm | Hành vi che giấu | Bằng chứng xác nhận | Hậu quả nếu bỏ lọt |
|---|---|---|---|
| Kẻ Phản Chiếu | Giấy tờ và nhịp tim bình thường. | Phản chiếu tiếp tục nhìn thẳng khi khách quay đầu theo yêu cầu. | Xuất hiện phía sau người chơi, tăng áp lực tỉnh táo. |
| Kẻ Mang Dấu | Hợp tác, ít thay đổi vị trí. | Dấu UV trên tay trùng mẫu cấm của ca hiện tại. | Làm hỏng bộ chuyển đổi điện. |
| Kẻ Rỗng | Bắt chước tốt khuôn mặt và hội thoại. | Không có nhịp qua hai lần đo hợp lệ và xuất hiện cấu trúc mắt phi nhân khi kiểm tra gần. | Tiến dần về cabin và đe dọa tài xế. |

Các dấu hiệu quyết định chỉ xuất hiện khi người chơi thực hiện đúng phép kiểm tra. Sau khi phép kiểm tra thành công, bằng chứng được lưu và không biến mất ngẫu nhiên.

#### Quy tắc công bằng

- Mỗi thực thể được sinh ra phải có ít nhất một đường xác minh bằng công cụ đang có trong ca.
- Quy tắc mới được thông báo trước khi sinh tình huống yêu cầu áp dụng.
- Máy lỗi phải hiển thị trạng thái lỗi hoặc tín hiệu không hợp lệ; không được âm thầm trả kết quả “bình thường”.
- Các chi tiết thẩm mỹ có thể ngẫu nhiên, nhưng đáp án của hành khách được cố định từ lúc lên xe.
- Thông tin được đọc và bằng chứng đã ghi luôn có thể xem lại.
- Không dùng âm thanh làm kênh thông tin duy nhất cho dấu hiệu bắt buộc.

### 3.4. Quyết định, thưởng và phạt

Người chơi có ba lựa chọn:

**Cho tiếp tục:** Hành khách được đánh dấu đã kiểm tra nhưng vẫn có thể bị tái kiểm tra nếu xuất hiện bằng chứng mới.

**Tái kiểm tra:** Gắn cờ nghi vấn và lưu các mâu thuẫn. Không có hình phạt tức thì, nhưng hành khách chưa được xử lý vẫn giữ nguyên nguy cơ.

**Cách ly:** Yêu cầu hành khách chuyển đến ghế an toàn cạnh cửa trước; bàn giao tại trạm tiếp theo. Chỉ có một vị trí cách ly.

| Kết quả | Hậu quả |
|---|---|
| Thông qua đúng người | Nhận đủ tiền vé và điểm nghiệp vụ. |
| Cách ly đúng thực thể | Ngăn hành vi phá hoại; nhận thưởng xử lý nguy cơ sau khi bàn giao. |
| Thông qua thực thể | Thực thể tăng mức đe dọa theo thời gian; người chơi vẫn có cơ hội phát hiện lại. |
| Cách ly nhầm người | Mất điểm nghiệp vụ và tiền bồi thường; chiếm vị trí cách ly cho tới trạm. |
| Bỏ qua vé không hợp lệ của người thật | Mất doanh thu, không kích hoạt hậu quả siêu nhiên. |
| Kết ca còn trường hợp chưa xử lý | Mất thưởng hoàn thành hồ sơ; thực thể còn trên xe được tính là bỏ lọt. |

Để tránh chiến thuật “cách ly tất cả”, game kết hợp giới hạn một ghế cách ly với hình phạt tài chính và đánh giá nghiệp vụ. Người chơi có thể hủy cách ly trước khi bàn giao nếu tìm được bằng chứng mới.

Lương dùng mua vật tư tiêu hao cho ca sau. Mỗi ca vẫn cấp bộ vật tư tối thiểu để người chơi không rơi vào vòng thua kéo dài vì thiếu tiền.

### 3.5. Đối đầu và ứng phó khẩn cấp

Thực thể chuyển qua các trạng thái:

**Ẩn mình → Gây nhiễu → Đe dọa → Tấn công.**

Trước khi tấn công, game phát ít nhất hai tín hiệu, chẳng hạn đèn trên ghế chớp và tiếng bước chân hướng về cabin. Người chơi có khoảng 8–12 giây để phản ứng trong cấu hình thử nghiệm.

Quy trình ứng phó chung:

1. Thoát màn hình kiểm tra.
2. Di chuyển đến công tắc khẩn cấp ở giữa xe.
3. Bật đèn trấn áp trong vài giây.
4. Khi thực thể bất động, tương tác để chuyển nó vào ghế cách ly.

Đèn khẩn cấp tiêu hao điện lớn, có thời gian hồi. Một bình điện dùng một lần được cấp đầu ca để vẫn có phương án ứng phó khi nguồn dự phòng cạn.

Không có cơ chế đánh trả. Nếu người chơi không phản ứng kịp, đợt tấn công gây một mức sát thương hoặc làm hỏng xe, sau đó chuyển sang thời gian hồi để tránh chuỗi sát thương không thể thoát.

### 3.6. Cấu trúc triển khai

Các hệ thống chính nên được tách riêng:

- `PassengerData`: Danh tính, vé, ghế, loại thực thể, dấu hiệu và trạng thái xử lý.
- `InspectionController`: Công cụ, vùng tương tác, kết quả đo và bằng chứng.
- `PassengerStateMachine`: Ngồi, di chuyển, cách ly, đe dọa, tấn công.
- `BusSystems`: Điện, cửa, đèn, độ toàn vẹn.
- `ShiftDirector`: Lịch trạm, sinh khách, sự kiện và nhịp căng thẳng.
- `RuleDatabase`: Quy tắc đang có hiệu lực và điều kiện xác nhận.

Giao diện cận cảnh và nhân vật top-down cùng tham chiếu một dữ liệu hành khách. Việc đổi góc nhìn không tạo lại hoặc đổi đáp án.

Bản lưu cần giữ cả hạt giống ngẫu nhiên, trạng thái hành khách, bằng chứng, tài nguyên và sự kiện đã kích hoạt.

---

## 4. Story & Atmosphere — Cốt truyện & Không khí

### 4.1. Tiền đề

Sau một loạt vụ mất tích, tuyến bus số 09 vẫn được phép hoạt động vì là phương tiện duy nhất nối khu dân cư với bệnh viện và khu công nghiệp bên kia rừng.

Người chơi nhận công việc thay cho một nhân viên đã nghỉ đột ngột. Trong tủ đồ của người tiền nhiệm có một cuốn sổ bị xé mất vài trang và dòng ghi chú:

> “Nếu anh nghe tôi gọi từ hàng ghế cuối, đừng trả lời.”

Tài xế ở sau vách kính, chỉ giao tiếp qua bộ đàm. Người chơi không chắc ông đang bảo vệ mình hay chỉ cố hoàn thành chuyến xe.

### 4.2. Diễn tiến qua năm ca

| Ca | Nội dung cơ chế | Diễn tiến truyện |
|---|---|---|
| 1 — Học việc | Vé, tuần tra và một dấu hiệu dễ xác minh. | Quy trình công ty bắt đầu có những điều bất thường. |
| 2 — Ghế trống | UV và giới hạn vị trí cách ly. | Một hành khách biết tên người tiền nhiệm. |
| 3 — Sai tuyến | Máy đo nhịp, hỏng nguồn và quản lý điện. | Bộ đàm phát hướng dẫn mâu thuẫn với tài liệu đã xác thực. |
| 4 — Người quen | Phối hợp công cụ, thực thể đổi chỗ. | Một gương mặt xuất hiện lại dù đã được bàn giao trước đó. |
| 5 — Trạm cuối | Vận dụng toàn bộ hệ thống đã học. | Người chơi quyết định kết thúc tuyến theo lệnh hay theo bằng chứng thu thập được. |

Kết thúc phụ thuộc vào số người được bảo vệ, bằng chứng cốt truyện đã tìm thấy và lựa chọn tại trạm cuối. Không yêu cầu một lượt chơi hoàn hảo mới được tiếp cận phần kết chính.

### 4.3. Nguyên tắc tạo không khí

- **Cô lập:** Ngoài cửa kính chủ yếu là bóng tối; trạm dừng hiếm người và thiếu ánh sáng.
- **Lặp lại có biến đổi:** Một biển quảng cáo xuất hiện lần nữa nhưng đổi khuôn mặt.
- **Nỗi sợ nghề nghiệp:** Khiếu nại, doanh thu và nhiệm vụ bảo dưỡng vẫn tồn tại giữa các hiện tượng siêu nhiên.
- **Bất an có khoảng nghỉ:** Sau một sự kiện lớn cần có đoạn đường yên tĩnh để người chơi phục hồi và tự nghi ngờ.
- **Thông tin truyện có thể không đáng tin:** Nhưng quy tắc vận hành bắt buộc phải có cách xác thực rõ ràng, chẳng hạn mã bản tin và dấu UV.

### 4.4. Nguồn cảm hứng

Các tác phẩm tham chiếu: *Shift at Midnight*, *The Mandela Catalogue*, *The Mortuary Assistant*, *Not Tonight* và *Papers, Please*.

Các nguồn cảm hứng được chuyển thành mục tiêu thiết kế: áp lực quy trình hành chính, kiểm tra từng trường hợp, công việc lặp lại bị xâm nhập bởi hiện tượng kinh dị và cảm giác con người bị bắt chước sai lệch. Thế giới, thực thể và hình tượng nhận diện cần được xây dựng riêng.

---

## 5. Art & Audio Direction — Định hướng hình ảnh & Âm thanh

### 5.1. Định hướng hình ảnh

**Đề xuất:** Pixel art cho pha top-down, tranh 2D độ phân giải vừa cho màn hình kiểm tra.

| Thành phần | Hướng thể hiện |
|---|---|
| Nội thất xe | Xanh lục xỉn, vàng đèn cũ, ghế sờn và cửa kính bám hơi nước. |
| Nhân vật top-down | Silhouette rõ, trang phục dễ phân biệt; ít khung hình chuyển động. |
| Chân dung kiểm tra | Ánh sáng yếu nhưng mắt, tay và giấy tờ đủ rõ để suy luận. |
| Dị thường | Chuyển động lệch nhịp, cấu trúc sai nhẹ, phản chiếu bất tuân; hạn chế biến dạng lớn ngay từ đầu. |
| Giao diện | Thiết bị cũ, màn hình đơn sắc, nút cơ học và chữ rõ ràng. |
| Ngoại cảnh | Các lớp rừng, cột điện, trạm chờ và biển báo cuộn theo chuyển động xe. |

Ưu tiên đọc được thông tin trước hiệu ứng kinh dị. Nhiễu CRT, scanline và quang sai chỉ nên mạnh ở màn hình thiết bị hoặc đoạn dựng truyện; có tùy chọn giảm hiệu ứng.

Để giảm khối lượng sản xuất, mỗi nhóm thực thể dùng chung bộ cơ thể với người thường và thay một số lớp mắt, tay, miệng hoặc phản chiếu.

### 5.2. Định hướng âm thanh

Thiết kế âm thanh chia thành ba lớp:

1. **Nền ổn định:** Động cơ, lốp trên đường ướt, mưa và rung thân xe.
2. **Thông tin vận hành:** Chuông dừng, relay điện, cửa khí nén, máy quét và bộ đàm.
3. **Dị thường:** Tiếng thở ở ghế trống, lời nói lặp, giọng có hai lớp lệch nhau, âm thanh phát từ vị trí không đúng.

Âm thanh cảnh báo phải có vị trí tương đối rõ: trước, giữa hoặc sau xe. Khi chuyển sang màn hình kiểm tra, vẫn giữ tiếng động của khoang xe để người chơi nhận biết nguy cơ bên ngoài.

Không dùng nhạc dồn dập ngay khi thực thể xuất hiện, vì sẽ làm lộ đáp án. Nhạc chỉ tăng cường độ khi mối đe dọa đã bước vào giai đoạn công khai.

### 5.3. Khả năng tiếp cận

- Phụ đề cho bộ đàm và lời hành khách.
- Chỉ báo trực quan cho âm thanh cảnh báo quan trọng.
- Không dùng màu sắc làm cách phân biệt duy nhất.
- Điều chỉnh độ rung màn hình, nhấp nháy, nhiễu và âm thanh đột ngột.
- Chế độ hỗ trợ kéo dài thời gian phản ứng mà giữ nguyên quy tắc suy luận.

### 5.4. Mốc prototype và tiêu chí đánh giá

**Prototype đầu tiên nên có:**

- Một chặng chơi 10–15 phút. Đây là chặng thử nghiệm mở rộng để học cơ chế; nhịp chặng mục tiêu của bản đầy đủ vẫn là 4–6 phút.
- Bốn hành khách, gồm người thật có biểu hiện đáng ngờ và tối đa một thực thể.
- Hai công cụ: UV và gương.
- Một sự cố mất điện.
- Một quy trình cách ly và một tình huống tấn công.
- Báo cáo cuối chặng giải thích bằng chứng đã bỏ sót.

**Tiêu chí kiểm chứng:**

- Người chơi hiểu vì sao một quyết định đúng hoặc sai sau khi xem báo cáo.
- Mỗi công cụ tạo ra thông tin có ích, không chỉ kéo dài thao tác.
- Người chơi có lý do thực tế để chuyển qua lại giữa hai góc nhìn.
- Một sai sót vẫn để lại cơ hội ứng phó.
- Cảm giác căng thẳng xuất hiện ngay cả khi chưa có jumpscare.

Nếu vòng lặp này chưa hiệu quả, cần điều chỉnh chi phí kiểm tra, nhịp sự cố và chất lượng bằng chứng trước khi mở rộng số ca hoặc số thực thể.
