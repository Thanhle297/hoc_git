# Ke hoach xay dung he thong Thuong mai dien tu

## 1) Tong hop stack cong nghe da chot

- Backend: `ASP.NET Core MVC` tren `.NET 8`
- View Engine: `Razor Views (.cshtml)`
- Frontend UI: `Tailwind CSS` (framework chinh)
- JavaScript: `jQuery` (giu lai cho cac tac vu can tuong thich MVC)
- Validation phia client: `jquery-validation` + `jquery-validation-unobtrusive`
- Database: `SQL Server 2022` (khuyen nghi su dung collation `Vietnamese_CI_AS` hoac `SQL_Latin1_General_CP1_CI_AS` tuy yeu cau tim kiem)
- ORM/Data access: `Entity Framework Core` + `Microsoft.EntityFrameworkCore.SqlServer`
- Kien truc trien khai: `MVC + Service layer` (Controller mong, business logic dat o Service)

---

## 2) Muc tieu du an

- Xay dung website thuong mai dien tu chuyen ban do dien tu day du tinh nang cho khach hang va quan tri.
- Uu tien chat luong kien truc, bao mat, hieu nang va kha nang mo rong.
- Trien khai theo lo trinh giai doan de de nghiem thu tung phan.

---

## 3) Ke hoach trien khai chi tiet

### Giai doan 0: Khoi dong va chot yeu cau (Tuan 1)

- Chot pham vi nghiep vu: san pham, bien the, ton kho, van chuyen, thanh toan, coupon, review, bao hanh.
- Chot vai tro nguoi dung: `Guest`, `Customer`, `Admin`, `Staff`.
- Chot luong nghiep vu chinh: dang ky/dang nhap, mua hang, thanh toan, xu ly don, huy/hoan.
- Chot KPI: toc do tai trang, conversion, ty le loi checkout.

**Deliverables**
- Tai lieu BRD ngan gon.
- Flowchart cac luong chinh.
- Product backlog theo muc uu tien `P0/P1/P2`.

### Giai doan 1: Nen tang ky thuat va kien truc (Tuan 2-3)

- Chuan hoa cau truc source:
  - `Controllers/`
  - `Models/Entities`, `Models/ViewModels`
  - `Services/Interfaces`, `Services/Implementations`
  - `Data/`
- Cau hinh SQL Server + EF Core + migration strategy.
- Tich hop ASP.NET Core Identity + phan quyen role-based.
- Cau hinh logging, global exception handling, validation pipeline.

**Deliverables**
- Kien truc source on dinh.
- Migration dau tien chay thanh cong.
- Dang nhap/phan quyen co ban hoat dong.

### Giai doan 2: Thiet ke du lieu e-commerce hoan chinh (Tuan 3-4)

- Implement schema:
  - `Users`, `Roles`, `Addresses`
  - `Categories`, `Brands`, `Products`, `ProductVariants`, `ProductImages`, `ProductSpecifications`
  - `Inventory`, `InventoryTransactions`
  - `Carts`, `CartItems`
  - `Orders`, `OrderItems`, `OrderStatusHistories`
  - `Payments`, `PaymentTransactions`
  - `Coupons`, `CouponUsages`
  - `Reviews`, `Wishlists`
- Them audit fields: `CreatedAt`, `UpdatedAt`, `CreatedBy`, `IsDeleted`.
- Toi uu index cho cot hay filter/search/sort.

**Deliverables**
- ERD chinh thuc.
- Bo migration day du.
- Tai lieu quy tac data integrity.

### Giai doan 3: Frontend foundation (Razor + Tailwind) (Tuan 4-5)

- Chuyen dong bo giao dien theo `Razor + Tailwind`.
- Tao design system: mau, spacing, typography, button/input/card/table.
- Tao reusable partials/components:
  - Header, footer, navbar
  - Breadcrumb
  - Product card
  - Filter panel
  - Pagination
- Dam bao responsive mobile-first cho catalog/cart/checkout.

**Deliverables**
- UI foundation hoan chinh.
- Layout chung + component tai su dung.

### Giai doan 4: Catalog va tim kiem (Tuan 5-7)

- Xay trang danh muc va chi tiet san pham:
  - Loc theo category/brand/price/specs
  - Sort + pagination
  - Hien thi ton kho theo bien the
- Search san pham theo tu khoa + goi y.
- SEO on-page cho product/category (slug/meta/canonical).

**Deliverables**
- Catalog hoat dong day du.
- Luong browse/search toi uu UX.

### Giai doan 5: Gio hang va Checkout (Tuan 7-8)

- Gio hang cho guest va customer.
- Merge cart sau dang nhap.
- Checkout nhieu buoc:
  - Thong tin nguoi nhan
  - Van chuyen
  - Xac nhan don
- Tinh gia day du: subtotal, shipping, discount, total.

**Deliverables**
- Luong dat hang hoan chinh tu gio.
- Validation day du phia server.

### Giai doan 6: Thanh toan va vong doi don hang (Tuan 8-10)

- Tich hop `COD` + 1 cong thanh toan online (`VNPay` hoac `MoMo`).
- Xu ly callback/webhook an toan, idempotent.
- Quan ly trang thai don:
  - `Pending`
  - `AwaitingPayment`
  - `Paid`
  - `Processing`
  - `Shipping`
  - `Completed`
  - `Cancelled`
  - `Refunded`
- Dong bo thanh toan voi ton kho va lich su don.

**Deliverables**
- Thanh toan online production-ready.
- Order lifecycle day du.

### Giai doan 7: Admin CMS va van hanh noi bo (Tuan 10-12)

- Dashboard: doanh thu, don hang, top san pham, ton kho thap.
- CRUD quan tri:
  - Products, categories, brands, specifications
  - Coupons
  - Users va permissions
- Quan ly don nang cao: xac nhan, cap nhat van chuyen, huy/hoan theo policy.

**Deliverables**
- Admin panel day du cho van hanh thuc te.

### Giai doan 8: Chat luong, bao mat, toi uu (Tuan 12-13)

- Test strategy:
  - Unit test cho service nghiep vu chinh
  - Integration test cho checkout/payment callback
- Bao mat:
  - Chong CSRF, XSS, input validation
  - Authorization policy theo role
  - Quan ly secrets theo moi truong
- Hieu nang:
  - Query tuning + index tuning
  - Image optimization + caching

**Deliverables**
- Bao cao test.
- Checklist bao mat/hieu nang dat nguong.

### Giai doan 9: UAT, release, go-live (Tuan 14)

- UAT voi du lieu mo phong gan thuc te.
- Chuan bi deployment:
  - Staging/Production
  - Migration release runbook
  - Backup/restore plan
- Monitoring sau go-live:
  - Error rate
  - Payment success rate
  - Abandoned cart

**Deliverables**
- Release notes.
- Go-live checklist.
- Ke hoach support sau phat hanh.

---

## 4) Backlog theo muc uu tien

### P0 (bat buoc)

- Auth + phan quyen
- Catalog + search/filter
- Cart + checkout
- Payment
- Order management
- Admin products/orders

### P1 (quan trong)

- Coupon nang cao
- Wishlist
- Review/Rating
- Email templates
- Dashboard analytics

### P2 (mo rong)

- Compare san pham
- Recommendation
- Loyalty points
- Multi-warehouse nang cao

---

## 5) Definition of Done (DoD)

- Hoan thanh code theo kien truc `MVC + Service layer`.
- Co migration va du lieu test lien quan.
- `dotnet build` pass.
- Neu co test lien quan: test pass.
- Khong co loi bao mat nghiem trong.
- Co tai lieu ngan: luong + cau hinh + cach kiem thu.

---

## 6) De xuat van hanh ky thuat

- CI/CD toi thieu: restore -> build -> test -> format check.
- Moi truong: Development, Staging, Production tach rieng.
- Log va metric theo module (Auth, Catalog, Checkout, Payment).
- Dinh ky backup DB va thu dien tap restore.

---

## 7) Moc nghiem thu de xuat

- Moc 1 (Het tuan 3): Nen tang, auth, DB migration.
- Moc 2 (Het tuan 7): Catalog + tim kiem + giao dien nguoi dung.
- Moc 3 (Het tuan 10): Cart + checkout + payment + order lifecycle.
- Moc 4 (Het tuan 12): Admin CMS day du.
- Moc 5 (Het tuan 14): UAT xong, san sang go-live.
