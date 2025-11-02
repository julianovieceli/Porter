# Porter

# Subindo container com PostgreSQL
    docker run -d --name PorterDb -p 5432:5432 -e POSTGRES_PASSWORD=teste123 postgres

    # Criando banco de dados e tabelas
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








