docker build -t sssm .
docker run -d -p 8080:80 --name abc sssm
docker rm --force abc

docker build -t sssm .
heroku container:push -a mh-sssm web
heroku container:release -a mh-sssm web