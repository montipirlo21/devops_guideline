# Gitlab on Docker # 

## DockerDesktop installation
Download and install Docker Desktop application from [Docker-Desktop](https://www.docker.com/products/docker-desktop/)

## GITLAB ON DOCKER ##

Create local folder to save "persistent data" of your gitlab docker installation.
A docker as a transient state so if you don't want to redo all procedure every time you destroy the container, save the data!

I personally choose these destinations:

c:/gitlab/config
c:/gitlab/data
c:/gitlab/gitlab-runner-config
c:/gitlab/logs

## Run Gitlab on docker

docker run --detach --hostname gitlab.local.com --publish 443:443 --publish 80:80 --publish 22:22 --name gitlab --restart always  --volume C:/Gitlab/config:/etc/gitlab  --volume C:/Gitlab/logs:/var/log/gitlab  --volume C:/Gitlab/data:/var/opt/gitlab  --shm-size 256m -e GITLAB_SKIP_UNMIGRATED_DATA_CHECK=true gitlab/gitlab-ee:latest 

## Installazione certificati gitlab

Aggiunto url gitlab.local.com nel file host di windows.

aggiunti ca e certificato crt nel file ca-bundle.crt in 
C:\Program Files\Git\mingw64\ssl\certs

https://devopscube.com/create-self-signed-certificates-openssl/
lancio creaCertificati.sh

Creo il pem
openssl x509 -in server.crt -out server.pem

https://docs.gitlab.com/omnibus/settings/ssl/


copio i files nella directory ssl di gitlab
modifico il file gitlab.rb

external_url "https://gitlab.local.com"
letsencrypt['enable'] = false
nginx['redirect_http_to_https'] = true

gitlab-ctl reconfigure
gitlab-ctl restart

