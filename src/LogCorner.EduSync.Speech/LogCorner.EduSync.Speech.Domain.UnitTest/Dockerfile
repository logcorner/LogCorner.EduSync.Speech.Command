FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster 
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/LogCorner.EduSync.Speech.Domain.UnitTest.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest/LogCorner.EduSync.Speech.Domain.UnitTest.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain.UnitTest"

