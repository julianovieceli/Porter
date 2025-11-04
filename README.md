# Porter

# Passos para rodar

# 1- Subindo um container com PostgreSQL
    docker pull postgres
    docker run -d --name PorterDb -p 5432:5432 -e POSTGRES_PASSWORD=teste123 postgres

# 2 - Rodar o script para criar o banco de dados e tabelas

```
CREATE database PorterDb ;

-- drop TABLE Booking;
-- drop TABLE Room;
-- drop table Client;

CREATE TABLE IF NOT EXISTS Client
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
        docto  VARCHAR(11) NOT NULL UNIQUE,
        name  VARCHAR(100) NOT NULL ,
    createTime TIMESTAMP WITHOUT TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS Room
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name  VARCHAR(255) NOT NULL UNIQUE,
    createTime TIMESTAMP WITHOUT TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS booking(
    id integer GENERATED ALWAYS AS IDENTITY NOT NULL,
    obs varchar(255) NULL,
    roomid integer not null,
    reservedbyid integer not null,
    startdate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    enddate TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
    createtime TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
    deleteddate TIMESTAMP WITHOUT TIME ZONE ,
    PRIMARY KEY(id),
    CONSTRAINT fk_roomid FOREIGN key(roomid) REFERENCES room(id),
    CONSTRAINT fk_clientid FOREIGN key(reservedbyid) REFERENCES client(id)
);


CREATE TABLE IF NOT EXISTS Log
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    action  VARCHAR(50) NOT NULL ,
    methodName  VARCHAR(200) NOT NULL ,
    entityType  VARCHAR(100) NOT NULL ,
    data JSONB,
    createTime TIMESTAMP WITHOUT TIME ZONE NOT NULL
);
```

Link **[Swaggwer] http://localhost:5259/swagger/index.html **


# 3 - Subir a aplicacao e cadastrar um client e uma room.
# 4 -Após isso, cadastrar uma booking utilizando os ids do client e da room criados e verificar os retornos.


# 5 Para rodar os testes unitarios basta somente excuta-los. Nao foi possivel usra o InMemory(estou verificando o pq), entao eu removo 
# todos os dados no startup.


