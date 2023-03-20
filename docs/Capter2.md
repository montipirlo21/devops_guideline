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

## Add host to etc\host

First of all add `gitlab.local.com` in your host windows file.<br>
You can find it at `C:\Windows\System32\drivers\etc` 
add a line at the bottom: 

```
127.0.0.1 gitlab.local.com
```

## Create Root and gitlab certificates

So you need to create a certificate! Actually not! You have to made TWO! A root cerificate and a child certificate<br>
I used this [procedure](https://devopscube.com/create-self-signed-certificates-openssl/) to create the two certificate<br>

Switch to WSL on the command line and launch the script<br>
You'll find under /certs/gitlab

```
./createRootAndGitlabCerticate.sh demo.mlopshub.com
```

You can find all my certs under /certs.<br>
There are the roots certificate and the gitlab certificate.<br>
And then i loaded the root certificate click on them and **putted under trusted root authority on windows computer**<br>

## Add certificates to git cli

Git doesn't like to use Http or https with untrusted-sign.<br>
So we have to append root certificate e and certificate in **ca-bundle.crt** <br>
You have to put both of them<br>

You'll find **ca-bundle.crt** under 

```
C:\Program Files\Git\mingw64\ssl\certs
```

## Add certificates to gitlat

We need the PEM format <br>
Switch to WSL <br>
Installa openssl if you don't have it <br>

```
openssl x509 -in server.crt -out server.pem
```

https://docs.gitlab.com/omnibus/settings/ssl/<br>
the copy the file in ssl gitlab directory ( in my case C:\Gitlab\config\ssl )<br>
then edit the gitlab.rb under (C:\Gitlab\config)<br>

All the lines are commented so i put this three line on top<br>

```
external_url "https://gitlab.local.com"
letsencrypt['enable'] = false
nginx['redirect_http_to_https'] = true
```

then i recofigured and restarted gitlab (Use dockerdesktop to open container cmd and run the commands )<br>

```
gitlab-ctl reconfigure
gitlab-ctl restart
```

