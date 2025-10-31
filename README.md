# Porter

# Subindo container com PostgreSQL
    docker run -d --name PorterDb -p 5432:5432 -e POSTGRES_PASSWORD=teste123 postgres

    # Criando banco de dados e tabelas
CREATE database PorterDb ;

-- drop TABLE Reservation;
-- drop TABLE Room;
-- drop table UserPorter;

create TABLE UserPorter
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
        login  VARCHAR(255) NOT NULL UNIQUE,
    createTime TIMESTAMP
);

create TABLE Room
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name  VARCHAR(255) NOT NULL UNIQUE,
    createTime TIMESTAMP
);


create TABLE Reservation
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name  VARCHAR(255) NOT NULL,
    roomId int,
    CONSTRAINT fk_roomId FOREIGN KEY (roomid) REFERENCES Room (id),
    reservedById int,
     CONSTRAINT fk_userPorterId FOREIGN KEY (reservedById) REFERENCES UserPorter (id),
     startDate TIMESTAMP,
     endDate TIMESTAMP,
    createTime TIMESTAMP
);







