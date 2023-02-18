#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LogCorner.EduSync.Speech.Presentation.UnitTests/LogCorner.EduSync.Speech.Presentation.UnitTests.csproj", "LogCorner.EduSync.Speech.Presentation.UnitTests/"]
COPY ["LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj", "LogCorner.EduSync.Speech.Presentation/"]
COPY ["LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj", "LogCorner.EduSync.Speech.Application/"]
COPY ["LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech.Domain/"]
COPY ["LogCorner.EduSync.Speech.SharedKernel/LogCorner.EduSync.Speech.SharedKernel.csproj", "LogCorner.EduSync.Speech.SharedKernel/"]
COPY ["LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech.Infrastructure/"]
RUN dotnet restore "LogCorner.EduSync.Speech.Presentation.UnitTests/LogCorner.EduSync.Speech.Presentation.UnitTests.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech.Presentation.UnitTests"
