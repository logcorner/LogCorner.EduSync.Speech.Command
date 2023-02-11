# LogCorner.EduSync
Building microservices through Event Driven Architecture

docker rmi -f $(docker images -a -q)
docker volume rm $(docker volume ls -q)

kubectl apply -f .

kubectl get pods
 kubectl get services


install ingress for docker desktop
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.3/deploy/static/provider/aws/deploy.yaml

kubectl get pods -n ingress-nginx --watch

C:\Windows\System32\drivers\etc\hosts

127.0.0.1 kubernetes.docker.com

curl http://kubernetes.docker.com
https://kubernetes.docker.com/speech-command-http-api/swagger/index.html


eneable ssl
download openssl
Win64 OpenSSL v3.0.7
https://slproweb.com/products/Win32OpenSSL.html

generate a certificate and a key

openssl req -x509 -nodes -days 365 -newkey rsa:2048 -out logcorner-ingress-tls.crt -keyout logcorner-ingress-tls.key -subj "/CN=kubernetes.docker.com/O=logcorner-ingress-tls"

deploy certificate to kubernetes
kubectl create secret tls logcorner-ingress-tls --namespace default --key logcorner-ingress-tls.key --cert logcorner-ingress-tls.crt

<!-- kubectl create namespace docker-desktop-ingress
kubectl create secret tls logcorner-ingress-tls --namespace docker-desktop-ingress --key logcorner-ingress-tls.key --cert logcorner-ingress-tls.crt -->

kubectl apply -f command-ingress.yml

https://kubernetes.docker.com/speech-command-http-api/swagger/index.html

{
  "title": "this is a title",
  "description": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
  "url": "http://test.com",
  "typeId": 1
}

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'MyC0m9l&xP@ssw0rd'

select name from sys.databases
go
use [LogCorner.EduSync.Speech.Database]
go
select * from [dbo].[Speech]
go