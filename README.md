# Telefon Rehberi Uygulaması

Bu proje, basit bir telefon rehberi uygulamasıdır. Kullanıcıların kişilerle ilgili işlemler yapabileceği ve raporlar oluşturabileceği bir sistem sunar.

## Kullanılan Teknolojiler
- **.NET Core**
- **Docker**
- **PostgreSQL**
- **RabbitMQ**
- **MongoDB**

## Gerçekleştirilen İşlevler
- Rehberde kişi oluşturma
- Rehberde kişi kaldırma
- Rehberdeki kişiye iletişim bilgisi ekleme
- Rehberdeki kişiden iletişim bilgisi kaldırma
- Rehberdeki kişilerin listelenmesi
- Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
- Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
- Sistemin oluşturduğu raporların listelenmesi
- Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi

## Raporlama
Rapor talepleri asenkron olarak çalışmaktadır. Kullanıcı bir rapor talep ettiğinde:
1. Sistem bu çalışmayı arka planda darboğaz yaratmadan sıralı bir biçimde ele alır.
2. Rapor tamamlandığında, kullanıcının "raporların listelendiği" endpoint üzerinden raporun durumunu "tamamlandı" olarak gözlemlemesi mümkündür.

## API Endpoints
- **Kişi İşlemleri**: [https://localhost:6060/swagger/index.html](https://localhost:6060/swagger/index.html)
- **Rapor İşlemleri**: [https://localhost:6061/swagger/index.html](https://localhost:6061/swagger/index.html)

## Test Raporları
Projenin test senaryoları da kodlanmıştır. Aşağıda ilgili test raporları yer almaktadır:

### Kişiler Servisi İçin Test Raporu
![Kişiler Test Raporu](https://github.com/mhdikmen/EContacts/blob/master/src/Services/Contact/Contact.API.Tests/TestResults.png)

### Rapor Servisi İçin Test Raporu
![Rapor Test Raporu](https://github.com/mhdikmen/EContacts/blob/master/src/Services/Report/Report.API.Tests/TestResults.png)

## Projenin Çalıştırılması
Projeyi çalıştırmak için **Docker**  ve **.NET 8.0** kurulu olmalıdır. Sırasıyla aşağıdaki komutlar çalıştırılmalıdır:

```bash
# 1. Projeyi klonlayın
git clone https://github.com/mhdikmen/EContacts.git

# 2. Proje dizinine geçiş yapın
cd EContacts/src

# 3. Docker Compose ile servisi başlatın
docker-compose up --build
```

Proje başarıyla başlatıldıktan sonra, yukarıdaki API endpointleri üzerinden işlemleri gerçekleştirebilirsiniz.


Ayrıca master branchinde değişiklikler yapıldıkça da oluşan servislerin imageları dockerhub 'a pushlanmaktadır.


## DockerHub Repository
- **contactapi**: [https://hub.docker.com/repository/docker/mhdikmen1/contactapi](https://hub.docker.com/repository/docker/mhdikmen1/contactapi)
- **reportapi**: [https://hub.docker.com/repository/docker/mhdikmen1/reportapi](https://hub.docker.com/repository/docker/mhdikmen1/reportapi)