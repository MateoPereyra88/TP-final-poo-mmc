drop DATABASE KrauseCord_BD;
CREATE DATABASE KrauseCord_BD;
USE KrauseCord_BD;

CREATE TABLE Usuario
(
     /* Atributo Característico de la Tabla */
	nombre VARCHAR(80) UNIQUE NOT NULL,
	pass VARCHAR(80) NOT NULL,

     /* Clave Primaria (PK) */
	PRIMARY KEY (nombre)
);

CREATE TABLE Mensaje
(
	/*nro_mensaje integer NOT NULL AUTO_INCREMENT,*/
	nom_usuario varchar(250) NOT NULL,
    mensaje VARCHAR(250) NOT NULL,
    
    
    /*PRIMARY KEY (nro_mensaje),*/
    FOREIGN KEY (nom_usuario) REFERENCES Usuario(nombre)
);



select * from Usuario;
select * from Mensaje;