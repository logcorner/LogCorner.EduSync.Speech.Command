#install helm 
https://helm.sh/docs/intro/install/

choco install kubernetes-helm

helm install  logcorner-command  logcorner.edusync.speech

helm get manifest logcorner-command

helm upgrade logcorner-command  logcorner.edusync.speech


helm uninstall logcorner-command  logcorner.edusync.speech