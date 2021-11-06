FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster 
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure.UnitTest/LogCorner.EduSync.Speech.Infrastructure.UnitTest.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure.UnitTest/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure.UnitTest/LogCorner.EduSync.Speech.Infrastructure.UnitTest.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure.UnitTest"

