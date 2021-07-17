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
DELETE FROM tb_FRAGMENTO
DELETE FROM tb_ARCHIVO
DELETE FROM tb_NODO
END
GO
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
@param_fragmento,
@local_id_nodo)
END
---------------------------------

CREATE PROCEDURE sp_INSERTAR_ARCHIVO
@param_archivo varchar(300)
AS
BEGIN
GO

<<<<<<< HEAD
------------------------------------------------------------------------
=======
INSERT INTO tb_ARCHIVO (NOMBRE) VALUES (@param_archivo);

END

CREATE PROCEDURE sp_OBTENER_CANTIDAD_NODOS
AS
BEGIN

		BEGIN
			SELECT COUNT (ID_NODO) AS CANTIDAD
			FROM tb_NODO
		END
>>>>>>> Randall

INSERT INTO tb_ARCHIVO (NOMBRE,ULT_MODIFICACION,TAMANO) VALUES (@param_archivo,@param_ult_mod,@param_tamano);
END
GO

------------------------------------------------------------------------

CREATE PROCEDURE sp_GET_FILE_NAMES
AS
BEGIN
	SELECT 
		NOMBRE
	FROM [dbo].[tb_ARCHIVO]
END
GO

----------------------------------------------------------------------

CREATE PROCEDURE sp_IS_NEW_CONFIG
AS
BEGIN

	 IF EXISTS (SELECT ID_AUDITORIA FROM tb_AUDITORIA)
		 BEGIN
			SELECT 1;
		 END
     ELSE
		BEGIN
			SELECT 0;
		END
END

CREATE PROCEDURE sp_DELETE_REGISTER_CONFIG
AS
BEGIN
		DELETE FROM tb_AUDITORIA;
END