# Initializes a new stage and sets the base image.
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
# Sets the current working directory inside the image and expose port 80 internally.
WORKDIR /app
EXPOSE 80
# Initializes a new build stage and sets the base image that we are going to use for build.
FROM microsoft/dotnet:2.2-sdk AS build
# Sets the current working directory inside the image.
WORKDIR /src
# Copy Presentation.csproj and project dependencies.
COPY LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation/
COPY LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/LogCorner.EduSync.Speech.Infrastructure.csproj LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Infrastructure/
COPY LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/LogCorner.EduSync.Speech.Domain.csproj LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Domain/
COPY LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/LogCorner.EduSync.Speech.Application.csproj LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Application/
# Restore the packages for the projects inside the image.
RUN dotnet restore LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation/LogCorner.EduSync.Speech.Presentation.csproj
# Copy all the files from the local system to the current working directory of the image except files specified in .dockerignore file. 
COPY . .
# Sets the current working directory inside the image.
WORKDIR /src/LogCorner.EduSync.Speech/LogCorner.EduSync.Speech.Presentation
# Build Presentation.csproj with Release configuration and app directory as outputs of the built binaries.
RUN dotnet build LogCorner.EduSync.Speech.Presentation.csproj -c Release -o /app
# Publish the application to /app
FROM build AS publish
RUN dotnet publish LogCorner.EduSync.Speech.Presentation.csproj -c Release -o /app
# configure the container to run as an executable
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LogCorner.EduSync.Speech.Presentation.dll"]
