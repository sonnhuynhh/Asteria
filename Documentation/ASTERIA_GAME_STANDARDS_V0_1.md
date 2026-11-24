# Asteria — Bộ quy chuẩn toàn game v0.1

**Trạng thái:** chuẩn khởi đầu cho cả nhóm; thông số cân bằng có thể chỉnh sau playtest.  
**Ngày:** 25-11-2026  
**Unity:** 6000.5.9f1  
**Ưu tiên:** hoàn thiện Chiến Dịch online trước, có co-op và khu PvPvE; sau đó mở rộng Dungeon và Đấu Trường.

## 1. Cách đọc và thứ tự ưu tiên

Quy tắc có nhãn **BẮT BUỘC** phải giữ để code, art và map ghép được với nhau. Giá trị **MẶC ĐỊNH** là điểm xuất phát có thể đổi khi có kết quả test. Thay đổi PPU, kích thước nhân vật, hitbox, damage, tốc độ, luật mất đồ hoặc số người chơi phải ghi lý do và tình huống kiểm thử; không sửa một thông số rải rác trong nhiều scene/script.

Thứ tự áp dụng khi tài liệu cũ mâu thuẫn:

1. Bộ quy chuẩn này.
2. Game design và architecture hiện hành.
3. Tài liệu riêng của map, nhân vật, skill hoặc item.
4. Giá trị tạm trong Inspector/script.

Tài liệu này thay thế giả định “bản đầu offline một người” trong tài liệu trước: Chiến Dịch phải được xây với online ngay từ kiến trúc. Cốt truyện, asset NRO, loot và extraction cũ vẫn giữ nếu không xung đột với multiplayer.

## 2. Bản sắc và luật chế độ

Asteria là RPG hành động 2D nhìn ngang, dùng võ thuật, HP/KI và kỹ năng có thể trang bị. Vòng chơi trọng tâm: chuẩn bị ở Trạm → vào vùng nguy hiểm → chiến đấu/tìm loot → chọn rút lui hay đi sâu.

| Khu vực/chế độ | Người chơi ban đầu | PvP | Luật |
|---|---:|---|---|
| Hub/Trạm | 1–8 | Tắt | Khu an toàn: không gây damage, không rơi item; chỉnh loadout, kho, shop và tổ đội. |
| Tutorial và hai operation đầu | 1–4 | Tắt | Dạy di chuyển, combat, loot và extraction; không bị phục kích bởi người chơi. |
| Chiến Dịch co-op | 1–4 mỗi tổ đội | Tắt | Cùng đánh quái/mục tiêu; không có sát thương đồng đội. |
| Chiến Dịch Contested | Tối đa 8, hai tổ đội tối đa 4 người | Bật theo operation | PvPvE có cảnh báo trước khi triển khai. Chỉ đối thủ khác tổ đội gây PvP; không bật free-for-all ở bản đầu. |
| Dungeon thám hiểm | 1–4 | Tắt | Co-op đánh phòng/quái/boss; phần thưởng theo clear, không mất loadout raid. |
| Đấu Trường | 1v1 trước, 2v2 sau | Bật | Không rơi đồ; chỉ số được chuẩn hóa theo luật trận. |

Mỗi operation phải khai báo rõ PvP bật/tắt, số người tối đa, power bracket, loot policy, điều kiện extraction và penalty. Không suy ra luật từ tên scene hoặc script.

### Luật Chiến Dịch

- Rút lui thành công: loot đang mang của người đã rút được chuyển vào kho.
- HP về 0 hoặc hết giờ: raid thất bại; mất đồ đang mang và loadout được đánh dấu rủi ro. Kho, tiến trình truyện, nhiệm vụ đã mở và unlock tài khoản vẫn được giữ.
- Safe slots mặc định 2 ô; giữ item qua thất bại nhưng không dùng được trong combat.
- Extraction theo từng người: giữ tương tác 3 giây; rời vùng hoặc bị đánh trúng làm tiến độ về 0. Server quyết định kết quả.
- Đồng đội có thể rút riêng; người còn ở lại tiếp tục raid. Không tự động kéo cả tổ đội ra ngoài.
- PvP chỉ có ở operation mang cờ Contested. Điểm xuất hiện được bảo hộ 5 giây; chủ động gây damage sẽ kết thúc bảo hộ.
- HP về 0 là bị hạ ngay trong bản đầu; hồi sinh giữa trận để sau khi đã test vòng loot online.

## 3. Chuẩn x4, PPU và độ nét

**x4 là nguồn hình ảnh chính cho nhân vật, map, hiệu ứng và vật phẩm.**

| Nguồn ảnh | PPU | Quy đổi |
|---|---:|---|
| Ảnh x4 nguyên bản | **96** | 96 pixel nguồn = 1 Unity unit |
| Ảnh x1 tương ứng khi thiếu bản x4 | 24 | 24 pixel nguồn = 1 Unity unit |

Cặp PPU 96/24 giữ nguyên kích thước thế giới vì ảnh x4 có số pixel gấp bốn. Tile x4 96×96 px ở PPU 96 và tile x1 24×24 px ở PPU 24 đều chiếm 1×1 unit. Không đổi PPU để phóng riêng nhân vật; hãy sửa canvas/pivot/import metadata có kiểm soát.

Project hiện có các PPU 24 và 96 phù hợp cặp trên. Một số sprite đang có metadata PPU 100, 128 hoặc 160; cần phân loại chúng là world art hay UI trước khi chuẩn hóa, không batch sửa toàn bộ vì có thể làm lệch reference cũ.

### Camera và import

- Grid cell Tilemap: 1×1 unit.
- Camera mẫu: 16:9, Orthographic Size **5.625**, khung nhìn cao 11.25 unit và rộng 20 unit.
- Màn hình tham chiếu duyệt art: 1920×1080. Kiểm tra 16:10/ultrawide; mở rộng ngang, không cắt gameplay theo chiều dọc.
- Pixel art dùng Filter Mode Point; tắt mipmap và texture compression cho sprite gameplay; giữ alpha và kích thước texture tối thiểu bằng nguồn x4.
- Sprite sheet nhiều frame dùng Multiple và slicing theo manifest. PNG của một bộ phận riêng dùng Single.
- Pivot mặc định nhân vật là bottom-center tại chân. Không dùng Bilinear/Trilinear, không scale bằng số lẻ tùy tiện, không áp Pixel Perfect Camera ở PPU 24 cho sprite x4 PPU 96.

### Kích thước nhân vật

- Player mặc định cao **1.5 unit**, rộng hình khoảng **0.75 unit** sau khi ghép.
- Collider đứng mặc định **0.72×1.2 unit**, tâm collider lệch lên 0.6 unit so với root tại chân.
- Root đặt ở giữa hai chân trên mặt đất; SpriteRenderer/parts là con của root. Collider và hitbox không đổi theo frame hình vẽ.
- Quái nhỏ: 0.75–1.0 unit; quái thường/người: 1.0–1.5; elite: 1.75–2.25; boss: 3–4.5. Boss lớn hơn phải có lý do gameplay/camera.
- So sánh bằng kích thước sau khi render trong Game View, không bằng kích thước PNG.

## 4. Di chuyển và vật lý

Đưa các giá trị vào PlayerMovementProfile hoặc cấu hình dùng chung.

| Thông số | Mặc định |
|---|---:|
| Move speed ngang | 5.5 unit/giây |
| Jump velocity | 10.5 unit/giây |
| Physics2D gravity | −9.81 |
| Rigidbody2D gravity scale | 3 |
| Fixed timestep | 0.02 giây |
| Coyote time | 0.10 giây |
| Jump buffer | 0.12 giây |

Độ cao nhảy lý thuyết khoảng 1.87 unit, thời gian trên không khoảng 0.71 giây. Map chừa biên an toàn cho va chạm và thao tác không chính xác:

- Khe bắt buộc để qua: tối đa 2.0 unit.
- Khe khó/đường thưởng: tối đa 2.5 unit; không yêu cầu skill mở khóa nếu không có đường thay thế.
- Bậc bắt buộc: cao tối đa 1.2 unit; bậc nâng cao tối đa 1.6 unit.
- Đọc input ở Update, áp vận tốc/lực trong FixedUpdate. Chỉ một script điều khiển Rigidbody2D của Player.
- Bật Freeze Rotation Z; GroundCheck và collision layer đặt trong prefab, không kéo lại tùy tiện theo scene.
- Không dùng transform scale của Player để sửa tốc độ hoặc collider.

## 5. Nhân vật modular và animation

### Ghép part

Mỗi nhân vật có Character ID cố định và manifest cho từng action/frame. Manifest phải ghi action, frame index, ID đầu/thân/chân/tay, offset X/Y, pivot, sorting order và thời lượng. Chỉ ghép part cùng nhân vật, cùng hướng và cùng tư thế.

Render từ sau ra trước: chân/tay sau → thân → chân trước → đầu → tay trước → vũ khí/đạo cụ → VFX trước. Tách Sorting Layer của nền, nhân vật, foreground, VFX và UI. Các part dùng cùng root/pivot; không căn từng frame bằng mắt.

Không ghép đầu nhìn thẳng vào thân đang quay nghiêng nếu không có pose manifest tương thích. Khi asset nguồn thiếu dữ liệu ghép, lưu ảnh composite kiểm tra và offset trước khi tạo clip.

### Bộ trạng thái tối thiểu

| Trạng thái | Số frame đề xuất | Tốc độ |
|---|---:|---:|
| Idle | 4–6 | 6–8 fps, loop |
| Run | 6–8 | 10–12 fps, loop |
| Jump Rise | 3–5 | 10–12 fps |
| Fall | 1–3 | giữ frame cuối hoặc loop chậm |
| Land | 2–3 | 10–12 fps |
| Melee Attack | 4–6 | 12–15 fps |
| Skill/Cast | 6–12 | 10–15 fps |
| Hit | 2–3 | 10–12 fps |
| Guard/Block | 2–4 | 8–10 fps |
| Dash/Teleport | 3–5 | 12–15 fps |
| Death | 5–8 | 8–10 fps, không loop |
| Extract/Interact | 3–6 | 8–10 fps |

Clip giữ cùng canvas, PPU, pivot và baseline chân để không nhảy kích thước khi đổi state. Đổi hướng bằng flipX trên visual root hoặc bộ part trái/phải đã chuẩn bị; không flip collider riêng.

Animation chỉ trình bày hành động. Cast, hit window, cooldown, KI và damage nằm trong SkillDefinition/CombatDefinition để server xác nhận. Animation Event chỉ dùng cho VFX, âm thanh và hiệu ứng hình; không dùng client event làm nguồn damage.

Clip hoàn thành khi ghép đủ body, silhouette không tách khớp, chân không trượt quá 2 pixel x4 khi chạy tại chỗ, attack có startup-active-recovery rõ và hình không lệch khỏi collider.

## 6. Combat và cân bằng

### Chỉ số và giá trị đầu

Chỉ dùng stat có ý nghĩa và có UI giải thích: HP, KI, Attack, Defense, Critical Chance, Attack Speed, Move Speed. Power là số tham khảo/điều kiện nội dung, không tăng riêng.

| Thông số | Mặc định |
|---|---:|
| Player HP / KI đầu trận | 100 / 100 |
| Basic melee damage | 20 |
| Tầm melee | 0.85 unit |
| Khoảng cách giữa đòn melee | 0.55 giây |
| Quái thường | khoảng 100 HP, cần 4–7 đòn cơ bản |
| Damage chạm của quái thường | khoảng 10 |
| Hồi KI ngoài lúc cast | 3/giây |

Skill mặc định nằm trong khoảng 1.5–2.5 lần damage đòn cơ bản hoặc đổi sang phòng thủ/di chuyển. Projectile có thể hụt và tốn KI; beam có charge, recovery và cửa sổ ngắt; khiên có thời lượng; dash không gây damage miễn phí. Số cân bằng nằm trong ScriptableObject, không hard-code trong UI/animation.

Quái thường có 1–2 pattern dễ đọc. Elite có telegraph và lựa chọn chiến thuật. Boss có 2–3 hành vi/phase; không tăng độ khó chỉ bằng cách nhân HP.

### DamageResolver

- Mitigation = min(0.35, Defense / (Defense + 100)).
- Damage cuối = làm tròn xuống, tối thiểu 1: (Attack × SkillMultiplier + FlatBonus) × CritMultiplier × (1 − Mitigation) × ModeMultiplier.
- CritMultiplier mặc định 1.5; tỷ lệ chí mạng tối đa 30%.
- Bonus Move Speed từ gear tối đa +15%; Attack Speed tối đa +20%; giảm damage tối đa 35%.
- Chỉ DamageResolver áp công thức và phát HP change; không để từng skill tự trừ máu.

PvP dùng balance profile riêng. Arena chuẩn hóa HP, chỉ số gear và power; item tạo lối chơi nhưng không làm tổng ngân sách sức mạnh lệch quá 10%. Contested Raid ghép trong power bracket có chênh lệch mục tiêu không quá 15%. PvP damage multiplier mặc định 0.60; một hit không lấy quá 25% HP tối đa của đối thủ đang đầy máu. Mục tiêu test duel ngang sức kéo dài 8–15 giây sau hit đầu, chưa tính thời gian tìm/tiếp cận.

## 7. Gear, inventory, rarity và loot

Slot gear đầu tiên: Găng, Áo, Quần, Giày, Rada/Utility, Balo và tối đa 3 Skill Seal. Vệ tinh hỗ trợ để sau khi vòng raid ổn định. Đòn đánh tay cơ bản luôn dùng được; skill chỉ dùng khi Skill Seal nằm ở slot hoạt động.

Item loại riêng: Gear, Skill Seal, Consumable, Material, Valuable, Collectible và Quest Item. ItemDefinition là mẫu bất biến; đồ người chơi sở hữu là instance có ID riêng. Không sửa định nghĩa mẫu khi nâng cấp một item instance.

Mặc định ban đầu: 10 ô balo raid, 2 safe slots, kho có capacity rõ ràng. UI phải cho biết item nào mất khi thất bại, item nào được bảo vệ và item nào đang mặc.

| Tier | Màu | Tên | Luật |
|---:|---|---|---|
| I | Xám | Thường | Không affix hiếm |
| II | Xanh lá | Không thường | Một option nhỏ hoặc gói vật liệu |
| III | Xanh dương | Hiếm | Một option rõ, hợp build |
| IV | Tím | Sử thi | Một option mạnh hoặc hai option nhỏ |
| V | Vàng | Huyền thoại | Hiệu ứng build đặc trưng, có cap stat |
| VI | Đỏ | Di vật | Boss/secret reward, nguồn rơi xác định |

Rarity không đồng nghĩa power requirement, level, giá bán hoặc chỉ số NRO gốc. Không thêm random affix cho mọi item ở vertical slice; dùng mẫu chỉ số cố định trước.

### Odds bắt đầu để playtest

Các tỷ lệ là một lần roll của rương/elite/boss, không phải từng lần quái thường chết.

| Khu | I | II | III | IV | V | VI |
|---|---:|---:|---:|---:|---:|---:|
| Tutorial/đồng cỏ | 70% | 25% | 5% | 0% | 0% | 0% |
| Chiến dịch thấp | 55% | 28% | 13% | 4% | 0% | 0% |
| Hang/dungeon | 40% | 30% | 18% | 9% | 2% | 1% |
| Doanh Trại | 25% | 27% | 23% | 15% | 8% | 2% |
| Boss/rương bí mật cuối | 15% | 22% | 25% | 20% | 13% | 5% |

Quái thường chủ yếu rơi 0–1 vật liệu. Elite có một roll giá trị với giới hạn theo vùng. Boss cho objective reward chắc chắn cộng hai roll loot. Rarity VI chỉ ở nội dung nguy hiểm/secret reward. Không bán collectible hoặc skill seal chỉ bằng một click nếu có nguy cơ mất item.

## 8. Chuẩn map

Map dùng Grid 1×1 unit, trục XY; Z chỉ dùng sorting. Tile collision tách khỏi ảnh trang trí. Mỗi map có Map ID, Operation ID, kích thước grid, tile/theme, entry/exit, spawn, objective, container, enemy group, PvP rule và loot table.

| Loại blockout | Kích thước ban đầu |
|---|---|
| Hub/Trạm | 32×20 cells |
| Tutorial | 52×24 cells |
| Operation thường | 64–96×20–24 cells |
| Phòng dungeon | 24×16 cells, nhiều phòng nối bằng cửa/waypoint |

Kích thước là khung thiết kế, không ép kéo giãn map nguồn. Khi tái dựng map, giữ grid/waypoint; ghi rõ extraction, container, hazard hoặc cửa nào do Asteria bổ sung.

Layer map: Background Far, Background Mid, Ground Visual, Ground Collision, Props, Foreground, Interactables, Spawn/Objective Markers. Ground Collision không dùng sprite trong suốt thay luật va chạm. Dùng TilemapCollider2D với CompositeCollider2D khi phù hợp.

Spawn không đặt trong collider/hazard hoặc sát tầm đánh đối thủ. Trong Contested, hai đội xuất hiện cách nhau ít nhất 8 unit, không nhìn thẳng thấy spawn đối phương; extraction không đặt sát spawn. Marker, điều kiện mở và HUD phải cùng đọc một OperationDefinition.

## 9. Multiplayer và dữ liệu online

### Hướng kỹ thuật

Project hiện dùng GameObject/MonoBehaviour và quy mô tối đa dự kiến 8 người/map. Baseline MVP: Netcode for GameObjects cho đồng bộ object và dịch vụ session/lobby tương thích. PvP phát hành phải dùng server authoritative/dedicated server; host mode chỉ để thử nội bộ, không được xem là chống gian lận.

Server là nguồn sự thật cho vị trí hợp lệ, va chạm, HP/KI, damage, cooldown, hit result, loot roll, pickup ownership, extraction, death penalty, stash và giao dịch. Client gửi input/ý định; không gửi kết quả damage, tiền, vị trí tùy ý hoặc loot result để server tin thẳng.

| Hạng mục | Chuẩn |
|---|---|
| Tổ đội co-op | 1–4 người |
| Operation Contested | 2 đội, tối đa 4 người/đội, tổng 8 |
| Network simulation target | 30 tick/giây |
| Unity physics local/server | Fixed timestep 0.02 giây; đồng bộ theo tick mạng |
| Client render | mục tiêu 60 FPS |
| Movement | client có thể dự đoán; server kiểm tra và sửa sai |
| Remote players | interpolation; client không sở hữu remote transform |
| PvP hit | server xác nhận team, hitbox, range, cooldown, trạng thái mục tiêu |
| Loot/economy | server transaction ID; một pickup chỉ cấp một lần |

Unity mô tả Netcode for GameObjects phù hợp game GameObject với số người nhỏ; competitive quickstart dùng client gửi input và server tính movement để giữ server authority. Đây là baseline phù hợp quy mô hiện tại; đánh giá lại nếu số người chơi hoặc yêu cầu cạnh tranh tăng mạnh.

### Điều kiện test mạng

- Chạy ít nhất 2 client và 1 server trong một operation; sau đó test đủ 4 người co-op và 8 người Contested.
- Mô phỏng RTT 50/100/150 ms, packet loss 0/2/5%; ghi độ trễ hit, sai lệch vị trí, disconnect và duplicate loot.
- Mục tiêu ban đầu: PvP đọc được ở RTT ≤150 ms; nếu không đạt, giảm nhịp/tầm skill hoặc cải thiện server validation trước khi mở rộng.
- Không xác nhận PvP cạnh tranh trên listen host. Test cuối phải có dedicated server.
- Reconnect, timeout, xử lý người rời trận và hoàn item khi server lỗi phải có luật trước khi mở live economy.

## 10. Dữ liệu, code và tệp

Asset gameplay mới tổ chức dưới Assets/Game; không di chuyển file cũ cho đến khi kiểm tra GUID/reference.

- Assets/Game/Scripts: Core, Networking, Player, Combat, Campaign, Inventory, Items, Economy, Maps, UI.
- Assets/Game/Data: Characters, Enemies, Skills, Items, LootTables, Maps, Operations.
- Assets/Game/Prefabs: Characters, Enemies, Items, Interactables, UI.
- Assets/Game/Animations: Character ID rồi đến state.
- Assets/Game/Art: Characters, Maps, Effects, UI; giữ x4 nguyên gốc và metadata import.
- Assets/Game/Scenes: Bootstrap, Hub, Campaign, Dungeon, Arena.

ScriptableObject chứa định nghĩa tĩnh; runtime model chứa trạng thái trận; profile lưu ở backend. Dùng ID ổn định, không dùng tên hiển thị làm khóa. Không để xác suất loot, damage hay luật mất đồ rải rác trong scene/script.

Quy ước tên: CHR_<id>, ENM_<id>, MAP_<id>, OP_<id>, SKL_<id>, ITEM_<id>, FX_<id>, UI_<id>. Animation: CHR_<id>_<State>; prefab: PFB_<id>; dữ liệu: DEF_<id>. Giữ nguyên .meta/GUID sau khi đã tham chiếu.

Repo theo Unity 6000.5.9f1; commit Packages/manifest.json, packages-lock.json, ProjectSettings và .meta. Không commit Library, Temp, Obj, Logs, UserSettings, build output hoặc credential/API key. Dùng Git LFS theo .gitattributes hiện có cho ảnh/âm thanh/asset nhị phân.

## 11. UI và ngôn ngữ

HUD chiến dịch luôn hiển thị HP, KI, skill slot/chi phí/cooldown, objective, raid timer, số ô balo, trạng thái extraction và cảnh báo PvP. Item thể hiện loại, rarity bằng màu lẫn chữ/icon, giá, power requirement và luật mất/giữ.

Không chỉ dùng màu để truyền rarity/trạng thái. UI hỗ trợ bàn phím, focus/tooltip rõ; chữ không che nhân vật hoặc telegraph. Tiếng Việt là ngôn ngữ thiết kế ban đầu; text đặt trong bảng localization, không ghép trực tiếp trong gameplay code.

## 12. Definition of Done

### Nhân vật/animation

- Source x4 PPU96; frame có ID/offset/pivot; nhân vật sau khi ghép cao mục tiêu 1.5 unit.
- Run/jump/attack không đổi kích thước root; collider không dao động theo hình vẽ.
- Không tách khớp hoặc ghép sai hướng đầu/thân; state cần thiết có preview sheet.

### Map

- Kích thước và điểm gameplay đọc theo grid; có collision, route, spawn, objective và exit khai báo.
- Cú nhảy bắt buộc nằm trong chuẩn; không spawn trong hazard/collider.
- Người khác có thể đọc MapDefinition và hiểu asset, grid, điểm đặt và luật operation.

### Combat/network

- Cùng input ở server tạo cùng kết quả; client không tự cấp loot/damage.
- Hai client thấy cùng HP, hit, death, loot ownership và extraction result.
- PvP tuân team rule, spawn protection, power bracket và damage cap; test disconnect.

### Economy

- Extract/fail commit đúng một lần; không duplicate khi spam pickup, reconnect hoặc load scene.
- Kết quả raid ghi item giữ/mất và lý do.
- Loot table weighted roll có tổng 100%; item rarity cao có source/zone rõ.

## 13. Lộ trình áp dụng

1. **Asset scale audit:** lập danh sách sprite world PPU96, tìm ngoại lệ 100/128/160, chốt nhân vật mẫu và kiểm tra tại 1920×1080.
2. **Network spike sớm:** 2 client + server, đồng bộ đi/chạy/nhảy/đánh/HP; chưa làm nhiều map trước khi mạng chạy.
3. **Online Campaign co-op:** tutorial, quái, loot, extraction và stash; server giữ quyền kết quả.
4. **Contested PvPvE:** bật operation flag, 8 người/2 đội, test spawn, protection, hit validation và loss.
5. **Vertical slice art:** thay blockout bằng x4 nhân vật/map/VFX; giữ physical dimensions và collision.
6. **Arena 1v1:** PvP normalization, không mất item; mở 2v2 sau khi 1v1 đạt mục tiêu TTK.
7. **Dungeon/boss/economy:** tiếp tục dùng cùng PPU, prefab/data contract và network ownership.

### Changelog

- v0.1 — Chốt x4 làm nguồn hình ảnh chính; x4 PPU96, x1 fallback PPU24; đặt mục tiêu Chiến Dịch online co-op/PvPvE và server authoritative.

### Tài liệu kỹ thuật tham khảo

- [Unity netcode packages](https://docs.unity.com/en-us/multiplayer/netcode/netcode)
- [Unity competitive multiplayer quickstart](https://docs.unity.com/en-us/multiplayer/quickstarts/competitive-quickstart)
