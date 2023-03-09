# GITLAB ON DOCKER

## installazione certificati git 

aggiunti ca e certificato crt nel file ca-bundle.crt in 
C:\Program Files\Git\mingw64\ssl\certs

## Installazione certificati gitlab

Aggiunto url gitlab.local.com nel file host di windows.

https://devopscube.com/create-self-signed-certificates-openssl/
lancio creaCertificati.sh

openssl x509 -in server.crt -out server.pem

copio i files nella directory ssl di gitlab
modifico il file gitlab.rb

gitlab-ctl reconfigure
gitlab-ctl restart

## Installazione gitlab comandi 
Comandi testati ma con problema di rete in locale

Password temporanea root
WwcGw8BWOrDA7nHEV+QP1N9AYugftF1bQh6/XCN77vU=

Utilizzo del docker-compose.yml di gitlab con il runner

Creazione delle cartelle locali su cui fare l'attach dei volumi

c:/gitlab/config
c:/gitlab/data
c:/gitlab/gitlab-runner-config
c:/gitlab/logs