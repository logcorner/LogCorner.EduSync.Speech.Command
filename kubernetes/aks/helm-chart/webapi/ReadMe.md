A chart is a definition of the application and a release is an instance of a chart
we can install a new release of a revision of an existing release

# install helm 
https://helm.sh/docs/intro/install/

choco install kubernetes-helm

helm version --short
kubectl config view

helm repo add "stable" "https://charts.helm.sh/stable"

helm env
# Install a Release  :  helm install [release] [chart]
# installing a chart named webapi with the release name logcorner-command
helm install  logcorner-command  webapi

# list all the releases deployed to a Kubernetes cluster
helm list

kubectl get pods
kubectl get services

# helm history [release]
helm history logcorner-command

# helm upgrade [release] [chart]
helm upgrade   logcorner-command  webapi

# helm rollback [release] [revision]
helm rollback logcorner-command 1

# helm status [release]
helm status logcorner-command

# helm get all [release]
helm get all logcorner-command

# helm get manifest  [release]
helm get manifest logcorner-command

# helm uninstall [release]
helm uninstall logcorner-command  webapi


http://51.124.4.112/swagger/index.html


{
  "title": "this is a title",
  "description": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
  "url": "http://test.com",
  "typeId": 1
}
