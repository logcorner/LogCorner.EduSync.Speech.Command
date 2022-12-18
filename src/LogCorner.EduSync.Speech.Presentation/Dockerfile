#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj", "LogCorner.EduSync.Speech.Presentation/"]
COPY ["LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj", "LogCorner.EduSync.Speech.Application/"]
COPY ["LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech.Domain/"]
COPY ["LogCorner.EduSync.Speech.SharedKernel/LogCorner.EduSync.Speech.Command.SharedKernel.csproj", "LogCorner.EduSync.Speech.SharedKernel/"]
COPY ["LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech.Infrastructure/"]
RUN dotnet restore "LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech.Presentation"
RUN dotnet build "LogCorner.EduSync.Speech.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogCorner.EduSync.Speech.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogCorner.EduSync.Speech.Presentation.dll"]