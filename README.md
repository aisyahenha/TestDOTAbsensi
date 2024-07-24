# API User Management
API untuk test interview **PT. Digdaya Olah Teknologi (DOT) Indonesia**.

## Instalasi
1. Clone repository ini dan pastikan sudah terinstall **.NET 8**.
2. Setelah di-clone, jangan lupa untuk merestore NuGet packagenya.
3. Pastikan PostgreSQL sudah terpasang dan running.
4. Connection string untuk database Supabase sudah terdapat di **appsettings.json**, namun bisa diganti menggunakan connection string PostgreSQL yang lain jika diperlukan.

## Design Pattern
Design pattern yang digunakan yaitu:  
1. **Repository Pattern**: untuk mengklasifikasi CRUD setiap model.
2. **Dependency Injection**: untuk menyuntikkan objek-objek service, repository yang sudah dibuat, dan package-package lain.

## Implementasi Error Handling
Untuk meng-handle error, saya menggunakan middleware untuk mengirim **Bad Request** dan **Not Found**.

## Implementasi Cache
Saya menggunakan **Redis** untuk menyimpan dan memvalidasi token JWT.

## Implementasi OOP / SOLID Principle / Functional Programming
Penerapan **OOP dan SOLID** dimulai dari objek **Model -> Repository -> Service**. Untuk **Functional Programming**, saya menerapkannya di dalam **Repository** menggunakan **LINQ** untuk menulis query yang lebih ekspresif.

## Endpoint
Terdapat 3 endpoint yang bisa diakses yaitu:
1. **/api/auth**
   - **Pathname**: /login dan **POST**: method untuk proses login.
   - **Pathname**: /logout dan **POST**: method untuk proses logout.

2. **/api/karyawan**
   - **Pathname**: /get-all dan **GET**: method untuk mengambil semua data karyawan.
   - **Pathname**: /detail/{id} dan **GET**: method untuk mengambil data karyawan berdasarkan id.
   - **Pathname**: /create dan **POST**: method untuk membuat karyawan baru.
   - **Pathname**: /update dan **PATCH**: method untuk mengupdate data karyawan.
   - **Pathname**: /delete dan **DELETE**: method untuk menghapus data karyawan.

3. **/api/absensi**
   - **Pathname**: /get-all dan **GET**: method untuk mengambil semua data absensi.
   - **Pathname**: /detail/{id} dan **GET**: method untuk mengambil data absensi berdasarkan id absensi.
   - **Pathname**: /detail-karyawan/{id} dan **GET**: method untuk mengambil data absensi berdasarkan id karyawan.
   - **Pathname**: /absen-in dan **POST**: method untuk melakukan insert data absen masuk karyawan.
   - **Pathname**: /absen-out/{idKaryawan} dan **POST**: method untuk melakukan absen pulang untuk karyawan.

**Catatan**: Endpoint **/api/karyawan** tidak bisa diakses apabila belum login, sedangkan untuk **/api/absensi** memiliki akses untuk public pada **absen-in** dan **absen-out**.

## Request Body
1. **Endpoint**: /api/auth
   - **Pathname**: /login dan **POST**
     - terdapat 2 inputan:
       - **property**: username, **type**: string
       - **property**: password, **type**: string

     contoh request body:
     ```json
     {
       "username": "admin",
       "password": "admin123"
     }
     ```

2. **Endpoint**: /api/karyawan
   - **Pathname**: /create dan **POST**
     - terdapat 4 inputan:
       - **property**: nik, **type**: string
       - **property**: name, **type**: string
       - **property**: address, **type**: string
       - **property**: position, **type**: string

     contoh request body:
     ```json
     {
       "nik": "12345",
       "name": "Burhan Kukuk",
       "address": "Jakarta",
       "position": "Staff"
     }
     ```

   - **Pathname**: /update dan **PATCH**
     - terdapat 5 inputan:
       - **property**: id, **type**: int
       - **property**: nik, **type**: string
       - **property**: name, **type**: string
       - **property**: address, **type**: string
       - **property**: position, **type**: string

     contoh request body:
     ```json
     {
       "id": 1,
       "nik": "12345",
       "name": "Burhan Kukuk",
       "address": "Jakarta",
       "position": "Manager"
     }
     ```
