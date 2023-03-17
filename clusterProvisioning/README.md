// Creo il cluster
kind create cluster --config=cluster-config.yaml

# INGRESS 

https://kind.sigs.k8s.io/docs/user/ingress/

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=90s

// Aggiunta del certificato come secrets
kubectl create secret tls grpc-secret --key ../certs/wildcard/wildcard.dev.com.key --cert ../certs/wildcard/wildcard.dev.com.crt
kubectl apply -f ../kustomize/kubernetes-manifests.yaml

## Installazione flux ##

https://fluxcd.io/flux/get-started/

Installazione choco da powershell admin

https://docs.chocolatey.org/en-us/choco/setup#install-with-powershell.exe

Installazione flux 
Deve essere avviato il cluster kind

flux check --pre

set GITLAB_TOKEN=glpat-HcPxdRMmDkYXwpZLu5au
flux bootstrap gitlab --owner=davide.monticelli --repository=fleet-infra  --branch=main --path=./clusters/my-cluster  --hostname=gitlab.local.com --token-auth --personal --ca-file="../certs/rootCA/rootCA.crt"

flux create source git micro-apps --url=https://gitlab.local.com/davide.monticelli/devops_guideline --branch=22-flux-installation --interval=30s --ca-file="../certs/rootCA/rootCA.crt" --export  > micro-apps-source2.yaml 
flux create kustomization micro-apps --target-namespace=default --source=micro-apps --path="./kustomize" --prune=true --interval=1m --export > micro-apps-kustomization.yaml



# SHORTLIST staging
kind create cluster --config=cluster-config-staging.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=90s
kubectl create secret tls grpc-secret --key ../certs/wildcard/wildcard.dev.com.key --cert ../certs/wildcard/wildcard.dev.com.crt

set GITLAB_TOKEN=glpat-HcPxdRMmDkYXwpZLu5au
flux bootstrap gitlab --owner=davide.monticelli --repository=fleet-infra  --branch=main --path=./clusters/staging  --hostname=gitlab.local.com --token-auth --personal --ca-file="../certs/rootCA/rootCA.crt" --context=kind-staging


# SHORTLIST production
kind create cluster --config=cluster-config-production.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=90s
kubectl create secret tls grpc-secret --key ../certs/wildcard/wildcard.dev.com.key --cert ../certs/wildcard/wildcard.dev.com.crt

set GITLAB_TOKEN=glpat-HcPxdRMmDkYXwpZLu5au
flux bootstrap gitlab --owner=davide.monticelli --repository=fleet-infra  --branch=27-create-two-cluster-staging-and-production-with-kind-and-flux --path=./clusters/production  --hostname=gitlab.local.com --token-auth --personal --ca-file="../certs/rootCA/rootCA.crt" --context=kind-production