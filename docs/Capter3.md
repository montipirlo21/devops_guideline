
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
