# Gitlab on Docker # 

## DockerDesktop installation
Download and install Docker Desktop application from [Docker-Desktop](https://www.docker.com/products/docker-desktop/)<br>

## GITLAB ON DOCKER ##

Create local folder to save "persistent data" of your gitlab docker installation.<br>
A docker as a transient state so if you don't want to redo all procedure every time you destroy the container, save the data! <br>
I personally choose these destinations:<br>

```
c:/gitlab/config
c:/gitlab/data
c:/gitlab/gitlab-runner-config
c:/gitlab/logs
```

## Run Gitlab on docker

Run the gitlab on the docker engine.<br>
It takes a while to start up based on your computer.<br>

Check the `hostname` parameter: it has to match your certificate. <br>
I choose the name `gitlab.local.com`.

```
docker run --detach --hostname gitlab.local.com --publish 443:443 --publish 80:80 --publish 22:22 --name gitlab --restart always  --volume C:/Gitlab/config:/etc/gitlab  --volume C:/Gitlab/logs:/var/log/gitlab  --volume C:/Gitlab/data:/var/opt/gitlab  --shm-size 256m -e GITLAB_SKIP_UNMIGRATED_DATA_CHECK=true gitlab/gitlab-ee:latest 
```

## Add gitlab certificates




Git clients are 

Aggiunto url gitlab.local.com nel file host di windows.<br>

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

