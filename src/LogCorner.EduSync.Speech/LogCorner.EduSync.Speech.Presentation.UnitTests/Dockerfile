FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster 
WORKDIR /src
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation.UnitTest/LogCorner.EduSync.Speech.Presentation.UnitTest.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation.UnitTest/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/"]
COPY ["LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj", "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/"]
RUN dotnet restore "LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation.UnitTest/LogCorner.EduSync.Speech.Presentation.UnitTest.csproj"
COPY . .
WORKDIR "/src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation.UnitTest"

