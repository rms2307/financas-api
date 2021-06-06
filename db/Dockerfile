FROM mysql:5.7.22
EXPOSE 3306
COPY ./db/ /home/database
COPY ./db/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh