kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.40.2/deploy/static/provider/cloud/deploy.yaml


kubectl get pods -n ingress-nginx --watch

https://slproweb.com/products/Win32OpenSSL.html


openssl req -x509 -nodes -days 365 -newkey rsa:2048 -out logcorner-ingress-tls.crt -keyout logcorner-ingress-tls.key -subj "/CN=kubernetes.docker.com/O=logcorner-ingress-tls"


kubectl create secret tls logcorner-ingress-tls --namespace default --key logcorner-ingress-tls.key --cert logcorner-ingress-tls.crt