# Enable the Ingress Controller add-on for a new AKS cluster with a new Application Gateway instance
az group create --name myResourceGroup --location canadacentral

az aks create -n myCluster -g myResourceGroup --network-plugin azure --enable-managed-identity -a ingress-appgw --appgw-name myApplicationGateway --appgw-subnet-cidr "10.2.0.0/16" --generate-ssh-keys

az aks get-credentials -n myCluster -g myResourceGroup

kubectl get ingress

kubectl get pods -n ingress-nginx --watch

C:\Windows\System32\drivers\etc\hosts

127.0.0.1 kubernetes.docker.com

curl http://kubernetes.docker.com


eneable ssl
https://slproweb.com/products/Win32OpenSSL.html


openssl req -x509 -nodes -days 365 -newkey rsa:2048 -out logcorner-ingress-tls.crt -keyout logcorner-ingress-tls.key -subj "/CN=kubernetes.docker.com/O=logcorner-ingress-tls"

kubectl create namespace aks
kubectl create secret tls logcorner-ingress-tls --namespace aks --key logcorner-ingress-tls.key --cert logcorner-ingress-tls.crt



docker-compose --env-file ./config/docker/.env build

kubectl create secret docker-registry regsecret --docker-server=logcornerregistryhub.azurecr.io --docker-username=logcornerregistryhub  --docker-password=pPxXcgAi7/Rs5ut2I7qzaQBgPopYGtEa  --docker-email=admin@azurecr.io  

az acr login --name locornermsacrtest
docker tag logcornerhub/logcorner-edusync-speech-mssql-tools locornermsacrtest.azurecr.io/logcorner-edusync-speech-mssql-tools
docker push locornermsacrtest.azurecr.io/logcorner-edusync-speech-mssql-tools

docker tag logcornerhub/logcorner-edusync-speech-command locornermsacrtest.azurecr.io/logcorner-edusync-speech-command
docker push locornermsacrtest.azurecr.io/logcorner-edusync-speech-command


kubectl apply -f .

kubectl apply -f CommandDatabase

kubectl apply -f CommandApi

kubectl get ingress -n aks

kubectl get pods -n aks

kubectl get services -n aks