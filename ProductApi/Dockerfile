# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем файлы проекта в контейнер
COPY *.csproj ./
RUN dotnet restore

# Копируем оставшиеся файлы и собираем приложение
COPY . ./
RUN dotnet publish -c Release -o out

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out .

# Указываем команду для запуска приложения
ENTRYPOINT ["dotnet", "ProductApi.dll"]
