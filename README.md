# Alpata

.Net Core Mvc Proje 

Amaç  

Çok katmanlı çalışan bir .net core uygulaması yazmak 

Teknik Gereksinimler 

·         .Net Core 7.0 ile çalışması +++

·         Model – Context – Api – Web katmanlarının ayrı olması  +++

·         Mssql veritabanı üzerinde çalışması +++

İş Gereksinimleri 

·         Kayıt olma sayfası (ad, soyad, email, telefon, şifre, profil resmi yükleme) +++

·         Giriş yapma sayfası (email, şifre) +++

·         Toplantı CRUD işlemleri (toplantı adı, başlangıç bitiş tarihi, açıklama ve doküman yükleme) +++

Extra Yapılabilecekler 

·         Kayıt sonrası hoş geldiniz maili atılması +++

·         Toplantı bilgilendirme maili atılması +++

·         Uygulamanın yayına alınması 

·         Geliştirme sayfaları tamamlandıkça Github a gönderilmesi +++

·         Veritabanı üzerinde parolanın şifrelenerek tutulması +++

·         Dosyaların erişim kontrolu 

·         Angular 16 ile frontend yazılması 

·         Dosyaların sisteme sıkıştırılıp yüklenmesi 

·         Api katmanında JWT auth desteği 

·         Api katmanında Swagger desteği +++

·         Dockerfile ve docker-compose dosyasının oluşturulması  +++


SQL:

Select 'Gets date and count group by date' as Description ,null as UserName,CONVERT(date, StartDate) as StartDate,COUNT(CONVERT(date, StartDate)) as Count
FROM dbo.Meetings
GROUP BY CONVERT(date, StartDate)
UNION ALL
Select 'gets only date group by date' as Description,null as UserName,CONVERT(date, StartDate) as StartDate,null as Count
From Meetings
UNION ALL
SElECT 'gets username, date and count group by name and date' as Description,u.Name as UserName,CONVERT(date, m.StartDate) as StartDate,COUNT(1) as Count
FROM Meetings as m
INNER JOIN Users as u on u.Id = m.UserId
group by u.Name, CONVERT(date, m.StartDate);



