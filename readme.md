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


two main api endpoints : `/register` , `/login` , `/<username>/permissions
`