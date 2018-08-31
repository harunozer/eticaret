# eticaret
Developer ortamı için

HelperLayer/Consts.cs de ConnectionString düzenlenir ve proje çalıştırılır.

Yapılacaklar kod içine TODO: ile not edildi. (task liste de görünür.)

Mvc Core ve EntityFramework Core ile e-ticaret yapısı.

**DataAccessLayer**

Bütün veritabanı işlemleri burada yapılır. 

**Models / NotMapped** klasöründe Db de tablo karşılığı olmayan sınıflar ve web modül ile iletişim sağlayan (Modeli olmayan LoginForm gibi) model yapıları vardır.

**Models** içinde tablo modelleri bulunur. Validation attirbute tanımlamaları direkt kullanılmaz. ValidationAttributes klasörü içinde mevcut base attirbute den türetilerek oluşturulur. Model de permission yapısı olacaksa sınıfta DataPermissions Permissions özelliği tanımlanır.

**ValidationAttributes** klasöründe custom validation attirbute sınıfları tanımlanır.

**Services** içinde db işlemleri yapılan sınıflar vardır. Bütün servisler BaseService den türetilir. Constructor servislerde override edilir ve PermissionModul de setlenir. Böylece bütün servislerde permission kontrolleri fonksiyonları tanımlanmış olur. Rol bazlı yetkilere ek tanımlamalar yapılacaksa Service sınıfında metod ezilir ve gerekli kontroller yapılır. BaseService constructor da DbContext nesnesi içinde ki CurrentUser da sessiondan setlenir. Standart yapıdaki Add, Update gibi komutlar BaseService de vardır. Değişik yapılarda override edilir. **ProcessBaseModel özelliği false setlenerek işlem yapılırsa UpdateTime, UpdatedBy gibi otomatik hesaplanan değerler hesaplanmaz.**

Servislerde bütün veri okuma işlemlerinde kullanılacak IQueryable veren getBaseQuery metodu override edilir.

Tek kayıt okumada get (int ID) kullanılan fonksiyonda kayıt yoksa NotFoundDataException fırlatılır. Kayıt Permisson sınıfı hesaplanıp gönderilir.

Add metodunda InsertPermission check edilir, yetki hatasında PermissionException üretilir. Veri kontrolleri yapılır (Uniq vs..) uygunluk yoksa ValidationException üretilir. ValidationException Controller da yakalanıp hata gösterilir.

Update metodunda güncellenecek kaydın Permissions.Update i kontrol edilir, kaydın yeni hali için updatePermission kontol edilir, veri validation kontrol edilir ve işlem yapılır. (hata durumlarında ilgili exceptionlar fırlatılır)

Delete silinecek kaydın ID si parametre olarak alınır. get ile kayıt okunur ve Permission.Delete kontrol edilir. CancelID setlenir ve kayıt güncellenir.

BaseServis den türetilen diğer servislerde getBaseQuery ve getValidationError metotları override edilir. Standart permission yapısına ek olarak yetki kontrolleri varsa ilgili permission kontrolü metodu da override edilir. CRUD işlemleri standart yapıda değil ise (CancelService deki gibi) ilgili metot override edilir.

**HelperLayer**

DataLayer ve www projelerinde ortak kullanılan Permission, Exception ve Web metodları tanımlanır. Projede ki sabitler consts.cs de tanımlanır.

**wwwAdmin**

Yönetim paneli sitesidir. Kod içinde yakalanmayan exceptionlar /Error sayfasına yönlendirilir.

**Filters** klasöründe yetki kontrollerinin yapıldığı AuthFilter sınıfı bulunur. BaseController da kullanılır. Bütün controllerda çalışır.

**Models** klasöründe form ve list sayfalarında kullanılan modeller tanımlanır.

Bütün Controller sınıfları BaseController sınıfından türetilir. IDisposible değişkeni vardır. Setlendiğinde request bittikten sonra dispose çalıştırılır ve view da kullanılan modelin dispose metodu çalıştırılmış olur. 

Controller constructor da kullanılacak servisler parametre olarak alınıp readonly değişkenlere atılır. Index metodu List sayfalarıdır. Form metodları Insert ve Update sayfalarıdır. Detay istekleri View, silme istekleri Delete metotlarıdır.

**List Sayfaları:**

List sayfalarında model olarak wwwAdmin.Models de oluşturulan XXXListModel (UserListModel gibi) (BaseListModel den türetilmiş) sınıfı oluşturulup filter listesi hazırlanır. BuildQuery metoduna HttpContext ve Listeyi barındıran IQueryable nesnesi verilir. Base model olarak hazırlanan model setlenir ve view basılır. View sayfasında InsertPermission kontrol edilerek ekleme linki eklenir. Html.RenderPartial ile filter partial view model deki ListProps verilerek oluşturulur. Order yapılabilir kolonlar için aşağıdaki şekilde spanlar eklenir.

<span class="orderspan" colname="ID">X</span>

Order yapılan "span"a asc – desc "class"ları otomatik eklenir.

Listede ki Düzenle – Detay – Sil butonları için modelde ki DataList içinde ki kayıtların Permission sınıflarında ki değerler kontrol edilir.

**Form Sayfaları:** 
Model olarak wwwAdmin.Models.FormModel.BaseFormModel den türetilen XXXFormModel (UserFormModel) kullanılır. İd parametresi gönderilirse FormType setlenir. Insert ve update için ilgili permisson kontrolü yapılır yetki hatası varsa PermissionException üretilir. Model de başka değerler varsa hesaplanır ve view gösterilir.

Form actionda ModelState.IsValid kontrolü yapılır. Hata varsa model değerleri basılır ve view basılır. Insert ise ilgili model direkt servisin add metoduna gönderilir. Permission validation kontrolleri serviste yapılır. Update için ilk önce kayıt tekrar okunur değişen alanlar setlenir ve servisin update metodu çalışırılır. Burada da permission validation kontrolleri serviste yapılır. Validation hatası dönerse setErrorMessage ile hata setlenir. Model tekrar hesaplanır ve view gösterilir. Hata çıkmazsa setSuccessMessage ile mesaj setlenir ve ilgili list sayfası gösterilir.

**Detay Sayfaları:**
View sayfaları model olarak ilgili model sınıfı tanımlanır. İlgili servisin get metodu ile kayıt direkt view e basılır. Yetki kontrolleri servis de kontrol edilir. View sayfasında BaseModel alanları için PartialViewBaseModel render edilerek gösterilir.

**Kayıt Silme:**
Delete metoduna gelen id parametresinin ile ilgili servisin delete metodu çalıştırılarak yapılır. Yetki kontrolleri servis de yapılır. setSuccessMessage setlenir ve liste sayfasına yönlendirilir.
