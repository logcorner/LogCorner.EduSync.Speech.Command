# pull microsoft/mssql-server-linux as base image
FROM mcr.microsoft.com/mssql/server:2019-latest

# add LogCorner.EduSync.Speech.Database_Create.sql to /home/resources/ and name it Restore.sql
ADD Restore/LogCorner.EduSync.Speech.Database_Create.sql /home/resources/Restore.sql

# add SA_PASSWORD as build-time variable
ARG SA_PASSWORD

# use that value to set the ENV variable
ENV SA_PASSWORD=${SA_PASSWORD}
ENV ACCEPT_EULA=Y

# run sqlservr, connect locally with sqlcmd and create Restore.sql script
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "SQL Server is now ready for client connections" \
  && sleep 10 \
  && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i /home/resources/Restore.sql \
  && pkill sqlservr



