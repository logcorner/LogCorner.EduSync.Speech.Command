#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster 
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTest/LogCorner.EduSync.Speech.Application.UnitTest.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTest/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTest/LogCorner.EduSync.Speech.Application.UnitTest.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTest"

