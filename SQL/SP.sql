CREATE PROCEDURE sp_INSERTAR_NODOS
@param_DIRECTORIO VARCHAR(300)
AS
BEGIN
INSERT INTO tb_NODO (DIRECCION) VALUES (@param_DIRECTORIO);
END

------------------------------------------

CREATE PROCEDURE sp_BORRAR_NODOS
AS
BEGIN
DELETE FROM tb_NODO
DELETE FROM tb_FRAGMENTO
DELETE FROM tb_ARCHIVO
END

----------------------------------

CREATE PROCEDURE sp_INSERTAR_FRAGMENTO
@param_nodo VARCHAR(50),
@param_fragmento VARCHAR(300),
@param_archivo VARCHAR(300)
AS
BEGIN
DECLARE @local_id_nodo INT=(SELECT ID_NODO FROM tb_NODO WHERE DIRECCION=@param_nodo);
DECLARE @local_id_archivo INT=(SELECT ID_ARCHIVO FROM tb_ARCHIVO WHERE NOMBRE=@param_archivo);

INSERT INTO tb_FRAGMENTO 
(ID_ARCHIVO
,NOMBRE_FRAGMENTO,
ID_NODO) 
VALUES 
(@local_id_archivo,
@param_archivo,
@local_id_nodo)
END

CREATE PROCEDURE sp_INSERTAR_ARCHIVO
@param_archivo varchar(300),
@param_ult_mod varchar(32),
@param_tamano varchar(32)
AS
BEGIN

INSERT INTO tb_ARCHIVO (NOMBRE,ULT_MODIFICACION,TAMANO) VALUES (@param_archivo,@param_ult_mod,@param_tamano);

END

