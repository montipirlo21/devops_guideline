# GITLAB ON DOCKER

Creazione delle cartelle locali su cui fare l'attach dei volumi
c:/gitlab/config
c:/gitlab/data
c:/gitlab/gitlab-runner-config
c:/gitlab/logs


## Installazione gitlab comandi 

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


## Installazione del runner

Inserito in C:/Gitlab/gitlab-runner-config/certs il crt e pem di gitlab.local.com

https://docs.gitlab.com/runner/install/docker.html
docker run -d --name gitlab-runner --restart always -v C:/Gitlab/gitlab-runner-config:/etc/gitlab-runner -v /var/run/docker.sock:/var/run/docker.sock  gitlab/gitlab-runner:latest

https://docs.gitlab.com/runner/register/index.html#docker
docker run --rm -it -v C:/Gitlab/gitlab-runner-config:/etc/gitlab-runner gitlab/gitlab-runner register

compilo la registrazione interattiva come da guida

Errore cannot connect to the Docker daemon at tcp://docker:2375.
Aggiunto variabili nel .gitlab-ci.yml
modificato config.toml del runner con:

image = "docker:latest"
privileged = true


-e GITLAB_SKIP_UNMIGRATED_DATA_CHECK=true