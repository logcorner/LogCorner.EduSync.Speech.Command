FROM microsoft/mssql-server-linux

ADD Restore/LogCorner.EduSync.Speech.Database_Create.sql /home/resources/Restore.sql

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=PassW0rd

RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "SQL Server is now ready for client connections" \
  && sleep 10 \
  && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'PassW0rd' -i /home/resources/Restore.sql \
  && pkill sqlservr