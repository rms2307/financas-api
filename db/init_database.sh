for i in `find /home/database/ -name "*.sql" | sort --version-sort`; do mysql -uroot -pdocker financas < $i; done;