
https://chocolatey.org/install

choco -?

https://helm.sh/docs/intro/install/
choco install kubernetes-helm
helm version
helm repo add stable https://charts.helm.sh/stable

helm install command-http logcorner.edusync.speech # --namespace helm

helm get manifest command-http

helm upgrade command-http logcorner.edusync.speech 



docker tag logcornerhub/logcorner-edusync-speech-mssql-tools   logcornerhub/logcorner-edusync-speech-mssql-tools:helm
docker push logcornerhub/logcorner-edusync-speech-mssql-tools:helm


docker tag logcornerhub/logcorner-edusync-speech-command   logcornerhub/logcorner-edusync-speech-command:helm
docker push logcornerhub/logcorner-edusync-speech-command:helm
helm uninstall command-http logcorner.edusync.speech

https://helm.kubernetes.docker.com/speech-command-http-api/swagger/index.html