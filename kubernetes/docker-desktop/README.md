# LogCorner.EduSync
Building microservices through Event Driven Architecture

docker rmi -f $(docker images -a -q)
docker volume rm $(docker volume ls -q)

# LogCorner.EduSync
Building microservices through Event Driven Architecture


docker rmi -f $(docker images -a -q)
docker volume rm $(docker volume ls -q)


run the following command to build the images, so need to locate the docker-compose.yml file under (\LogCorner.EduSync.Speech.Command\src)

docker-compose build
The build will produce 2 images : logcornerhub/logcorner-edusync-speech-command  and logcornerhub/logcorner-edusync-speech-mssql-tools

# login to your azure account and set your Azure default Subscription
az login 

az account set --name Microsoft Azure Sponsorship

# in ths tutorial, I will use docker-desktop and enable kubernetes 

kubectl config get-contexts 
kubectl config use-context  docker-desktop 
kubectl cluster-info



inside folder \LogCorner.EduSync.Speech.Command\kubernetes\docker-desktop\CommandDatabase  you can find the kubernetes configuration files of the database : the deployment configuration file and the service configuration file
db-deployment.yml file : 
db-job.yml file
db-pvc.yml file
db-service.yml file
db-secret.yml file

kubectl apply -f ./kubernetes/docker-desktop/CommandDatabase

kubectl get pods
kubectl get services

# connect to database 

inside folder \LogCorner.EduSync.Speech.Command\kubernetes\docker-desktop\CommandApi  you can find the kubernetes configuration files of the command http api : the deployment configuration file and the service configuration file


kubectl apply -f ./kubernetes/docker-desktop/CommandApi

kubectl get pods
kubectl get services



http://localhost:30124/swagger/index.html

{
  "title": "this is a title",
  "description": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
  "url": "http://test.com",
  "typeId": 1
}

