Nauther

a system for permission-based user access management system

it aims to provide out of box user access management for other projects

you can connect your application(api) to nauther via http protocol

nauther has a ui panel 

for creating/updating/deleting

admins , roles , permissions ; 

assigning permissions to roles;

assigning permissions to users;

assigning roles to users



for api communication
your api needs to talk to nauther . for that you need to include nauther api key in your header like this : 

```X-API-KEY : <api key here>```


two main api endpoints : `/register` , `/checkpassword` , `/<username>/permissions

```
curl -X 'POST' \
  'https://localhost:5001/api/User/register' \
  -H 'accept: */*' \
  -H 'X-API-KEY: <put api key here>' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": <id of your own user you want to register it>,
  "password": <raw password of your user>,
  "confirmPassword": <again , raw password of your user>,
  "roles": [
    <put here list of id's of a role (which exists in nauther)>
  ],
  "permissions": [
    <put here list of id's of a permission (which exists in nauther)>
  ]
}'
```


```
curl -X 'GET' \
  'https://localhost:5001/api/Admin/<USERNAME>/permission' \
  -H 'accept: */*'
```

```
curl -X 'POST' \
  'https://localhost:5001/api/Auth/checkPassword' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "username": "<USERNAME>",
  "password": "<PASSWORD>"
}'

```