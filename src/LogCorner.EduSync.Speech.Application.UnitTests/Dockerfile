#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTests/LogCorner.EduSync.Speech.Application.UnitTests.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTests/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.SharedKernel/LogCorner.EduSync.Speech.Command.SharedKernel.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.SharedKernel/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTests/LogCorner.EduSync.Speech.Application.UnitTests.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application.UnitTests"

