install ingress for doecker desktop
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.40.2/deploy/static/provider/cloud/deploy.yaml


kubectl get pods -n ingress-nginx --watch

C:\Windows\System32\drivers\etc\hosts

127.0.0.1 kubernetes.docker.com

curl http://kubernetes.docker.com


eneable ssl
https://slproweb.com/products/Win32OpenSSL.html


openssl req -x509 -nodes -days 365 -newkey rsa:2048 -out logcorner-ingress-tls.crt -keyout logcorner-ingress-tls.key -subj "/CN=kubernetes.docker.com/O=logcorner-ingress-tls"

kubectl create namespace qa
kubectl create secret tls logcorner-ingress-tls --namespace qa --key logcorner-ingress-tls.key --cert logcorner-ingress-tls.crt



docker-compose --env-file ./config/docker/.env build

kubectl create secret docker-registry regsecret --docker-server=logcornerregistryhub.azurecr.io --docker-username=logcornerregistryhub  --docker-password=pPxXcgAi7/Rs5ut2I7qzaQBgPopYGtEa  --docker-email=admin@azurecr.io  

az acr login --name tfqdemotfquickstartacr
docker tag logcornerhub/logcorner-edusync-speech-mssql-tools tfqdemotfquickstartacr.azurecr.io/logcorner-edusync-speech-mssql-tools
docker push logcornerterraformacr.azurecr.io/logcorner-edusync-speech-mssql-tools


# use keyvault : https://pumpingco.de/blog/use-azure-keyvault-with-asp-net-core-running-in-an-aks-cluster-using-aad-pod-identity/
$ az identity create -g LOGCORNER-MICROSERVICES-IAC -n logcorneridentity -o json


$ az role assignment create  --role Reader --assignee a4ba5e20-7ca4-4529-b0b3-ecdaf12a84e5   --scope /subscriptions/023b2039-5c23-44b8-844e-c002f8ed431d/resourcegroups/LOGCORNER-MICROSERVICES-IAC

az keyvault set-policy --name logcornersecretstore --secret-permissions list get  --object-id a4ba5e20-7ca4-4529-b0b3-ecdaf12a84e5 --resource-group  LOGCORNER-MICROSERVICES-IAC



az role assignment create  --role "Managed Identity Operator"  --assignee d8f3ef20-e2f6-4422-8859-8837975f3b8f   --scope /subscriptions/023b2039-5c23-44b8-844e-c002f8ed431d/resourceGroups/LOGCORNER-MICROSERVICES-IAC/providers/Microsoft.ManagedIdentity/userAssignedIdentities/logcorneridentity