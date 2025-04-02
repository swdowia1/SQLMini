# SQL mini
Aplikacja do podglądu tabel w bazie

## Opis
![image](https://github.com/user-attachments/assets/40517b5d-54d0-4437-8070-3d0b31db3b93)

w katalogu aplikacji znajduje się katalog "Serwery"<br />
Każdy podkatalog zawiera plik pol.txt<br />
aplikacja angielski<br />
workstation id=aaaswsw.mssql.somee.com;packet size=4096;user id=swdowia1_SQLLogin_2;pwd=haslo;data source=aaaswsw.mssql.somee.com;persist security info=False;initial catalog=aaaswsw;TrustServerCertificate=True<br />
 gdzie <br />
 1 linia opis połączenia<br />
 2 linia połączenie do bazy<br />

 ## dodanie połączenia
 Uruchomić plik katalog bat
 - cd SQLMini\bin\Debug
 - mkdir Serwery
 - cd Serwery
 - mkdir ang
 - tworzy plik **pol.txt** zawiera nazwa połączenia i samo połączenie



 ## W planach
 - w każdym katalogu serwerów dodać specyficzne zapytania sql
 - wykorzystać RabbitMQ w celu kolejkowania "długich: zadań


