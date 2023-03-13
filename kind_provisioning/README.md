# INGRESS GRPC 
// Creo il cluster
kind create cluster --config=cluster-config.yaml

https://kind.sigs.k8s.io/docs/user/ingress/

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=90s

<!-- kubectl label nodes kind-control-plane ingress-ready=true -->

// Aggiunta del certificato come secrets
kubectl create secret tls grpc-secret --key ../certs/wildcard/wildcard.dev.com.key --cert ../certs/wildcard/wildcard.dev.com.crt

kubectl apply -f ../release/kubernetes-manifests.yaml


