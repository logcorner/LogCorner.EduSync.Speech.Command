A chart is a definition of the application and a release is an instance of a chart
we can install a new release of a revision of an existing release


Action
Install a Release
Upgrade a Release revision
Rollback to a Release revision
Print Release history
Display Release status
Show details of a release
Uninstall a Release
List Releases
Command
helm install [release] [chart]
helm upgrade [release] [chart]
helm rollback [release] [revision]
helm history [release]
helm status [release]
helm get all [release]
helm uninstall [release]
helm list


#install helm 
https://helm.sh/docs/intro/install/

helm version --short
kubectl config view

helm repo add "stable" "https://charts.helm.sh/stable"

helm env

# enable ingress
# add helm.kubernetes.docker.com to host

choco install kubernetes-helm


helm install [release] [chart]
helm install  logcorner-command  logcorner.edusync.speech

helm list --short  => list release name

helm get manifest logcorner-command




https://helm.kubernetes.docker.com/speech-command-http-api/swagger/index.html


#UPGRADE RELEASE
update appVersion: "1.1"  and description 
version: 1.0.0 do not change  because chart is unchanged
change image version 
run the command

helm upgrade logcorner-command  logcorner.edusync.speech

helm rollback logcorner-command 1

helm history logcorner-command 

helm uninstall logcorner-command  logcorner.edusync.speech