# Calendar App
Bu projede, kullanıcıların bir olay yani toplantı oluşturabilecekleri, güncelleyebilecekleri ve silebilecekleri bir web platformu geliştirilmiştir. Ayrıca, bir toplantının başlama zamanı geldiğinde, kullanıcılara bildirim sesiyle birlikte toastr mesajı sunulmaktadır.
## Teknoloji Yığını ve Mimarisi
Projede aşağıdaki teknolojiler ve mimariler kullanılmıştır:

- Backend: .NET 5.0 Web API
- Frontend: React ve TUI kütüphanesi
- Mimariler: Onion Architecture
- Design Pattern: Unit of Work (Repository için kullanıldı)
## Kurulum
1. Projeyi klonlayın veya indirin.
git clone https://github.com/ibrahimoguzhany/calendarapp.git
2. "cd" komutuyla frontend klasüründe proje dizinine gidin 
3. npm install komutuyla gerekli paketleri yükleyin.
4. Microsoft Visual Studio ve MS SQL Server yükledikten sonra backend klasöründeki projeyi .sln uzatılı dosyaya tıklayarak açın. API projesindeki appsettings.json içinde ConnectionStrings'i kendi server name'iniz ile değiştirin.
5. Package manager console'u açıp add-migration InitialCreate isminde bir migration oluşturun ve ardından uptade-database komutuyla migration'lari gerçekleştirin.
6. frontend klasörü altındaki projeye "cd" komutuyla gidip "npm start" komutuyla uygulamayı kullanmaya başlayabilirsiniz.
## Kullanım
Kullanıcılar, platforma kayıt olabilir ve oturum açabilirler. Kayıt olmak için gereken bilgiler kullanıcı adı, şifre, TC kimlik no, telefon, email ve adres bilgileridir. Kayıt olduktan sonra, kullanıcılar bir olay yani toplantı oluşturabilir, güncelleyebilir ve silebilirler. Ayrıca, bir toplantının başlama zamanı geldiğinde, kullanıcılara bildirim sesiyle birlikte toastr mesajı sunulur.
## Katkıda Bulunma
Katkıda bulunmak isteyenler için adımlar:

Fork'layın (https://github.com/ibrahimoguzhany/calendarapp/fork)
Feature branch oluşturun (git checkout -b feature/fooBar)
Değişikliklerinizi commit edin (git commit -am 'Add some fooBar')
Branch'inizi Push edin (git push origin feature/fooBar)
Yeni bir Pull Request oluşturun.
## Lisans
MIT Lisansı

Telif Hakkı (c) 2023 İbrahim Oğuzhan Yılmaz

İşbu belge ile, yazılım ve ilgili belgelerin bir kopyasını alan herkese, yazılımı sınırlama olmaksızın kullanma, kopyalama, değiştirme, birleştirme, yayınlama, dağıtma, alt lisanslama ve/veya satma hakları ücretsiz olarak verilir. Yazılımı, yazılımdan yararlanacak kişilere, aşağıdaki koşullara uygun olarak sunma hakkı da tanınmıştır:

Yukarıdaki telif hakkı bildirimi ve bu izin bildirimi, yazılımın tüm kopyalarına veya önemli bölümlerine dahil edilmelidir.

YAZILIM, HERHANGİ BİR GARANTİ OLMAKSIZIN "OLDUĞU GİBİ" SAĞLANMIŞTIR. AÇIKÇA VEYA İMA İLE, PAZARLANABİLİRLİK, BELİRLİ BİR AMACA UYGUNLUK VE İHLAL OLMAMASI DA DAHİL OLMAK ÜZERE, HERHANGİ BİR GARANTİYİ REDDEDER. YAZARLAR VEYA TELİF HAKKI SAHİPLERİ, YAZILIMLA YA DA YAZILIMIN KULLANIMI VEYA DİĞER İŞLEMLERİYLE İLGİLİ HERHANGİ BİR SÖZLEŞME, HAKSIZ FİİL YA DA BAŞKA HERHANGİ BİR DURUMDA, HERHANGİ BİR İDDİA, HASAR VEYA DİĞER YÜKÜMLÜLÜKLERDEN SORUMLU TUTULAMAZ.

## İletişim
- Email: ibrahimoguzhany@gmail.com
- LinkedIn: https://www.linkedin.com/in/ibrahimoguzhany/
