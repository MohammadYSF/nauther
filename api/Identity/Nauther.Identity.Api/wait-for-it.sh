#!/bin/bash
# wait-for-it.sh

set -e

host="$1"
shift
cmd="$@"

until (echo > /dev/tcp/${host%:*}/${host#*:}) >/dev/null 2>&1; do
  >&2 echo "SQL Server is unavailable - sleeping"
  sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $cmd 