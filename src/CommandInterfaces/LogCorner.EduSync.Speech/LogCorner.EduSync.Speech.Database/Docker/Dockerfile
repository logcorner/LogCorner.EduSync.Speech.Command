FROM microsoft/mssql-server-linux

ADD Restore/LogCorner.EduSync.Speech.Database_Create.sql /home/resources/Restore.sql

# expect a build-time variable
ARG SA_PASSWORD

# use the value to set the ENV var default
ENV SA_PASSWORD=${SA_PASSWORD}
ENV ACCEPT_EULA=Y

RUN echo "Oh dang look at that V_SA_PASSWORD = $V_SA_PASSWORD"
RUN echo "Oh dang look at that SA_PASSWORD =$SA_PASSWORD"

RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "SQL Server is now ready for client connections" \
  && sleep 10 \
  && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i /home/resources/Restore.sql \
  && pkill sqlservr