#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/LogCorner.EduSync.Speech.Domain.UnitTest.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/LogCorner.EduSync.Speech.Domain.UnitTest.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest"
