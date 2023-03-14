# KUBERNATES MANIFEST

kubectl apply -f kubernetes-manifests.yaml

On kind necessario port-forward esplicito o dichiato alla creazione del cluster
kubectl port-forward service/cutterservice 50051:50051
kubectl port-forward service/hammerservice 30002:5171

