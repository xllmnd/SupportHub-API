1. Hazırlık: Test Projesini Oluştur
Ana çözüm (solution) klasöründe terminali aç ve yeni bir test projesi ekle:
bash
# 1. xUnit test projesi oluştur
dotnet new xunit -n SupportHub.Application.UnitTests

# 2. Test projesini ana çözüme (sln) ekle
dotnet sln add SupportHub.Application.UnitTests

# 3. Test projesine 'Application' katmanını referans olarak ekle (Test edeceğimiz yer burası)
dotnet add SupportHub.Application.UnitTests reference SupportHub.Application

# 4. Gerekli kütüphaneyi (Moq) yükle (Veritabanını taklit etmek için)
dotnet add SupportHub.Application.UnitTests package Moq