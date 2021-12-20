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